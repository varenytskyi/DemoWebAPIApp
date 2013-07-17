function Person(koObject) {
	this._proxy = new PersonWebProxy();
	this._mode = 'view';
	this.koContent = koObject;
}
Person.prototype = $.extend({}, ObjectBase, {
	genders: [
		{ name: 'Male', value: 0 },
		{ name: 'Female', value: 1 }
	],
	_getTemplate: function () {
		return 'person-' + this._mode;
	},
	getGenderName: function () {
		for (var i = 0; i < this.genders.length; i++) {
			if (this.genders[i].value == this.koContent().Gender())
				return this.genders[i].name;
		}
	}
});