app.controller('admin-ssd', ['$scope', '$http', 'Upload', function ($scope, $http, Upload) {
    $scope.product_id = '';
    $scope.product_name = '';
    $scope.price = 0;
    $scope.category_id = 'ssd';
    $scope.provider_id = 'asus';
    $scope.picture = '';
    $scope.quantity = 0;
    $scope.decription = '';
    $scope.flag = true;
    $scope.product = {

        'product_id': '',
        'category_id': 'ssd',
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
    $scope.ssd = {
        'write_speed': $('input#write_speed').val(),
        'read_speed': $('input#read_speed').val(),
        'size': $('input#size').val(),
        'connector': $('input#connector').val()

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
                    link: '/ssd/'
                }
            }).then(function (res) {
                $scope.product.picture = res.data;
                console.log($scope.product.picture)
            })
        };
    }



    $scope.Create = function () {

        var request = {
            'pd': $scope.product,
            's': $scope.ssd
        };
        $http({
            method: 'Post',
            datatype: 'Json',
            url: '/Admin/ssds/Createssd',
            data: JSON.stringify(request)
        }).then(function (res) {
            alert(res.data);
        });

    };
    $scope.Edit = function () {
        var request = {
            'pd': $scope.product,
            's': $scope.ssd
        };
        $http({
            method: 'Post',
            datatype: 'Json',
            url: '/Admin/ssds/Editssd',
            data: request
        }).then(function (res) {
            alert(res.data);
            location.reload();
        });

    };



}])