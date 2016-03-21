angular.module('app').controller('SchoolAdministratorDashboardController', function ($scope, AuthenticationService, SchoolAdministratorDashboardResource) {
    function activate() {
        SchoolAdministratorDashboardResource.getUserSchool().then(function (response) {
            $scope.school = response;
        });
        SchoolAdministratorDashboardResource.getUserSchoolBranches().then(function (response) {
            $scope.branches = response;
        });
        SchoolAdministratorDashboardResource.getUserCourses().then(function (response) {
            $scope.courses = response;
        });
        SchoolAdministratorDashboardResource.getUserEnrollments().then(function (response) {
            $scope.enrollments = response;
        });
    }

    $scope.getBranchCount = function () {
        var count = 0;
        angular.forEach($scope.branches, function (branch) {
            count += branch ? 1 : 0;
        });
        return count;
    }

    $scope.getCourseCount = function () {
        var count = 0;
        angular.forEach($scope.course, function (course) {
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

    activate();
});