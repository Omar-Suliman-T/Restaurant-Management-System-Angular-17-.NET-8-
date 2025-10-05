using My_Resturant.DTOs.Item;
using My_Resturant.Entities;
namespace My_Resturant.Interfaces
{
    public interface IItemServices
    {
        Task<List<GetItemDTOs>> GetAllItems();
        Task<GetItemDTOs>GetItem(int id);
        Task DeleteItem(int id);
        Task<string> UpdateItem(int id,UpdateItemDTOs item,IFormFile?image);
        Task AddItem(CreateItemDTOs item);
        Task<List<Item>> GetMostPopularItems();

    }
}
