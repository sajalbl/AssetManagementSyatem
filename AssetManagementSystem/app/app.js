var app = angular.module('AmsApp', ['ngRoute', 'ngSanitize', 'ngCookies', 'ui.bootstrap']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "./app/views/home.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});


