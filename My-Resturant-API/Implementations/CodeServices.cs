using Microsoft.EntityFrameworkCore;
using My_Resturant.Context;
using My_Resturant.Entities;
using My_Resturant.Interfaces;

namespace My_Resturant.Implementations
{
    public class CodeServices : ICodeServices
    {
        private readonly RestDbContext _context;
        public CodeServices(RestDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task UpdateDiscountCode(string? code)
        {
            if (code == null)
            {
                _context.Codes.Remove(await _context.Codes.FirstOrDefaultAsync());
                await _context.SaveChangesAsync();
            }
            else
            {
                var existingCode=await _context.Codes.FirstOrDefaultAsync();
                if (existingCode != null)
                {
                    _context.Codes.Remove(existingCode);
                    await _context.SaveChangesAsync();
                }
                
                await _context.Codes.AddAsync(new Code { discountCode = code, isActive = true });
                await _context.SaveChangesAsync();
                
            }
        }

        public async Task<string> GetDiscountCode()
        {
            var code =(await _context.Codes.FirstOrDefaultAsync());
            if(code == null)
            {
                return null;
            }
            else
            {
               return code.discountCode.ToString();
            }
        }
    }
}
