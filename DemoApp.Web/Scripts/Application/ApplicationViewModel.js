function ApplicationViewModel() {
    this._contentBrowser = new ContentBrowser();
    this._detailsArea = new DetailsArea();
    this._draw();
}
ApplicationViewModel.prototype = {
	_contentBrowser: null,
	_detailsArea: null,
	html: null,
	_draw: function () {
		this._contentBrowser.draw();
		this._detailsArea.draw();
		this.html = $($('#application-content').html());
		this.html
			.append(this._contentBrowser.html)
			.append(this._detailsArea.html)
			.appendTo('.main');
	}
}