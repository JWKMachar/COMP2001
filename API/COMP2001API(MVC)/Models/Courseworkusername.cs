using System;
using System.Collections.Generic;

#nullable disable

namespace COMP2001API_MVC_.Models
{
    public partial class Courseworkusername
    {
        public Courseworkusername()
        {
            Courseworkpasswords = new HashSet<Courseworkpassword>();
            Courseworksessions = new HashSet<Courseworksession>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string CurrentPassword { get; set; }

        public virtual ICollection<Courseworkpassword> Courseworkpasswords { get; set; }
        public virtual ICollection<Courseworksession> Courseworksessions { get; set; }
    }
}
