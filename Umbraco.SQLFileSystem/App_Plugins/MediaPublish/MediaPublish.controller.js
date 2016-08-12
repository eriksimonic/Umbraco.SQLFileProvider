function mediaPublishController($scope, mediaPublishResource, treeService, navigationService, editorState, $location, dialogService, notificationsService) {

    $scope.folderNode = {
         publishChildren : true
    };
    $scope.currentNode.loadingInline = 1;

    $scope.showMasPublish = function () {
        return $scope.currentNode.metaData.contentType !== 'Folder';
    }

   
    $scope.publishNode = function () {
        $scope.currentNode.loadingInline = 2; 
        mediaPublishResource.publishById($scope.currentNode.id).then(function (data) {
            $scope.currentNode.loadingInline = 4;
            setTimeout(function () { navigationService.hideDialog(); $scope.$apply(); }, 2000);
        }, function () {
           // console.log(arguments);
            $scope.currentNode.loadingInline = 3;
            setTimeout(function () { navigationService.hideDialog(); $scope.$apply(); }, 2000);
        });

        console.log(notificationsService);
    }
    $scope.cancel = function () {
        navigationService.hideDialog();
    };
}


function mediaPublishHistory($scope, mediaPublishResource, treeService, navigationService, editorState, $location, dialogService, notificationsService)
{
    $scope.history = [];

    $scope.getStatus = function (m) {
        switch(m.StatusCode)
        {
            case 1: return "icon icon-checkbox color-green";
            case 0: return "icon icon-checkbox-dotted color-black";
            case 2: return "icon icon-checkbox-empty color-red";
            default: return "icon icon-alert-alt color-red";
        }
    }

    mediaPublishResource.getHistory($scope.currentNode.id).then(function (data) {
        console.log(data);
        $scope.history = data;
    }, function () {

    });

    $scope.cancel = function () {
        navigationService.hideDialog();
    };

    $scope.revertTo = function (nodeId, versionId) {
        console.log(nodeId, versionId);
    }
}
angular.module("umbraco").controller("Umbraco.Dashboard.MediaPublish", mediaPublishController);
angular.module("umbraco").controller("Umbraco.Dashboard.MediaPublish.History", mediaPublishHistory)