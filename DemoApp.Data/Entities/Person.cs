using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoApp.Data.Entities
{
    public class Person: ContentObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Gender { get; set; }
    }
}
