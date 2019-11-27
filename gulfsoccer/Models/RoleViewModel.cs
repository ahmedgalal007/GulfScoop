using gulfsoccer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gulfsoccer.Models
{
    public class RoleViewModel
    {
        public RoleViewModel() { }
        public RoleViewModel(ApplicationRole role)
        {
            id = role.Id;
            name = role.Name;
        }

        public string id { get; set; }
        public string name { get; set; }
    }
}