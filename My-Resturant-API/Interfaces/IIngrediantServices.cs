using My_Resturant.DTOs.Category;
using My_Resturant.DTOs.Ingredieants;
using My_Resturant.Entities;

namespace My_Resturant.Interfaces
{
    public interface IIngrediantServices
    {
        Task<List<GetIngrediantDTOs>> GetIngrediants();
        Task<GetIngrediantDTOs> GetIngrediantById(int id);
        Task<string>UpdateIngrediant(int id, UpdateIngrediantDTOs ingrediant,IFormFile? image);
        Task DeleteIngrediant(int id);
        Task AddIngrediant(AddIngrediantsDTOs ingrediant);
    }
}
