angular.module('app')
    .controller('SchoolAdministratorCourseDetailController', [
        '$scope',
        '$stateParams',
        'SchoolAdministratorResource',
        'SchoolAdministratorCoursesResource',
        function ($scope, $stateParams, SchoolAdministratorResource, CoursesResource) {
            $scope.course = CoursesResource.get({ courseId: $stateParams.id });
          
            $scope.updateCourse = function () {
                $scope.course.$update(function () {
                    toastr.success('Update successful');
                });
            };

            function activate() {
                $scope.initializeDone = false;

                SchoolAdministratorResource.getCourseProjects($stateParams.id)
                    .then(function (courseProjectsResp) {
                        $scope.courseProjects = courseProjectsResp;
                        return SchoolAdministratorResource.getCourseInstructors($stateParams.id)
                    }).then(function (courseInstructorsResp) {
                        $scope.courseInstructors = courseInstructorsResp;
                        return SchoolAdministratorResource.getCourseStudentInvites($stateParams.id)
                    }).then(function (courseStudentInvitesResp) {
                        $scope.courseStudentInvites = courseStudentInvitesResp;
                        return SchoolAdministratorResource.getCourseStudents($stateParams.id)
                    }).then(function (courseStudentsResp) {
                        $scope.courseStudents = courseStudentsResp;
                        $scope.initializeDone = true;
                    }).catch(function (errorResponse) {
                        $scope.error = errorResponse;
                    });
            };

            activate();
}]);