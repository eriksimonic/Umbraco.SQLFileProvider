function mediaPublishController($scope, mediaPublishResource, treeService, navigationService, editorState, $location, dialogService, notificationsService) {

    $scope.folderNode = {
         publishChildren : true
    };
    $scope.currentNode.loadingInline = 1;

    $scope.showMasPublish = function () {
        return $scope.currentNode.metaData.contentType != 'Folder';
    }

    $scope.publishNode = function () {
        $scope.currentNode.loadingInline = 2;
        mediaPublishResource.publishById($scope.currentNode.id).then(function (data) {
            $scope.currentNode.loadingInline = 4;
            setTimeout(function () { navigationService.hideDialog(); $scope.$apply(); }, 2000);
        }, function () {
            console.log(arguments);
            $scope.currentNode.loadingInline = 3;
        });

        console.log(notificationsService);
    }
    $scope.cancel = function () {
        navigationService.hideDialog();
    };
}
angular.module("umbraco").controller("Umbraco.Dashboard.MediaPublish", mediaPublishController);