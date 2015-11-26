
app.controller('navController', ['$scope', '$route', 'localStorageService', function ($scope, $route, localStorageService) {
    
    $scope.$on('LogIn', function (event, status) {
        
        if (status == 'company') {
            $scope.showNav = true;
            $scope.showNavigation = false;
        }
        else
        {
            $scope.showNavigation = true;
            $scope.showNav = false;
        }
       
    });
    
    //$scope.$on('Success', function (event, status) {
    //    $scope.showNavigation = status;
       
    //});

    //$scope.logout = function () {
    //    localStorageService.clear();
    //    $route.reload();
    //};

    $scope.logout = function () {
        localStorage.clear();
        location.reload();
    };


}]);