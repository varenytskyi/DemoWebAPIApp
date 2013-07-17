/// <reference path="../typings/angularjs/angular.d.ts" />
/// <reference path="../typings/angularjs/angular-resource.d.ts" />
module Application {

    export interface IScope<T> extends ng.IScope {
        Collection: T[];
    }

    export interface IResource extends ng.resource.IResourceClass {

    }

    export interface IResourceService extends ng.resource.IResourceService {
        (url: string, paramDefaults?: any, actionDescriptors?: any): IResource;
    }

    export interface IRouteParams {
        id: number;
    }

    export interface IService<T> {
        Get(id: number, success: (model: T) => void , error: (reason) => void ): void;
        GetAll(filter: any, success: (model: T[]) => void , error: (reason) => void ): void;
        Create(model: T, success: (model: T) => void , error: (reason) => void ): void;
        Update(model: T, success: (model: T) => void , error: (reason) => void ): void;
        Delete(model: T, success: (model: T) => void , error: (reason) => void ): void;
    }

    export class Service<T> implements IService<T> {
        private $provider: IResource;
        constructor(private $path: string, private $resource: IResourceService) {
            this.$provider = this.$resource('Api/' + this.$path + '/:id', { id: '@Id' }, { update: { method: 'PUT' } });
        }

        Get(id: number, success: (model: T) => void , error: (reason: any) => void ) {
            this.$provider.get({ id: id }, success, error);
        }

        GetAll(filter: any, success: (model: T[]) => void , error: (reason: any) => void ) {
            this.$provider.query(filter, success, error);
        }

        Create(model: T, success: (model: T) => void , error: (reason: any) => void ) {
            this.$provider.save(model, success, error);
        }

        Update(model: T, success: (model: T) => void , error: (reason: any) => void ) {
            this.$provider.update(model, success, error);
        }

        Delete(model: T, success: (model: T) => void , error: (reason: any) => void ) {
            this.$provider.delete(model, success, error);
        }
    }
}