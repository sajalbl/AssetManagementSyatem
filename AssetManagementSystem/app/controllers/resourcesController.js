'use strict';
app.controller('resourcesController', ['$rootScope', '$scope', '$modal', '$http', 'localStorageService', '$location', function ($rootScope, $scope, $modal, $http, localStorageService, $location) {
    var serviceBase = 'http://localhost:14597/';
    var service = 'http://localhost:58474/';
    $scope.query = "";

    $scope.imageList = [];
    //$scope.resourceCompany = localStorageService.get("companyData");

    var resourceCompany = localStorageService.get("Company");

    //$rootScope.$on("CallShowMethod", function () {
    //    $scope.show();
    //});

   // $scope.ResourceBase = ""

    $http.post(serviceBase + 'api/manage/showResources', JSON.stringify(resourceCompany)).then(function (results) {
        $scope.resourceList = JSON.parse(results.data.ResourcesList);
        console.log($scope.resourceList);
    });

    $http.post(service + 'api/manage/downloadCsv', JSON.stringify(resourceCompany)).then(function (results) {
        if (results.data.csvDowloaded == false)
        {
            $scope.message = "Data not found";
        }
    });
    
    $scope.show = function () {
        var resourceCompany = localStorageService.get("Company");
        
        $http.post(serviceBase + 'api/manage/showResources', JSON.stringify(resourceCompany)).then(function (results) {
            $scope.resourceList = JSON.parse(results.data.ResourcesList);
            console.log($scope.resourceList);
        });
    };

    $scope.editRow = function (resource) {
        $location.path("/edit");
        localStorageService.set("resourceDetail", resource);
    };
    
    $scope.remove = function (resource) {
        
        $scope.modalConfirmationResult = 'cancel';
       
        var modalInstance = $modal.open({
            templateUrl: 'deleteModal.html',
            controller: openModal,
            resolve: {
                toDelete: function () {
                    return $scope.deleteResources;
                },
                whichRes: function () {
                    return resource;
                }
            }
        });
    };

    var openModal = function ($scope, $modalInstance, toDelete, whichRes) {
        $scope.delete = toDelete;
        $scope.ok = function () {
            $scope.delete(whichRes);
            $modalInstance.dismiss('cancel');
        };
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };

    $scope.showImage = function (Serial) {
        
        $scope.modalConfirmationResult = 'cancel';

        var modalInstance = $modal.open({
            templateUrl: 'imageModal.html',
            controller: openImage,
            resolve: {
                ser: function () {
                    return Serial;
                }
            }
        
        });
    };

    var openImage = function ($scope, $modalInstance, ser, $http) {
        //$scope.image = toImage;

        //$scope.image(ser);
        $scope.source = ser;
        $scope.imageList = [];
        var text = { "Serial": ser };
        $http.post(serviceBase + 'api/manage/showImage', JSON.stringify(text)).then(function (results) {
            
            $scope.imageList = JSON.parse(results.data.resourceImage);
            console.log($scope.imageList);
        });

        $scope.close = function () {
            $modalInstance.dismiss('cancel');
        };
    };

    //$scope.showPicture = function (Serial) {
        
    //    var text = { "Serial": Serial };
    //    $http.post(serviceBase + 'api/manage/showImage', JSON.stringify(text)).then(function (results) {

    //        $scope.imageList = JSON.parse(results.data.resourceImage);
            
    //    });
    //};
        

    $scope.deleteResources = function (resource) {

        var text = {"CompanyID":resource.CompanyID, "CompanyName": resource.CompanyName, "NameOfDevice": resource.NameOfDevice, "Type": resource.Type, "EmployeeID": resource.EmployeeID, "Serial": resource.Serial };
        $http.post(serviceBase + 'api/manage/deleteResources', JSON.stringify(text)).then(function (results) {
            
            $scope.show();
        });
    };
  

    $scope.allocate = function (Serial) {

        $scope.modalConfirmationResult = 'cancel';

        var modalInstance = $modal.open({
            templateUrl: 'allocate.html',
            controller: openAlloc,
            resolve: {
                ser: function () {
                    return Serial;

                },
                show: function () {
                    return $scope.show;
                }
            }

        })
    };

   

    var openAlloc = function ($scope, $modalInstance, ser, localStorageService, $http, show) {
        
        $scope.detail = show;
        var companyName = localStorageService.get("Company");

        $http.post(serviceBase + 'api/company/EmployeeID', JSON.stringify(companyName)).then(function (results) {
            if(results.data.EmployeeList != null)
            {
                $scope.employeeList = JSON.parse(results.data.EmployeeList);
            }

        })

        //$scope.employee = { EmployeeID: "" };
        
        $scope.ok = function () {
           
            $scope.employee = {"UserName": $scope.EmployeeID, "Serial": ser, "Allocate": true };

            $http.post(serviceBase + 'api/manage/allocate', JSON.stringify($scope.employee)).then(function (results) {
                if (results.data.allocated) {
                    
                    $scope.detail();
                    $modalInstance.dismiss('cancel');
                    
                }

            });

            
        };
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    };


    $scope.deallocate = function (Serial) {

        var serial = { "Serial": Serial, "Allocate": false };

        $http.post(serviceBase + 'api/manage/allocate', JSON.stringify(serial)).then(function (results) {
            if (results.data.allocated == false) {
                $scope.show();
            }

        });
    };

    $scope.restore = function (Serial,CompanyID) {

        var text = { "Serial": Serial, "CompanyID": CompanyID, "Deleted":true };

        $http.post(serviceBase + 'api/manage/deleteResources', JSON.stringify(text)).then(function (results) {

            if(results.data.ResourceDeleted == true)
            {
                $scope.show();
            }
        });
    };
    
}]);