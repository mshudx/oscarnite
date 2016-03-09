using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Mshudx.OscarNite.Web.Models;
using Mshudx.OscarNite.Web.ViewModels;
using Microsoft.AspNet.Http.Internal;
using Microsoft.AspNet.Authorization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Mshudx.OscarNite.Web.Controllers
{
    [Authorize(Roles = "Admin,Report")]
    public class ReportingController : Controller
    {
        OscarNiteDbContext dbContext;

        public ReportingController(OscarNiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var question = dbContext.Questions.OrderBy(q => q.Order).ThenBy(q => q.Id).First();
            var votes = dbContext.Votes.Count();
            ViewData["totalVotes"] = votes;
            ViewData["questionId"] = question.Id;

            return View();
        }

        public IActionResult Results(string id)
        {
            // Force relevant tables to be cached into local memory for speed
            var questions = dbContext.Questions.OrderBy(q => q.Order).ThenBy(q => q.Id).ToList().SkipWhile(q => q.Id != id).Take(2);
            var answers = dbContext.Answers.Where(a => a.Question.Id == id).ToList();
            var options = dbContext.Options.ToList();
            var question = questions.First();
            var next = questions.Skip(1).FirstOrDefault();
            var result =
                new ResultViewModel
                {
                    QuestionText = question.Text,
                    QuestionResults = options
                        .Select(option => new ResultDataViewModel
                        {
                            OptionText = option.Text,
                            OptionCount = answers.Where(answer => answer.Question == question).Count(a => a.Option == option),
                            OptionPercent = answers.Where(a => a.Question == question).Count() > 0 ?
                                string.Format("{0:0.00}%", (float)answers.Where(a => a.Question == question).Count(a => a.Option == option) / answers.Where(a => a.Question == question).Count() * 100) : "0%"
                        })
                        .OrderByDescending(r => r.OptionCount)
                        .ToList(),
                    Next = next?.Id,
                };
            var moviesData = String.Join(",", result.QuestionResults.Select(r => String.Format("'{1} - {0}'", r.OptionText, r.OptionCount)));
            var votesData = String.Join(",", result.QuestionResults.Select(r => r.OptionCount));
            ViewData["moviesData"] = moviesData;
            ViewData["votesData"] = votesData;
            return View("Results", result);
        }
    }
}