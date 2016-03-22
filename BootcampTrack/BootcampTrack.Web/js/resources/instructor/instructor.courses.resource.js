angular.module('app')
    .factory('InstructorCoursesResource', [
        'apiUrl',
        '$resource',
        function (apiUrl, $resource) {
            return $resource(apiUrl + '/courses/:courseId', { schoolBranchId: '@CourseId' },
            {
                'update': {
                    method: 'PUT'
                }
            });
}]);