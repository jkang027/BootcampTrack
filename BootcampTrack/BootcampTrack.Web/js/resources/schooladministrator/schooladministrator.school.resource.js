angular.module('app')
    .factory('SchoolAdministratorSchoolResource', [
        'apiUrl',
        '$resource',
        function (apiUrl, $resource) {
            return $resource(apiUrl + '/schools/:schoolId', { schoolId: '@SchoolId' },
            {
                'update': {
                    method: 'PUT'
                }
            });
}]);