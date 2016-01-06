'use strict';
app.controller('taskController', ['$scope', '$rootScope', '$modal', '$http', 'localStorageService', '$location', function ($scope, $rootScope, $modal, $http, localStorageService, $location) {
    var serviceBase = 'http://localhost:14597/';
    
    var employeeID = localStorageService.get("Employee");
    //var prefix = localStorageService.get("CompanyInfo");

    
    //$scope.task = { "EmployeeID": $scope.EmployeeID, "EmployeeName": $scope.EmployeeName, "Description": $scope.Description };

    $http.post(serviceBase + 'api/Employee/checkManager', JSON.stringify(employeeID)).then(function (results) {
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

        //var text = { "EmployeeName": employeeID.EmployeeName, "Email": employeeID.Email, "Prefix": prefix.Prefix };

        //var employeeID = localStorageService.get("Employee");
        
        $http.post(serviceBase + 'api/Employee/managerEmployees', JSON.stringify(employeeID)).then(function (results) {
            $scope.managerEmployees = JSON.parse(results.data.ManagerEmployees);
            console.log($scope.managerEmployees);
        });

    };



    $scope.task = function () {

        
        $http.post(serviceBase + 'api/manage/TaskList', JSON.stringify(employeeID)).then(function (results) {
            $scope.taskList = JSON.parse(results.data.TaskList);

            console.log($scope.taskList);
        });
    };

    $scope.TaskAssign = function () {
        

        $http.post(serviceBase + 'api/manage/taskAssign', JSON.stringify(employeeID)).then(function (results) {
            if (results.data.TaskAssign != null)
            {
                $scope.taskPresent = true;
            $scope.taskAssign = JSON.parse(results.data.TaskAssign);
            console.log($scope.taskAssign);
            }
        });
    };

    $scope.assign = function (EmployeeID, EmployeeName) {

        $location.path("/taskAssign");
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
        var text = { "UserName": EmployeeID, "Description": Description, "ManagerConfirm": "Approved" , "Accept": true };
       
        $http.post(serviceBase + 'api/manage/approval', JSON.stringify(text)).then(function (results) {
            if(results.data.ConfirmManager)
            {
                $scope.approval = "Approved";
            }
            else
            {
                var modalInstance = $modal.open({
                    templateUrl: 'pending.html',
                    controller: pendingModal,
                });
            }
            $scope.TaskAssign();
        });
    };

    $scope.decline = function (EmployeeID, Description) {
        var text = { "UserName": EmployeeID, "Description": Description, "ManagerConfirm": "Declined", "Accept": false };

        $http.post(serviceBase + 'api/manage/approval', JSON.stringify(text)).then(function (results) {
            if (results.data.ConfirmManager)
            {
                $scope.approval = "Declined";
            }
            else
            {
                var modalInstance = $modal.open({
                    templateUrl: 'pending.html',
                    controller: pendingModal,
                });
            }
            $scope.TaskAssign();
        });
    };


    var pendingModal = function ($scope, $modalInstance) {
        $scope.ok = function () {
            $modalInstance.dismiss();
        };
    };

    //$scope.pending = function (EmployeeID, Description, AssignedBy) {
    //    var text = { "EmployeeID": EmployeeID, "Description": Description, "AssignedBy": AssignedBy, "EmployeeConfirm": "Pending" };

    //    $http.post(serviceBase + 'api/manage/employeeConfirm', JSON.stringify(text)).then(function (results) {
    //        $scope.task();
    //    });
    //};


    $scope.completed = function (EmployeeID,Description,AssignedBy,EmployeeConfirm) {
        EmployeeConfirm = "Completed";
        var text = { "UserName": EmployeeID, "Description": Description, "AssignedBy": AssignedBy, "EmployeeConfirm": EmployeeConfirm };

        localStorageService.set("status", text);
    };

    $scope.pending = function (EmployeeID, Description, AssignedBy, EmployeeConfirm) {
        EmployeeConfirm = "Pending";
        var text = { "UserName": EmployeeID, "Description": Description, "AssignedBy": AssignedBy, "EmployeeConfirm": EmployeeConfirm };

        localStorageService.set("status", text);
    };

    $scope.update = function (EmployeeID, EmployeeName, Description, AssignedBy, EmployeeConfirm, ManagerConfirm) {

        $scope.Update = localStorageService.get("status");

        $http.post(serviceBase + 'api/manage/employeeConfirm', JSON.stringify($scope.Update)).then(function (results) {
                  $scope.task();
        });

    };

    //$scope.deleteTask = function (EmployeeID, Description) {

    //    if ($window.confirm("are you sure you want to delete this ?"))
    //    {
    //            var text = { "EmployeeID": EmployeeID, "Description": Description };

    //            $http.post(serviceBase + 'api/manage/deleteTask', JSON.stringify(text)).then(function (results) {
                    
    //                    $scope.approval = "Deleted";
                    
    //                    $scope.TaskAssign();
    //            });
    //        }
    //    else
    //    {
    //            $scope.approval = "";
    //        }
    //    };

        //var text = { "EmployeeID": EmployeeID, "EmployeeName": EmployeeName, "Description": Description, "AssignedBy": EmployeeID, "EmployeeConfirm": EmployeeConfirm, "ManagerConfirm": ManagerConfirm };

        //var update = JsonConvert.SerializeObject(text);

    $scope.deleteTask = function (EmployeeID, Description) {
        var text = { "UserName": EmployeeID, "Description": Description };
        $scope.modalConfirmationResult = 'cancel';

        var modalInstance = $modal.open({
            templateUrl: 'deleteModal.html',
            controller: openModal,
            resolve: {
                toDelete: function () {
                    return $scope.delete;
                },
                whichTask: function () {
                    return text;
                }
            }
        });
    };

    var openModal = function ($scope, $modalInstance, toDelete, whichTask) {
        $scope.delete = toDelete;

        $scope.ok = function () {
            $scope.delete(whichTask);
            $modalInstance.dismiss('cancel');
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }

    $scope.delete = function (text) {

        //var text = { "EmployeeID": EmployeeID, "Description": Description };

        $http.post(serviceBase + 'api/manage/deleteTask', JSON.stringify(text)).then(function (results) {

            $scope.approval = "Deleted";

            $scope.TaskAssign();
        });
    };

}]);