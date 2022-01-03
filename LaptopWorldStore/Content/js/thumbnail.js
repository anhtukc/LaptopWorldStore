app.controller("news", function ($scope, $http) {
    $scope.index = 0;
    let thumbnailslides, thumbnailstate, textthumbnail;

    $scope.autosilide = function () {
        let index = $scope.index;
        $scope.slides(index);

        setTimeout($scope.autosilide, 3000);
    };

    $scope.slides = function (indexOfInput) {
        for (let count = 0; count < thumbnailslides.length; count++) {
            thumbnailslides[count].style.display = `none`;
            thumbnailstate[count].classList.remove(`thumbnailactive`);
            textthumbnail[count].classList.remove(`textactive`);
            thumbnailslides[indexOfInput].style.display = `block`;
            thumbnailstate[indexOfInput].classList.add(`thumbnailactive`);
            textthumbnail[indexOfInput].classList.add(`textactive`);
        };
        console.log(indexOfInput);
        
        $scope.index = indexOfInput + 1;
        if ($scope.index > thumbnailslides.length - 1) { $scope.index = 0; }

    };
    var GetNew = function () {
        $http({
            method: "GET",
            url: "/_new/GetNewHomePage"
        }).then(function (res) {
            $scope.listdata = res.data;

        }), (function (error) {
            console.log(error);
        })
    };
    GetNew();
    angular.element(function () {
        thumbnailslides = document.getElementsByClassName(`thumbnailimageitem`);
        thumbnailstate = document.getElementsByClassName(`thumbnailstate`);
        textthumbnail = document.getElementsByClassName(`textthumbnail`);
        $scope.autosilide();
    });

})