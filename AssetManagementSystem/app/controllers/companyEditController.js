'use strict';
app.controller('companyEditController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';

    $scope.companyInfo = localStorageService.get("CompanyInfo");
    $scope.message = '';

    $scope.update = function () {
        var text = { "CompanyName": $scope.companyInfo.CompanyName, "OwnerName": $scope.companyInfo.OwnerName, "Address": $scope.companyInfo.Address, "Contact": $scope.companyInfo.Contact, "Email": $scope.companyInfo.Email };

        $http.post(serviceBase + 'api/Company/updateCompany', JSON.stringify(text)).then(function (results) {
            if (results.data.IsSuccess != false && results.data.IsCompanyUpdated) {
                //Re-store the cookie after successful update.
                localStorageService.set("CompanyInfo", $scope.companyInfo)
                $scope.message = "Details Updated Successfully";
            }
        });
    };

    //Add a validation method here for update.


}]);