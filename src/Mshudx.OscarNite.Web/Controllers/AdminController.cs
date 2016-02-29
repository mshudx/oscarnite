using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Mshudx.OscarNite.Web.Security;
using Microsoft.AspNet.Authorization;
using Mshudx.OscarNite.Web.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Mshudx.OscarNite.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        OscarNiteDbContext dbContext;

        public AdminController(OscarNiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            await Task.Yield();
            return View();
        }

        public async Task<IActionResult> Reset()
        {
            await Task.Yield();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetConfirmed()
        {
            dbContext.Answers.RemoveRange(dbContext.Answers.ToList());
            dbContext.Votes.RemoveRange(dbContext.Votes.ToList());
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
