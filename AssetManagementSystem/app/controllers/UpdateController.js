'use strict';
app.controller('UpdateController', ['$scope', '$http','localStorageService', function ($scope, $http , localStorageService ) {
    var serviceBase = 'http://localhost:14597/';
  
    $scope.companyData = localStorageService.get("companyDetail");
    
    $scope.update = function () {
        var text = { "CompanyName": $scope.companyData.CompanyName, "OwnerName": $scope.companyData.OwnerName, "Address": $scope.companyData.Address, "Contact": $scope.companyData.Contact, "Email": $scope.companyData.Email };
        $http.post(serviceBase + 'api/manage/updateCompany', JSON.stringify(text)).then(function (results) {
            $scope.companyData = "";
        });
        $scope.status = "Details Updated Successfully";
    };

   
}]);