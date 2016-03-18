angular.module('app').controller('SchoolAdministratorSchoolBranchController', function ($scope, AuthenticationService, SchoolResource) {

    function activate() {
        SchoolResource.getUserSchoolBranches().then(function (response) {
            $scope.branches = response;
        });
    }

    activate();
});