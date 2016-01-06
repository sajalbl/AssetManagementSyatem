'use strict';
app.controller('loginController', ['$scope', '$http', 'localStorageService', '$rootScope', '$location', function ($scope, $http, localStorageService, $rootScope, $location) {
    var target = angular.element(document.querySelector('#app'));
    target.addClass('body-wide');

    var serviceBase = 'http://localhost:14597/';
    $scope.message = "";
    $scope.allowedAuth = 0;
    $scope.companyLogin = true;

    $scope.company = { name: '' };
    $scope.employee = { UserName: '', email: '' };

    $scope.authStatus = [
            { Name: 'Company', Value: 0, Checked: true },
            { Name: 'Employee', Value: 1 },
    ];

    $scope.selectAuth = function (stat) {
        $scope.allowedAuth = stat.Value;
        if (stat.Name == "Employee") {
            $scope.employeeLogin = true;
            $scope.companyLogin = false;
        }
        else {
            $scope.companyLogin = true;
            $scope.employeeLogin = false;
        }
    };

    //$scope.CompanyName = "";
    //$scope.OwnerName = "";
    //$scope.companyList = [];
    //$scope.status = "";
    //$scope.CompanyID = "";
    //$scope.EmployeeName = "";

    //$scope.company = function () {
    //    $scope.companyLogin = true;
    //    $scope.employeeLogin = false;
    //};

    //$scope.employee = function () {
    //    $scope.companyLogin = false;
    //    $scope.employeeLogin = true;
    //};

    //$scope.$watch('message', function (newValue, oldValue) {
    //    if (newValue != oldValue) {
    //        //$scope.changed = JSON.stringify($scope.Data);
    //        //$scope.message = angular.equals($scope.changed, $scope.original);
    //    }
    //}, true);

    $scope.login = function () {
        $scope.message = "";
        var val = $scope.valid();
        if (val) {
            if ($scope.allowedAuth == 0) {
                var company = { "CompanyName": $scope.company.name };
                $http.post(serviceBase + 'api/Company/searchCompany', JSON.stringify(company)).then(function (results) {
                    if (results.data.IsSuccess != false && results.data.IsCompanyExist) {
                        localStorageService.set("CompanyInfo", results.data.CompanyInfo);
                        $rootScope.$broadcast("CompanyLogin", company);
                        $location.path('/companyDetail');
                    }
                    else {
                        $scope.message = "Company authentication failed. Please try again!!"
                    }
                });
            }
            else {
                var employee = { "Email": $scope.employee.email, "UserName": $scope.employee.UserName };
                $http.post(serviceBase + 'api/Employee/searchEmployee', JSON.stringify(employee)).then(function (results) {
                    if (results.data.IsSuccess != false && results.data.IsEmployeeExist) {
                        localStorageService.set("EmployeeInfo", results.data.EmployeeInfo);
                        $rootScope.$broadcast("EmployeeLogin", employee);
                        $location.path('/profile');
                    }
                    else {
                        $scope.message = "Employee authentication failed. Please try again!!"
                    }
                });
            }
        }
    }

    //$scope.search = function () {
    //    var val = $scope.valid();
    //    if (val) {
    //        var text = { "CompanyName": $scope.CompanyName, "OwnerName": $scope.OwnerName };
    //        var detail = { "CompanyName": $scope.CompanyName };
    //        $http.post(serviceBase + 'api/manage/checkCompany', JSON.stringify(text)).then(function (results) {
    //            if (results.data.IsCompanyExist) {
    //                $scope.status = "LogIn successful";
    //                localStorageService.set("companyText", text);
    //                localStorageService.set("companyData", detail);
    //                localStorageService.set("addResource", detail);
    //                localStorageService.set("againCompanyName", detail);
    //                localStorageService.set("ResourceCount", detail);
    //                localStorageService.set("forEmployee", detail);
    //                localStorageService.set("employeeList", detail);
    //                localStorageService.set("EditLink", true);
    //                //$rootScope.$broadcast('Success', detail);

    //                $rootScope.$broadcast('LogIn', 'company');



    //            }
    //            else {
    //                $scope.status = "User doesnot exist";
    //            }

    //        });


    //    }
    //};

    //$scope.login = function () {
    //    var text = { "EmployeeID": $scope.EmployeeID, "EmployeeName": $scope.EmployeeName };
    //    var detail = { "EmployeeID": $scope.EmployeeID };
    //    $http.post(serviceBase + 'api/manage/checkEmployee', JSON.stringify(text)).then(function (results) {
    //        if (results.data.IsEmployeeExist) {
    //            $scope.status = "Login Successful";
    //            localStorageService.set("employeeid", detail);
    //            localStorageService.set("employee", detail);
    //            localStorageService.set("task", detail);
    //            localStorageService.set("editProfile", detail);
    //            localStorageService.set("taskAssign", detail);
    //            $rootScope.$broadcast('LogIn', 'employee');

    //        }
    //        else {
    //            $scope.status = "User Doesnot Exist";
    //        }
    //    });
    //};

    $scope.valid = function () {
        var isValid = true;
        //if ($scope.CompanyName == null && $scope.CompanyName == "") {
        //    alert("Enter Company name");
        //    isValid = false;
        //}
        //if ($scope.OwnerName == null && $scope.OwnerName == "") {
        //    alert("Enter Admin name");
        //    isValid = false;
        //}
        return isValid;

    };

}]);
