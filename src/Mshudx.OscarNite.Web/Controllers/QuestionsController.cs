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
    public class QuestionsController : Controller
    {
        private OscarNiteDbContext _context;

        public QuestionsController(OscarNiteDbContext context)
        {
            _context = context;    
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Questions.OrderBy(q => q.Order).ToListAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Question question = await _context.Questions.SingleAsync(m => m.Id == id);
            if (question == null)
            {
                return HttpNotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
        public IActionResult Create()
        {
            return View(new Question() { Order = _context.Questions.Count() + 1 });
        }

        // POST: Questions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Question question)
        {
            if (ModelState.IsValid)
            {
                question.Id = Guid.NewGuid().ToString();
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Question question = await _context.Questions.SingleAsync(m => m.Id == id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Update(question);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Question question = await _context.Questions.SingleAsync(m => m.Id == id);
            if (question == null)
            {
                return HttpNotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            _context.Answers.Where(a => a.Question.Id == id).ToList().ForEach(a => _context.Answers.Remove(a));

            Question question = await _context.Questions.SingleAsync(m => m.Id == id);
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
