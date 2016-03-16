angular.module('app').controller('HomeController', function ($scope, AuthenticationService) {
    $scope.loginData = {};
    $scope.registration = {};

    $scope.login = function () {
        AuthenticationService.login($scope.loginData).then(
            function (response) {
                $('myModal').modal('hide');
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                location.replace('#/schooladministrator/dashboard');
            },
            function (err) {
                alert(err.error_description);
            }
        );
    };

    $scope.register = function () {
        AuthenticationService.register($scope.registration).then(
            function (response) {
                alert("Registration Complete. You can now sign in.");
                $scope.registration = {};
            },
            function (error) {
                alert("Failed To Register");
            }
        );
    };
});