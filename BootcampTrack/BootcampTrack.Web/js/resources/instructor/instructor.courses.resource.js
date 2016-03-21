angular.module('app').factory('InstructorCoursesResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/courses/:courseId', { schoolBranchId: '@CourseId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});