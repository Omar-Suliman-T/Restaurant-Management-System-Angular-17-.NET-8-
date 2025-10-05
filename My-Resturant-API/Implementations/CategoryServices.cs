using Microsoft.EntityFrameworkCore;
using My_Resturant.Context;
using My_Resturant.DTOs.Category;
using My_Resturant.Entities;
using My_Resturant.Interfaces;

namespace My_Resturant.Implementations
{
    public class CategoryServices:ICategoryServises
    {
        private readonly RestDbContext _context;
        public CategoryServices(RestDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetCategoryDTOs>> GetCategories()
        {
            var response = await _context.Categories
                .Join(_context.LookupItems,
                    category => category.name, 
                    lookup => lookup.id,        
                    (category, lookup) => new GetCategoryDTOs
                    {
                        name = lookup.name,     
                        description = category.description,
                        isActive = category.isActive,
                        creationDate = category.creationDate,
                        id = category.id,
                    })
                .ToListAsync();

            if (response == null || !response.Any())
            {
                throw new Exception("There are no categories");
            }

            return response;
        }
        public async Task<GetCategoryDTOs> GetCategoryById(int id)
        {
            var category = await (
                from c in _context.Categories
                join li in _context.LookupItems on c.name equals li.id
                where c.id == id
                select new GetCategoryDTOs
                {
                    id = c.id,
                    description = c.description,
                    isActive = c.isActive,
                    creationDate = c.creationDate,
                    name = li.name,
                    modificationDate = li.modificationDate,
                }).FirstOrDefaultAsync();

            if (category == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            }

            return category;
        }

        public async Task<string> UpdateCategory(int id, UpdateCategoryDTOs category)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                int changed = 0;
                var myCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.id == id);

                if (myCategory == null)
                {
                    throw new Exception($"Couldn't find any category with the id: {id}");
                }

                var lookupItem = await _context.LookupItems
                    .FirstOrDefaultAsync(li => li.id == myCategory.name);

                if (lookupItem == null)
                {
                    throw new Exception($"Associated LookupItem for Category {id} not found.");
                }

                if (!string.IsNullOrEmpty(category.name))
                {
                    lookupItem.name = category.name;
                    changed++;
                }

                if (category.description != null)
                {
                    myCategory.description = category.description;
                    changed++;
                }

                if (category.isActive != null)
                {
                    myCategory.isActive = category.isActive.Value;
                    changed++;
                }


                myCategory.modificationDate = DateTime.Now;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return $"{changed} elements have been changed";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Failed to update category: {ex.Message}");
            }
        }
        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.id == id);

            if (category == null)
            {
                throw new Exception($"Category with ID {id} not found.");
            }

            var lookupItem = await _context.LookupItems
                .FirstOrDefaultAsync(li => li.id == category.name);

            if (lookupItem == null)
            {
                throw new Exception($"Associated LookupItem for Category {id} not found.");
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Categories.Remove(category);
                _context.LookupItems.Remove(lookupItem);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Failed to delete Category {id}: {ex.Message}");
            }
        }
        public async Task AddCategory(CreateCategoryDTOs category)
        {
            var lookupItem = new LookupItem()
            {
                lookupTypeID = 1,  
                name = category.name,
                                     
            };

            await _context.LookupItems.AddAsync(lookupItem);
            await _context.SaveChangesAsync();  

            Category newCategory = new Category()
            {
                name = lookupItem.id,  
                description = category.description,
                isActive = category.isActive,
            };

            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();  
        }

    }
}
