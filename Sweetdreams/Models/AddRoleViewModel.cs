namespace Sweetdreams.Models
{
    public class AddRoleViewModel
    {
        public IList<string> Roles { get; set;}

        public ApplicationUser User { get; set;}

        public ApplicationRole Role { get; set;}

        public AddRoleViewModel(IList<string> roles, ApplicationUser user)
        {
            Roles = roles;
            User = user;
            Role = new ApplicationRole();
        }
    }
}
