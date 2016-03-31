angular.module('app')
    .controller('SchoolAdministratorSchoolBranchesController', [
        '$scope',
        'SchoolAdministratorResource',
        'SchoolAdministratorSchoolBranchesResource',
        'CurrentSchoolService',
        function ($scope, SchoolAdministratorResource, SchoolBranchesResource, CurrentSchoolService) {

            CurrentSchoolService.getCurrentSchool().then(function (data) {
                $scope.currentSchool = data;
            });

            $scope.newBranch = {};

            function activate() {
                $scope.intializeDone = false;

                SchoolAdministratorResource.getUserSchoolBranches()
                    .then(function (userSchoolBranchResp) {
                        $scope.branches = userSchoolBranchResp;
                        $scope.initializeDone = true;
                    }).catch(function (errorResponse) {
                        $scope.error = errorResponse;
                    });
            };

            $scope.addNewBranch = function () {
                SchoolBranchesResource.save($scope.newBranch)
                    .$promise.then(function () {
                        $scope.newBranch = {};
                        toastr.success("Branch successfully added.");
                        $('#newBranchModal').modal('hide');
                        $('body').removeClass('modal-open');
                        $('.modal-backdrop').remove();
                    }).then(function () {
                        activate();
                    });
            };

            activate();
}]);