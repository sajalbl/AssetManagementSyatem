'use strict';
app.controller('taskAssignController', ['$scope', '$rootScope', '$http', 'localStorageService', '$window', function ($scope, $rootScope, $http, localStorageService, $window) {
    var serviceBase = 'http://localhost:14597/';

    $scope.task = localStorageService.get("assign");
    $scope.employeeID = localStorageService.get("taskAssign");

    $scope.detail = { "EmployeeID": $scope.task.EmployeeID, "EmployeeName": $scope.task.EmployeeName, "Description": $scope.Description };

    $scope.submit = function () {
            var text = { "EmployeeID": $scope.task.EmployeeID, "EmployeeName": $scope.task.EmployeeName, "Description": $scope.detail.Description , "AssignedBy": $scope.employeeID.EmployeeID ,"EmployeeConfirm": "Pending"};

            $http.post(serviceBase + 'api/manage/task', JSON.stringify(text)).then(function (results) {

                $scope.status = "Task added successfully";
                $scope.detail = "";
                
            });
   
        };

}]);