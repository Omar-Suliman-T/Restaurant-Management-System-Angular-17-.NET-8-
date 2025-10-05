using My_Resturant.Entities;

namespace My_Resturant.DTOs.Person
{
    public class GetPersonDTOs:MainEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string? passwordResetCode { get; set; }
        public DateTime? passwordResetExpiry { get; set; }
    }
}
