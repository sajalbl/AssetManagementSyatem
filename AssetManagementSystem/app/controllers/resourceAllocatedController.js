'use strict';
app.controller('resourceAllocatedController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    $scope.query = "";

    $scope.employeeID = localStorageService.get("employeeid");

    $http.post(serviceBase + 'api/manage/resourceAllocated', JSON.stringify($scope.employeeID)).then(function (results) {
        $scope.resourceList = JSON.parse(results.data.ResourcesAllocated);
        console.log($scope.resourceList);
    });

}]);