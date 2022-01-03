app.controller('SellBills', function ($scope, $http) {
    $scope.sellbill_id = '';
    $scope.product_id = '';
    $scope.product_name = '';
    $scope.quantity = 1;
    $scope.price = 0;
    $scope.remaining_quantity = 0;
    $scope.grand_paid = 0;
    $scope.total_paid = 0;
    $scope.Details = [];
    $scope.user = {
        'name': "",
        'address': "",
        'shipping': "cod",
        'phonenumber': "",
        'flag': true
    };
    let CheckAddAble = true;
    let CheckCreateBillAble = true;
    const url = "/sellbills/";
    $scope.LogName = function(){
        console.log($scope.product_id);
        console.log($scope.quantity);
        console.log($scope.price);
        console.log($scope.product_name);
        $.each($scope.productList, function (index,item) {
            if (item.product_id == $scope.product_id) {
                $scope.product_name = item.product_name;
                $scope.remaining_quantity = item.quantity;
                $scope.price = item.price;
            }
        })
    }
    $scope.GetDetail = function (bill_id) {  
        $http({
            method: "Get",
            url: url+"/GetDetails/",
            params: { sellbill_id: bill_id}
            
        }).then(function (res) {
             
            $scope.Details = res.data;
            $scope.caculateTotalPaid();
           
        })
    };
    $scope.GetProduct = function () {
        $http({
            method: "Get",
            url: url +"GetAllProduct/"

        }).then(function (res) {
            console.log(res);
            $scope.productList = res.data;

        })
    }
    $scope.CheckEmptyProductId = function () {
        if ($scope.product_id == '') {
            alert('Không được bỏ trống mã sản phẩm');
            CheckAddAble = false;
        }      
    }

    $scope.CheckIdInsideList = function () {
        let check = false;
        $.each($scope.productList, function (index, item) {
            if (item.product_id == $scope.product_id) {
                check = true;
            }
        });

        if (check == false) {
            alert('Mã sản phẩm không hợp lệ. Vui lòng chọn lại');
            CheckAddAble = false;
        }
    }
    $scope.QuantityCantSmallThan1 = function () {
        if ($scope.quantity < 1 || $scope.remaining_quantity <1) {
            alert('Số lượng không được nhỏ 1 sản phẩm');
            CheckAddAble = false;
        }
        
    }

    $scope.AddBillDetails = function () {
        let checkDuplicate = false;
        $scope.CheckEmptyProductId();
        $scope.QuantityCantSmallThan1();
        $scope.CheckIdInsideList();
        if (CheckAddAble == true) {
            $.each($scope.Details, function (index, item) {
                if (item.product_id == $scope.product_id) {
                    item.quantity += $scope.quantity;
                    item.grand_paid = item.quantity * item.price;
                    checkDuplicate = true;
                }
            })

            $.each($scope.productList, function (index, item) {
                if (item.product_id == $scope.product_id) {
                    item.quantity -= $scope.quantity;
                    
                }
            })

            if (checkDuplicate == false) {
                $scope.Details.push({
                    "product_id": $scope.product_id,
                    "product_name": $scope.product_name,
                    "price": $scope.price,
                    "quantity": $scope.quantity,
                    "grand_paid": $scope.price * $scope.quantity,
                });
            }
            $scope.caculateTotalPaid();
        }
        CheckAddAble = true;

        console.log($scope.Details);
    }

    $scope.caculateTotalPaid = function () {
        $scope.total_paid = 0;
        $.each($scope.Details, function (index, item) {
            $scope.total_paid += item.grand_paid;
        })
    }

    $scope.DeleteDetail = function (id) {
        $.each($scope.Details, function (index, item) {
            if (item.product_id == id) {
                $scope.Details.splice(index, 1);
            }
        })
        $scope.caculateTotalPaid();
    }

    $scope.NewBill = function () {
        $scope.sellbill_id = '';
        $scope.product_id = '';
        $scope.product_name = '';
        $scope.quantity = 1;
        $scope.price = 0;
        $scope.remaining_quantity = 0;
        $scope.grand_paid = 0;
        $scope.total_paid = 0;
        $scope.Details = [];
        $scope.user = {
            'name': "",
            'address': "",
            'shipping': "cod",
            'phonenumber': "",
            'flag': true
        };
    }

    $scope.CheckEmptyDetailList = function () {
        if ($scope.Details == null) {
            alert('Vui lòng chọn sản phẩm cho hóa đơn');
            CheckCreateBillAble = false;
        }
    }

    $scope.CheckEmptyCustomer = function () {
        let mes = '';
        if ($scope.user.name == '') {
            mes += 'Vui lòng nhập họ tên \n';
            CheckCreateBillAble = false;
        }
        if ($scope.user.phonenumber == '') {
            mes += 'Vui lòng nhập số điện thoại \n';
            CheckCreateBillAble = false;
        }
        if ($scope.user.address == '') {
            mes += 'Vui lòng nhập địa chỉ \n';
            CheckCreateBillAble = false;
        }
        if (mes != '') {
            alert(mes);
        }
    }
    $scope.CreateBill = function () {
        $scope.CheckEmptyCustomer();
        $scope.CheckEmptyDetailList();
        let resquest = {
            'phonenumber':  $scope.user.phonenumber,
            'shippingtype': $scope.user.shipping,
            'totalprice': $scope.total_paid,
            'list': $scope.Details
        };

        if (CheckCreateBillAble == true) {
            $http({
                method: 'Post',
                url: '/sellbills/CheckCustomerInfo',
                datatype: 'Json',
                data: JSON.stringify($scope.user)
            }).then($http({
                method: 'Post',
                url: '/sellbills/Create',
                datatype: 'Json',
                data: JSON.stringify(resquest)
            }).then(function (res) {
                if (res.data == 'CreateSuccessful') {
                    alert('Tạo hóa đơn thành công');
                }
            }))
        }
        CheckCreateBillAble = true;
    }

    $scope.DeleteBill = function (id) {
        let Confirme = window.confirm('Bạn có chắc chắn xóa hóa đơn?');
        if (Confirme) {
            $http({
                method: 'Post',
                url: 'sellbills/Delete',
                params: {id : id}
            }).then(function (res) {
                if (res.data == 'DeletedSuccessful') {
                    alert('Xóa thành công');
                    location.reload();
                }
            })
        }
    }

    $scope.ChangeQuantity = function (id, value) {
        console.log(id);
        $.each($scope.Details, function (index, item) {
            if (item.product_id == id) {
                item.quantity += value;
                item.grand_paid = item.quantity * item.price;
            }
            
        })
        $scope.caculateTotalPaid();
    }

    $scope.SaveChange = function () {
        $http({
            method: 'Post',
            url: '/sellbills/EditBills',
            data: {
                'sellbill_id': $scope.Details[0].sellbill_id,
                'total_paid': $scope.total_paid,
                'list': $scope.Details
            }
        }).then(function (res) {
            console.log(res.data);
        })
    }
    $scope.GetProduct();
})