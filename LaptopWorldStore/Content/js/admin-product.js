app.controller('adminproduct', ['$scope', '$http', '$location', '$window', function ($scope, $http, $location, $window) {
    $scope.product = {
        'product_id': '',
        'product_name': '',
        'picture': '',
        'category_id': 'laptop',
        'provider_id': '',
        'quantity': 0,
        'price': 0,
        'decription': '',
        'flag': true
    }
    let url = '/Admin/';
    $scope.RedicrecToCreatePage = function () {
        url += $scope.product.category_id + 's/Create';
        $window.location.href = url;
    }

    $scope.DeleteProduct = function(id){
        var mes = 'Bạn có muốn xóa sản phẩm?';
        var confirm = window.confirm(mes);
        if (confirm) {
            $http({
                method: 'Post',
                url: '/Admin/products/delete',
                params: {id: id}
            }).then(function (res) {
                if (res.data == 'hideproduct') {
                    alert('Đã ẩn sản phẩm');
                }
                if (res.data == 'deletesuccessful') {
                    alert('Đã xóa sản phẩm');
                    location.reload();
                }
            })
        }
    }
}])