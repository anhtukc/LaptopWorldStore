app.controller('searchpd', ['$scope', '$http','$location', function ($scope, $http,$location) {
    $scope.searchstring = '';
    let url = '';
    $scope.Search = function () {
        url = '/searchPage/Index/?searchString=' + $scope.searchstring;

        window.location.assign(url);
    }
   
    $scope.GetProduct = function () {
        $http({
            method: 'Get',
            url: '/products/GetProduct',
            datatype: 'Json'
        }).then(function (res) {
            $scope.Details = res.data;
        });
    }
    $scope.GetProduct();
}])
