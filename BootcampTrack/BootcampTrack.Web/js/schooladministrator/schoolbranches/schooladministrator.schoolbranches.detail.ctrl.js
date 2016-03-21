angular.module('app').controller('SchoolAdministratorSchoolBranchDetailController', function ($scope, $stateParams, SchoolAdministratorSchoolBranchesResource) {
    $scope.schoolBranch = SchoolAdministratorSchoolBranchesResource.get({ schoolBranchId: $stateParams.id });

    $scope.saveSchoolBranch = function () {
        $scope.schoolBranch.$update(function () {
            alert('save successful');
            activate();
        });
    };
});