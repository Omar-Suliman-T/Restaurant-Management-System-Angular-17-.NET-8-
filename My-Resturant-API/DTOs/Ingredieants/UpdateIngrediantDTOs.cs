namespace My_Resturant.DTOs.Ingredieants
{
    public class UpdateIngrediantDTOs
    {
        public string? name { get; set; }
        public string? unit { get; set; }
        public bool? isActive { get; set; }
        public IFormFile?image { get; set; }
    }
}
