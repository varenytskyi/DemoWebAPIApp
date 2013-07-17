/// <reference path="../typings/angularjs/angular.d.ts" />
/// <reference path="Base.ts" />
/// <reference path="Content.ts" />
/// <reference path="Person.ts" />
/// <reference path="Book.ts" />

module Application {
    angular.module('Application', ['ngResource'])
        .config(['$routeProvider', '$locationProvider',
            ($routeProvider: ng.IRouteProvider, $locationProvider: ng.ILocationProvider) => {
                $locationProvider.hashPrefix('!');
                $routeProvider.when('/', {
                    controller: ContentController
                });
                $routeProvider.when('/person/:id', {
                    templateUrl: PersonController.$viewTemplateUrl,
                    controller: PersonController
                });
                $routeProvider.when('/person/edit/:id', {
                    templateUrl: PersonController.$editTemplateUrl,
                    controller: PersonController
                });
                $routeProvider.when('/book/:id', {
                    templateUrl: BookController.$viewTemplateUrl,
                    controller: BookController
                });
                $routeProvider.when('/book/edit/:id', {
                    templateUrl: BookController.$editTemplateUrl,
                    controller: BookController
                });
                $routeProvider.otherwise({ redirectTo: '/' });
            } ])
        .controller('ContentController', ($scope, $resource) => {
            return new ContentController($scope, $resource);
        } )
        .controller('PersonController', ($scope, $rootScope, $resource, $routeParams) => {
            return new PersonController($scope, $rootScope, $resource, $routeParams);
        } )
        .controller('BookController', ($scope, $rootScope, $resource, $routeParams) => {
            return new BookController($scope, $rootScope, $resource, $routeParams);
        } );
}