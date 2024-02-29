using IdentityDataApi.Data;
using IdentityDataApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityDataApi.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ResultModel<AppUser>> Create(string email);
        Task<ResultModel<AppUser>> Delete(string email);
        Task<ResultModel<string>> Login(string token);
    }
}