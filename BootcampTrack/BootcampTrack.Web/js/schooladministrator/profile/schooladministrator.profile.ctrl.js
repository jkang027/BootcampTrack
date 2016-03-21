angular.module('app').controller('SchoolAdministratorProfileController', function ($scope, AuthenticationService, SchoolAdministratorProfileResource, SchoolResource) {
    function activate() {
        SchoolAdministratorProfileResource.getUserSchool().then(function (response) {
            $scope.school = response;
        });
        SchoolAdministratorProfileResource.getUserProfile().then(function (response) {
            $scope.profile = response;
        });
    }

    activate();
});