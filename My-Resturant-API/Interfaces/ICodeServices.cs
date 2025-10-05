namespace My_Resturant.Interfaces
{
    public interface ICodeServices
    {
        Task<string> GetDiscountCode();
        Task UpdateDiscountCode(string? code);
    }
}
