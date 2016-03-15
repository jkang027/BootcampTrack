﻿angular.module('app').controller('SchoolAdminController', function ($scope, AuthenticationService) {
	$scope.loginData = {};

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

	$scope.logout = function () {
		AuthenticationService.logout();
	}
});