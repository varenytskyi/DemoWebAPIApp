using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using DemoApp.Data.Maps;
using DemoApp.Data.Entities;

namespace DemoApp.Data
{
	public class EntitiesContextInitializer : DropCreateDatabaseIfModelChanges<DemoAppDataContext> {
		protected override void Seed(DemoAppDataContext context) {
			List<Person> persons = new List<Person>
			{
				new Person { Name = "John Smith", Description = "Cool man", Gender = 0, BirthDate = DateTime.Now.AddYears(-40), FirstName = "John", LastName = "Smith" },
				new Person { Name = "Tom Smith", Description = "Cool man", Gender = 0, BirthDate = DateTime.Now.AddYears(-40), FirstName = "Tom", LastName = "Smith" },
				new Person { Name = "Dan Smith", Description = "Cool man", Gender = 0, BirthDate = DateTime.Now.AddYears(-40), FirstName = "Dan", LastName = "Smith" },
				
			};
			persons.ForEach((b) => context.Set<Person>().Add(b));
			List<Book> roles = new List<Book>
			{
				new Book { Name = "Book 1", Description = "Sample book 1", Published = DateTime.Now, Copyright = "Copyright 2012", Author = persons[1] },
				new Book { Name = "Book 2", Description = "Sample book 2", Published = DateTime.Now, Copyright = "Copyright 2012", Author = persons[1] },
				new Book { Name = "Book 3", Description = "Sample book 3", Published = DateTime.Now, Copyright = "Copyright 2012", Author = persons[1] },
				new Book { Name = "Book 4", Description = "Sample book 4", Published = DateTime.Now, Copyright = "Copyright 2012", Author = persons[1] }
			};
			roles.ForEach((b)=> context.Set<Book>().Add(b));

			context.SaveChanges();
		}
	}
    public class DemoAppDataContext : DbContext
    {
        static DemoAppDataContext()
        {
			Database.SetInitializer<DemoAppDataContext>(new EntitiesContextInitializer());
        }

        public DemoAppDataContext()
            : base("Name=DemoAppDataContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContentObjectMap());
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new BookMap());
        }
    }
}