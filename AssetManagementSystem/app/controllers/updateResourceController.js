'use strict';
app.controller('updateResourceController', ['uploadFileService', '$scope', '$http', 'localStorageService', function (uploadFileService, $scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    var uploadBase = "";

    $scope.status = '';
    $scope.resources = localStorageService.get("resourceDetail");

    //$scope.$on('Success', function (event, detail) {
    //    $scope.companyName = detail;
    //});
    $scope.companyName = localStorageService.get("Company");
     
    $scope.update = function () {
        var text = { "CompanyName": $scope.companyName.CompanyName, "NameOfDevice": $scope.resources.NameOfDevice, "Type": $scope.resources.Type, "IssuedFrom": $scope.resources.IssuedFrom, "EmployeeID": $scope.resources.EmployeeID, "Serial": $scope.resources.Serial };

        uploadFileService.fileUpload($scope.resources.Serial, $scope.Picture, uploadBase).then(function (results) {
    
        });

        $http.post(serviceBase + 'api/manage/updateResources', JSON.stringify(text)).then(function (results) {
            $scope.resources = "";
            $scope.status = "Resources Updated Successfully";
        });
        
    };
            
}]);