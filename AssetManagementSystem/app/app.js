var app = angular.module('AmsApp', ['ngRoute', 'ngSanitize', 'ngCookies', 'ui.bootstrap', 'LocalStorageModule']);

app.config(function (localStorageServiceProvider) {
    localStorageServiceProvider
      .setStorageType('localStorage')
});

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "./app/views/home.html"
    });


    $routeProvider.when("/newCompany", {
        controller: "newCompanyController",
        templateUrl: "./app/views/newCompany.html"
    });

    $routeProvider.when("/update", {
        controller: "UpdateController",
        templateUrl: "./app/views/update.html"
    });

    $routeProvider.when("/companyDetail", {
        controller: "companyDetailController",
        templateUrl: "./app/views/companyDetail.html"
    });

    $routeProvider.when("/addResources", {
        controller: "addResourcesController",
        templateUrl: "./app/views/addResources.html"
    });

    $routeProvider.when("/resourceDetail", {
        controller: "resourcesController",
        templateUrl: "./app/views/resources.html"
    });

    $routeProvider.when("/edit", {
        controller: "updateResourceController",
        templateUrl: "./app/views/updateResource.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });
});


