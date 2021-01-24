using System;
using System.Collections.Generic;

#nullable disable

namespace COMP2001API_MVC_.Models
{
    public partial class Courseworksession
    {
        public DateTime TimeIssued { get; set; }
        public int UserId { get; set; }

        public virtual Courseworkusername User { get; set; }
    }
}
