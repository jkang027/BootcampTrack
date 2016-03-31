angular.module('app')
    .controller('SchoolAdministratorStudentsController', [
        '$scope',
        'SchoolAdministratorResource',
        'InstructorStudentInviteResource',
        'CurrentSchoolService',
        function ($scope, DashboardService, InviteStudentService, CurrentSchoolService) {

            CurrentSchoolService.getCurrentSchool().then(function (data) {
                $scope.currentSchool = data;
            });

            $scope.newStudentInvite = {};

            $scope.inviteStudent = function () {
                InviteStudentService.postStudentInvite($scope.newStudentInvite)
                    .then(function () {
                        $scope.newStudentInvite = {};
                        alert("Student Invited.");
                        $('#newStudentInviteModal').modal('hide');
                        $('body').removeClass('modal-open');
                        $('.modal-backdrop').remove();
                    }).then(function () {
                        activate();
                    });
            };

            function activate() {
                $scope.intializeDone = false;

                DashboardService.getSchoolStudents()
                    .then(function (userSchoolStudentsResp) {
                        $scope.schoolStudents = userSchoolStudentsResp;
                        $scope.initializeDone = true;
                    }).catch(function (errorResponse) {
                        $scope.error = errorResponse;
                    });
            };

            activate();
        }]);