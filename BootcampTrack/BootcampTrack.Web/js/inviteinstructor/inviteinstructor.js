angular.module('app')
    .controller('InviteInstructor', [
        '$scope',
        'AuthenticationService',
        '$stateParams',
        '$http',
        'apiUrl',
        '$state',
        function ($scope, AuthService, $stateParams, $http, apiUrl, $state) {

            $scope.loading = true;

            var token = $stateParams.token;

            $http.get(apiUrl + "invite/verify/instructor/" + token)
                 .success(function (response) {
                     $scope.tokenResult = response.data;
                     $scope.loading = false;
                 })
                 .error(function () {
                     $scope.loading = false;
                 });

            $scope.register = function () {
                $scope.registration.Token = token;
                $http.post(apiUrl + "accounts/register/instructor", $scope.registration)
                     .success(function (response) {
                         alert("Successfully registered. Please login.");
                         $state.go("home");
                     })
                     .error(function () {
                         alert("Failed to register.");
                     });
            }
}]);