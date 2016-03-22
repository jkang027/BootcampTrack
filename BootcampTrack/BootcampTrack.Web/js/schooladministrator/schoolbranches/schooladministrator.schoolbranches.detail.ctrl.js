angular.module('app')
    .controller('SchoolAdministratorSchoolBranchDetailController', [
        '$scope',
        '$stateParams',
        'SchoolAdministratorSchoolBranchesResource',
        function ($scope, $stateParams, SchoolBranchesResource) {
            $scope.schoolBranch = SchoolBranchesResource.get({ schoolBranchId: $stateParams.id });

            $scope.saveSchoolBranch = function () {
                $scope.schoolBranch.$update(function () {
                    alert('save successful');
                    activate();
                });
            };
}]);