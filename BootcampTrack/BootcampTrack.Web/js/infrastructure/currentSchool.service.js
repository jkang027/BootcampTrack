angular.module('app')
    .factory('CurrentSchoolService', [
        '$q',
        '$http',
        'apiUrl',
        function ($q, $http, apiUrl) {

            var cachedDashboard = null;

            function getCurrentSchool() {
                var deferred = $q.defer();

                if (!cachedDashboard) {
                    $http.get(apiUrl + 'dashboard')
                     .success(function (response) {
                         cachedDashboard = response;
                         cachedDashboard.downloadedDate = new Date();
                         deferred.resolve(response);
                     })
                    .error(function () {
                        deferred.reject('An error occurred getting dashboard information');
                    });
                } else {
                    deferred.resolve(cachedDashboard);
                }

                return deferred.promise;
            }

            return {
                getCurrentSchool: getCurrentSchool
            };
}]);