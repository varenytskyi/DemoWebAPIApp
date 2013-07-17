function DetailsArea() {
}
DetailsArea.prototype = {
	_registry: new Registry(), // TODO: make this object global
	_object: null,
	html: null,
	initialize: function (content) {
		this.html.empty();
		this._object = new (this._registry.get(content().Type()))(content);
		this._object.draw();
		this.html.append(this._object.html);
	},
	draw: function () {
		if (this.html)
			this.html.empty();
		else
			this.html = $($('#details-area').html());
	}
}