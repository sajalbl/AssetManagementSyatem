'use strict';
app.controller('editProfileController', ['uploadFileService', '$scope', '$http', 'localStorageService', function (uploadFileService, $scope, $http, localStorageService) {

    var serviceBase = 'http://localhost:14597/';
    var uploadBase = '';

    $scope.employeeID = localStorageService.get("editProfile");
    $scope.profile = localStorageService.get("employeeProfile");

    $scope.update = function () {
        var text = { "EmployeeID": $scope.employeeID.EmployeeID, "DOB": $scope.profile.DOB, "Address": $scope.profile.Address, "Contact": $scope.profile.Contact, "Email": $scope.profile.Email, "Picture": $scope.uploadFile.name };
        
        uploadFileService.fileUpload($scope.employeeID.EmployeeID, $scope.uploadFile, uploadBase).then(function (results) {

        });

        $http.post(serviceBase + 'api/manage/updateEmployee', JSON.stringify(text)).then(function (results) {
            $scope.status = "Details Updated Successfully";
        });

    };


}]);