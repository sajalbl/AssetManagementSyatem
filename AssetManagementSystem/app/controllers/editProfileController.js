'use strict';
app.controller('editProfileController', ['uploadFileService', '$scope', '$http', 'localStorageService', function (uploadFileService, $scope, $http, localStorageService) {

    var serviceBase = 'http://localhost:14597/';
    var uploadBase = '';

    $scope.status = '';
    $scope.employeeID = localStorageService.get("Employee");
    $scope.profile = localStorageService.get("employeeProfile");

   

    //$scope.employeeInfo = {
    //    "EmployeeInfo": [{
    //        "info": [{
    //            "type": "info",
    //            Address: $scope.profile.Address,
    //            Contact: $scope.profile.Contact,
    //            Department: $scope.profile.Department,
    //            DOB: $scope.profile.DOB
    //        }]
    //    }]
    //}

    //$scope.pic = {
    //    "EmployeeInfo": [{
    //        "info": [{
    //            "type": "image",
    //            Picture: ""
    //        }]
    //    }]
    //}

    $scope.upload = function () {

       // var image = { "Pict": $scope.uploadFile.name };

       

        $scope.employeeInfo = {
            "EmployeeInfo": [{
                "info": [{
                    "type": "info",
                    Address: $scope.profile.Address,
                    Contact: $scope.profile.Contact,
                    Department: $scope.profile.Department,
                    DOB: $scope.profile.DOB
                }, {
                    "type": "image",
                    Picture: $scope.uploadFile.name
                }]
            }]
        }

        var info = JSON.stringify($scope.employeeInfo);
       // var image = JSON.stringify($scope.pic);
       var text = { "UserName": $scope.employeeID.UserName,"EmployeeInfo": info, "Email": $scope.profile.Email };
        
       uploadFileService.fileUpload(text.UserName, $scope.uploadFile, uploadBase).then(function (result) {

           
       });

       $http.post(serviceBase + 'api/Employee/updateEmployee', JSON.stringify(text)).then(function (results) {
 
           $scope.status = "Details Updated Successfully";
       });

        
    };

        $scope.update = function () {
            $scope.employeeInfo = {
                "EmployeeInfo": [{
                    "info": [{
                        "type": "info",
                        Address: $scope.profile.Address,
                        Contact: $scope.profile.Contact,
                        Department: $scope.profile.Department,
                        DOB: $scope.profile.DOB
                    }, {
                        "type": "image",
                        Picture: $scope.profile.Picture
                    }]
                }]
            }
            var info = JSON.stringify($scope.employeeInfo);

            var text = { "UserName": $scope.employeeID.UserName, "EmployeeInfo": info, "Email": $scope.profile.Email };

            $http.post(serviceBase + 'api/Employee/updateEmployee', JSON.stringify(text)).then(function (results) {

                $scope.status = "Details Updated Successfully";
            });
        };

  


}]);