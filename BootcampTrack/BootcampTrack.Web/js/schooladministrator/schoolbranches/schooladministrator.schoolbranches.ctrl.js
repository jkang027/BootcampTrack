angular.module('app').controller('SchoolAdministratorSchoolBranchController', function ($scope, AuthenticationService, SchoolAdministratorDashboardResource) {

    function activate() {
        SchoolAdministratorDashboardResource.getUserSchoolBranches().then(function (response) {
            $scope.branches = response;
        });
    }

    activate();
});