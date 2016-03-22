angular.module('app')
    .controller('SchoolAdministratorProfileController', [
        '$scope',
        'SchoolAdministratorProfileResource',
        function ($scope, ProfileService) {

            function activate() {
                $scope.initializeDone = false;

                ProfileService.getUserSchool()
                    .then(function (userSchoolResp) {
                        $scope.school = userSchoolResp;
                        return ProfileService.getUserProfile();
                    }).then(function (userProfileResp) {
                        $scope.profile = userProfileResp;
                        $scope.initializeDone = true;
                    }).catch(function (errorResponse) {
                        $scope.error = errorResponse;
                    });
            };

            $scope.updateProfile = function () {
                ProfileService.updateProfile($scope.profile);
                ProfileService.updateSchool($scope.school);
            }

            activate();
}]);