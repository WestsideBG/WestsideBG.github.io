using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Sweetdreams.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string? SomeCustomProp { get; set; }

        public ApplicationUser() : base() { }
        public ApplicationUser(string username) : base(username) { }

    }
}
