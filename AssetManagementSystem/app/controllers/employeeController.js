﻿'use strict';
app.controller('employeeController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    $scope.query = "";

    var employee = localStorageService.get("Company");

    $http.post(serviceBase + 'api/manage/employeeDetail', JSON.stringify(employee)).then(function (results) {

        $scope.employeeList = JSON.parse(results.data.EmployeeDetail);
        console.log($scope.employeeList);
    });

    $scope.showProfile = function (EmployeeID) {
        location.href = "#/profile";

        var detail = {"EmployeeID": EmployeeID };
        localStorageService.set("employee",detail);
    };

}]);