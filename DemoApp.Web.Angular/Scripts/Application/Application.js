var Application;
(function (Application) {
    angular.module('Application', [
        'ngResource'
    ]).config([
        '$routeProvider', 
        '$locationProvider', 
        function ($routeProvider, $locationProvider) {
            $locationProvider.hashPrefix('!');
            $routeProvider.when('/', {
                controller: Application.ContentController
            });
            $routeProvider.when('/person/:id', {
                templateUrl: Application.PersonController.$viewTemplateUrl,
                controller: Application.PersonController
            });
            $routeProvider.when('/person/edit/:id', {
                templateUrl: Application.PersonController.$editTemplateUrl,
                controller: Application.PersonController
            });
            $routeProvider.when('/book/:id', {
                templateUrl: Application.BookController.$viewTemplateUrl,
                controller: Application.BookController
            });
            $routeProvider.when('/book/edit/:id', {
                templateUrl: Application.BookController.$editTemplateUrl,
                controller: Application.BookController
            });
            $routeProvider.otherwise({
                redirectTo: '/'
            });
        }    ]).controller('ContentController', function ($scope, $resource) {
        return new Application.ContentController($scope, $resource);
    }).controller('PersonController', function ($scope, $rootScope, $resource, $routeParams) {
        return new Application.PersonController($scope, $rootScope, $resource, $routeParams);
    }).controller('BookController', function ($scope, $rootScope, $resource, $routeParams) {
        return new Application.BookController($scope, $rootScope, $resource, $routeParams);
    });
})(Application || (Application = {}));
