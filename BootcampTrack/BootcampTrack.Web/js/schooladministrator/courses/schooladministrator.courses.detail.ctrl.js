angular.module('app')
    .controller('SchoolAdministratorCourseDetailController', [
        '$scope',
        '$stateParams',
        'CourseResource',
        function ($scope, $stateParams, CourseResource) {
            $scope.course = CourseResource.get({ courseId: $stateParams.id });

            $scope.saveCourse = function () {
                $scope.course.$update(function () {
                    alert('save successful');
                    activate();
                });
            };
}]);