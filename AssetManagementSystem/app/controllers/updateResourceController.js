'use strict';
app.controller('updateResourceController', ['$scope', '$http','localStorageService', function ($scope, $http , localStorageService ) {
    var serviceBase = 'http://localhost:14597/';

    $scope.resources = localStorageService.get("resourceDetail");
     
    $scope.update = function () {
        var text = { "NameOfDevice": $scope.resources.NameOfDevice, "Type": $scope.resources.Type, "IssuedTo": $scope.resources.IssuedTo, "IssuedFrom": $scope.resources.IssuedFrom };
        $http.post(serviceBase + 'api/manage/updateResources', JSON.stringify(text)).then(function (results) {
            $scope.resources = "";
        });
        $scope.status = "Resources Updated Successfully";
    };



}]);