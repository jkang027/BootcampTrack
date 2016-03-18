angular.module('app').controller('SchoolAdministratorSchoolBranchDetailController', function ($scope, $stateParams, SchoolBranchResource) {
    $scope.schoolBranch = SchoolBranchResource.get({ schoolBranchId: $stateParams.id });

    $scope.saveSchoolBranch = function () {
        $scope.schoolBranch.$update(function () {
            alert('save successful');
            activate();
        });
    };
});