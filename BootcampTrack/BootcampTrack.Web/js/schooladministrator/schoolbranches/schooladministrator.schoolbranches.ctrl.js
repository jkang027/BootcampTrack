angular.module('app')
    .controller('SchoolAdministratorSchoolBranchesController', [
        '$scope',
        'SchoolAdministratorResource',
        'SchoolAdministratorSchoolBranchesResource',
        function ($scope, SchoolAdministratorResource, SchoolBranchesResource) {

            $scope.newBranch = {};

            function activate() {
                $scope.intializeDone = false;

                SchoolAdministratorResource.getUserSchoolBranches()
                    .then(function (userSchoolBranchResp) {
                        $scope.branches = userSchoolBranchResp;
                        return SchoolAdministratorResource.getUserSchool();
                    }).then(function (userSchoolResponse) {
                        $scope.school = userSchoolResponse;
                        $scope.initializeDone = true;
                    }).catch(function (errorResponse) {
                        $scope.error = errorResponse;
                    });
            };

            $scope.addNewBranch = function () {
                SchoolBranchesResource.save($scope.newBranch)
                    .$promise.then(function () {
                        $scope.newBranch = {};
                        alert("Branch successfully added.");
                        $('#newBranchModal').modal('hide');
                        $('body').removeClass('modal-open');
                        $('.modal-backdrop').remove();
                    }).then(function () {
                        activate();
                    });
            };

            activate();
}]);