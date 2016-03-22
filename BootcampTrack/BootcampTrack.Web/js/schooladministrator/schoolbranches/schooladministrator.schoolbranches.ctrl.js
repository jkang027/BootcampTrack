angular.module('app')
    .controller('SchoolAdministratorSchoolBranchController', [
        '$scope',
        'SchoolAdministratorDashboardResource',
        function ($scope, DashboardResource) {

            function activate() {
                DashboardResource.getUserSchoolBranches().then(function (response) {
                    $scope.branches = response;
                });
            }

            activate();
}]);