using My_Resturant.Entities;

namespace My_Resturant.DTOs.Category
{
    public class GetCategoryDTOs:MainEntity
    {
        public string name { get; set; }
        public string description { get; set; }
        public bool isActive { get; set; }
    }
}
