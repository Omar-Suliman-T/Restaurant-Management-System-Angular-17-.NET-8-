using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using My_Resturant.Implementations;
using My_Resturant.Interfaces;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace My_Resturant.Helper
{
    public class TokenHelper
    {
       private readonly Func<string,Task<bool>> _IsTokenInBlackList;
       public TokenHelper(Func<string,Task<bool>> IsTokenInBlackList)
        {
            _IsTokenInBlackList = IsTokenInBlackList;
        }
        
        public async Task<string> GenerateToken(string personId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("LongSecrectStringForModulekodestartppopopopsdfjnshbvhueFGDKJSFBYJDSAGVYKDSGKFUYDGASYGFskc vhHJVCBYHVSKDGHASVBCL");
            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim("PersonId",personId),
                        new Claim(ClaimTypes.Role,role)
                }),
                Expires = DateTime.Now.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey)
                , SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptior);
            return tokenHandler.WriteToken(token);
        }
        public async Task<bool> ValidateToken(string tokenString, string role)
        {
            String token = "Bearer " + tokenString;
            var jwtEncodedString = token.Substring(7);
            if (await _IsTokenInBlackList(tokenString))
            {
                return false;
            }
            var Token = new JwtSecurityToken(jwtEncodedString: jwtEncodedString);
            var roleString = (Token.Claims.First(c => c.Type == "role").Value.ToString());
            if (Token.ValidTo > DateTime.UtcNow && roleString.Equals(role, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
        public async Task<string> GeneratePasswordResetTokenAsync(string personId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("LongSecretKeyForYourApp"); 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("PersonId", personId),
            new Claim("Purpose", "ResetPassword")
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<int> ValidatePsswordResetTokenAsync(string tokenString)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("LongSecretKeyForYourApp");

            try
            {
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true, 
                    IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(tokenString, tokenValidationParameters, out var validatedToken);

                var purpose = principal.Claims.FirstOrDefault(c => c.Type == "Purpose")?.Value;

                var personId = principal.Claims.First(c => c.Type == "PersonId").Value;
                if (purpose != "ResetPassword" || personId==null)
                    throw new Exception("not valid token");
                else
                {
                    return int.Parse(personId);
                }

            }
            catch
            {
                throw new Exception("not valid token");
            }
        }

    }
}



