angular.module('app')
    .controller('SchoolAdministratorSchoolBranchDetailController', [
        '$scope',
        '$stateParams',
        'SchoolAdministratorResource',
        'SchoolAdministratorSchoolBranchesResource',
        function ($scope, $stateParams, SchoolAdministratorResource, SchoolBranchesResource) {
            $scope.branch = SchoolBranchesResource.get({ schoolBranchId: $stateParams.id });

            $scope.updateSchoolBranch = function () {
                $scope.branch.$update(function () {
                    toastr.success('Update Successful');
                });
            };

            function activate() {
                $scope.initializeDone = false;

                SchoolAdministratorResource.getSchoolBranchCourses($stateParams.id)
                    .then(function (userSchoolBranchCoursesResp) {
                        $scope.schoolBranchCourses = userSchoolBranchCoursesResp;
                        return SchoolAdministratorResource.getSchoolBranchInstructors($stateParams.id)
                    }).then(function (userSchoolBranchInstructorsResp) {
                        $scope.schoolBranchInstructors = userSchoolBranchInstructorsResp;
                        return SchoolAdministratorResource.getSchoolBranchInstructorInvites($stateParams.id)
                    }).then(function (userSchoolBranchInstructorInvitesResp) {
                        $scope.schoolBranchInstructorInvites = userSchoolBranchInstructorInvitesResp;
                        $scope.initializeDone = true;
                    }).catch(function (errorResponse) {
                        $scope.error = errorResponse;
                    });
            };

            activate();
}]);