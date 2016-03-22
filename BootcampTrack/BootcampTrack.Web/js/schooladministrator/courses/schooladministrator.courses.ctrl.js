angular.module('app').controller('SchoolAdministratorCourseController', function ($scope, AuthenticationService) {

    function activate() {
        SchoolAdministratorDashboardResource.getUserSchoolBranches().then(function (response) {
            $scope.branches = response;
        });
    }

    activate();
});;