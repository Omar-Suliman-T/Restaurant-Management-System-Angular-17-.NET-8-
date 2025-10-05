using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.EntityFrameworkCore;
using My_Resturant.Context;
using My_Resturant.DTOs.Auth;
using My_Resturant.Helper;
using My_Resturant.Interfaces;

namespace My_Resturant.Implementations
{
    public class AuthServices : IAuthServices
    {
        private readonly TokenHelper _tokenHelper;
        private readonly RestDbContext _context;
        private static List<string> TokenBlackList = new List<string>();
        public AuthServices(RestDbContext context)
        {
            _context = context;
            _tokenHelper = new TokenHelper(IsTokenInBlackList);
        }
        public async Task<string> LogIn(LogInDTOs input)

        {
            if (input != null)
            {
                if (!string.IsNullOrEmpty(input.email) && !string.IsNullOrEmpty(input.Password))
                {
                    input.email = EncryptionHelper.GenerateSHA384String(input.email);
                    input.Password = EncryptionHelper.GenerateSHA384String(input.Password);
                    var authUser = await (from p in _context.People
                                          join lookup in _context.LookupItems
                                          on p.role equals lookup.id
                                          where p.email == input.email && p.password == input.Password
                                          select new
                                          {
                                              PersonId = p.id.ToString(),
                                              Role = lookup.name.ToString(),
                                          }).FirstOrDefaultAsync();
                    return authUser != null ? await _tokenHelper.GenerateToken(authUser.PersonId, authUser.Role) : "Authantication Failed";

                }
                else
                {
                    throw new Exception("Email and Password Are Required");
                }
            }
            else
            {

                throw new Exception("Email and Password Are Required");

            }
        }
        public async Task<bool> LogOut(string token)
        {
            TokenBlackList.Add(token);
            return true;
        }
        public async Task<bool> IsTokenInBlackList(string token)
        {
            bool response=TokenBlackList.Contains(token);
            return response;
        }
    }
}
