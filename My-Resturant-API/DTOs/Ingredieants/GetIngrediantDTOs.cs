using My_Resturant.Entities;

namespace My_Resturant.DTOs.Ingredieants
{
    public class GetIngrediantDTOs:MainEntity
    {
        public string name { get; set; }
        public string unit { get; set; }
        public string? image { get; set; }
    }
}
