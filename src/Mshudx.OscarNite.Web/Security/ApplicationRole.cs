using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mshudx.OscarNite.Web.Security
{
    public class ApplicationRole
    {
        public string Name { get; set; }
        public static readonly ApplicationRole Admin = new ApplicationRole() { Name = "Admin" };
        public static readonly ApplicationRole Report = new ApplicationRole() { Name = "Report" };
    }
}
