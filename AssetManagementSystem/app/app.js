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
    //    controller: "logOutController",
    //    templateUrl:  "./app/views/logOut.html"
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

app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function () {
                scope.$apply(function () {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);
