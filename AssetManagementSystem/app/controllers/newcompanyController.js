'use strict';
app.controller('newCompanyController', ['$scope', function ($scope) {

    $scope.Cname = "";
    $scope.Oname = "";
    $scope.resources = '';
    $scope.address = "";
    $scope.contact = '';
    $scope.email = "";

    $scope.validate = function () {
        var isValid = true;
        if ($scope.Cname == null && $scope.Cname == "") {
            alert("Enter Company name");
            isValid = false;
        }
        if ($scope.Oname == null && $scope.Oname == "") {
            alert("Enter Owner's name");
            isValid = false;
        }
        if ($scope.resources == null && $scope.resources == "") {
            alert("Enter no. of resources");
            isValid = false;
        }
        if ($scope.address == null && $scope.address == "") {
            alert("Enter address");
            isValid = false;
        }
        if ($scope.contact == null || $scope.contact == "") {
            alert("please enter the contact");
            isValid = false;
        }

        //var p =  /^[0-9]*$/
        if (!/^[0-9]*$/.test($scope.contact) && ($scope.contact != null || $scope.contact != "")) {
            alert("enter valid phone no.");
            isValid = false;
        }
        if ($scope.email == null || $scope.email == "") {
            alert("please enter the email");
            isValid = false;
        }
        //var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
        if (!/^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test($scope.email) && ($scope.email != null || $scope.email != "")) {
            alert("Enter valid email");
            isValid = false;
        }
        return isValid;
    };
     
}]);