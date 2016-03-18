angular.module('app').factory('SchoolBranchResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/schoolbranches/:schoolBranchId', { schoolBranchId: '@SchoolBranchId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});