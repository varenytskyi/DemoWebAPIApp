/// <reference path="Base.ts" />

module Application {

    export class Content {
        Id: number;
        Name: string;
        Description: string;
        Type: string;
        constructor(model: Content) {
            this.Id = model.Id;
            this.Name = model.Name;
            this.Description = model.Description;
            this.Type = model.Type;
        }
    }

    export interface IContentScope<Content> extends IScope<Content> {
    }

    export class ContentService extends Service<Content> {
        constructor($resource: IResourceService) {
            super('Content', $resource);
        }
    }

    export class ContentController {
        private $service: ContentService;
        constructor($scope: IContentScope, $resource: IResourceService) {
            if (this.$service == null)
                this.$service = new ContentService($resource);

            this.SetupScope($scope);

            $scope.$broadcast('updateCollection');
        }

        SetupScope($scope: IContentScope) {
            $scope.$on('updateCollection', () =>
            {
                this.$service.GetAll({}, (data) => {
                    $scope.Collection = data;
                } , (error) => { console.log(error); } );
            } );
        }
    }
}
