using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mshudx.OscarNite.Web.ViewModels
{
    public class ResultViewModel
    {
        public string Next { get; set; }
        public string QuestionText { get; set; }
        public List<ResultDataViewModel> QuestionResults { get; set; }
    }
}
