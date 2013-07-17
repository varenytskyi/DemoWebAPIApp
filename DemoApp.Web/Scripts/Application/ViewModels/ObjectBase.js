var ObjectBase = {
	html: null, // html representation
	_mode: null, // edit, view
	_proxy: null, // proxy to perform web operations
	koContent: null, //KnockOut object model
	_shouldGetData: true,
	_getTemplate: function () {
		alert('Method _getTemplate should be specified in derived class. It should return template id based on object type and mode');
	},
	_processResult: function (data) {
		this.koContent(ko.mapping.fromJS(data));
	},
	_getData: function (callback) {
		var _this = this;
		this._proxy.get(this.koContent().Id(), function (data) {
			_this._shouldGetData = false;
			_this._processResult(data);
			callback.call(_this);
		});
	},
	draw: function () {
		if (this._shouldGetData) {
			this.html = $('<div>Loading...</div>')
			this._getData(this.draw);
			return;
		}
		var html = $($('#' + this._getTemplate()).html());
		if (this.html) {
			this.html.replaceWith(html);
		}
		this.html = html;
		ko.applyBindings(this, this.html[0]);
	},
	edit: function () {
		this._mode = 'edit';
		this.draw();
	},
	view: function () {
		this._mode = 'view';
		this.draw();
	},
	save: function () {
		if (this._mode != 'edit')
			return;
		var _this = this;
		var handler = function (data) {
			_this.koContent(ko.mapping.fromJS(data));
			_this.view();
		}
		if (this.koContent().Id)
			this._proxy.put(ko.mapping.toJS(this.koContent()), handler);
		else
			this._proxy.post(ko.mapping.toJS(this.koContent()), handler);
	}
}