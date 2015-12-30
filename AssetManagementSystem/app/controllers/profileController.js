'use strict';
app.controller('profileController', ['$scope', '$http', '$location', 'localStorageService', function ($scope, $http, $location, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    
    var target = angular.element(document.querySelector('#app'));
    target.removeClass('body-wide');

    //$scope.ShowEdit = false;
    
    var employee = localStorageService.get("Employee");

   var detail = localStorageService.get("employeeDetail");
    

    //$scope.source = "http://localhost:58474/images/";
     
   
    $http.post(serviceBase + 'api/Employee/employees', JSON.stringify(employee)).then(function (results) {

        $scope.employee = JSON.parse(results.data.EmployeeList);
        $scope.info = JSON.parse($scope.employee.EmployeeInfo);
        
        console.log($scope.employeeList);
    });


    $http.post(serviceBase + 'api/Employee/employees', JSON.stringify(detail)).then(function (results) {

        $scope.employee = JSON.parse(results.data.EmployeeList);
        $scope.info = JSON.parse($scope.employee.EmployeeInfo);

        console.log($scope.employeeList);
    });


    //$scope.$on('EditLink', function (event, status) {
    //    $scope.HideEdit = status;
    //});

    $scope.HideEdit = localStorageService.get("EditLink");
    

    $scope.edit = function (Address, Contact, DOB, Department, Email, EmployeeID) {

        $location.path("/editProfile");

        var detail = {"EmployeeID": EmployeeID, "Address": Address, "Contact": Contact,"DOB": DOB, "Department": Department, "Email": Email };
        localStorageService.set("employeeProfile", detail);
    };


           
}]);

