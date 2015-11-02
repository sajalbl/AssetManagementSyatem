'use strict';
app.controller('homeController', ['$scope', '$rootScope', '$http', 'localStorageService', function ($scope, $rootScope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';

        $scope.CompanyName = "";
        $scope.OwnerName = "";
        $scope.companyList = [];
        $scope.status = "";
        
       
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
                        $rootScope.$broadcast('LogIn', true);
                    }
                    else
                    {
                        $scope.status = "User doesnot exist";
                    }

                        
                });
            }
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
