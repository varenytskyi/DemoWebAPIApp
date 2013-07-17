using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoApp.Data.Entities
{
    public class Book: ContentObject
    {
        public int AuthorId { get; set; }
        public DateTime Published { get; set; }
        public string Copyright { get; set; }

        public virtual Person Author { get; set; }
    }
}