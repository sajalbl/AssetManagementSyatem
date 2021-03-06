﻿'use strict';
app.controller('newEmployeeController', ['uploadFileService', '$modal', '$scope', '$http', 'localStorageService', function (uploadFileService, $modal, $scope, $http, localStorageService) {

    var serviceBase = 'http://localhost:14597/';
    var service = 'http://localhost:58474/';
    var uploadBase = "";
    $scope.message = "";
    $scope.status = false;
    
    var company = localStorageService.get("Company");
    var prefix = localStorageService.get("CompanyInfo");
    
    //$scope.companyName = localStorageService.get("forEmployee");

    $scope.designations = [
           { Name: 'Employee' },
           { Name: 'Manager' },
    ];

    $scope.employeeInfo = {
        "EmployeeInfo": [{
            "info": [{
                "type": "info",
                Address: "",
                Contact: "",
                DOB: "",
                Department:""

            }, {
                "type": "imageDefault",
                Picture: "defaultProfile.png"
            }]
        }]
    }

    $scope.employee = {CompanyName:company.CompanyName, EmployeeName: "", ManagerID: "", Designation: "", Email: "", Prefix: prefix.Prefix };

    

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

        var IsValid = $scope.validate();
        if (IsValid == true)
            {
        //$scope.designation = localStorageService.get("empDes");
        //var text = { "EmployeeName": $scope.employee.EmployeeName, "EmployeeID": $scope.employee.EmployeeID, "Department": $scope.employee.Department, "Designation": $scope.designation.Designation, "CompanyName": $scope.companyName.CompanyName, "ManagerID": $scope.employee.ManagerID, "DOB": $scope.employee.DOB };
      
        var info = JSON.stringify($scope.employeeInfo);
        $scope.employee.EmployeeInfo = info;
        $scope.employee.CompanyName = company.CompanyName;

        $http.post(serviceBase + 'api/Employee/newEmployee', JSON.stringify($scope.employee)).then(function (results) {
            if (results.data.EmployeeID != null) {
                $scope.message = "Employee ID - " + results.data.EmployeeID;
            }
            else {
                $scope.message = "Employee already Exist";
            }
        });
        }
    };

    $scope.update = function () {
        uploadFileService.CSVUpload($scope.csvFile, uploadBase).then(function (results) {

            $scope.employeeInfo = {
                "EmployeeInfo": [{
                    "info": [{
                        "type": "info",
                        "Address": "",
                        "Contact": "",
                        "DOB": "",
                        "Department": ""

                    }]
                }]
            }

            var info = JSON.stringify($scope.employeeInfo);

            var text = { "CompanyName": company.CompanyName, "FileName": $scope.csvFile.name, "Employee": true, "EmployeeInfo": info };

            $http.post(service + 'api/manage/csvController', JSON.stringify(text)).then(function (results) {

                if (results.data.csvUploaded == false)
                {
                    $scope.replaceTable = true;
                    $scope.replaceList = JSON.parse(results.data.Duplicate);
                }
                else {
                    $scope.message = "Details added successfully";

                }
            });

        });

    };            
                       
         $scope.rep = function (list) {

                 var text = { "UserName": list.UserName, "EmployeeName": list.EmployeeName, "CompanyName": list.CompanyName, "ManagerID": list.ManagerID, "Designation": list.Designation, "Email": list.Email };
                             
                     $http.post(serviceBase + 'api/Employee/replaceEmployee', JSON.stringify(text)).then(function (results) {

                         if(results.data.IsEmployeeReplaced)
                             {
                             $scope.message = "Details added successfully";
                             }

                 });
                            
         };

         $scope.skip = function () {
             $scope.message = "Skipped";
         }


         $scope.validate = function () {
             var isvalid = true;

             if($scope.employee.EmployeeName == null || $scope.employee.EmployeeName == "")
             {
                 alert("Enter Employee's name");
                return isvalid = false;
             }
             if ($scope.employee.Email == null || $scope.employee.Email == "")
             {
                 alert("Enter Email");
                return isvalid = false;
             }
             if ($scope.employeeInfo.EmployeeInfo[0].info[0].Address == null || $scope.employeeInfo.EmployeeInfo[0].info[0].Address == "")
             {
                 alert("Enter Address");
                 return isvalid = false;
             }
             if ($scope.employeeInfo.EmployeeInfo[0].info[0].Contact == null || $scope.employeeInfo.EmployeeInfo[0].info[0].Contact == "") {
                 alert("Enter Contact");
               return  isvalid = false;
             }
             if ($scope.employeeInfo.EmployeeInfo[0].info[0].DOB == null || $scope.employeeInfo.EmployeeInfo[0].info[0].DOB == "") {
                 alert("Enter Date of Birth");
                return isvalid = false;
             }
             if ($scope.employeeInfo.EmployeeInfo[0].info[0].Department == null || $scope.employeeInfo.EmployeeInfo[0].info[0].Department == "") {
                 alert("Enter Department");
                return isvalid = false;
             }
            
             return isvalid;
         }
    

}]);