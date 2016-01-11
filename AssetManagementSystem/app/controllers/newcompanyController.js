'use strict';
app.controller('newCompanyController', ['$scope', '$http', function ($scope, $http) {

    var target = angular.element(document.querySelector('#app'));
    target.addClass('body-wide');

    var serviceBase = 'http://localhost:14597/';
    $scope.company = { CompanyName: "", OwnerName: "", Address: "", Contact: "", Email: "", Prefix:"" };
    $scope.message = '';

    $scope.submit = function () {
        var valid = $scope.validate();
        if (valid)
            //var text = { "CompanyName": $scope.company.CompanyName, "OwnerName": $scope.company.OwnerName, "Address": $scope.company.Address, "Contact": $scope.company.Contact, "Email": $scope.company.Email };
            $http.post(serviceBase + 'api/Company/newCompany', JSON.stringify($scope.company)).then(function (results) {
            if (results.data.IsCompanyCreated)
            {
                $scope.message = "Company Details added Successfully";
                $scope.company = { CompanyName: "", OwnerName: "", Address: "", Contact: "", Email: "", Prefix:"" };
            }
            else {
                $scope.message = "Prefix already exist";
            }
        });
    };

    $scope.validate = function () {
        var isValid = true;
        if ($scope.company.CompanyName == null || $scope.company.CompanyName == "") {
            alert("Enter Company name");
           return isValid = false;
        };
        if ($scope.company.OwnerName == null || $scope.company.OwnerName == "") {
            alert("Enter Owner's name");
           return isValid = false;
        };
       
        if ($scope.company.Address == null || $scope.company.Address == "") {
            alert("Enter address");
           return isValid = false;
        };
        if ($scope.company.Contact == null || $scope.company.Contact == "") {
            alert("please enter the contact");
           return isValid = false;
        };
        if (!/^[0-9]*$/.test($scope.company.Contact) && ($scope.company.Contact != null || $scope.company.Contact != "")) {
            alert("enter valid phone no.");
            return isValid = false;
        };
        if ($scope.company.Email == null || $scope.company.Email == "") {
            alert("please enter the email");
            return isValid = false;
        };
        if (!/^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test($scope.company.Email) && ($scope.company.Email != null || $scope.company.Email != "")) {
            alert("Enter valid email");
            return isValid = false;
        };
        return isValid;
    };

}]);