
app.controller('navController', ['$scope', '$route', 'localStorageService', function ($scope, $route, localStorageService) {
    
    $scope.$on('LogIn', function (event, status) {
        
        $scope.showNav = status;
    });
    
}]);