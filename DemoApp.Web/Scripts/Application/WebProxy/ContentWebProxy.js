function ContentWebProxy(baseUrl) {
	this.baseUrl = baseUrl || this.baseUrl;
}
ContentWebProxy.prototype = $.extend({}, WebProxyBase, {
    _serviceUrl: '/Content',
    getAll: function (page, pageSize, success, error) {
        this._asyncJsonCallBase(this._serviceUrl, 'GET', { page: page, pageSize: pageSize }, success, error);
    },
    get: function (id, success, error) {
        this._asyncJsonCallBase(this._serviceUrl+ '/' + id, 'GET', null, success, error);
    },
    post: function (content, success, error) {
        this._asyncJsonCallBase(this._serviceUrl, 'POST', JSON.stringify(content), success, error);
    },
    put: function (content, success, error) {
        this._asyncJsonCallBase(this._serviceUrl + '/' + content.Id, 'PUT', JSON.stringify(content), success, error);
    },
    remove: function (id, success, error) {
        this._asyncJsonCallBase(this._serviceUrl + '/' + id, 'DELETE', null, success, error);
    }
});