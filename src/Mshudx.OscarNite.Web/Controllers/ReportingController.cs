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
    //[Authorize(Roles = "Admin,Report")]
    public class ReportingController : Controller
    {
        OscarNiteDbContext dbContext;

        public ReportingController(OscarNiteDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            // Force relevant tables to be cached into local memory for speed
            var questions = dbContext.Questions.ToList();
            var answers = dbContext.Answers.ToList();
            var options = dbContext.Options.ToList();
            var votes = dbContext.Votes.ToList();

            // Assemble report object
            var results =
                new
                {
                    totalVotes = votes.Count,
                    questions = questions.Select(question =>
                    new
                    {
                        questionText = question.Text,
                        questionResults = options.Select(option => new
                        {
                            optionText = option.Text,
                            optionCount = answers.Where(answer => answer.Question == question).Count(a => a.Option == option),
                            optionPercent = answers.Where(a => a.Question == question).Count() > 0 ?
                                string.Format("{0:0.00}%", (float)answers.Where(a => a.Question == question).Count(a => a.Option == option) / answers.Where(a => a.Question == question).Count() * 100) : "0%"
                        })
                    })
                };

            ViewBag.Results = JToken.FromObject(results);

            return View();
        }
    }
}