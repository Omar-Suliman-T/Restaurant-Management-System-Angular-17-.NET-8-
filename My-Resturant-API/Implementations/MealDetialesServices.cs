using Azure;
using Microsoft.EntityFrameworkCore;
using My_Resturant.Context;
using My_Resturant.DTOs.Meal;
using My_Resturant.Entities;
using My_Resturant.Interfaces;

namespace My_Resturant.Implementations
{
    public class MealDetialesServices : IMealDetialsServices
    {
        private readonly RestDbContext _context;
        public MealDetialesServices(RestDbContext context) 
        {
            _context = context;
        }
        public async Task DeleteItemFromMeal(int mealId, int ItemId)
        {
            double TheItemPrice;
            var TheElement = await _context.Items.FirstOrDefaultAsync(item => item.id == ItemId);
            if (TheElement == null)
            {
                throw new Exception("couldn't find an item with this id");
            }
            else
            {
                TheItemPrice = TheElement.price;
            }
            var deletedElement=await _context.MealDetials.FirstOrDefaultAsync(item=> item.mealID == mealId && item.itemID == ItemId);
            double theDeference = deletedElement.quantity * TheItemPrice;
            var theMeal=await _context.Meals.FirstOrDefaultAsync(theMeal => theMeal.id == mealId);
            theMeal.price -= theDeference;
            await _context.SaveChangesAsync();

            if (deletedElement != null) 
            {
                _context.MealDetials.Remove(deletedElement);
                await _context.SaveChangesAsync();
            }
        }


        public async Task AddItemToMealOrChangeQuantity(UpdateMealDetailesDTOs updateMealDetailesDTOs)
        {
            var theMeal = await _context.Meals.FirstOrDefaultAsync(item => item.id == updateMealDetailesDTOs.mealId);
            var theMealDeitail = await _context.MealDetials.FirstOrDefaultAsync(item => item.itemID == updateMealDetailesDTOs.itemId && item.mealID==updateMealDetailesDTOs.mealId);
            var theItem = await _context.Items.FirstOrDefaultAsync(item => item.id == updateMealDetailesDTOs.itemId);

            if (theMeal == null)
            {
                throw new Exception("there is no meal with this id");
            }
            else if (theItem == null)
            {
                throw new Exception("there is no item with this id");

            }
            else
            {
                if (theMealDeitail == null)
                {
                    double itemPrice = theItem.price;
                    mealDetials mealDetials = new mealDetials
                    {
                        mealID = updateMealDetailesDTOs.mealId,
                        itemID = updateMealDetailesDTOs.itemId,
                        quantity = updateMealDetailesDTOs.quantity ?? 0,
                    };
                    await _context.MealDetials.AddAsync(mealDetials);
                    await _context.SaveChangesAsync();
                    theMeal.price += (itemPrice * updateMealDetailesDTOs.quantity ?? 0);
                    await _context.SaveChangesAsync();
                }
                else{
                    int theDeference = theMealDeitail.quantity - updateMealDetailesDTOs.quantity ?? 0;
                    theMeal.price += theItem.price * theDeference;
                    await _context.SaveChangesAsync();   
                    theMealDeitail.quantity = updateMealDetailesDTOs.quantity ?? 0;
                    await _context.SaveChangesAsync();

                }
            }
            
        }
       public async Task DecreasItemQuantityInAMeal(UpdateMealDetailesDTOs updateMealDetailesDTOs)
        {
            var theMeal = await _context.Meals.FirstOrDefaultAsync(item => item.id == updateMealDetailesDTOs.mealId);
            var theMealDeitail = await _context.MealDetials.FirstOrDefaultAsync(item => item.itemID == updateMealDetailesDTOs.itemId && item.mealID == updateMealDetailesDTOs.mealId);
            var theItem = await _context.Items.FirstOrDefaultAsync(item => item.id == updateMealDetailesDTOs.itemId);
            if (theMealDeitail == null)
            {
                throw new Exception("there is no meal with this id");

            }
            else if(theItem == null)
            {
                throw new Exception("there is no item with this id");

            }
            else
            {
                if (updateMealDetailesDTOs.quantity == null)
                {
                    double theDeference = theItem.price * theMealDeitail.quantity;
                    _context.Remove(theMealDeitail);
                    await _context.SaveChangesAsync();
                    _context.Meals.FirstOrDefault(item => item.id == updateMealDetailesDTOs.mealId).price -= theDeference;
                    _context.SaveChanges();
                }
                else
                {
                    int theDeference = theMealDeitail.quantity - updateMealDetailesDTOs.quantity??0;
                    theMealDeitail.quantity = updateMealDetailesDTOs.quantity ?? 0;
                    await _context.SaveChangesAsync();
                    theMeal.price -= theItem.price * theDeference;
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<GetMealItemsDTOS>> GetMealItems(int mealId)
        {
            var response = await(from detail in _context.MealDetials
                                   join item in _context.Items
                                   on detail.itemID equals item.id
                                   where detail.mealID == mealId
                                   select new GetMealItemsDTOS
                                   {
                                       itemId = item.id,
                                       itemName = item.name,
                                       quantity = detail.quantity,
                                       image=item.image
                                   }).ToListAsync();

            return response;
        }

    }
}
