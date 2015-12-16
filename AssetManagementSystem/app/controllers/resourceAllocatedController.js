'use strict';
app.controller('resourceAllocatedController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    $scope.query = "";

    var employeeID = localStorageService.get("Employee");

    $http.post(serviceBase + 'api/manage/resourceAllocated', JSON.stringify(employeeID)).then(function (results) {
        $scope.resourceList = JSON.parse(results.data.ResourcesAllocated);
        console.log($scope.resourceList);
    });

}]);