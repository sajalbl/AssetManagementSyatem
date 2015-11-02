'use strict';
app.controller('companyDetailController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';

    $scope.detail = localStorageService.get("companyText");
    
    $http.post(serviceBase + 'api/manage/searchCompany', JSON.stringify($scope.detail)).then(function (results) {
        $scope.companyList = JSON.parse(results.data.CompanyList);
        $scope.ShowTable = true;
        console.log($scope.companyList);


    });
    $scope.editRow = function (company) {
        localStorageService.set("companyDetail", company);
    };

    $scope.removeRow = function (CompanyName,OwnerName) {
        var text = { "CompanyName": CompanyName, "OwnerName": OwnerName };
        $http.post(serviceBase + 'api/manage/deleteCompany', JSON.stringify(text)).then(function (results) {
            $scope.ShowTable = false;
            $scope.hideTable = true;
            $scope.status = "Deleted";
        });
    };

}]);