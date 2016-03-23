angular.module('app')
    .factory('SchoolAdministratorCoursesResource', [
        'apiUrl',
        '$resource',
        function (apiUrl, $resource) {
            return $resource(apiUrl + 'courses/:courseId', { courseId: '@CourseId' },
            {
                'update': {
                    method: 'PUT'
                }
            });
}]);