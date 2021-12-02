

app.controller("products",  function ($scope, $http) {
    $scope.product_id = '';
    $scope.product_name = '';  
    $scope.price = 0;
    $scope.quatity = 0;
    $scope.grand_paid = 0;
    $scope.picture = "";
    $scope.totalprice = 0;
    $scope.user = {
        'name': "",
        'address': "",
        'shipping': "cod",
        'phonenumber':""
    };
   
    $scope.ListInBag = JSON.parse(localStorage.getItem("ProductInBag")) || [];

    $scope.TransferInputCheck = function (shipping) {
        if (shipping == 'transfer') {
            $('#Bank').css({ 'display': 'block' });
        }
        else {
            $('#Bank').css({ 'display': 'none' });

        }
    }
    $scope.CheckQuantity = function () {
        var quantitybuy = $scope.ListInBag.length;
        $("#quatityshoppingbag").text("");

        $("#quatityshoppingbag").append("<span>" + quantitybuy + "</span>");
    };
    $scope.DeleteItem = function (id) {
        if (confirm('Bạn muốn xóa sản phẩm khỏi giỏ hàng?')) {
            $.each($scope.ListInBag, function (i, item) {
                if (item.product_id == id) {
                    $scope.ListInBag.splice(i, 1);
                    $scope.CaculateTotalprice();
                    $scope.SetLocalStorage();
                    $scope.CheckQuantity();
                    $scope.CheckBag();
                    return false;
                };
            });
        } else {
            
        }
       
       
    };
    $scope.AddShoppingBag = function (product_id, product_name, price, picture) {
        var check = false;
        if ($scope.ListInBag.length > 0) {

            $.each($scope.ListInBag, function (i, item) {
                if ($scope.ListInBag[i].product_id == product_id) {
                    console.log($scope.ListInBag[i].product_id, product_id);
                    $scope.ListInBag[i].quantity += 1;
                    $scope.ListInBag[i].grand_paid = $scope.ListInBag[i].quantity * $scope.ListInBag[i].price;
                    check = true;
                    console.log($scope.ListInBag[i]);
                }
            });
        }
        if (check == false) {
            
            $scope.ListInBag.push({
                "product_id": product_id,
                "product_name": product_name,
                "price": price,
                "quantity": 1,
                "grand_paid": price,
                "picture":picture
            });
        }
        $scope.CheckQuantity();
        alert("Thêm sản phẩm thành công")
        $scope.SetLocalStorage();
    };

    $scope.CaculateTotalprice = function () {
        $scope.totalprice = 0;
        $.each($scope.ListInBag, function (i, item) {
            $scope.totalprice += item.price * item.quantity;
        });
    };


    $scope.ChangeQuantity = function (id,number) {
        $.each($scope.ListInBag, function (i, item) {
            if (item.product_id == id) {
                item.quantity += number;
                if (item.quantity <= 0) {
                    item.quantity = 1;
                }
                item.grand_paid = item.price * item.quantity;
                console.log(item);
            }
        });
        $scope.totalprice = 0;
        $scope.CaculateTotalprice();
        $scope.SetLocalStorage();
    }

    $scope.SetLocalStorage = function () {
        localStorage.setItem("ProductInBag", JSON.stringify($scope.ListInBag));
    };
    $scope.CheckBag = function () {
        if ($scope.ListInBag.length == 0) {
            $("#shoppingbag").css({ "display": "none"});
            $("#NoproductinBag").css({ "display": "block" });
        };
        if ($scope.ListInBag.length != 0) {
            $("#shoppingbag").css({ "display": "block" });
            $("#NoproductinBag").css({ "display": "none" });
        };
    };

    $scope.CheckCustomer = function () {
        var mes = '';
        var checked = true;
        if ($scope.user.name == '') {
            mes += 'Vui lòng nhập họ tên \n';
            checked = false;
        }
        if ($scope.user.phonenumber == '') {
            mes += 'Vui lòng nhập số điện thoại \n';
            checked = false;
        }
        if ($scope.user.address == '') {
            mes += 'Vui lòng nhập địa chỉ \n';
            checked = false;
        }
        if (mes != '') {
            alert(mes);
        }
        return checked;
    }
    $scope.OrderBuy = function () {
        let list = [];
        $.each($scope.ListInBag, function (i, item) {
            list.push({
                "sellbilldetail_id": "",
                "sellbill_id": "",
                "product_id": item.product_id,
                "product_name": item.product_name,
                "price": item.price,
                "quantity": 1,
                "grand_paid": item.grand_paid,
                "flag": true
            })
        })
        var customer = {
            'customer_name': $scope.user.name,
            'phonenumber': '0' + $scope.user.phonenumber,
            'address': $scope.user.address,
            'flag': true
        };
        var resquest = {
            'phonenumber': '0' + $scope.user.phonenumber,
            'shippingtype': $scope.user.shipping,
            'totalprice': $scope.totalprice,
            'list': list
        }
        var checked = $scope.CheckCustomer();
        if (checked == true) {
            $http({
                method: 'Post',
                type: 'Json',
                url: "/ShoppingBag/CheckCustomerInfo",
                data: JSON.stringify(customer)
            }).then(function () {
                $http({
                    method: "Post",
                    type: "Json",
                    url: "/ShoppingBag/Create",
                    data: JSON.stringify(resquest)
                }).then(function (res) {
                    alert(res.data);
                    if (res.data == "Đặt hàng thành công") {
                        $scope.ListInBag = null;
                        list = null;
                        SetLocalStorage();
                        $scope.CheckBag();
                        $scope.CheckQuantity();
                    }
                });
            });
        }

    };
    $scope.CheckBag();
    $scope.TransferInputCheck('cod');
  
});