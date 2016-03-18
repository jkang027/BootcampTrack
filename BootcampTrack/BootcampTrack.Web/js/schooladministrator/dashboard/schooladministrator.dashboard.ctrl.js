angular.module('app').controller('SchoolAdministratorDashboardController', function ($scope, AuthenticationService, SchoolResource) {
    function activate() {
        SchoolResource.getUserSchoolBranches().then(function (response) {
            $scope.branches = response;
        });
        SchoolResource.getUserSchools().then(function (response) {
            $scope.schools = response;
        });
        SchoolResource.getUserCourses().then(function (response) {
            $scope.courses = response;
        });
        SchoolResource.getUserEnrollments().then(function (response) {
            $scope.enrollments = response;
        });
    }

    activate();
});