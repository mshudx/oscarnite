using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mshudx.OscarNite.Web.Models
{
    public class Answer
    {
        public virtual string Id { get; set; }
        public virtual Question Question { get; set; }
        public virtual Option Option { get; set; }
        public virtual Vote Vote { get; set; }
    }
}
