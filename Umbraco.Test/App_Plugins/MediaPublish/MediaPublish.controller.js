function mediaPublishController($scope, mediaPublishResource, treeService, navigationService, editorState, $location, dialogService, notificationsService) {
    //$scope.generate = function () {
    //    $scope.generating = true;
    //    umbRequestHelper.resourcePromise(
    //            $http.post(umbRequestHelper.getApiUrl("modelsBuilderBaseUrl", "BuildModels")),
    //            'Failed to generate.')
    //        .then(function (result) {
    //            $scope.generating = false;
    //            $scope.dashboard = result;
    //        });
    //};

    //$scope.reload = function () {
    //    $scope.ready = false;
    //    modelsBuilderResource.getDashboard().then(function (result) {
    //        $scope.dashboard = result;
    //        $scope.ready = true;
    //    });
    //};

    $scope.publishNode = function () {
        //debugger;
        mediaPublishResource.publishById($scope.currentNode.id).then(function () {
            console.log("Success", arguments);
        }, function () {
            console.log("Faill", arguments);
        });
    }
    $scope.cancel = function () {
        navigationService.hideDialog();
    };

    //function init() {
    //    modelsBuilderResource.getDashboard().then(function (result) {
    //        $scope.dashboard = result;
    //        $scope.ready = true;
    //    });
    //}

    //init();
}
angular.module("umbraco").controller("Umbraco.Dashboard.MediaPublish", mediaPublishController);