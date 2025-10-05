namespace My_Resturant.Entities
{
    public class Person:MainEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }
        public int? role { get; set; }
        public string? passwordResetCode { get; set; }
        public DateTime? passwordResetExpiry { get; set; }

    }
}
