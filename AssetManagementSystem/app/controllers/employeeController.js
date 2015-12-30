'use strict';
app.controller('employeeController', ['$scope', '$http', '$location', 'localStorageService', function ($scope, $http, $location, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    var service = 'http://localhost:58474/';
    $scope.query = "";

    var employee = localStorageService.get("Company");

    var text = { "CompanyName": employee.CompanyName, "Employee": true };

    $http.post(serviceBase + 'api/Employee/employeeDetail', JSON.stringify(employee)).then(function (results) {

        $scope.employeeList = JSON.parse(results.data.EmployeeInfo);
        console.log($scope.employeeList);
    });

    $http.post(service + 'api/manage/downloadCsv', JSON.stringify(text)).then(function (results) {

        if(results.data.csvDowloaded == false)
        {
            $scope.message = "Data not found";
        }

    });

    $scope.showProfile = function (EmployeeName,Email) {
        $location.path("/profile");

        var detail = {"EmployeeName": EmployeeName, "Email": Email };
        localStorageService.set("employeeDetail",detail);
    };

}]);