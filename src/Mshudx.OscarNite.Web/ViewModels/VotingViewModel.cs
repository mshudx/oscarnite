using Mshudx.OscarNite.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mshudx.OscarNite.Web.ViewModels
{
    public class VotingViewModel
    {
        public List<Question> Questions { get; set; }
        public List<Option> Options { get; set; }
    }
}
