using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mshudx.OscarNite.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Code { get; set; }
    }
}
