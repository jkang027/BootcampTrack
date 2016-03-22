angular.module('app').factory('SchoolAdministratorCoursesResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/courses/:courseId', { courseId: '@CourseId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});