using System;

namespace DemoApp.Business.Models {
	public class Book : Content {
		public string Published { get; set; }
		public string Copyright { get; set; }
		public Person Author { get; set; }
	}
}