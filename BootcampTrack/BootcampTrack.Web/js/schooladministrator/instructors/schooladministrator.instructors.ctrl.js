angular.module('app')
    .controller('SchoolAdministratorInstructorsController', [
        '$scope',
        'SchoolAdministratorResource',
        'SchoolAdministratorInstructorInviteResource',
        'CurrentSchoolService',
        function ($scope, DashboardService, InviteInstructorService, CurrentSchoolService) {

            CurrentSchoolService.getCurrentSchool().then(function (data) {
                $scope.currentSchool = data;
            });

            $scope.newInstructorInvite = {};

            $scope.inviteInstructor = function () {
                InviteInstructorService.postInstructorInvite($scope.newInstructorInvite)
                    .then(function () {
                        $scope.newInstructorInvite = {};
                        toastr.success("Instructor Invited.");
                        $('#newInstructorInviteModal').modal('hide');
                        $('body').removeClass('modal-open');
                        $('.modal-backdrop').remove();
                    }).then(function () {
                        activate();
                    });
            };

            function activate() {
                $scope.intializeDone = false;

                DashboardService.getSchoolInstructors()
                    .then(function (userSchoolInstructorsResp) {
                        $scope.schoolInstructors = userSchoolInstructorsResp;
                        $scope.initializeDone = true;
                    }).catch(function (errorResponse) {
                        $scope.error = errorResponse;
                    });
            };

            activate();
}]);