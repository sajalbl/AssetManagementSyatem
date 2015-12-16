'use strict';
app.controller('newCompanyController', ['$scope', '$http', function ($scope, $http) {

    var target = angular.element(document.querySelector('#app'));
    target.addClass('body-wide');

    var serviceBase = 'http://localhost:14597/';
    $scope.company = { CompanyName: "", OwnerName: "", Address: "", Contact: "", Email: "" };
    $scope.message = '';

    $scope.submit = function () {
        var valid = $scope.validate();
        if (valid)
            //var text = { "CompanyName": $scope.company.CompanyName, "OwnerName": $scope.company.OwnerName, "Address": $scope.company.Address, "Contact": $scope.company.Contact, "Email": $scope.company.Email };
            $http.post(serviceBase + 'api/Company/newCompany', JSON.stringify($scope.company)).then(function (results) {
            if (results.data.IsSuccess != false && results.data.IsCompanyCreated)
            {
                $scope.message = "Company Details added Successfully";
                $scope.company = { CompanyName: "", OwnerName: "", Address: "", Contact: "", Email: "" };
            }
            else {
                $scope.message = "Try again.";
            }
        });
    };

    $scope.validate = function () {
        var isValid = true;
        if ($scope.company.CompanyName == null && $scope.company.CompanyName == "") {
            alert("Enter Company name");
            isValid = false;
        };
        if ($scope.company.OwnerName == null && $scope.company.OwnerName == "") {
            alert("Enter Owner's name");
            isValid = false;
        };
       
        if ($scope.company.Address == null && $scope.company.Address == "") {
            alert("Enter address");
            isValid = false;
        };
        if ($scope.company.Contact == null || $scope.company.Contact == "") {
            alert("please enter the contact");
            isValid = false;
        };
        if (!/^[0-9]*$/.test($scope.company.Contact) && ($scope.company.Contact != null || $scope.company.Contact != "")) {
            alert("enter valid phone no.");
            isValid = false;
        };
        if ($scope.company.Email == null || $scope.company.Email == "") {
            alert("please enter the email");
            isValid = false;
        };
        if (!/^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test($scope.company.Email) && ($scope.company.Email != null || $scope.company.Email != "")) {
            alert("Enter valid email");
            isValid = false;
        };
        return isValid;
    };

}]);