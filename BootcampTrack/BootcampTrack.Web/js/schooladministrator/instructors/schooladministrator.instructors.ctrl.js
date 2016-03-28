angular.module('app')
    .controller('SchoolAdministratorInstructorsController', [
        '$scope',
        'SchoolAdministratorResource',
        'SchoolAdministratorInstructorInviteResource',
        function ($scope, DashboardService, InviteInstructorService) {
            $scope.newInstructorInvite = {};

            $scope.inviteInstructor = function () {
                InviteInstructorService.postInstructorInvite($scope.newInstructorInvite)
                    .then(function () {
                        $scope.newInstructorInvite = {};
                        alert("Instructor Invited.");
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
                        return DashboardService.getUserSchool();
                    }).then(function (userSchoolResponse) {
                        $scope.school = userSchoolResponse;
                        $scope.initializeDone = true;
                    }).catch(function (errorResponse) {
                        $scope.error = errorResponse;
                    });
            };

            activate();
}]);