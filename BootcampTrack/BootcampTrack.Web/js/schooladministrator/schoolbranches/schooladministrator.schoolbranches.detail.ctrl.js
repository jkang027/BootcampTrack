angular.module('app')
    .controller('SchoolAdministratorSchoolBranchDetailController', [
        '$scope',
        '$stateParams',
        'SchoolAdministratorSchoolBranchesResource',
        function ($scope, $stateParams, SchoolBranchesResource) {
            $scope.branch = SchoolBranchesResource.get({ schoolBranchId: $stateParams.id });

            $scope.saveSchoolBranch = function () {
                $scope.branch.$update(function () {
                    alert('save successful');
                });
            };
}]);