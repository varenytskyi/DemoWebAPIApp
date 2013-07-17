using System.Collections.Generic;
using System.Linq;
using DemoApp.Repositories;
using DemoApp.Business.Models;
using System;
using DemoApp.Core;

namespace DemoApp.Business {
	public interface IBookService {
		IEnumerable<Book> GetBooks();
		Book GetBook(int aId);
		Book AddBook(Book aBook);
		Book SaveBook(Book aBook);
		void DeleteBook(int aId);
	}

	public class BookService : IBookService {
        private IContentObjectRepository _bookRepository;
        private SimpleMapper _simpleMapper;

        private IContentObjectRepository Repository
        {
            get { return _bookRepository; }
        }

		public BookService(IContentObjectRepository aRepository) {
			_bookRepository = aRepository;
            _simpleMapper = new SimpleMapper();

            _simpleMapper.AddMapper<Data.Entities.Book, Book>((aBook) => new Book {
                Id = aBook.Id,
                Name = aBook.Name,
                Description = aBook.Description,
                Author = _simpleMapper.Map<Data.Entities.Person, Person>(aBook.Author),
                Copyright = aBook.Copyright,
                Published = aBook.Published.ToShortDateString(),
				Type = "book"
            });
            _simpleMapper.AddMapper<Book, Data.Entities.Book>((Book aBook) => new Data.Entities.Book {
				Id = aBook.Id,
				Name = aBook.Name,
				Description = aBook.Description,
				AuthorId = aBook.Author.Id,
				Author = _simpleMapper.Map<Person, Data.Entities.Person>(aBook.Author),
				Copyright = aBook.Copyright,
				Published = DateTime.Parse(aBook.Published),
			});
            _simpleMapper.AddMapper<Data.Entities.Person, Person>((aPerson) => new Person
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
            _simpleMapper.AddMapper<Person, Data.Entities.Person>((Person aPerson) => new Data.Entities.Person
            {
                Id = aPerson.Id,
                Name = aPerson.Name,
                Description = aPerson.Description,
                BirthDate = DateTime.Parse(aPerson.BirthDate),
                FirstName = aPerson.FirstName,
                LastName = aPerson.LastName,
                Gender = (int)aPerson.Gender
            });
		}
		
		public IEnumerable<Book> GetBooks() {
            return Repository.Get().OfType<Data.Entities.Book>().Map(_simpleMapper.Map<Data.Entities.Book, Book>);
		}
		public Book GetBook(int aId) {
            return _simpleMapper.Map<Data.Entities.Book, Book>(Repository.Get().OfType<Data.Entities.Book>().Single(x => x.Id == aId));
		}
		public Book AddBook(Book aBook) {
			using (var tran = Repository.BeginTran()) {
				var entity = Repository.Create(_simpleMapper.Map<Book, Data.Entities.Book>(aBook));
				aBook.Id = entity.Id;
				tran.Commit();
			}
			return aBook;
		}
		public Book SaveBook(Book aBook) {
			using (var tran = Repository.BeginTran()) {
				var entity = Repository.Get().OfType<Data.Entities.Book>().Single(x => x.Id == aBook.Id);
                if (aBook.Author != null)
                {
                    entity.AuthorId = aBook.Author.Id;
                    entity.Author = Repository.Get().OfType<Data.Entities.Person>().Single(x => x.Id == aBook.Author.Id);
                }
                entity.Copyright = aBook.Copyright;
                entity.Description = aBook.Description;
                entity.Name = aBook.Name;
                entity.Published = DateTime.Parse(aBook.Published);
                entity = (Data.Entities.Book)Repository.Save(entity);
				tran.Commit();
			}
			return aBook;
		}
		public void DeleteBook(int aId) {
			Repository.Delete(Repository.Get().OfType<Data.Entities.Book>().Single(x => x.Id == aId));
		}
	}
}