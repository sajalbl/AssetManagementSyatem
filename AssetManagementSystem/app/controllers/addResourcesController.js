'use strict';
app.controller('addResourcesController', ['uploadFileService', '$scope', '$http', 'localStorageService', function (uploadFileService, $scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
    var service = 'http://localhost:58474/';
    var uploadBase = '';
    //$scope.$on('Success', function (event, detail) {

    //    $scope.companyName = detail;
    //});

    $scope.status = '';

    $scope.companyName = localStorageService.get("Company");
    $scope.resources = { "NameOfDevice": $scope.NameOfDevice, "Type": $scope.Type,  "IssuedFrom": $scope.IssuedFrom, "Serial": $scope.Serial};

    $scope.add = function () {

        var valid = $scope.validate();

        if (valid)
            {
            var text = { "CompanyName": $scope.companyName.CompanyName, "NameOfDevice": $scope.resources.NameOfDevice, "Type": $scope.resources.Type, "IssuedFrom": $scope.resources.IssuedFrom, "Serial": $scope.resources.Serial };
        $http.post(serviceBase + 'api/manage/newResources', JSON.stringify(text)).then(function (results) {
            if (results.data.IsResourcesCreated)
                {
                $scope.resources = "";
                $scope.status = "Resource detail added";
            }
            else
            {
                $scope.status = "Resource already exist";
            }
      
           
        });
        }
        
    };

    $scope.upload = function () {

        uploadFileService.CSVUpload($scope.csvFile, uploadBase).then(function (results) {

            var text = { "CompanyName": $scope.companyName.CompanyName, "FileName": $scope.csvFile.name };
        $http.post(service + 'api/manage/csvController', JSON.stringify(text)).then(function (results) {

            if (results.data.csvUploaded == false)
            {
                $scope.replaceTable = true;
                $scope.replaceList = JSON.parse(results.data.Duplicate);
            }
            else {
                $scope.status = "Details added successfully";

            }
        });

        });
    };

    $scope.rep = function (list) {

        var text = { "UserName": list.UserName, "Serial": list.Serial, "NameOfDevice": list.NameOfDevice, "Type": list.Type, "CompanyName": list.CompanyName, "IssuedFrom": list.IssuedFrom };

        $http.post(serviceBase + 'api/manage/replaceResource', JSON.stringify(text)).then(function (results) {

            if (results.data.IsResourceReplaced) {

                $scope.status = "Details added successfully";
            }

        });

    };

    $scope.validate = function () {
        var isValid = true;

        if ($scope.resources.NameOfDevice == null || $scope.resources.NameOfDevice == "")
        {
            alert("Enter Device name");
           return isValid = false;
        }
        if ($scope.resources.Type == null || $scope.resources.Type == "")
        {
            alert("Enter Resource type");
           return isValid = false;
        }
        //if ($scope.resources.IssuedTo == null && $scope.resources.IssuedTo == "") {
        //    alert("Enter Issued On Date and Time");
        //    isValid = false;
        //}
        if ($scope.resources.IssuedFrom == null || $scope.resources.IssuedFrom == "")
        {
            alert("Enter Issued From date and time");
           return isValid = false;
        }

        if ($scope.resources.Serial == null || $scope.resources.Serial == "") {
            alert("Enter Serial no. of resource");
           return isValid = false;
        }
        return isValid;
    };

}]);