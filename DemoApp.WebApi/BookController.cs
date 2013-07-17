using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoApp.Business;
using DemoApp.Business.Models;

namespace DemoApp.WebApi {
	public class BookController : ApiController {
		private IBookService _bookService;

		protected IBookService BookService {
			get { return _bookService; }
		}

		public BookController(IBookService aBookService) {
			_bookService = aBookService;
		}

		public IEnumerable<Book> Get() {
			return BookService.GetBooks();
		}
		public Book Get(int id) {
			try {
				return BookService.GetBook(id);
			}
			catch {
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
			}
		}
		public Book Post(Book book) {
			return BookService.AddBook(book);
		}
		public Book Put(int id, Book book) {
			book.Id = id;
			return BookService.SaveBook(book);
		}
		public void Delete(int id) {
			BookService.DeleteBook(id);
		}
	}
}