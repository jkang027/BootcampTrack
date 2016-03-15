angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule']);

angular.module('app').value('apiUrl', 'http://localhost:64716/api/');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.interceptors.push('AuthenticationInterceptor');
    $urlRouterProvider.otherwise('home');
    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('schooladmin', { url: '/schooladmin', templateUrl: '/templates/schooladmin/schooladmin.html', controller: 'SchoolAdminController' })
            .state('schooladmin.login', { url: '/login', templateUrl: '/templates/schooladmin/login/schooladmin.login.html', controller: 'SchoolAdminLoginController' })
    ;
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});