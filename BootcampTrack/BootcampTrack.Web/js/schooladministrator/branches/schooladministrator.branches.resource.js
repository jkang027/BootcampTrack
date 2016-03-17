angular.module('app').factory('BranchResource', function (apiUrl, $resource) {
    return $resource(apiUrl + '/branches/:branchId', { branchId: '@BranchId' },
    {
        'update': {
            method: 'PUT'
        }
    });
});