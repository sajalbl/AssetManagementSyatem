'use strict';
app.controller('taskController', ['$scope', '$rootScope', '$http', 'localStorageService', function ($scope, $rootScope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';

    $scope.employeeID = localStorageService.get("task");

    $scope.task = { "EmployeeID": $scope.EmployeeID, "EmployeeName": $scope.EmployeeName, "Description": $scope.Description };

    $http.post(serviceBase + 'api/manage/checkManager', JSON.stringify($scope.employeeID)).then(function (results) {
        if(results.data.IsManager)
        {
            $scope.manager = true;
        }
        else
        {
            $scope.employee = true;
        }
    });

    $http.post(serviceBase + 'api/manage/TaskList', JSON.stringify($scope.employeeID)).then(function (results) {
        $scope.taskList = JSON.parse(results.data.TaskList);
        console.log($scope.taskList);
    });

    $scope.submit = function () {
        var text = { "EmployeeID": $scope.task.EmployeeID, "EmployeeName": $scope.task.EmployeeName, "Description": $scope.task.Description };

        $http.post(serviceBase + 'api/manage/task', JSON.stringify(text)).then(function (results) {
            $scope.status = "Task added successfully";
            $scope.task = "";
        });
    };
}]);