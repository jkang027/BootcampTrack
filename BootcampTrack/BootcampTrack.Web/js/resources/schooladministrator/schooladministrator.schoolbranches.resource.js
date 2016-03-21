angular.module('app').factory('SchoolAdministratorSchoolBranchesResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/schoolbranches/:schoolBranchId', { schoolBranchId: '@SchoolBranchId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});