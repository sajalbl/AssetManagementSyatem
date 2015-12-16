'use strict';
app.controller('addResourcesController', ['$scope', '$http', 'localStorageService', function ($scope, $http, localStorageService) {
    var serviceBase = 'http://localhost:14597/';
     
    //$scope.$on('Success', function (event, detail) {

    //    $scope.companyName = detail;
    //});

    $scope.companyName = localStorageService.get("Company");
    $scope.resources = { "NameOfDevice": $scope.NameOfDevice, "Type": $scope.Type,  "IssuedFrom": $scope.IssuedFrom, "EmployeeID": $scope.EmployeeID, "Serial": $scope.Serial};

    $scope.add = function () {

        var valid = $scope.validate();

        if(valid)
            var text = { "CompanyName": $scope.companyName.CompanyName, "NameOfDevice": $scope.resources.NameOfDevice, "Type": $scope.resources.Type, "IssuedFrom": $scope.resources.IssuedFrom, "EmployeeID": $scope.resources.EmployeeID, "Serial": $scope.resources.Serial };
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
        
    };

    $scope.validate = function () {
        var isValid = true;

        if ($scope.resources.NameOfDevice == null && $scope.resources.NameOfDevice == "")
        {
            alert("Enter Device name");
            isValid = false;
        }
        if ($scope.resources.Type == null && $scope.resources.Type == "")
        {
            alert("Enter Resource type");
            isValid = false;
        }
        //if ($scope.resources.IssuedTo == null && $scope.resources.IssuedTo == "") {
        //    alert("Enter Issued On Date and Time");
        //    isValid = false;
        //}
        if ($scope.resources.IssuedFrom == null && $scope.resources.IssuedFrom == "")
        {
            alert("Enter Issued From date and time");
            isValid = false;
        }
        return isValid;
    };

}]);