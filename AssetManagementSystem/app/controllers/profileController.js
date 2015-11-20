'use strict';
app.controller('profileController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    $scope.query = "";

    $scope.ShowEdit = false;

    $scope.employeeID = localStorageService.get("employee");

   
    $http.post(serviceBase + 'api/manage/employees', JSON.stringify($scope.employeeID)).then(function (results) {
        $scope.employeeList = JSON.parse(results.data.EmployeeList);
        
        console.log($scope.employeeList);
    });

    $scope.$on('EditLink', function (event, status) {
        $scope.ShowEdit = status;
    });

    $scope.edit = function (DOB, Address, Contact, Email) {
        var detail = { "DOB": DOB, "Address": Address, "Contact": Contact, "Email": Email };
        localStorageService.set("employeeProfile", detail);
    }

}]);