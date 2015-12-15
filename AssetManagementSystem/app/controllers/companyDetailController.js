'use strict';
app.controller('companyDetailController', ['$rootScope', '$modal', '$scope', '$http', 'localStorageService', '$window', function ($rootScope, $modal, $scope, $http, localStorageService, $window) {
    var serviceBase = 'http://localhost:14597/';

    var target = angular.element(document.querySelector('#app'));
    target.removeClass('body-wide');

    var company = localStorageService.get("Company");
    if (company != null)
        $http.post(serviceBase + 'api/manage/searchCompany', JSON.stringify(company)).then(function (results) {
            //this api return every info about the company
            $scope.companyInfo = JSON.parse(results.data.CompanyInfo);
        });


    //$scope.detail = localStorageService.get("companyText");

    //$scope.resourcesNumber = localStorageService.get("ResourceCount");

    //$http.post(serviceBase + 'api/manage/searchCompany', JSON.stringify($scope.detail)).then(function (results) {

    //    $scope.companyList = JSON.parse(results.data.CompanyList);
    //    $scope.ShowTable = true;
    //    console.log($scope.companyList);

    //});

    //NO NEED for this , the above api returns the count for both of these.
    //var details = { "CompanyName": $scope.resourcesNumber.CompanyName };
    //$http.post(serviceBase + 'api/manage/resourceCount', JSON.stringify(details)).then(function (results) {
    //    $scope.resourceCount = results.data.Resourcecount;

    //});

    //Declaring two variables with same info????
    //var employee = { "CompanyName": $scope.resourcesNumber.CompanyName };
    //$http.post(serviceBase + 'api/manage/employeeCount', JSON.stringify(employee)).then(function (results) {
    //    $scope.employeeCount = results.data.EmployeeCount;
    //});


    $scope.editRow = function (companyInfo) {
        location.href = "#/companyEdit";
        localStorageService.set("CompanyInfo", companyInfo);
    };

    $scope.removeRow = function (CompanyName, OwnerName) {

        var text = { "CompanyName": CompanyName, "OwnerName": OwnerName };
        localStorageService.set("companymodal", text);

        //$rootScope.$emit('modal', 'company');

        var modalInstance = $modal.open({
            templateUrl: './app/views/deleteModal.html',
            controller: 'deleteModalController'

        });

        //if ($window.confirm("are you sure you want to delete this ?")) {
        //    var text = { "CompanyName": CompanyName, "OwnerName": OwnerName };
        //    var detailed = { "CompanyName": CompanyName };
        //    $http.post(serviceBase + 'api/manage/deleteCompany', JSON.stringify(text)).then(function (results) {
        //        $scope.ShowTable = false;
        //        $scope.hideTable = true;
        //        $scope.status = "Deleted";


        //        $http.post(serviceBase + 'api/manage/companyDeleted', JSON.stringify(detailed)).then(function (results) {
        //            if (results.data.DeletedCompany) {
        //                $scope.stat = "";
        //            }
        //        });
        //    });
        //}
        //else
        //{
        //    $scope.status = "";
        //}
    };

}]);