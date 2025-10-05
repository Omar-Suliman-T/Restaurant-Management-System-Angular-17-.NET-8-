using Microsoft.EntityFrameworkCore;
using My_Resturant.Context;
using My_Resturant.DTOs.Category;
using My_Resturant.DTOs.Ingredieants;
using My_Resturant.DTOs.Order;
using My_Resturant.Entities;
using My_Resturant.Helper;
using My_Resturant.Interfaces;

namespace My_Resturant.Implementations
{
    public class IngrediantServces : IIngrediantServices
    {
        private readonly RestDbContext _context;
        private readonly SaveImages _saveImages;
        public IngrediantServces(RestDbContext context,SaveImages saveImages)
        {
            _context = context;
            _saveImages = saveImages;
        }
        public async Task AddIngrediant(AddIngrediantsDTOs ingrediant)
        {
            var unit = await _context.LookupItems.FirstOrDefaultAsync(li => li.name == ingrediant.unit);

            string theImage = await _saveImages.saveImages(ingrediant.image);
            Ingrediant newIngrediant = new Ingrediant()
            {
                name = ingrediant.name,
                unit = unit.id,
                image = theImage,

            };
            await _context.AddAsync(newIngrediant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngrediant(int id)
        {
            var response = await _context.ingrediants.FirstOrDefaultAsync(ingrediant => ingrediant.id == id);
            if (response == null)
            {
                throw new Exception($"Unable to delete ingrediant {id}");
            }
            else
            {
                _context.ingrediants.Remove(response);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<GetIngrediantDTOs>> GetIngrediants()
        {
            var response = await (
                from ingrediant in _context.ingrediants
                join li in _context.LookupItems on ingrediant.unit equals li.id
                select new GetIngrediantDTOs
                {
                    id = ingrediant.id,
                    name = ingrediant.name,
                    unit = li.name,
                    creationDate = li.creationDate,
                    image = ingrediant.image,
                    isActive = ingrediant.isActive,
                    modificationDate = ingrediant.modificationDate,
                }).ToListAsync();

            if (response == null || !response.Any())
            {
                throw new Exception("There are no ingredients.");
            }

            return response;
        }

        public async Task<GetIngrediantDTOs> GetIngrediantById(int id)
        {
            var ingrediant = await (
                from i in _context.ingrediants
                join li in _context.LookupItems on i.unit equals li.id
                where i.id == id
                select new GetIngrediantDTOs
                {
                    id=i.id,
                    name = i.name,
                    unit = li.name,
                    creationDate = li.creationDate,
                    image = i.image,
                    isActive = i.isActive,
                    modificationDate = i.modificationDate,
                     
                }).FirstOrDefaultAsync();

            if (ingrediant == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            }

            return ingrediant;
        }

        public async Task<string> UpdateIngrediant(int id, UpdateIngrediantDTOs ingrediant, IFormFile? image)
        {
            var unit = await _context.LookupItems.FirstOrDefaultAsync(li => li.name == ingrediant.unit);

            string newImage = "";
            int changed = 0;
            var myIngrediant = await _context.ingrediants.SingleOrDefaultAsync(myIngrediant => myIngrediant.id == id);
            if(image != null)
            {
                newImage = await _saveImages.saveImages(image);

            }
            if (myIngrediant == null)
            {
                throw new Exception($"coudn't found any ingrediant with the id: {id}");
            }
            else
            {
                if (ingrediant.name != null)
                {
                    myIngrediant.name = ingrediant.name;
                    changed++;
                }
                if (ingrediant.unit != null)
                {
                    myIngrediant.unit = unit.id;
                    changed++;
                }
                if (image != null)
                {
                    myIngrediant.image = newImage;
                    changed++;
                }
                if (ingrediant.isActive != null)
                {
                    myIngrediant.isActive = ingrediant.isActive??true;
                    changed++;
                }
                myIngrediant.modificationDate = DateTime.Now;
                await _context.SaveChangesAsync();
                return ($"{changed} elements has been changed");
            }
        }
    }
}
