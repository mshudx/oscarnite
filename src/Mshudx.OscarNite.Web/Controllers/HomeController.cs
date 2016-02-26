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
            VotingViewModel viewModel = new VotingViewModel();
            viewModel.Questions = dbContext.Questions.ToList();
            viewModel.Options = dbContext.Options.ToList();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(VotingViewModel viewModel)
        {
            Vote vote = new Vote();
            vote.Id = Guid.NewGuid().ToString();
            vote.Created = DateTimeOffset.Now;
            vote.Voter = Request.Form["voter"];
            vote.Answers = new List<Answer>();

            // This collection is lazily loaded, force it to be downloaded locally
            dbContext.Options.ToList();

            foreach (var question in dbContext.Questions)
            {
                Answer answer = new Answer();
                answer.Id = Guid.NewGuid().ToString();

                answer.Vote = vote;
                vote.Answers.Add(answer);
                answer.Question = question;
                answer.Option = dbContext.Options.Single(o => o.Id == Request.Form[question.Id]);
            }

            dbContext.Votes.Add(vote);
            dbContext.SaveChanges();

            return View("ThanksForVoting");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
