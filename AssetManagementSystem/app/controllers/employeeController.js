'use strict';
app.controller('employeeController', ['$scope', '$http', '$location', 'localStorageService', function ($scope, $http, $location, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    var service = 'http://localhost:58474/';
    $scope.query = "";

    var employee = localStorageService.get("Company");
    var prefix = localStorageService.get("CompanyInfo");

    var data = { "CompanyName": employee.CompanyName, "Prefix": prefix.Prefix };

    var text = { "CompanyName": employee.CompanyName, "Employee": true };

    $http.post(serviceBase + 'api/Employee/employeeDetail', JSON.stringify(data)).then(function (results) {

        $scope.employeeList = JSON.parse(results.data.EmployeeInfo);
        console.log($scope.employeeList);
    });

    $http.post(service + 'api/manage/downloadCsv', JSON.stringify(text)).then(function (results) {

        if(results.data.csvDowloaded == false)
        {
            $scope.message = "Data not found";
        }

    });

    $scope.show = function () {
        $http.post(serviceBase + 'api/Employee/employeeDetail', JSON.stringify(data)).then(function (results) {

            $scope.employeeList = JSON.parse(results.data.EmployeeInfo);
            console.log($scope.employeeList);
        });

        $http.post(service + 'api/manage/downloadCsv', JSON.stringify(text)).then(function (results) {

            if (results.data.csvDowloaded == false) {
                $scope.message = "Data not found";
            }

        });
    };


    $scope.showProfile = function (EmployeeID) {
        $location.path("/profile");

        var detail = { "UserName": EmployeeID };
        localStorageService.set("employeeDetail",detail);
    };

    //$scope.delete = function (employee) {
       
    //    var text = { "UserName": employee.EmployeeID, "EmployeeName": employee.EmployeeName, "Email": employee.Email, "Designation": employee.Designation, "ManagerID": employee.ManagerID, "CompanyID": employee.CompanyID };

    //    $http.post(serviceBase + 'api/Employee/deleteEmployee', JSON.stringify(text)).then(function (results) {

    //        $scope.show();
    //    });
    //};


}]);