angular.module('app')
    .controller('SchoolAdministratorCourseDetailController', [
        '$scope',
        '$stateParams',
        'SchoolAdministratorCoursesResource',
        function ($scope, $stateParams, CoursesResource) {
            if ($stateParams.id) {
                $scope.course = CoursesResource.get({ courseId: $stateParams.id });
            } else {
                $scope.course = new CoursesResource();
            }

            $scope.saveCourse = function () {
                if ($stateParams.id) {
                    $scope.course.$update(function () {
                        alert('update successful');
                    });
                } else {
                    $scope.course.$save(function () {
                        alert('save successful');
                    });
                }
            };
}]);