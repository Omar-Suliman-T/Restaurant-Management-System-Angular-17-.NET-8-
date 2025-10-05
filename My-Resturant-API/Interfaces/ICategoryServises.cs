using My_Resturant.DTOs.Category;
using My_Resturant.Entities;

namespace My_Resturant.Interfaces
{
    public interface ICategoryServises

    {
        Task<List<GetCategoryDTOs>> GetCategories();
        Task <GetCategoryDTOs> GetCategoryById(int id);
        
        Task<string> UpdateCategory(int id,UpdateCategoryDTOs category);
        Task DeleteCategory(int id);
        Task AddCategory(CreateCategoryDTOs categoty);
    }
}
