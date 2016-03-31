angular.module('app')
    .controller('HomeController', [
        '$scope',
        'AuthenticationService',
        function ($scope, AuthenticationService) {

            $scope.loginData = {};
            $scope.registration = {};
            $scope.instructorLoginData = {};
            $scope.studentLoginData = {};

            $scope.login = function () {
                AuthenticationService.login($scope.loginData)
                    .then(function (response) {
                        $('#myModal').modal('hide');
                        $("body").css('padding', '0');
                        $('body').removeClass('modal-open');
                        $('.modal-backdrop').remove();
                        location.replace('#/schooladministrator/dashboard');
                    },
                    function (err) {
                        alert(err.error_description);
                    }
                );
            };

            $scope.instructorLogin = function () {
                AuthenticationService.login($scope.instructorLoginData)
                    .then(function (response) {
                        $('#myModal').modal('hide');
                        $("body").css('padding', '0');
                        $('body').removeClass('modal-open');
                        $('.modal-backdrop').remove();
                        location.replace('#/courseinstructor/dashboard');
                    },
                    function (err) {
                        alert(err.error_description);
                    }
                );
            };

            $scope.studentLogin = function () {
                AuthenticationService.login($scope.studentLoginData)
                    .then(function (response) {
                        $('#myModal').modal('hide');
                        $("body").css('padding', '0');
                        $('body').removeClass('modal-open');
                        $('.modal-backdrop').remove();
                        location.replace('#/student/dashboard');
                    },
                    function (err) {
                        alert(err.error_description);
                    }
                );
            };

            $scope.register = function () {
                AuthenticationService.register($scope.registration)
                    .then(function (response) {
                        toastr.success("Registration Complete. You can now sign in.");
                        $scope.registration = {};
                    },
                    function (error) {
                        toastr.error("Failed To Register");
                    }
                );
            };
}]);