using My_Resturant.Entities;

namespace My_Resturant.DTOs.Person
{
    public class CreatePersonDTOs:MainEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public string? role { get; set; }
    }
}
