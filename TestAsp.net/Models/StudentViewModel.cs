using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAsp.net.Models
{
    public class StudentViewModel
    {
        public IEnumerable<Country> Countries { get; set; }
        public Student Student { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<City> Cities {get; set; }
    }
}