angular.module('app')
    .factory('SchoolAdministratorResource', [
        'apiUrl',
        '$http',
        function (apiUrl, $http) {

            function getUserSchool() {
                return $http.get(apiUrl + 'user/school')
                            .then(function (response) {
                                return response.data;
                            });
            }

            function getSchoolProjects() {
                return $http.get(apiUrl + 'user/school/projects')
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

            function getSchoolBranchInstructors(id) {
                return $http.get(apiUrl + 'schoolbranches/' + id + '/instructors')
                            .then(function (response) {
                                return response.data;
                            });
            }

            function getSchoolBranchInstructorInvites(id) {
                return $http.get(apiUrl + 'schoolbranches/' + id + '/instructorinvites')
                            .then(function (response) {
                                return response.data;
                            });
            }

            function getCourseProjects(id) {
                return $http.get(apiUrl + 'courses/' + id + '/projects')
                            .then(function (response) {
                                return response.data;
                            });
            }

            function getCourseInstructors(id) {
                return $http.get(apiUrl + 'courses/' + id + '/courseinstructors')
                            .then(function (response) {
                                return response.data;
                            });
            }

            function getCourseStudents(id) {
                return $http.get(apiUrl + 'courses/' + id + '/students')
                            .then(function (response) {
                                return response.data;
                            });
            }

            function getCourseStudentInvites(id) {
                return $http.get(apiUrl + 'courses/' + id + '/studentinvites')
                            .then(function (response) {
                                return response.data;
                            });
            }

            function getSchoolInstructors() {
                return $http.get(apiUrl + 'user/school/instructors')
                            .then(function (response) {
                                return response.data;
                            });
            }

            function getSchoolStudents() {
                return $http.get(apiUrl + 'user/school/students')
                            .then(function (response) {
                                return response.data;
                            });
            }

            return {
                getUserSchool: getUserSchool,
                getSchoolProjects: getSchoolProjects,
                getUserSchoolBranches: getUserSchoolBranches,
                getUserCourses: getUserCourses,
                getUserEnrollments: getUserEnrollments,
                getSchoolBranchCourses: getSchoolBranchCourses,
                getSchoolInstructors: getSchoolInstructors,
                getSchoolStudents: getSchoolStudents,
                getSchoolBranchInstructors: getSchoolBranchInstructors,
                getSchoolBranchInstructorInvites: getSchoolBranchInstructorInvites,
                getCourseProjects: getCourseProjects,
                getCourseInstructors: getCourseInstructors,
                getCourseStudents: getCourseStudents,
                getCourseStudentInvites: getCourseStudentInvites
            };
}]);