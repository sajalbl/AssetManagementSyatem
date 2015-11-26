'use strict';
app.controller('homeController', ['$scope', '$rootScope', '$http', 'localStorageService', function ($scope, $rootScope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';

        $scope.CompanyName = "";
        $scope.OwnerName = "";
        $scope.companyList = [];
        $scope.status = "";
        $scope.CompanyID = "";
        $scope.EmployeeName = "";
        
        $scope.company = function () {
            $scope.companyLogin = true;
            $scope.employeeLogin = false;
        };

        $scope.employee = function() {
            $scope.companyLogin = false;
            $scope.employeeLogin = true;
        };
       
        $scope.search = function () {
            var val = $scope.valid();
            if (val) {
                var text = { "CompanyName": $scope.CompanyName, "OwnerName": $scope.OwnerName };
                var detail = { "CompanyName": $scope.CompanyName };
                $http.post(serviceBase + 'api/manage/checkCompany', JSON.stringify(text)).then(function (results) {
                    if (results.data.IsCompanyExist) {
                        $scope.status = "LogIn successful";
                        localStorageService.set("companyText", text);
                        localStorageService.set("companyData", detail);
                        localStorageService.set("addResource", detail);
                        localStorageService.set("againCompanyName", detail);
                        localStorageService.set("ResourceCount", detail);
                        localStorageService.set("forEmployee", detail);
                        localStorageService.set("employeeList", detail);
                        localStorageService.set("EditLink", true);
                        //$rootScope.$broadcast('Success', detail);

                        $rootScope.$broadcast('LogIn', 'company');

                       

                    }
                    else
                    {
                        $scope.status = "User doesnot exist";
                    }
                        
                });

               
            }
        };

        $scope.login = function () {
            var text = { "EmployeeID": $scope.EmployeeID, "EmployeeName": $scope.EmployeeName };
            var detail = { "EmployeeID": $scope.EmployeeID };
            $http.post(serviceBase + 'api/manage/checkEmployee', JSON.stringify(text)).then(function (results) {
                if(results.data.IsEmployeeExist)
                {
                    $scope.status = "Login Successful";
                    localStorageService.set("employeeid", detail);
                    localStorageService.set("employee", detail);
                    localStorageService.set("task", detail);
                    localStorageService.set("editProfile", detail);
                    localStorageService.set("taskAssign", detail);
                    $rootScope.$broadcast('LogIn', 'employee');

                }
                else
                {
                    $scope.status = "User Doesnot Exist";
                }
            });
        };

        $scope.valid = function () {
            var isValid = true;
            if ($scope.CompanyName == null && $scope.CompanyName == "") {
                alert("Enter Company name");
                isValid = false;
            }
            if ($scope.OwnerName == null && $scope.OwnerName == "") {
                alert("Enter Admin name");
                isValid = false;
            }
            return isValid;

        };
       
}]);
