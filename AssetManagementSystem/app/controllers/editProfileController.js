'use strict';
app.controller('editProfileController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';

    $scope.employeeID = localStorageService.get("editProfile");
    $scope.profile = localStorageService.get("employeeProfile");
    
   

    $scope.update = function () {

        console.log($scope.uploadFile);
        var text = { "EmployeeID": $scope.employeeID.EmployeeID, "DOB": $scope.profile.DOB, "Address": $scope.profile.Address, "Contact": $scope.profile.Contact, "Email": $scope.profile.Email, "Picture": $scope.uploadFile.name };
        
        var data = new FormData();
        console.log();
        data.append('EmployeeID', $scope.employeeID.EmployeeID);
        data.append('UploadImage', $scope.uploadFile.name);
        data.append('FolderPath', "http://localhost:58474/Images/");
        console.log(data);
         $http.post(serviceBase + 'api/manage/imageUpload', data, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).then(function (results) {
            console.log(results);

         $http.post(serviceBase + 'api/manage/updateEmployee', JSON.stringify(text)).then(function (results) {

             $scope.status = "Details Updated Successfully";

            });
        });

     
        
    };

}]);