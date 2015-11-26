'use strict';
app.controller('newEmployeeController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {

    var serviceBase = 'http://localhost:14597/';
    $scope.employee = { "EmployeeName": $scope.EmployeeName, "EmployeeID": $scope.EmployeeID, "Department": $scope.Department, "ManagerID": $scope.ManagerID };
    $scope.companyName = localStorageService.get("forEmployee");

    $scope.ifEmployee = function () {
        var text = {  "Designation": "employee" };
        localStorageService.set("empDes", text);
    };

    $scope.ifManager = function () {
        var detail = {  "Designation": "manager" };
        localStorageService.set("empDes", detail);
    };

        
    $scope.submit = function () {
        $scope.designation = localStorageService.get("empDes");
        var text = { "EmployeeName": $scope.employee.EmployeeName, "EmployeeID": $scope.employee.EmployeeID, "Department": $scope.employee.Department, "Designation": $scope.designation.Designation , "CompanyName": $scope.companyName.CompanyName, "ManagerID": $scope.employee.ManagerID };

        $http.post(serviceBase + 'api/manage/newEmployee', JSON.stringify(text)).then(function (results) {
            if (results.data.IsEmployeeCreated)
            {
                $scope.status = "Details added successfully";

            }
            else
            {
                $scope.status = "Employee ID already Exist";
            }
            $scope.employee = "";
        });
    };
}]);