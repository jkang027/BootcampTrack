angular.module('app').controller('SchoolAdministratorCourseDetailController', function ($scope, $stateParams, SchoolAdministratorCoursesResource) {
    $scope.course = SchoolAdministratorCoursesResource.get({ courseId: $stateParams.id });

    $scope.saveCourse = function () {
        $scope.course.$update(function () {
            alert('save successful');
            activate();
        });
    };
});