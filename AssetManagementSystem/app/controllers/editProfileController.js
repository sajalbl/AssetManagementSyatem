'use strict';
app.controller('editProfileController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';

    $scope.employeeID = localStorageService.get("editProfile");
    $scope.profile = localStorageService.get("employeeProfile");
    

    $scope.update = function () {
        var text = {"EmployeeID":$scope.employeeID.EmployeeID, "DOB": $scope.profile.DOB, "Address": $scope.profile.Address,  "Contact": $scope.profile.Contact, "Email": $scope.profile.Email };
        $http.post(serviceBase + 'api/manage/updateEmployee', JSON.stringify(text)).then(function (results) {
            $scope.employee = "";
        });
        $scope.status = "Details Updated Successfully";
    };


}]);