using Microsoft.AspNetCore.Identity;

namespace ZwajApp.API.Data.DAL.Models
{
    public class UserRole : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}