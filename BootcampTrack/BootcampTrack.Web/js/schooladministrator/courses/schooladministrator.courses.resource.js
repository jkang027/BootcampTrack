angular.module('app').factory('CoursesResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/courses/:courseId', { schoolBranchId: '@CourseId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});