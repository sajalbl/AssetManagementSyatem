'use strict';
app.controller('updateResourceController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';

    $scope.resources = localStorageService.get("resourceDetail");

    //$scope.$on('Success', function (event, detail) {
    //    $scope.companyName = detail;
    //});
    $scope.companyName = localStorageService.get("againCompanyName");
     
    $scope.update = function () {
        var text = { "CompanyName": $scope.companyName.CompanyName, "NameOfDevice": $scope.resources.NameOfDevice, "Type": $scope.resources.Type, "IssuedTo": $scope.resources.IssuedTo, "IssuedFrom": $scope.resources.IssuedFrom,"EmployeeID": $scope.resources.EmployeeID };
        $http.post(serviceBase + 'api/manage/updateResources', JSON.stringify(text)).then(function (results) {
            $scope.resources = "";
        });
        $scope.status = "Resources Updated Successfully";
    };



}]);