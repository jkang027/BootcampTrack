angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule']);

angular.module('app').value('apiUrl', 'http://localhost:64716/api/');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.interceptors.push('AuthenticationInterceptor');
    $urlRouterProvider.otherwise('home');
    $stateProvider
        .state('home', { url: '/home', templateUrl: '/html/home/home.html', controller: 'HomeController' })
        .state('inviteinstructor', { url: '/inviteinstructor?token', templateUrl: '/html/schooladministrator/inviteinstructor/inviteinstructor.html', controller: 'InviteInstructorController'})
        .state('schooladministrator', { url: '/schooladministrator', templateUrl: '/html/schooladministrator/schooladministrator.html', controller: 'SchoolAdministratorController' })
            .state('schooladministrator.dashboard', { url: '/dashboard', templateUrl: '/html/schooladministrator/dashboard/schooladministrator.dashboard.html', controller: 'SchoolAdministratorDashboardController' })
            .state('schooladministrator.profile', { url: '/profile', templateUrl: '/html/schooladministrator/profile/schooladministrator.profile.html', controller: 'SchoolAdministratorProfileController' })
            .state('schooladministrator.schoolbranch', { url: '/schoolbranch', templateUrl: '/html/schooladministrator/schoolbranches/schooladministrator.schoolbranches.html', controller: 'SchoolAdministratorSchoolBranchController' })
            .state('schooladministrator.schoolbranchdetail', { url: '/schoolbranch/:id', templateUrl: '/html/schooladministrator/schoolbranches/schooladministrator.schoolbranches.detail.html', controller: 'SchoolAdministratorSchoolBranchDetailController' })
            .state('schooladministrator.course', { url: '/course', templateUrl: '/html/schooladministrator/courses/schooladministrator.courses.html', controller: 'SchoolAdministratorCourseController' })
            .state('schooladministrator.coursedetail', { url: '/course/:id', templateUrl: '/html/schooladministrator/courses/schooladministrator.courses.detail.html', controller: 'SchoolAdministratorCourseDetailController' })
    ;
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});