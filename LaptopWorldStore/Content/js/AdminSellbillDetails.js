app.controller('SellBills', function ($scope, $http) {
    $scope.sellbill_id = '';
    $scope.product_id = '';
    $scope.product_name = '';
    $scope.price = 0;
    $scope.grand_paid = 0;
    $scope.GetDetail = function (bill_id) {
        var url = "/Sellbills/GetDetails/";
        $http({
            method: "Get",
            type: "Json",
            url: url,
            data: bill_id
        }).then(function (res) {
            $scope.Details = res.data;
            console.log(res.data);
        })
    };
})