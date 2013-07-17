using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DemoApp.Business;
using DemoApp.Business.Models;

namespace DemoApp.WebApi
{
    public class ContentController : ApiController {
		private IContentService _contentService;

		protected IContentService ContentService {
			get { return _contentService; }
		}

		public ContentController(IContentService aContentService) {
			_contentService = aContentService;
		}

		public IEnumerable<Content> Get() {
			return ContentService.GetContents();
		}
		public Content Get(int id) {
			try {
				return ContentService.GetContent(id);
			}
			catch {
				throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
			}
		}
		public Content Post(Content content) {
			return ContentService.AddContent(content);
		}
		public Content Put(int id, Content content) {
			content.Id = id;
			return ContentService.SaveContent(content);
		}
		public void Delete(int id) {
			ContentService.DeleteContent(id);
		}
    }
}