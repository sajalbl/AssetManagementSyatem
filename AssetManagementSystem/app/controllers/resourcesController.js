'use strict';
app.controller('resourcesController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    $scope.query = "";

    //$scope.$on('Success', function (event, detail) {
    //    $scope.resourceCompany = detail;
    //});
    // $scope.show();
    $scope.resourceCompany = localStorageService.get("companyData");
    
        $http.post(serviceBase + 'api/manage/showResources', JSON.stringify($scope.resourceCompany)).then(function (results) {
            $scope.resourceList = JSON.parse(results.data.ResourcesList);
            console.log($scope.resourceList);
        });

    $scope.editRow = function (resource) {
        localStorageService.set("resourceDetail", resource);
    };
    
    $scope.remove = function (CompanyName, NameOfDevice, Type) {
        alert("Are you sure ?");
        var text = {"CompanyName" : CompanyName , "NameOfDevice": NameOfDevice , "Type" : Type };
        $http.post(serviceBase + 'api/manage/deleteResources', JSON.stringify(text)).then(function (results) {
            
            $scope.status = "Deleted";

            location.reload();
                
        });

    };

    //$scope.filterFunction = function (element) {
    //    return element.name.match(/^Ma/) ? true : false;
    //};

}]);