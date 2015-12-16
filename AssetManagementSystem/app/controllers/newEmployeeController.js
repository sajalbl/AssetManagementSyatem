'use strict';
app.controller('newEmployeeController', ['uploadFileService', '$scope', '$http', 'localStorageService', function (uploadFileService, $scope, $http, localStorageService) {

    var serviceBase = 'http://localhost:14597/';
    var uploadBase = "";
    $scope.message = "";
    $scope.employee = { EmployeeName: "", Department: "", ManagerID: "", DOB: "", Designation: "" };
    var company = localStorageService.get("Company");
    $scope.employee.CompanyName = company.CompanyName;
    //$scope.companyName = localStorageService.get("forEmployee");

    $scope.designations = [
           { Name: 'Employee', Checked: true },
           { Name: 'Manager' },
    ];

    //Service to get list of all employees of the company who are managers
    $http.post(serviceBase + 'api/Company/allManagers', JSON.stringify(company)).then(function (results) {
        if (results.data.IsSuccess != false && results.data.ManagerList != null)
            $scope.managers = JSON.parse(results.data.ManagerList);
    });

    //$scope.ifEmployee = function () {
    //    var text = { "Designation": "employee" };
    //    localStorageService.set("empDes", text);
    //};

    //$scope.ifManager = function () {
    //    var detail = { "Designation": "manager" };
    //    localStorageService.set("empDes", detail);
    //};


    $scope.selectDesig = function (desig) {
        $scope.employee.Designation = desig.Name;
    };

    $scope.submit = function () {
        //$scope.designation = localStorageService.get("empDes");
        //var text = { "EmployeeName": $scope.employee.EmployeeName, "EmployeeID": $scope.employee.EmployeeID, "Department": $scope.employee.Department, "Designation": $scope.designation.Designation, "CompanyName": $scope.companyName.CompanyName, "ManagerID": $scope.employee.ManagerID, "DOB": $scope.employee.DOB };

        $http.post(serviceBase + 'api/Employee/newEmployee', JSON.stringify($scope.employee)).then(function (results) {
            if (results.data.IsSuccess != false && results.data.IsEmployeeCreated) {
                $scope.message = "Details added successfully";
            }
            else {
                $scope.message = "Employee already Exist";
            }
        });
    };

    $scope.update = function () {
        uploadFileService.CSVUpload($scope.csvFile, uploadBase).then(function (results) {

        });
    };
}]);