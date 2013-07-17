using System.Collections.Generic;
using System.Linq;
using DemoApp.Repositories;
using DemoApp.Business.Models;
using DemoApp.Core;

namespace DemoApp.Business {
	public interface IContentService {
		IEnumerable<Content> GetContents();
		Content GetContent(int aId);
		Content AddContent(Content aContent);
		Content SaveContent(Content aContent);
		void DeleteContent(int aId);
	}

	public class ContentService : IContentService {
		private IContentObjectRepository _contentObjectRepository;
		private SimpleMapper _simpleMapper;

		protected IContentObjectRepository Repository {
			get { return _contentObjectRepository; }
		}

		public ContentService(IContentObjectRepository aRepository)
		{
			_contentObjectRepository = aRepository;
			_simpleMapper = new SimpleMapper();
			_simpleMapper.AddMapper<Data.Entities.ContentObject, Content>((aContentObject) =>
				new Content
				{
					Id = aContentObject.Id,
					Name = aContentObject.Name,
					Description = aContentObject.Description,
					Type = aContentObject is Data.Entities.Book ? "book" : "person"
				});
			_simpleMapper.AddMapper<Content, Data.Entities.ContentObject>((aContent) =>
				new Data.Entities.ContentObject
				{
					Id = aContent.Id,
					Name = aContent.Name,
					Description = aContent.Description
				});
		}

		public IEnumerable<Content> GetContents() {
			return Repository.Get().Map(_simpleMapper.Map<Data.Entities.ContentObject, Content>);
		}
		public Content GetContent(int aId) {
			return _simpleMapper.Map<Data.Entities.ContentObject, Content>(Repository.Get().Single(x => x.Id == aId));
		}
		public Content AddContent(Content aContent) {
			using (var tran = Repository.BeginTran()) {
				var entity = Repository.Create(_simpleMapper.Map<Content, Data.Entities.ContentObject>(aContent));
				aContent.Id = entity.Id;
				tran.Commit();
			}
			return aContent;
		}
		public Content SaveContent(Content aContent) {
			using (var tran = Repository.BeginTran()) {
				var entity = Repository.Get().Single(x => x.Id == aContent.Id);
				entity.Description = aContent.Description;
				entity.Name = aContent.Name;
				entity = Repository.Save(entity);
				tran.Commit();
				return _simpleMapper.Map<Data.Entities.ContentObject, Content>(entity);
			}
		}
		public void DeleteContent(int aId) {
			Repository.Delete(Repository.Get().Single(x => x.Id == aId));
		}
	}
}