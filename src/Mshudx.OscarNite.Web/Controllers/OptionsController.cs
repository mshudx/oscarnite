using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using Mshudx.OscarNite.Web.Models;
using Microsoft.AspNet.Authorization;
using System;

namespace Mshudx.OscarNite.Web.Controllers
{
    [Authorize]
    public class OptionsController : Controller
    {
        private OscarNiteDbContext _context;

        public OptionsController(OscarNiteDbContext context)
        {
            _context = context;    
        }

        // GET: Options
        public async Task<IActionResult> Index()
        {
            return View(await _context.Options.OrderBy(o => o.Text).ToListAsync());
        }

        // GET: Options/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Option option = await _context.Options.SingleAsync(m => m.Id == id);
            if (option == null)
            {
                return HttpNotFound();
            }

            return View(option);
        }

        // GET: Options/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Options/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Option option)
        {
            if (ModelState.IsValid)
            {
                option.Id = Guid.NewGuid().ToString();
                _context.Options.Add(option);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(option);
        }

        // GET: Options/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Option option = await _context.Options.SingleAsync(m => m.Id == id);
            if (option == null)
            {
                return HttpNotFound();
            }
            return View(option);
        }

        // POST: Options/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Option option)
        {
            if (ModelState.IsValid)
            {
                _context.Update(option);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(option);
        }

        // GET: Options/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Option option = await _context.Options.SingleAsync(m => m.Id == id);
            if (option == null)
            {
                return HttpNotFound();
            }

            return View(option);
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            _context.Answers.Where(a => a.Option.Id == id).ToList().ForEach(a => _context.Answers.Remove(a));
            Option option = await _context.Options.SingleAsync(m => m.Id == id);
            _context.Options.Remove(option);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
