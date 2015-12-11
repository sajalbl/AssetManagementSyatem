'use strict';
app.controller('resourcesController', ['$rootScope', '$scope', '$modal', '$http', 'localStorageService', '$window', function ($rootScope,$scope, $modal, $http, localStorageService, $window) {
    var serviceBase = 'http://localhost:14597/';
    $scope.query = "";

    
    //$scope.resourceCompany = localStorageService.get("companyData");

    $scope.resourceCompany = localStorageService.get("companyData");

    //$rootScope.$on("CallShowMethod", function () {
    //    $scope.show();
    //});

    $http.post(serviceBase + 'api/manage/showResources', JSON.stringify($scope.resourceCompany)).then(function (results) {
        $scope.resourceList = JSON.parse(results.data.ResourcesList);
        console.log($scope.resourceList);
    });
    
    $scope.show = function () {
        $scope.resourceCompany = localStorageService.get("companyData");
        
        $http.post(serviceBase + 'api/manage/showResources', JSON.stringify($scope.resourceCompany)).then(function (results) {
            $scope.resourceList = JSON.parse(results.data.ResourcesList);
            console.log($scope.resourceList);
        });
    };

    $scope.editRow = function (resource) {
        location.href = "#/edit";
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

    var openImage = function ($scope, $modalInstance, ser) {
        //$scope.image = toImage;

        //$scope.image(ser);
        $scope.source = ser;
        var text = { "Serial": ser };
        $http.post(serviceBase + 'api/manage/showImage', JSON.stringify(text)).then(function (results) {

            $scope.imageList = JSON.parse(results.data.resourceImage);

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

        var text = { "CompanyName": resource.CompanyName, "NameOfDevice": resource.NameOfDevice, "Type": resource.Type, "EmployeeID": resource.EmployeeID, "Serial": resource.Serial };
        $http.post(serviceBase + 'api/manage/deleteResources', JSON.stringify(text)).then(function (results) {
            
            $scope.show();
        });
    };
  

    
}]);