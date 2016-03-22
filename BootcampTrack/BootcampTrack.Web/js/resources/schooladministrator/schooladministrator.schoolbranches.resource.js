angular.module('app')
    .factory('SchoolAdministratorSchoolBranchesResource', [
        'apiUrl',
        '$resource',
        function (apiUrl, $resource) {
            return $resource(apiUrl + '/schoolbranches/:schoolBranchId', { schoolBranchId: '@SchoolBranchId' },
            {
                'update': {
                    method: 'PUT'
                }
            });
}]);