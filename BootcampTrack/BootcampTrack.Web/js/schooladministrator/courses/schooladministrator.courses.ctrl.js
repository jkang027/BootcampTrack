angular.module('app')
    .controller('SchoolAdministratorCoursesController', [
        '$scope',
        'SchoolAdministratorResource',
        'SchoolAdministratorCoursesResource',
        'CurrentSchoolService',
        function ($scope, DashboardResource, CoursesResource, CurrentSchoolService) {

            CurrentSchoolService.getCurrentSchool().then(function (data) {
                $scope.currentSchool = data;
            });
            
            $scope.newCourse = {};
            $scope.newCourseSelectedBranch = {};

            $scope.newCourseBranchSelect = function (branch) {
                $scope.newCourseSelectedBranch = branch;
                $scope.newCourse.SchoolBranchId = branch.SchoolBranchId;
            }

            $scope.addNewCourse = function () {
                CoursesResource.save($scope.newCourse)
                    .$promise.then(function () {
                        $scope.newCourseSelectedBranch = {};
                        $scope.newCourse = {};
                        toastr.success("Course successfully added.");
                        $('#newCourseModal').modal('hide');
                        $('body').removeClass('modal-open');
                        $('.modal-backdrop').remove();
                    }).then(function () {
                        DashboardResource.getUserCourses()
                            .then(function (schoolBranchCoursesResp) {
                                $scope.schoolBranchCourses = schoolBranchCoursesResp;
                            });
                    });
            };

            function activate() {
                $scope.initializeDone = false;

                DashboardResource.getUserCourses()
                    .then(function (userCoursesResp) {
                        $scope.schoolBranchCourses = userCoursesResp;
                        return DashboardResource.getUserSchoolBranches();
                    }).then(function (userSchoolBranchesResp) {
                        $scope.schoolBranches = userSchoolBranchesResp;
                        $scope.initializeDone = true;
                    }).catch(function (errorResponse) {
                        $scope.error = errorResponse;
                    });
            };

            activate();
}]);