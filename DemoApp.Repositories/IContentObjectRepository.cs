using DemoApp.Data.Entities;
using System.Data.Objects;

namespace DemoApp.Repositories {
	public interface IContentObjectRepository : IRepository<ContentObject> {}

	public class ContentObjectRepository : Repository<ContentObject>, IContentObjectRepository {
		public ContentObjectRepository(ObjectContext aContext)
			: base(aContext) {
		}
	}
}