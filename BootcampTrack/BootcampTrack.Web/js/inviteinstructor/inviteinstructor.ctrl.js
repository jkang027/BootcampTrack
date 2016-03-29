angular.module('app')
    .controller('InviteInstructorController', [
        '$scope',
        'AuthenticationService',
        '$stateParams',
        '$http',
        'apiUrl',
        '$state',
        function ($scope, AuthService, $stateParams, $http, apiUrl, $state) {

            $scope.initializeDone = false;

            var token = $stateParams.token;

            //TODO: not working

            $http.get(apiUrl + "invite/verify/instructor/" + token)
                 .success(function (response) {
                     $scope.tokenResult = response.data;
                     $scope.initializeDone = true;
                 })
                 .error(function () {
                     $scope.initializeDone = true;
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