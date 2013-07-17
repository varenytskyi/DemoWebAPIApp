var Application;
(function (Application) {
    var Service = (function () {
        function Service($path, $resource) {
            this.$path = $path;
            this.$resource = $resource;
            this.$provider = this.$resource('Api/' + this.$path + '/:id', {
                id: '@Id'
            }, {
                update: {
                    method: 'PUT'
                }
            });
        }
        Service.prototype.Get = function (id, success, error) {
            this.$provider.get({
                id: id
            }, success, error);
        };
        Service.prototype.GetAll = function (filter, success, error) {
            this.$provider.query(filter, success, error);
        };
        Service.prototype.Create = function (model, success, error) {
            this.$provider.save(model, success, error);
        };
        Service.prototype.Update = function (model, success, error) {
            this.$provider.update(model, success, error);
        };
        Service.prototype.Delete = function (model, success, error) {
            this.$provider.delete(model, success, error);
        };
        return Service;
    })();
    Application.Service = Service;    
})(Application || (Application = {}));
