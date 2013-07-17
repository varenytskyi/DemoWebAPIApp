function Content(koObject) {
	this._proxy = new ContentWebProxy();
	this._mode = 'view';
	this._shouldGetData = false,
    this.koContent = koObject;
}
Content.prototype = $.extend({}, ObjectBase, {
	_getTemplate: function () {
		return 'content-' + this._mode;
	},
	onclick: function () {
		window.App._detailsArea.initialize(this.koContent);
	}
});