angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule']);

angular.module('app').value('apiUrl', 'http://localhost:64716/api/');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.interceptors.push('AuthenticationInterceptor');
    $urlRouterProvider.otherwise('home');
    $stateProvider
        .state('home', { url: '/home', templateUrl: '/html/home/home.html', controller: 'HomeController' })
        .state('schooladministrator', { url: '/schooladministrator', templateUrl: '/html/schooladministrator/schooladministrator.html', controller: 'SchoolAdministratorController' })
            .state('schooladministrator.dashboard', { url: '/dashboard', templateUrl: '/html/schooladministrator/dashboard/schooladministrator.dashboard.html', controller: 'SchoolAdministratorDashboardController' })
    ;
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});