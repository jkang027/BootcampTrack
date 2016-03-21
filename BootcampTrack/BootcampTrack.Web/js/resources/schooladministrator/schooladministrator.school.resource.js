angular.module('app').factory('SchoolAdministratorSchoolResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/schools/:schoolId', { schoolId: '@SchoolId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});