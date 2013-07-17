function Book(koObject) {
	this._proxy = new BookWebProxy();
	this._authorProxy = new PersonWebProxy();
	this._mode = 'view';
	this.koContent = koObject;
}
Book.prototype = $.extend({}, ObjectBase, {
	_authorProxy: null,
	_authors: null,
	_getTemplate: function () {
		return 'book-' + this._mode;
	},
	getAuthorName: function () {
		return this.koContent().Author.Name();
	},
	getAuthors: function () {
		if (!this._authors) {
			var _this = this;
			this._authors = ko.observableArray();
			this._authorProxy.getAll(null, null, function (data) {
				ko.mapping.fromJS(data, {}, _this._authors);
			});
		}
		return this._authors;
	}
});