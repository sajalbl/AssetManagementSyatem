'use strict';
app.controller('resourcesController', ['$scope', '$http', 'localStorageService', '$window', function ($scope, $http, localStorageService, $window) {
    var serviceBase = 'http://localhost:14597/';
    $scope.query = "";

    
    //$scope.resourceCompany = localStorageService.get("companyData");

    $scope.resourceCompany = localStorageService.get("companyData");

    $http.post(serviceBase + 'api/manage/showResources', JSON.stringify($scope.resourceCompany)).then(function (results) {
        $scope.resourceList = JSON.parse(results.data.ResourcesList);
        console.log($scope.resourceList);
    });
    
    $scope.show = function () {
        $scope.resourceCompany = localStorageService.get("companyData");
        
        $http.post(serviceBase + 'api/manage/showResources', JSON.stringify($scope.resourceCompany)).then(function (results) {
            $scope.resourceList = JSON.parse(results.data.ResourcesList);
            console.log($scope.resourceList);
        });
    };

    $scope.editRow = function (resource) {
        location.href = "#/edit";
        localStorageService.set("resourceDetail", resource);
    };
    
    $scope.remove = function (CompanyName, NameOfDevice, Type, EmployeeID) {
        //alert("are you sure you want to delete this ?");

        if ($window.confirm("are you sure you want to delete this ?"))
        {
            var text = { "CompanyName": CompanyName, "NameOfDevice": NameOfDevice, "Type": Type, "EmployeeID": EmployeeID };
            $http.post(serviceBase + 'api/manage/deleteResources', JSON.stringify(text)).then(function (results) {

                $scope.status = "Deleted";

                $scope.show();

            });
        }
        else
        {
            $scope.status = "";
        }
        

    };

    
}]);