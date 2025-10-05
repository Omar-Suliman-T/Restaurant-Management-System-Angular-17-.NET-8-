using Microsoft.EntityFrameworkCore;
using My_Resturant.Context;
using My_Resturant.DTOs.Ingredieants;
using My_Resturant.DTOs.Item;
using My_Resturant.Entities;
using My_Resturant.Helper;
using My_Resturant.Interfaces;

namespace My_Resturant.Implementations
{
    public class ItemServices:IItemServices
    {
        private readonly RestDbContext _context;
        private readonly SaveImages _saveImages;
        public ItemServices(RestDbContext constext, SaveImages saveImages)
        {
            _context = constext;
            _saveImages = saveImages;
        }
        public async Task<List<GetItemDTOs>> GetAllItems()
        {
            var response = await (
               from item in _context.Items
               join li in _context.LookupItems on item.category equals li.id
               select new GetItemDTOs
               {
                     id=item.id,
                     category=li.name,
                     description=item.description,
                     image=item.image,
                     name=item.name,
                     ingrediants=item.ingrediants,
                     price=item.price,
                     stock=item.stock,
                     isActive=item.isActive,
                           
               }).ToListAsync();

            if (response == null || !response.Any())
            {
                throw new Exception("There are no ingredients.");
            }

            return response;
        }
        public async Task<GetItemDTOs> GetItem(int id)
        {
            var theItem = await (
               from item in _context.Items
               join li in _context.LookupItems on item.category equals li.id
               where item.id == id
               select new GetItemDTOs
               {
                    category = li.name,
                    description=item.description,
                    image=item.image,
                    name=item.name,
                    stock = item.stock,
                    price=item.price,
                    ingrediants=item.ingrediants

               }).FirstOrDefaultAsync();

            if (theItem == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            }

            return theItem;
        }
        public async Task DeleteItem(int id)
        {
            var response = await _context.Items.FirstOrDefaultAsync(item => item.id == id);
            if (response == null)
            {
                throw new Exception($"Unable to delete item {id}");
            }
            else
            {
                _context.Items.Remove(response);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<string> UpdateItem(int id, UpdateItemDTOs item, IFormFile? image)
        {
            var category = await _context.LookupItems.FirstOrDefaultAsync(li => li.name == item.category);
            int changed = 0;
            var myItem = await _context.Items.SingleOrDefaultAsync(myItem => myItem.id == id);
            if (myItem == null)
            {
                throw new Exception($"coudn't found any team with the id: {id}");
            }
            else
            {
                
                if (item.ingrediants != null)
                {
                    myItem.ingrediants = item.ingrediants.ToList();
                    changed++;
                }
                if (item.stock != null)
                {
                    string value = item.stock.ToString();
                    myItem.stock = int.Parse(value);
                    changed++;
                }
                if (item.name != null)
                {
                    myItem.name = item.name;
                    changed++;
                }
                if (item.price != null)
                {
                    double priceDeference = (item.price??0) - myItem.price;
                    _context.MealDetials.Where(meal => meal.itemID == id).ToList().ForEach(async meal =>
                    {
                        double theDeference = meal.quantity * priceDeference;
                        var theMeal= _context.Meals.FirstOrDefault(themeal => themeal.id == meal.mealID);
                        theMeal.price += theDeference;
                        await _context.SaveChangesAsync();
                    });

                    string value = item.price.ToString();
                    myItem.price = int.Parse(value);
                    changed++;
                }
                if (item.description != null)
                {
                    myItem.description = item.description;
                    changed++;
                }
                if (item.category != null)
                {
                    myItem.category = category.id;
                    changed++;
                }
                if(image != null) 
                {
                    string newImage =await _saveImages.saveImages(image);
                    myItem.image = newImage;
                    changed++;
                }
                if (item.isActive != null)
                {
                    myItem.isActive = item.isActive??true;
                    changed++;
                }
                myItem.modificationDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return ($"{changed} elements has been changed");
            }
        }
        public async Task AddItem(CreateItemDTOs item)
        {
           string newImage=await _saveImages.saveImages(item.image);
           var category = await _context.LookupItems.FirstOrDefaultAsync(li => li.name == item.category);

            Item newItem = new Item()
            {
                category=category.id,
                description=item.description,
                ingrediants=item.ingrediants,
                isActive=item.isActive,
                name = item.name,
                price = item.price,
                stock = item.stock,
                         

            };
            await _context.AddAsync(newItem);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Item>> GetMostPopularItems()
        {
            var topItemIds = await _context.OrderItemDetails
                .GroupBy(o => o.itemId)
                .Select(g => new { ItemId = g.Key, TotalQuantity = g.Sum(x => x.quantity) }) // g is every group of ths same itemId 
                .OrderByDescending(x => x.TotalQuantity)
                .Take(3)
                .Select(x => x.ItemId)
                .ToListAsync();

            var items = await _context.Items
                .Where(i => topItemIds.Contains(i.id))
                .ToListAsync();

            return items.OrderBy(i => topItemIds.IndexOf(i.id)).ToList();
        }

    }
}
