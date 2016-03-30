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
                        $scope.initializeDone = true;
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

            activate();
}]);