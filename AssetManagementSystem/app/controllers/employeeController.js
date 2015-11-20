'use strict';
app.controller('employeeController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    $scope.query = "";

    $scope.employee = localStorageService.get("employeeList");

    $http.post(serviceBase + 'api/manage/employeeDetail', JSON.stringify($scope.employee)).then(function (results) {

        $scope.employeeList = JSON.parse(results.data.EmployeeDetail);
        console.log($scope.employeeList);
    });

    $scope.showProfile = function (EmployeeID) {
        var detail = {"EmployeeID": EmployeeID };
        localStorageService.set("employee",detail);
    };
}]);