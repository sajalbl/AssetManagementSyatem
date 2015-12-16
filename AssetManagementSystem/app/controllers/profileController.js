'use strict';
app.controller('profileController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService ) {
    var serviceBase = 'http://localhost:14597/';
    
    var target = angular.element(document.querySelector('#app'));
    target.removeClass('body-wide');

    //$scope.ShowEdit = false;
    
    var employeeID = localStorageService.get("Employee");
    //$scope.source = "http://localhost:58474/images/";
     
   
    $http.post(serviceBase + 'api/manage/employees', JSON.stringify(employeeID)).then(function (results) {
        $scope.employeeList = results.data.EmployeeList;
        
        
        console.log($scope.employeeList);
    });



    //$scope.$on('EditLink', function (event, status) {
    //    $scope.HideEdit = status;
    //});

    $scope.HideEdit = localStorageService.get("EditLink");
    

    $scope.edit = function (DOB, Address, Contact, Email) {

        location.href = "#/editProfile";

        var detail = { "DOB": DOB, "Address": Address, "Contact": Contact, "Email": Email };
        localStorageService.set("employeeProfile", detail);
    };


           
}]);

