angular.module('app')
    .factory('InstructorStudentInviteResource', [
        'apiUrl',
        '$http',
        function (apiUrl, $http) {

            function postStudentInvite(studentInvitation) {
                return $http.post(apiUrl + 'invite/student', studentInvitation)
                            .then(function (response) {
                                return response.data;
                            });
            }

            return {
                postStudentInvite: postStudentInvite
            };
        }]);