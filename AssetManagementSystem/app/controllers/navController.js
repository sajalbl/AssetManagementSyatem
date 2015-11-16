
app.controller('navController', ['$scope', '$route', 'localStorageService', function ($scope, $route, localStorageService) {
    
    $scope.$on('LogIn', function (event, status) {
        
        $scope.showNav = status;
    });
    
    $scope.$on('Success', function (event, status) {
        $scope.showNavigation = status;
    });

    //$scope.logout = function () {
    //    localStorageService.clear();
    //    $route.reload();
    //};


}]);