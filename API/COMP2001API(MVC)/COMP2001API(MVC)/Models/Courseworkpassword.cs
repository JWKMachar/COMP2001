using System;
using System.Collections.Generic;

#nullable disable

namespace COMP2001API_MVC_.Models
{
    public partial class Courseworkpassword
    {
        public string OldPassword { get; set; }
        public DateTime DateChanged { get; set; }
        public int UserId { get; set; }

        public virtual Courseworkusername User { get; set; }
    }
}
