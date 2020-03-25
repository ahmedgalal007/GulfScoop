using gulfsoccer.Controllers;

namespace gulfsoccer.Models
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
        }

        public RoleViewModel(ApplicationRole role)
        {
            id = role.Id;
            name = role.Name;
        }

        public string id { get; set; }
        public string name { get; set; }
    }
}