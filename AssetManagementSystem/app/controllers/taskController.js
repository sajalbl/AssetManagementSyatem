'use strict';
app.controller('taskController', ['$scope', '$rootScope', '$http', 'localStorageService', '$window', function ($scope, $rootScope, $http, localStorageService, $window) {
    var serviceBase = 'http://localhost:14597/';
    
    $scope.employeeID = localStorageService.get("task");
        
    $scope.task = { "EmployeeID": $scope.EmployeeID, "EmployeeName": $scope.EmployeeName, "Description": $scope.Description };

    $http.post(serviceBase + 'api/manage/checkManager', JSON.stringify($scope.employeeID)).then(function (results) {
        if(results.data.IsManager)
        {
            $scope.manager = true;
            $scope.TaskAssign();
            $scope.employees();
        }
        else
        {
            $scope.employee = true;
            $scope.task();
            
        }
    });

    $scope.employees = function () {

        var text = { "EmployeeID": $scope.employeeID.EmployeeID };
        $http.post(serviceBase + 'api/manage/managerEmployees', JSON.stringify(text)).then(function (results) {
            $scope.managerEmployees = JSON.parse(results.data.ManagerEmployees);
            console.log($scope.managerEmployees);
        });

    };



    $scope.task = function () {
        var detail = { "EmployeeID": $scope.employeeID.EmployeeID };
        $http.post(serviceBase + 'api/manage/TaskList', JSON.stringify(detail)).then(function (results) {
            $scope.taskList = results.data.TaskList;

            console.log($scope.taskList);
        });
    };

    $scope.TaskAssign = function () {
        var detail = { "EmployeeID": $scope.employeeID.EmployeeID };

        $http.post(serviceBase + 'api/manage/taskAssign', JSON.stringify(detail)).then(function (results) {
            $scope.taskAssign = JSON.parse(results.data.TaskAssign);
            console.log($scope.taskAssign);
        });
    };

    $scope.assign = function (EmployeeID, EmployeeName) {

        location.href = "#/taskAssign";
        var text = { "EmployeeID": EmployeeID, "EmployeeName": EmployeeName };
        localStorageService.set("assign", text);
    };


    //$scope.submit = function () {
    //    var text = { "EmployeeID": $scope.task.EmployeeID, "EmployeeName": $scope.task.EmployeeName, "Description": $scope.task.Description , "AssignedBy": $scope.employeeID.EmployeeID ,"EmployeeConfirm": "Pending"};

    //    $http.post(serviceBase + 'api/manage/task', JSON.stringify(text)).then(function (results) {
    //        $scope.status = "Task added successfully";
    //        $scope.task = "";
    //        $scope.TaskAssign();
    //    });
   
    //};


    //$scope.completed = function (EmployeeID, Description, AssignedBy) {
        
    //        var text = { "EmployeeID": EmployeeID, "Description": Description, "AssignedBy": AssignedBy, "EmployeeConfirm": "Completed" };

    //        $http.post(serviceBase + 'api/manage/employeeConfirm', JSON.stringify(text)).then(function (results) {
    //            $scope.task();
    //    });
    //};

    $scope.approved = function (EmployeeID, Description) {
        var text = { "EmployeeID": EmployeeID, "Description": Description, "ManagerConfirm": "Approved" , "Accept": true };
       
        $http.post(serviceBase + 'api/manage/approval', JSON.stringify(text)).then(function (results) {
            if(results.data.ConfirmManager)
            {
                $scope.approval = "Approved";
            }
            else
            {
                $scope.approval = "Status is still pending";
            }
            $scope.TaskAssign();
        });
    };

    $scope.decline = function (EmployeeID, Description) {
        var text = { "EmployeeID": EmployeeID, "Description": Description, "ManagerConfirm": "Declined" , "Accept": false};

        $http.post(serviceBase + 'api/manage/approval', JSON.stringify(text)).then(function (results) {
            if (results.data.ConfirmManager)
            {
                $scope.approval = "Declined";
            }
            else
            {
                $scope.approval = "Status is still pending";
            }
            $scope.TaskAssign();
        });
    };


    //$scope.pending = function (EmployeeID, Description, AssignedBy) {
    //    var text = { "EmployeeID": EmployeeID, "Description": Description, "AssignedBy": AssignedBy, "EmployeeConfirm": "Pending" };

    //    $http.post(serviceBase + 'api/manage/employeeConfirm', JSON.stringify(text)).then(function (results) {
    //        $scope.task();
    //    });
    //};


    $scope.completed = function (EmployeeID,Description,AssignedBy,EmployeeConfirm) {
        EmployeeConfirm = "Completed";
        var text = {"EmployeeID": EmployeeID , "Description": Description , "AssignedBy": AssignedBy , "EmployeeConfirm": EmployeeConfirm}; 

        localStorageService.set("status", text);
    };

    $scope.pending = function (EmployeeID, Description, AssignedBy, EmployeeConfirm) {
        EmployeeConfirm = "Pending";
        var text = { "EmployeeID": EmployeeID, "Description": Description, "AssignedBy": AssignedBy, "EmployeeConfirm": EmployeeConfirm };

        localStorageService.set("status", text);
    };

    $scope.update = function (EmployeeID, EmployeeName, Description, AssignedBy, EmployeeConfirm, ManagerConfirm) {

        $scope.Update = localStorageService.get("status");

        $http.post(serviceBase + 'api/manage/employeeConfirm', JSON.stringify($scope.Update)).then(function (results) {
                  $scope.task();
        });

    };

    $scope.deleteTask = function (EmployeeID, Description) {

        if ($window.confirm("are you sure you want to delete this ?"))
        {
                var text = { "EmployeeID": EmployeeID, "Description": Description };

                $http.post(serviceBase + 'api/manage/deleteTask', JSON.stringify(text)).then(function (results) {
                    
                        $scope.approval = "Deleted";
                    
                        $scope.TaskAssign();
                });
            }
        else
        {
                $scope.approval = "";
            }
        };

        //var text = { "EmployeeID": EmployeeID, "EmployeeName": EmployeeName, "Description": Description, "AssignedBy": EmployeeID, "EmployeeConfirm": EmployeeConfirm, "ManagerConfirm": ManagerConfirm };

        //var update = JsonConvert.SerializeObject(text);

 

}]);