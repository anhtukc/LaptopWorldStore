app.controller('receiptbill', ['$scope', '$http', function ($scope, $http) {
    $scope.employee_id = '';
    $scope.provider_id = '';
    $scope.total_paid = 0;
    $scope.product_id = '';
    $scope.product_name = '';
    $scope.quantity = 0;
    $scope.price = 0;
    $scope.grand_paid = 0;
    $scope.Details = [];

    $scope.GetProvider = function () {
        $http({
            method: 'Get',
            url:'/receiptbills/GetProviders',
            datatype: 'Json'
        }).then(function (res) {
            $scope.provider = res.data;
            console.log($scope.provider);
            $scope.provider_id = $scope.provider[0].provider_id;
        })
    }

    $scope.GetEmployee = function () {
        $http({
            method: 'Get',
            url: '/receiptbills/GetEmployees',
            datatype: 'Json'
        }).then(function (res) {
            $scope.employee = res.data;
            console.log($scope.employee);
            $scope.employee_id = $scope.employee[0].employee_id;

        })
    }

    $scope.GetDetail = function (id) {
        $http({
            method: 'Get',
            url: '/receiptbills/GetDetails',
            datatype: 'Json',
            params: {id:id}
        }).then(function (res) {
            $scope.Details = res.data;
            $scope.CaculateTotal();
        })
    }
    $scope.CaculateTotal = function () {
        $scope.total_paid = 0
        $.each($scope.Details, function (index, item) {
 
                $scope.total_paid += item.grand_paid;
            
        })
    }

    $scope.NewBill = function () {
   
        $scope.product_id = '';
        $scope.product_name = '';
        $scope.quantity = 0;
        $scope.price = 0;
        $scope.grand_paid = 0;
       
    }
    $scope.AddBillDetails = function () {
        let check = true;
        $.each($scope.Details, function (index, item) {
            if (item.product_id == $scope.product_id) {
                item.quantity += $scope.quantity;
                item.product_name = $scope.product_name;
                item.price = $scope.price;
                item.grand_paid = item.quantity * item.price;
                check = false;
            }
        })

        if (check == true) {
            $scope.Details.push({
                'product_id': $scope.product_id,
                'product_name': $scope.product_name,
                'quantity': $scope.quantity,
                'price': $scope.price,
                'grand_paid': $scope.price * $scope.quantity
            });
            
        }
        $scope.CaculateTotal();
    }

    $scope.EditBillDetails = function (id) {
        $.each($scope.Details, function (index, item) {
            if (item.product_id == id) {
                $scope.product_id = item.product_id;
                $scope.product_name = item.product_name;
                $scope.quantity = item.quantity;
                $scope.price = item.price;
            }
        })
    }
    $scope.DeleteBillDetail = function (id) {
        $.each($scope.Details, function (index, item) {
            if (item.product_id == id) {
                $scope.Details.splice(index, 1);
            }
        })

        $scope.CaculateTotal();
    }

    $scope.CreateBill = function () {
        $scope.receiptbill = {
            'receiptbill_id': '',
            'employee_id': $scope.employee_id,
            'provider_id': $scope.provider_id,
            'total_paid': $scope.total_paid,
            'date_created': '',
            'flag': true
        };
        if ($scope.Details != null) {
            $http({
                method: 'Post',
                datatype: 'Json',
                url: '/receiptbills/CreateBill',
                data: {
                    'receiptbill': $scope.receiptbill,
                    'list': $scope.Details
                }
            }).then(function (res) {
                alert(res.data);
                $scope.Details = [];
                $scope.total_paid = 0;
            })
        }
    }

    $scope.DeleteBill = function (id) {
        var confirme = window.confirm('Bạn có chắc chắn xóa?');
        if (confirme) {
            $http({
                method: 'Post',
                url: 'receiptbills/Deletebill',
                params: { id: id }
            }).then(function (res) {
                alert(res.data);
                location.reload();
            })
        }
    }

    $scope.EditBill = function (id, total) {
        var request = {
            'receiptbill_id': id,
            'list': $scope.Details
        };
        $http({
            method: 'Post',
            url: '/receiptbills/EditBill',
            datatype: 'Json',
            data: JSON.stringify(request)
        }).then(function (res) {
            alert(res.data);
            location.reload();
        })
    }
    $scope.GetEmployee()
    $scope.GetProvider();
}])