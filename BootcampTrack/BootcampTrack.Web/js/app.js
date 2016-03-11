angular.module('app', ['ngResource', 'ui.router', 'LocalStorageModule']);

angular.module('app').value('apiUrl', 'http://localhost:64716/api/');

angular.module('app').config(function ($stateProvider, $urlRouterProvider, $httpProvider) {
    $httpProvider.interceptors.push('AuthenticationInterceptor');
    $urlRouterProvider.otherwise('home');
    $stateProvider
        .state('home', { url: '/home', templateUrl: '/templates/home/home.html', controller: 'HomeController' })
        .state('register', { url: '/register', templateUrl: '/templates/register/register.html', controller: 'RegisterController' })
        .state('app', { url: '/app', templateUrl: '/templates/app/app.html', controller: 'AppController' })
            .state('app.dashboard', { url: '/app/dashboard', templateUrl: '/templates/app/dashboard/dashboard.html', controller: 'DashboardController' })
});

angular.module('app').run(function (AuthenticationService) {
    AuthenticationService.initialize();
});