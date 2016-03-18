angular.module('app').controller('SchoolAdministratorBranchController', function ($scope, AuthenticationService, SchoolResource) {

    function activate() {
        SchoolResource.getUserSchoolBranches().then(function (response) {
            $scope.branches = response;
        });
    }

    activate();
});