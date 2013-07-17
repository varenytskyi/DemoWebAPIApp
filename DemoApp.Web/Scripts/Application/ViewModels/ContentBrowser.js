function ContentBrowser() {
	this._contentWebProxy = new ContentWebProxy();
}
ContentBrowser.prototype = {
	_registry: new Registry(), // TODO: make this object global
	_items: null,
	_contentWebProxy: null,
	_shouldGetData: true,
	_page: 1,
	_pageItemsCount: 10,
	html: null,
	_getData: function (success) {
		var _this = this;
		this._contentWebProxy.getAll(this._page, this._pageItemsCount, function (data) {
			var mappedArray = ko.observableArray();
			ko.mapping.fromJS(data, {}, mappedArray);
			_this._shouldGetData = false;
			_this._items = [];
			for (var i = 0, count = mappedArray().length; i < count; ++i)
				_this._items.push(new Content(ko.observable(mappedArray()[i])));
			success.call(_this);
		});
	},
	draw: function () {
		if (this.html)
			this.html.empty();
		else
			this.html = $($('#content-browser').html());
		if (this._shouldGetData) {
			this._getData(this.draw);
			return;
		}
		for (var i = 0, count = this._items.length; i < count; ++i) {
			this._items[i].draw();
			this.html.append(this._items[i].html);
		}
	}
}