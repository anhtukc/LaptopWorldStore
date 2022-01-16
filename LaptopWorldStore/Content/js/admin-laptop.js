app.controller('admin-laptop', ['$scope', '$http', 'Upload', function ($scope, $http, Upload) {
    $scope.product_id = '';
    $scope.product_name = '';
    $scope.price = 0;
    $scope.category_id = 'laptop';
    $scope.provider_id = 'asus';
    $scope.picture = 'a';
    $scope.quantity = 0;
    $scope.decription = '';
    $scope.flag = true;
    $scope.product = {

        'product_id': '',
        'category_id': 'laptop',
        'provider_id': $('select#provider_id').val(),
        'product_name': $('input#product_name').val(),
        'picture': '',
        'price': parseInt($('input#price').val()),
        'quantity': parseInt($('input#quantity').val()),
        'decription': $('input#decription').val(),
        'flag': true

    };
    $scope.GetInfo = function (id, url) {
        $scope.product.product_id = id;
        $scope.product.picture = url;
    }
    $scope.laptop = {
        'monitor': $('input#monitor').val(),
        'cpu': $('input#cpu').val(),
        'gpu': $('input#gpu').val(),
        'ssd': $('input#ssd').val(),
        'ram': $('input#ram').val(),
        'os': $('input#ssd').val(),
        'weight': $('input#weight').val(),
        'battery': $('input#battery').val(),
    }
    console.log($scope.product.provider_id)
    $scope.uploadFiles = function (file) {
        $scope.image = file;

        if (file != null) {
            Upload.upload({
                method: 'Post',
                url: '/Admin/products/Upload',
                data: {
                    picture: file,
                    link: '/laptop/'
                }
            }).then(function (res) {
                $scope.product.picture = res.data;
                console.log($scope.product.picture)
            })
        };
    }



    $scope.CreateLaptop = function () {
        
        var request = {
            'pd': $scope.product,
            'lt': $scope.laptop
        };
        $http({
            method: 'Post',
            datatype: 'Json',
            url: '/Admin/laptops/CreateLaptop',
            data: JSON.stringify(request)
        }).then(function (res) {
            alert(res.data);
        });

    };
    $scope.Edit = function (id) {
        var request = {
            'pd': $scope.product,
            'lt': $scope.laptop
        };
        $http({
            method: 'Post',
            datatype: 'Json',
            url: '/Admin/laptops/EditLaptop',
            data: request
        }).then(function (res) {
            alert(res.data);
            location.reload();
        });

    };
   
    $scope.GetLaptop = function (id) {
        $http({
            method: 'Post',
            datatype: 'Json',
            url: '/Admin/laptops/GetLaptop',
            params: {'id':id}
        }).then(function (res) {
            $scope.product = res.Product;
            $scope.laptop = res.Laptop;
        });
    }

}])