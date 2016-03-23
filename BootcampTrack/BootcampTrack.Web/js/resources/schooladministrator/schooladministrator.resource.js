angular.module('app')
    .factory('SchoolAdministratorResource',
    function (apiUrl, $http) {

    function getUserSchool() {
        return $http.get(apiUrl + 'user/school')
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

    function getSchoolBranchCourses(id) {
        return $http.get(apiUrl + 'schoolbranches/' + id + '/courses')
                    .then(function (response) {
                        return response.data;
                    });
    }

    return {
        getUserSchool: getUserSchool,
        getUserSchoolBranches: getUserSchoolBranches,
        getUserCourses: getUserCourses,
        getUserEnrollments: getUserEnrollments,
        getSchoolBranchCourses: getSchoolBranchCourses
    };
});