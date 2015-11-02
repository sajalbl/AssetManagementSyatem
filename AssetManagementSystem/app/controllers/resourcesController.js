'use strict';
app.controller('resourcesController', ['$scope', '$http','localStorageService', function ($scope, $http ,localStorageService) {
    var serviceBase = 'http://localhost:14597/';

    $scope.resourceCompany = localStorageService.get("companyData");
    $http.post(serviceBase + 'api/manage/showResources', JSON.stringify($scope.resourceCompany)).then(function (results) {
        $scope.resourceList = JSON.parse(results.data.ResourcesList);
        console.log($scope.resourceList);
    });

    $scope.editRow = function (resource) {
        localStorageService.set("resourceDetail", resource);
    };
    
    $scope.removeRow = function (NameOfDevice) {
        var text = { "NameOfDevice": NameOfDevice };
        $http.post(serviceBase + 'api/manage/deleteResources', JSON.stringify(text)).then(function (results) {
            $scope.status = "";
        });

    };
}]);