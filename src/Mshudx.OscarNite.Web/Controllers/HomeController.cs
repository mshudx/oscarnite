using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Mshudx.OscarNite.Web.Models;
using Mshudx.OscarNite.Web.ViewModels;
using Microsoft.AspNet.Http.Internal;

namespace Mshudx.OscarNite.Web.Controllers
{
    public class HomeController : Controller
    {
        OscarNiteDbContext dbContext;

        public HomeController(OscarNiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(new AliasViewModel());
        }

        [HttpPost]
        public IActionResult Index(AliasViewModel model)
        {
            if (!ModelState.IsValid || String.IsNullOrEmpty(model.Alias) || model.Alias.Length < 5)
            {
                ModelState.AddModelError("alias", "Invalid alias!");
                return View(model);
            }
            return RedirectToAction("Vote", new { alias = model.Alias });
        }

        public IActionResult Vote(string alias)
        {
            VotingViewModel viewModel = new VotingViewModel();
            viewModel.Options = dbContext.Options.OrderBy(o => o.Text).ToList();

            viewModel.Questions =
                dbContext
                    .Questions
                    .OrderBy(q => q.Order)
                    .Select(
                        q => new QuestionVotingViewModel()
                        {
                            QuestionId = q.Id,
                            Text = q.Text,
                        })
                    .ToList();
            ViewData["alias"] = alias;
            return View("Vote", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Vote(VotingViewModel viewModel, string alias)
        {
            if (!ModelState.IsValid || viewModel.Questions.Any(q => String.IsNullOrEmpty(q.Voted)))
            {
                ViewData["alias"] = alias;

                ModelState.AddModelError("", "Please vote in each category!");
                viewModel.Options = dbContext.Options.OrderBy(o => o.Text).ToList();

                viewModel.Questions =
                    dbContext
                        .Questions
                        .OrderBy(q => q.Order)
                        .Select(
                            q => new QuestionVotingViewModel()
                            {
                                QuestionId = q.Id,
                                Text = q.Text,
                                Voted = viewModel.Questions.Where(qo => qo.QuestionId == q.Id).Select(qo => qo.Voted).FirstOrDefault(),
                            })
                        .ToList();
                return View(viewModel);
            }
            var vote = dbContext.Votes.SingleOrDefault(v => v.Voter.ToLowerInvariant() == alias.ToLowerInvariant());
            if (vote == null)
            {
                vote = new Vote();
                vote.Voter = alias;
                vote.Id = Guid.NewGuid().ToString();
                vote.Answers = new List<Answer>();
                dbContext.Votes.Add(vote);
            }

            var questions = dbContext.Questions.ToList();
            var options = dbContext.Options.ToList();
            dbContext.Answers.RemoveRange(dbContext.Answers.Where(a => a.Vote.Id == vote.Id));
            vote.Created = DateTimeOffset.Now;
            vote.Answers =
                viewModel
                    .Questions
                    .Select(
                        q => new Answer()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Question = questions.FirstOrDefault(qo => qo.Id == q.QuestionId),
                            Vote = vote,
                            Option = options.FirstOrDefault(oo => oo.Id == q.Voted),
                        })
                    .ToList();

            await dbContext.SaveChangesAsync();

            return View("ThanksForVoting");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
