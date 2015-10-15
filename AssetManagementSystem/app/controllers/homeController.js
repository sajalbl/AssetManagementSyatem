'use strict';
app.controller('homeController', ['$scope','$http', function ($scope,$http) {

        $scope.companyName = "";
        $scope.Admin = "";

        $scope.valid = function () {
            var isValid = true;
            if ($scope.companyName == null && $scope.companyName == "") {
                alert("Enter Company name");
                isValid = false;
            }
            if ($scope.Admin == null && $scope.Admin == "") {
                alert("Enter Admin name");
                isValid = false;
            }
            return isValid;

        };
}]);
