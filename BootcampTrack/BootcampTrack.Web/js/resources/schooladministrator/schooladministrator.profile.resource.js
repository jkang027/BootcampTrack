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

    function updateSchool(school) {
        return $http.put(apiUrl + 'user/school/' + school.SchoolAdministratorId, school)
                    .then(function () {
                        return true;
                    });
    }

    function updateProfile(profile) {
        return $http.put(apiUrl + 'user/profile/' + profile.Id, profile)
                    .then(function () {
                        return true;
                    });
    }

    return {
        getUserSchool: getUserSchool,
        getUserProfile: getUserProfile,
        updateSchool: updateSchool,
        updateProfile: updateProfile
    };
});