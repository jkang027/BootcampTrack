angular.module('app')
    .factory('SchoolAdministratorInstructorInviteResource', [
        'apiUrl',
        '$http',
        function (apiUrl, $http) {

            function postInstructorInvite(instructorInvitation) {
                return $http.post(apiUrl + 'invite/instructor', instructorInvitation)
                            .then(function (response) {
                                return response.data;
                            });
            }

            function getBranchInstructorInvites(schoolBranchId) {
                return $http.get(apiUrl + 'invite/instructor')
                            .then(function (response) {
                                return response.data;
                            });
            }

            return {
                postInstructorInvite : postInstructorInvite
            };
}]);