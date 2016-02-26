using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mshudx.OscarNite.Web.Security
{
    public class ApplicationUser
    {
        public string Name { get; set; }

        public ApplicationRole Role { get; private set; }

        public static readonly ApplicationUser Admin = new ApplicationUser() { Name = "Admin", Role = ApplicationRole.Admin };
        public static readonly ApplicationUser Report = new ApplicationUser() { Name = "Report", Role = ApplicationRole.Report };
    }
}
