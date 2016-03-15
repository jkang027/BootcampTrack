angular.module('app').controller('HomeController', function ($scope, $timeout, AuthenticationService) {
    $scope.loginData = {};
    $scope.registration = {};

    $scope.login = function () {
        AuthenticationService.login($scope.loginData).then(
            function (response) {
                location.replace('/#/app/dashboard');
            },
            function (err) {
                alert(err.error_description);
            }
        );
    };

    $scope.register = function () {
        AuthenticationService.register($scope.registration).then(
            function (response) {
                bootbox.alert("Registration Complete");
                $timeout(function () {
                    location.replace('/#/login');
                }, 2000);
            },
            function (error) {
                bootbox.alert("Failed To Register");
            }
        );
    };
});