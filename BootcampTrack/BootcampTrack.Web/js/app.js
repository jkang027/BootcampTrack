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
            .state('schooladministrator.school', { url: '/school', templateUrl: '/html/schooladministrator/schools/schooladministrator.schools.html', controller: 'SchoolAdministratorSchoolController' })
            .state('schooladministrator.branch', { url: '/branch', templateUrl: '/html/schooladministrator/branches/schooladministrator.branches.html', controller: 'SchoolAdministratorBranchController' })
            .state('schooladministrator.course', { url: '/course', templateUrl: '/html/schooladministrator/courses/schooladministrator.courses.html', controller: 'SchoolAdministratorCourseController' })
    ;
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});