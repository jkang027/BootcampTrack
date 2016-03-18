angular.module('app').factory('SchoolResource', function (apiUrl, $http) {

    function getUserSchools() {
        return $http.get(apiUrl + 'user/schools')
                    .then(function (response) {
                        return response.data;
                    });
    }

    function getUserSchoolBranches() {
        return $http.get(apiUrl + 'user/schoolbranches')
                    .then(function (response) {
                        return response.data;
                    });
    }

    function getUserCourses() {
        return $http.get(apiUrl + 'user/courses')
                    .then(function (response) {
                        return response.data;
                    });
    }

    function getUserEnrollments() {
        return $http.get(apiUrl + 'user/enrollments')
                    .then(function (response) {
                        return response.data;
                    });
    }

    return {
        getUserSchools: getUserSchools,
        getUserSchoolBranches: getUserSchoolBranches,
        getUserCourses: getUserCourses,
        getUserEnrollments: getUserEnrollments
    };
});