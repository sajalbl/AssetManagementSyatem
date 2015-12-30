'use strict';
app.controller('taskAssignController', ['$scope', '$rootScope', '$modal', '$http', 'localStorageService', '$window', function ($scope, $rootScope, $modal, $http, localStorageService, $window) {
    var serviceBase = 'http://localhost:14597/';

    $scope.task = localStorageService.get("assign");
    var employeeID = localStorageService.get("Employee");
    $scope.detail = { "EmployeeID": $scope.task.EmployeeID, "EmployeeName": $scope.task.EmployeeName, "Description": $scope.Description };

    $scope.submit = function () {
        var text = { "EmployeeID": $scope.task.EmployeeID, "EmployeeName": $scope.task.EmployeeName, "Description": $scope.detail.Description, "EmployeeConfirm": "Pending", "Email": employeeID.Email, "AssignedBy": employeeID.EmployeeName };

            $http.post(serviceBase + 'api/manage/task', JSON.stringify(text)).then(function (results) {

                var modalInstance = $modal.open({
                    templateUrl: 'okModal.html',
                    controller: openModal,
                   
                });

                //$scope.status = "Task added successfully";
                $scope.detail = "";
                
            });
   
    };

    var openModal = function ($scope, $modalInstance) {
        $scope.ok = function () {
            $modalInstance.dismiss();
        };
    };

}]);