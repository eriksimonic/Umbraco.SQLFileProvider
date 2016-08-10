function mediaPublishResource($q, $http, umbRequestHelper) {

    return {
     
        publishById: function (id) {
            return umbRequestHelper.resourcePromise(
                   $http.post(
                "backoffice/MediapublishApi/MediaPublish/PublishById" 
                ));
                         //umbRequestHelper.post(
                         //      ,
                         //      [{ id: id }])));
        },



        doSomething: function () {
            alert('A');
        }
    };
}
angular.module("umbraco.resources").factory("mediaPublishResource", mediaPublishResource);
