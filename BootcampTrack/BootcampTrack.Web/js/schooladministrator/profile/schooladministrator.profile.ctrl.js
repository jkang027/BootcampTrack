angular.module('app').controller('SchoolAdministratorProfileController', function ($scope, AuthenticationService, SchoolAdministratorProfileResource) {
    function activate() {
        SchoolAdministratorProfileResource.getUserSchool().then(function (response) {
            $scope.school = response;
        });
        SchoolAdministratorProfileResource.getUserProfile().then(function (response) {
            $scope.profile = response;
        });
    }

    $scope.updateProfile = function () {
        SchoolAdministratorProfileResource.updateProfile($scope.profile);
        SchoolAdministratorProfileResource.updateSchool($scope.school);
    }

    activate();
});