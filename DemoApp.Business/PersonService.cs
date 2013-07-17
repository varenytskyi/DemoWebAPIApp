using System.Collections.Generic;
using System.Linq;
using DemoApp.Repositories;
using DemoApp.Business.Models;
using DemoApp.Core;
using System;

namespace DemoApp.Business {
	public interface IPersonService {
		IEnumerable<Person> GetPersons();
		Person GetPerson(int aId);
		Person AddPerson(Person aPerson);
		Person SavePerson(Person aPerson);
		void DeletePerson(int aId);
	}

	public class PersonService : IPersonService {
		private IContentObjectRepository _personRepository;
        private SimpleMapper _simpleMapper;

		protected IContentObjectRepository Repository {
			get { return _personRepository; }
		}

        public PersonService(IContentObjectRepository aRepository)
        {
			_personRepository = aRepository;
            _simpleMapper = new SimpleMapper();
            _simpleMapper.AddMapper<Data.Entities.Person, Person>((Data.Entities.Person aPerson) => new Person
                {
                    Id = aPerson.Id,
					Name = aPerson.Name,
                    Description = aPerson.Description,
                    BirthDate = aPerson.BirthDate.ToShortDateString(),
                    FirstName = aPerson.FirstName,
                    LastName = aPerson.LastName,
                    Gender = (Gender)aPerson.Gender,
					Type = "person"
                });
            _simpleMapper.AddMapper((Person aPerson) =>
            new Data.Entities.Person
            {
                Id = aPerson.Id,
                Name = aPerson.FirstName + " " + aPerson.LastName,
                Description = aPerson.Description,
                BirthDate = DateTime.Parse(aPerson.BirthDate),
                FirstName = aPerson.FirstName,
                LastName = aPerson.LastName,
                Gender = (int)aPerson.Gender
            });
		}

		public IEnumerable<Person> GetPersons() {
            return Repository.Get().OfType<Data.Entities.Person>().Map(_simpleMapper.Map<Data.Entities.Person, Person>);
		}
		public Person GetPerson(int aId) {
            return _simpleMapper.Map<Data.Entities.Person, Person>(Repository.Get().OfType<Data.Entities.Person>().Single(x => x.Id == aId));
		}
		public Person AddPerson(Person aPerson) {
			using (var tran = Repository.BeginTran()) {
                var entity = Repository.Create(_simpleMapper.Map<Person, Data.Entities.Person>(aPerson));
				aPerson.Id = entity.Id;
				tran.Commit();
			}
			return aPerson;
		}
		public Person SavePerson(Person aPerson) {
			using (var tran = Repository.BeginTran()) {
                var entity = Repository.Get().OfType<Data.Entities.Person>().Single(x => x.Id == aPerson.Id);
                entity.BirthDate = DateTime.Parse(aPerson.BirthDate);
                entity.Description = aPerson.Description;
                entity.FirstName = aPerson.FirstName;
                entity.Gender = (int)aPerson.Gender;
                entity.LastName = aPerson.LastName;
                entity.Name = string.Format("{0} {1}", aPerson.FirstName, aPerson.LastName);
                entity = (Data.Entities.Person)Repository.Save(entity);
				tran.Commit();
                return _simpleMapper.Map<Data.Entities.Person, Person>(entity);
			}
		}
		public void DeletePerson(int aId) {
            Repository.Delete(Repository.Get().OfType<Data.Entities.Person>().Single(x => x.Id == aId));
		}
	}
}