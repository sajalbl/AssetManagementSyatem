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

    $routeProvider.when("/addResource", {
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

    //$routeProvider.when("/logout", {
    //        resolve: {
    //            redirect: function (localStorage) {
    //                localStorage.clear();
    //                return "/home";
    //            }
    //        }
    //    });


    $routeProvider.when("/newEmployee", {
        controller: "newEmployeeController",
        templateUrl: "./app/views/newEmployee.html"
    });

    $routeProvider.when("/resourceAllocated", {
        controller: "resourceAllocatedController",
        templateUrl: "./app/views/resourceAllocated.html"
    });

    $routeProvider.when("/profile", {
        controller: "profileController",
        templateUrl: "./app/views/profile.html"
    });

    $routeProvider.when("/tasks", {
        controller: "taskController",
        templateUrl: "./app/views/task.html"
    });

    $routeProvider.when("/editProfile", {
        controller: "editProfileController",
        templateUrl: "./app/views/editProfile.html"
    });

    $routeProvider.when("/employeeDetail", {
        controller: "employeeController",
        templateUrl: "./app/views/employee.html"
    });

    $routeProvider.when("/taskAssign", {
        controller: "taskAssignController",
        templateUrl: "./app/views/taskAssign.html"
    });


    $routeProvider.otherwise({ redirectTo: "/home" });
});


