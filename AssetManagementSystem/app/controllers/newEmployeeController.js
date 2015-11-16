'use strict';
app.controller('newEmployeeController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {

    var serviceBase = 'http://localhost:14597/';

    $scope.employee = { "EmployeeName": $scope.EmployeeName, "EmployeeID": $scope.EmployeeID, "Department": $scope.Department , "Designation": $scope.Designation };
    $scope.companyName = localStorageService.get("forEmployee");
    $scope.submit = function () {
        var text = { "EmployeeName": $scope.employee.EmployeeName, "EmployeeID": $scope.employee.EmployeeID, "Department": $scope.employee.Department ,"Designation": $scope.employee.Designation , "CompanyName": $scope.companyName.CompanyName };
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