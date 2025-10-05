namespace My_Resturant.DTOs.Auth
{
    public class ResetRequestDTOs
    {
        public string email {  get; set; }
        public string? code { get; set; }
        public string? newPassword { get; set; }
    }
}
