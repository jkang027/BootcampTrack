angular.module('app').factory('SchoolAdministratorProfileResource', function (apiUrl, $http) {

    function getUserSchool() {
        return $http.get(apiUrl + 'user/school')
                    .then(function (response) {
                        return response.data;
                    });
    }

    function getUserProfile() {
        return $http.get(apiUrl + 'user/profile')
                    .then(function (response) {
                        return response.data
                    });
    }

    return {
        getUserSchool: getUserSchool,
        getUserProfile: getUserProfile
    };
});