angular.module('app').factory('CourseResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/courses/:courseId', { courseId: '@CourseId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});