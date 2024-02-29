using BeFit.Core;
using IdentityDataApi.Data;
using IdentityDataApi.Models;
using IdentityDataApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace IdentityDataApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ResultModel<AppUser>> Create(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@(howest\.be|student\.howest\.be)$";
            Regex regex = new Regex(pattern);
            if(!regex.IsMatch(email))
            {
                return new ResultModel<AppUser>()
                {
                    IsSuccess = false,
                    Errors = ["Email must be valid and from howest domain"],
                };
            }
            AppUser user = new AppUser()
            {
                Email = email,
                UserName = email,
                Oid = null,
            };
            var result = await _userManager.CreateAsync(user);
            if(!result.Succeeded)
            {
                return new ResultModel<AppUser>()
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => $"Code: {e.Code} ; Descr: {e.Description}").ToList(),
                };
            }
            string studentPattern = @"^[a-zA-Z0-9._%+-]+@(student\.howest\.be)$";
            string lectorPattern = @"^[a-zA-Z0-9._%+-]+@(howest\.be)$";
            Regex studentRegex = new Regex(studentPattern);
            Regex lectorRegex = new Regex(lectorPattern);
            List<Claim> claims = new List<Claim>();
            if (studentRegex.IsMatch(email))
            {
                claims.Add(new Claim(ClaimTypes.Role, BefitClaimConstants.StudentRole));
            }
            else if (lectorRegex.IsMatch(email))
            {
                claims.Add(new Claim(ClaimTypes.Role, BefitClaimConstants.LectorRole));
                claims.Add(new Claim(BefitClaimConstants.HasEvaluationPermission, bool.TrueString));
            }
            claims.Add(new Claim(ClaimTypes.Email, email));
            result = await _userManager.AddClaimsAsync(user, claims);
            if (!result.Succeeded)
            {
                return new ResultModel<AppUser>()
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => $"Code: {e.Code} ; Descr: {e.Description}").ToList(),
                };
            }
            return new ResultModel<AppUser>()
            {
                IsSuccess = true,
                Items = [user],
            };
        }

        public async Task<ResultModel<AppUser>> Delete(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                return new ResultModel<AppUser>
                {
                    IsSuccess = false,
                    Errors = [$"User not found for email address {email}."],
                };
            }
            var result = await _userManager.DeleteAsync(user);
            if(!result.Succeeded)
            {
                return new ResultModel<AppUser>
                {
                    IsSuccess = false,
                    Errors = result.Errors.Select(e => $"Code: {e.Code} ; Descr: {e.Description}").ToList(),
                };
            }
            return new ResultModel<AppUser>
            {
                IsSuccess = true,
                Items = [user],
            };
        }

        public async Task<ResultModel<string>> Login(string ssoToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(ssoToken);
            var ssoClaims = jwtSecurityToken.Claims;
            var oid = ssoClaims.FirstOrDefault(c => c.Type.Equals("oid"))?.Value;
            var email = ssoClaims.FirstOrDefault(c => c.Type.Equals("emails"))?.Value;
            if(email is null)
            {
                return new ResultModel<string>()
                {
                    IsSuccess = false,
                    Errors = ["Email not found."],
                };
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
            {
                return new ResultModel<string>()
                {
                    IsSuccess = false,
                    Errors = ["User not found."],
                };
            }
            if(user.Oid is null)
            {
                user.Oid = oid;
                await _userManager.UpdateAsync(user);
                await _userManager.AddClaimAsync(user, new Claim(BefitClaimConstants.Oid, user.Oid));
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTConfiguration:SigningKey")));

            var identityToken = new JwtSecurityToken(
                audience: _configuration.GetValue<string>("JWTConfiguration:Audience"),
                issuer: _configuration.GetValue<string>("JWTConfiguration:Issuer"),
                claims: userClaims,
                expires: DateTime.Now.AddDays(_configuration.GetValue<int>("JWTConfiguration:TokenExpiration")),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );
            var serializedIdentityToken = tokenHandler.WriteToken(identityToken);

            return new ResultModel<string>()
            {
                IsSuccess = true,
                Items = [serializedIdentityToken],
            };
        }

        
    }
}
