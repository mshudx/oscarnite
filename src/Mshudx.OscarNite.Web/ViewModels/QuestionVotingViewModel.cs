using Mshudx.OscarNite.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mshudx.OscarNite.Web.ViewModels
{
    public class QuestionVotingViewModel
    {
        public string QuestionId { get; set; }
        public string Text { get; set; }
        public string Voted { get; set; }
    }
}
