using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ModelsClassLibrary
{
    public class APIAuthMDL
    {
        private string _UserName;
        private string _Password;
        [Required(ErrorMessage ="User name is required!")]
        public string UserName { get => _UserName; set => _UserName = value; }
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get => _Password; set => _Password = value; }
    }
}
