angular.module('app')
    .factory('CurrentSchoolService', [
        '$q',
        '$http',
        'apiUrl',
        function ($q, $http, apiUrl) {

            function getCurrentSchool() {
                return $http.get(apiUrl + 'dashboard')
                    .then(function (response) {
                        return response.data;
                    });
            }

            return {
                getCurrentSchool: getCurrentSchool
            };
}]);