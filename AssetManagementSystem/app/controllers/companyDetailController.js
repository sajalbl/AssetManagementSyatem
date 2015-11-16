﻿'use strict';
app.controller('companyDetailController', ['$scope',  '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';

    $scope.detail = localStorageService.get("companyText");

    //$scope.$on('Success', function (event, detail) {
    //    $scope.resourcesNumber = detail;
    //});
    $scope.resourcesNumber = localStorageService.get("ResourceCount");
    
    $http.post(serviceBase + 'api/manage/searchCompany', JSON.stringify($scope.detail)).then(function (results) {
        
        $scope.companyList = JSON.parse(results.data.CompanyList);
        $scope.ShowTable = true;
        console.log($scope.companyList);

    });

    var details = { "CompanyName": $scope.resourcesNumber.CompanyName };
    $http.post(serviceBase + 'api/manage/resourceCount', JSON.stringify(details)).then(function (results) {
        $scope.resourceCount = results.data.Resourcecount;

    });

    var employee = { "CompanyName": $scope.resourcesNumber.CompanyName };
    $http.post(serviceBase + 'api/manage/employeeCount', JSON.stringify(employee)).then(function (results) {
        $scope.employeeCount = results.data.EmployeeCount;
    });


    $scope.editRow = function (company) {
        localStorageService.set("companyDetail", company);
    };

    $scope.removeRow = function (CompanyName, OwnerName) {
        alert("Are you sure ?");
        alert("Resource details will also be deleted");
        var text = { "CompanyName": CompanyName, "OwnerName": OwnerName };
        var detailed = { "CompanyName": CompanyName };
        $http.post(serviceBase + 'api/manage/deleteCompany', JSON.stringify(text)).then(function (results) {
            $scope.ShowTable = false;
            $scope.hideTable = true;
            $scope.status = "Deleted";

        
            $http.post(serviceBase + 'api/manage/companyDeleted', JSON.stringify(detailed)).then(function (results) {
                if (results.data.DeletedCompany) {
                    $scope.stat = "";
                }
            });
        });
    };

}]);