angular.module('app')
    .factory('AuthenticationInterceptor', [
        '$q',
        'localStorageService',
        function ($q, storageService) {

            function interceptRequest(request) {
                var token = storageService.get('token');
                if (token) {
                    request.headers.Authorization = 'Bearer ' + token.token;
                }
                return request;
            }

            function interceptResponse(response) {
                if (response.status === 401) {
                    location.replace('/#/home');
                }
                return $q.reject(response);
            }

            return {
                request: interceptRequest,
                responseError: interceptResponse
            };
}]);