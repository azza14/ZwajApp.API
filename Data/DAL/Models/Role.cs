using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ZwajApp.API.Data.DAL.Models
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}