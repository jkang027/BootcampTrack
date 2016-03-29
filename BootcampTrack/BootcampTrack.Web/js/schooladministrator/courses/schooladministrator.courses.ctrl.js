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
            $scope.selectedBranch = {};
            $scope.newCourseSelectedBranch = {};
            $scope.coursesTable = {};

            $scope.branchSelect = function (branch) {
                $scope.selectedBranch = branch;
                DashboardResource.getSchoolBranchCourses(branch.SchoolBranchId)
                    .then(function (schoolBranchCoursesResp) {
                        $scope.schoolBranchCourses = schoolBranchCoursesResp;
                    });
            }

            $scope.newCourseBranchSelect = function (branch) {
                $scope.newCourseSelectedBranch = branch;
                $scope.newCourse.SchoolBranchId = branch.SchoolBranchId;
            }

            $scope.addNewCourse = function () {
                CoursesResource.save($scope.newCourse)
                    .$promise.then(function () {
                        $scope.newCourse = {};
                        alert("Course successfully added.");
                        $('#newCourseModal').modal('hide');
                        $('body').removeClass('modal-open');
                        $('.modal-backdrop').remove();
                    }).then(function () {
                        DashboardResource.getSchoolBranchCourses($scope.selectedBranch.SchoolBranchId)
                            .then(function (schoolBranchCoursesResp) {
                                $scope.schoolBranchCourses = schoolBranchCoursesResp;
                            });
                    });
            };

            function activate() {
                $scope.initializeDone = false;

                DashboardResource.getUserSchool()
                    .then(function (userSchoolResp) {
                        $scope.school = userSchoolResp;
                        return DashboardResource.getUserCourses();
                    }).then(function (userCoursesResp) {
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