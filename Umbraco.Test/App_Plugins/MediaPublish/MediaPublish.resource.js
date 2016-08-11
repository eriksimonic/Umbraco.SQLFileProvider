function mediaPublishResource($q, $http, umbRequestHelper) {
    return {
        publishById: function (id, children) {
            return umbRequestHelper.resourcePromise(
                   $http.get( "backoffice/Api/MediaPublishingApi/PublishBsyId", {
                                params: { nodeId: id, includeChildren: children || false }
                   }));

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