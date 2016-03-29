angular.module('app')
    .controller('SchoolAdministratorDashboardController', [
        '$scope',
        'AuthenticationService',
        'SchoolAdministratorResource',
        'CurrentSchoolService',
        function ($scope, AuthenticationService, DashboardService, CurrentSchoolService) {

            var activate = function () {
                $scope.initializeDone = false;

                CurrentSchoolService.getCurrentSchool().then(
                    function (data) {
                        $scope.dashboard = data;
                    },
                    function (error) {
                        alert(error);
                    }
                );

                //DashboardService.getUserSchool()
                //    .then(function (userSchoolResp) {
                //        $scope.school = userSchoolResp;
                //        return DashboardService.getUserSchoolBranches();

                //    }).then(function (userSchoolBranchResp) {
                //        $scope.branches = userSchoolBranchResp;
                //        return DashboardService.getUserCourses();

                //    }).then(function (userCoursesResp) {
                //        $scope.courses = userCoursesResp;
                //        return DashboardService.getUserEnrollments();

                //    }).then(function (enrollmentsResp) {
                //        $scope.enrollments = enrollmentsResp;
                //        return DashboardService.getSchoolProjects();

                //    }).then(function (schoolProjectsResp) {
                //        $scope.projects = schoolProjectsResp;
                //        $scope.initializeDone = true;

                //    }).catch(function (errorResponse) {
                //        $scope.error = errorResponse;
                //    });
            };

            $scope.getBranchCount = function () {
                var count = 0;
                angular.forEach($scope.branches, function (branch) {
                    count += branch ? 1 : 0;
                });
                return count;
            }

            $scope.getCourseCount = function () {
                var count = 0;
                angular.forEach($scope.courses, function (course) {
                    count += course ? 1 : 0;
                });
                return count;
            }

            $scope.getEnrollmentCount = function () {
                var count = 0;
                angular.forEach($scope.enrollments, function (enrollment) {
                    count += enrollment ? 1 : 0;
                });
                return count;
            }

            $scope.getProjectCount = function () {
                var count = 0;
                angular.forEach($scope.projects, function (project) {
                    count += project ? 1 : 0;
                });
                return count;
            }

            activate();
        }]);