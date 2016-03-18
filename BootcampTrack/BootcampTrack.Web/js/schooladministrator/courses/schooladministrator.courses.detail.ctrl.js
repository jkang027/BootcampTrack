angular.module('app').controller('SchoolAdministratorCourseDetailController', function ($scope, $stateParams, CourseResource) {
    $scope.lease = CourseResource.get({ courseId: $stateParams.id });

    $scope.saveCourse = function () {
        $scope.course.$update(function () {
            alert('save successful');
            activate();
        });
    };
});