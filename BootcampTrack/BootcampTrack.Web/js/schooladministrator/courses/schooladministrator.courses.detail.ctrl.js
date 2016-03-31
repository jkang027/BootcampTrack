angular.module('app')
    .controller('SchoolAdministratorCourseDetailController', [
        '$scope',
        '$stateParams',
        'SchoolAdministratorCoursesResource',
        function ($scope, $stateParams, CoursesResource) {
            $scope.course = CoursesResource.get({ courseId: $stateParams.id });
          
            $scope.updateCourse = function () {
                $scope.course.$update(function () {
                    toastr.success('Update successful');
                });
            };
}]);