'use strict';
app.controller('indexController', ['$rootScope', '$scope', function ($rootScope, $scope) {
   
    var $window;
    return $window = $(window),
    $scope.main = { brand: "Rainbow", name: "Sajal Sood" },
        $scope.pageTransitionOpts = [{ name: "Scale up", "class": "ainmate-scale-up" }, { name: "Fade up", "class": "animate-fade-up" }, { name: "Slide in from right", "class": "animate-slide-in-right" }, { name: "Flip Y", "class": "animate-flip-y" }],
        $scope.admin = { layout: "wide", menu: "vertical", fixedHeader: !0, fixedSidebar: !1, pageTransition: $scope.pageTransitionOpts[0] },
        $scope.$watch("admin", function (newVal, oldVal) {
            return "horizontal" === newVal.menu && "vertical" === oldVal.menu ? void $rootScope.$broadcast("nav:reset") : newVal.fixedHeader === !1 && newVal.fixedSidebar === !0 ? (oldVal.fixedHeader === !1 && oldVal.fixedSidebar === !1 && ($scope.admin.fixedHeader = !0, $scope.admin.fixedSidebar = !0), void (oldVal.fixedHeader === !0 && oldVal.fixedSidebar === !0 && ($scope.admin.fixedHeader = !1,
                $scope.admin.fixedSidebar = !1))) : (newVal.fixedSidebar === !0 && ($scope.admin.fixedHeader = !0), void (newVal.fixedHeader === !1 && ($scope.admin.fixedSidebar = 1)))
        }, !0),
        $scope.color = { primary: "#248AAF", success: "#3CBC8D", info: "#29B7D3", infoAlt: "#666699", warning: "#FAC552", danger: "#E9422E" }
}]);