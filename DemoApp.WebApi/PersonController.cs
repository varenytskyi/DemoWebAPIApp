using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoApp.Business;
using DemoApp.Business.Models;

namespace DemoApp.WebApi {
	public class PersonController : ApiController {
		private IPersonService _personService;

		protected IPersonService PersonService {
			get { return _personService; }
		}

		public PersonController(IPersonService aPersonService) {
			_personService = aPersonService;
		}

		public IEnumerable<Person> Get() {
			return PersonService.GetPersons();
		}
		public Person Get(int id) {
			try {
				return PersonService.GetPerson(id);
			}
			catch {
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
			}
		}
		public Person Post(Person person) {
			return PersonService.AddPerson(person);
		}
		public Person Put(int id, Person person) {
			person.Id = id;
			return PersonService.SavePerson(person);
		}
		public void Delete(int id) {
			PersonService.DeletePerson(id);
		}
	}
}