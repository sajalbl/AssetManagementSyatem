'use strict';
app.controller('taskAssignController', ['$scope', '$rootScope', '$modal', '$http', 'localStorageService', '$window', function ($scope, $rootScope, $modal, $http, localStorageService, $window) {
    var serviceBase = 'http://localhost:14597/';

    $scope.task = localStorageService.get("assign");
    var employeeID = localStorageService.get("Employee");
    $scope.detail = { "UserName": $scope.task.EmployeeID, "EmployeeName": $scope.task.EmployeeName, "Description": $scope.Description };

    $scope.submit = function () {

        var valid = $scope.validate();
        if (valid)
            {
        var text = { "UserName": $scope.task.EmployeeID, "EmployeeName": $scope.task.EmployeeName, "Description": $scope.detail.Description, "EmployeeConfirm": "Pending", "Email": employeeID.Email, "AssignedBy": employeeID.UserName };

            $http.post(serviceBase + 'api/manage/task', JSON.stringify(text)).then(function (results) {

                var modalInstance = $modal.open({
                    templateUrl: 'okModal.html',
                    controller: openModal,
                   
                });

                //$scope.status = "Task added successfully";
                $scope.detail = "";
                
            });
        }
    };

    var openModal = function ($scope, $modalInstance) {
        $scope.ok = function () {
            $modalInstance.dismiss();
        };
    };

    $scope.validate = function () {

       var isvalid = true;

        if($scope.detail.Description == null || $scope.detail.Description == "")
        {
            alert("Enter Description");
            isvalid = false;
        }
        return isvalid;
    }

}]);