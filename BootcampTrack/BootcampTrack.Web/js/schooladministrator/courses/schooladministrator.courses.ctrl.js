angular.module('app')
    .controller('SchoolAdministratorCourseController', [
        '$scope',
        'SchoolAdministratorDashboardResource',
        function ($scope, DashboardResource) {

            function activate() {
                DashboardResource.getUserCourses().then(function (response) {
                    $scope.courses = response;
                });
            }

            activate();
}]);