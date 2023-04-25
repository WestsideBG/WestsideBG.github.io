using Microsoft.AspNetCore.Identity;

namespace Sweetdreams.Models.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
    }
}
