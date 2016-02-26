using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mshudx.OscarNite.Web.Models
{
    public class Vote
    {
        public virtual string Id { get; set; }
        public virtual DateTimeOffset Created { get; set; }
        public virtual string Voter { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
    }
}
