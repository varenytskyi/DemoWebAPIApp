function Registry() {
    this._elements = [];
    this._initialize();
}
Registry.prototype = {
	_elements: null,
	_initialize: function () {
		this._register('content', Content);
		this._register('book', Book);
		this._register('person', Person);
	},
	_register: function (type, obj) {
		if (this.get(type))
			throw 'Such element is already registered';
		this._elements.push({ type: type, obj: obj });
	},
	get: function (type) {
		for (var i = 0, count = this._elements.length; i < count; ++i)
			if (this._elements[i].type == type)
				return this._elements[i].obj;
	}
}