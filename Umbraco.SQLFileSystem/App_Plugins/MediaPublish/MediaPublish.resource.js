function mediaPublishResource($q, $http, umbRequestHelper) {
    return {
        publishById: function (id, children) {
            return umbRequestHelper.resourcePromise(
                   $http.post("backoffice/mediaPublish/MediaPublishingApi/PublishById", {
                       params: { nodeId: id, includeChildren: children || false }
                   }));
        },
        getHistory: function (id) {
            return umbRequestHelper.resourcePromise(
                  $http.post("backoffice/mediaPublish/MediaPublishingApi/GetHistory", {
                      params: { nodeId: id }
                  }));
        },
        revertVersion: function (id, versionId) {
            return umbRequestHelper.resourcePromise(
                    $http.post("backoffice/mediaPublish/MediaPublishingApi/RevertVersion", {
                        params: { nodeId: id, versionId: versionId }
                    }));
        },
        purgeHistory: function (id) {
            return umbRequestHelper.resourcePromise(
                  $http.post("backoffice/mediaPublish/MediaPublishingApi/PurgeHistory", {
                      params: { nodeId: id }
                  }));
        },
    };
}
angular.module("umbraco.resources").factory("mediaPublishResource", mediaPublishResource);