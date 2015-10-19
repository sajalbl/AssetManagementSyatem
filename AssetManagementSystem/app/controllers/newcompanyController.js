'use strict';
app.controller('newCompanyController', ['$scope', '$http', function ($scope, $http) {

    var serviceBase = 'http://localhost:14597/';

    $scope.Cname = "";
    $scope.Oname = "";
    $scope.resources = '';
    $scope.address = "";
    $scope.contact = '';
    $scope.email = "";

    $scope.submit = function () {
        var valid = $scope.validate();
        if(valid)
        {
            //var text = { "CompanyName": $scope.Cname, "OwnerName": $scope.Oname, "Resources": $scope.resources, "Address": $scope.address, "Contact": $scope.contact, "Email": $scope.email };
            var text = { "CompanyName": $scope.Cname};
            $http.post(serviceBase + 'api/manage/newCompany', JSON.stringify(text)).then(function (results) {
                return results;
            })
        }
    }

    $scope.validate = function () {
        var isValid = true;
        if ($scope.Cname == null && $scope.Cname == "")
        {
            alert("Enter Company name");
            isValid = false;
        }
        //if ($scope.Oname == null && $scope.Oname == "")
        //{
        //    alert("Enter Owner's name");
        //    isValid = false;
        //}
        //if ($scope.resources == null && $scope.resources == "")
        //{
        //    alert("Enter no. of resources");
        //    isValid = false;
        //}
        //if ($scope.address == null && $scope.address == "")
        //{
        //    alert("Enter address");
        //    isValid = false;
        //}
        //if ($scope.contact == null || $scope.contact == "")
        //{
        //    alert("please enter the contact");
        //    isValid = false;
        //}

        ////var p =  /^[0-9]*$/
        //if (!/^[0-9]*$/.test($scope.contact) && ($scope.contact != null || $scope.contact != ""))
        //{
        //    alert("enter valid phone no.");
        //    isValid = false;
        //}
        //if ($scope.email == null || $scope.email == "")
        //{
        //    alert("please enter the email");
        //    isValid = false;
        //}
        ////var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
        //if (!/^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test($scope.email) && ($scope.email != null || $scope.email != ""))
        //{
        //    alert("Enter valid email");
        //    isValid = false;
        //}

        //var text = { "CompanyName":$scope.Cname, "OwnerName": $scope.Oname, "Resources": $scope.resources, "Address": $scope.address, "Contact": $scope.contact, "Email": $scope.email };
        //text = new XMLHttpRequest();
        //text.send();
        return isValid;
    };
     
}]);