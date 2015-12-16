'use strict';
app.controller('navController', ['$scope', '$rootScope', '$route', 'localStorageService', function ($scope, $rootScope, $route, localStorageService) {

    //$scope.$on('LogIn', function (event, status) {
    //    if (status == 'company') {
    //        $scope.showNav = true;
    //        $scope.showNavigation = false;
    //    }
    //    else
    //    {
    //        $scope.showNavigation = true;
    //        $scope.showNav = false;
    //    }
    //});

    var company = localStorageService.get("Company");
    if (company != null) {
        $scope.companyLogin = true;
        $scope.employeeLogin = false;
    }

    var employee = localStorageService.get("Employee");
    if (employee != null) {
        $scope.companyLogin = false;
        $scope.employeeLogin = true;
    }

    $scope.$on('CompanyLogin', function (event, data) {
        if (data != undefined && data != '') {
            localStorageService.set("Company", data);
            $scope.companyLogin = true;
            $scope.employeeLogin = false;
        };
    });

    $scope.$on('EmployeeLogin', function (event, data) {
        if (data != undefined && data != '') {
            localStorageService.set("Employee", data);
            $scope.employeeLogin = true;
            $scope.companyLogin = false;
        };
    });

    //$scope.$on('Success', function (event, status) {
    //    $scope.showNavigation = status;

    //});

    //$scope.logout = function () {
    //    localStorageService.clear();
    //    $route.reload();
    //};

    $scope.logout = function () {
        localStorage.clear();
    };
}]);