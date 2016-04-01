angular.module('app')
    .controller('SchoolAdministratorController', [
        '$scope',
        'AuthenticationService',
        function ($scope, AuthService) {

            $scope.logout = function () {
                AuthService.logout()
                    .then(function (response) {
                        location.replace('#/home');
                    },
                    function (err) {
                        alert(err.error_description);
                    }
                );
            };
}]);