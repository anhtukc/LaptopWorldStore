

app.service('CrudService', function ($http) {

    this.getall = function (apiRoute) {
        var urlget = apiRoute;
        return $http.get(urlget);
    }

})