using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityDataApi.Data
{
    public class AppUser : IdentityUser
    {
        public string? Oid { get; set; }
    }
}
