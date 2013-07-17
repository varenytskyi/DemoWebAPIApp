using System;

namespace DemoApp.Business.Models {
	public class Person : Content {
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string BirthDate { get; set; }
		public Gender Gender { get; set; }
	}
	public enum Gender { Male = 0, Female = 1 }
}
