using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using My_Resturant.Context;
using My_Resturant.DTOs.Item;
using My_Resturant.DTOs.Meal;
using My_Resturant.Entities;
using My_Resturant.Helper;
using My_Resturant.Interfaces;
using System.Text.Json;

namespace My_Resturant.Implementations
{
    public class MealServices : IMealServices
    {
        private readonly RestDbContext _context;
        private readonly SaveImages _saveImages;
        public MealServices(RestDbContext context,SaveImages saveImages)
        {
            _context = context;
            _saveImages = saveImages;
        }
        public async Task AddMeal(AddMealDTOs meal)
        {
            var detailsList = JsonSerializer.Deserialize<List<AddMealDetailsDTOs>>(meal.details);

            double price = 0;

            foreach (var listmealdetials in detailsList)
            {
                var item = await _context.Items.FirstOrDefaultAsync(i => i.id == listmealdetials.itemID);
                price += listmealdetials.quantity * (item?.price ?? 0);

            }
            string imageUrl =await _saveImages.saveImages(meal.image);
            Meal myMeal = new Meal
            {
                name= meal.name,
                category = meal.category,
                price = price,
                description = meal.description,
                image = imageUrl,
                stock = meal.stock,
            };
            await _context.Meals.AddAsync(myMeal);
            await _context.SaveChangesAsync();
            //var lastMeal = await _context.Meals.OrderBy(meal => meal.id).LastOrDefaultAsync();
            //var lastMealId = lastMeal?.id??0;
            foreach (var item in detailsList)
            {
                mealDetials mealDetials = new mealDetials();
                mealDetials.quantity = item.quantity;
                mealDetials.itemID = item.itemID;
                mealDetials.mealID = myMeal.id;
                await _context.MealDetials.AddAsync(mealDetials);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteMeal(int id)
        {
            var theMeal = await _context.Meals.FirstOrDefaultAsync(Meal => Meal.id == id);
            if (theMeal == null)
            {
                throw new Exception($"There is no mael with the id: {id}");
            }
            else
            {
                _context.Meals.Remove(theMeal);
                await _context.SaveChangesAsync();
            }

            var mealDetials = _context.MealDetials.Where(meal => meal.mealID == id).ToList();
            foreach (var item in mealDetials)
            {
                _context.MealDetials.Remove(item);
            }

        }

        public async Task<List<GetMealDTOs>> GetAllMeals()
        {
            var response = await (
               from meal in _context.Meals
               join li in _context.LookupItems on meal.category equals li.id
               select new GetMealDTOs
               {
                   Id = meal.id,
                    category=li.name,
                    name=meal.name,
                    stock=meal.stock,
                    price =meal.price,
                    description=meal.description,
                    image=meal.image,

               }).ToListAsync();

            if (response == null || !response.Any())
            {
                throw new Exception("There are no ingredients.");
            }

            return response;
        }

        public async Task<string> UpdateMeal(int id, UpdateMealDTOs meal)
        {
            int changed = 0;
            var myMeal = await _context.Meals.SingleOrDefaultAsync(myMeal => myMeal.id == id);
            if (myMeal == null)
            {
                throw new Exception($"coudn't found any meal with the id: {id}");
            }
            else
            {
                
                if (meal.stock != null)
                {
                    string value = meal.stock.ToString();
                    myMeal.stock = int.Parse(value);
                    changed++;
                }
                if (meal.name != null)
                {
                    myMeal.name = meal.name;
                    changed++;
                }
                if (meal.image != null)
                {
                   string newImage=await _saveImages.saveImages(meal.image);
                    myMeal.image = newImage;
                    changed++;
                }
                if (meal.description != null)
                {
                    myMeal.description = meal.description;
                    changed++;
                }
                if (meal.category != null)
                {
                    string value = meal.category.ToString();
                    myMeal.category = int.Parse(value);
                    changed++;
                }
                if(meal.isActive != null)
                {
                    myMeal.isActive = meal.isActive??true;
                }
                myMeal.modificationDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return ($"{changed} elements has been changed");
            }
        }
        public async Task<GetSpecificMealDetialesDTOs> GetSpecificMealDetiales(int mealid)
        {
            var theMealDetailes = _context.MealDetials.Where(meal => meal.id == mealid).ToList();


            var theMeal = await (
               from meal in _context.Meals
               join li in _context.LookupItems on meal.category equals li.id
               where meal.id == mealid
               select new GetSpecificMealDetialesDTOs
               {
                    category=li.name,
                    description=meal.description,
                    name=meal.name,
                    image=meal.image,
                    mealDetials=theMealDetailes,
                    price=meal.price,
                    stock=meal.stock,
                    isActive=meal.isActive,

               }).FirstOrDefaultAsync();

            if (theMeal == null)
            {
                throw new Exception($"Category with ID {mealid} not found.");
            }

            return theMeal;
        }
        
    }
}
