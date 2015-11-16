'use strict';
app.controller('profileController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    $scope.query = "";

    $scope.employeeID = localStorageService.get("employee");

    $http.post(serviceBase + 'api/manage/employees', JSON.stringify($scope.employeeID)).then(function (results) {
        $scope.employeeList = JSON.parse(results.data.EmployeeList);
        console.log($scope.employeeList);
    });

}]);