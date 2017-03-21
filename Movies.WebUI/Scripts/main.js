var app = angular.module("myMovies", ['ngSanitize']);
app.directive('onErrorSrc', function () {
    return {
        link: function (scope, element, attrs) {
            element.bind('error', function () {
                if (attrs.src !== attrs.onErrorSrc) {
                    attrs.$set('src', attrs.onErrorSrc);
                }
            });
        }
    }
});

app.controller("MovieController", ['$scope', '$http', '$timeout', '$compile', function ($scope, $http, $timeout, $compile) {
    $scope.showSubscribeSuccess = false;
    $scope.showSubscribeFail = false;
    $scope.canSearch = true;
    $scope.keywords = "";
    $scope.cartBadge = 0;
    $scope.email = "";
    $scope.genre = {
        current: 'all',
        items: ['all', 'action', 'adventure', 'fantasy']
    };

    $scope.country = {
        current: 'all',
        items: ['all', 'USA']
    };

    $scope.year = {
        current: 'all',
        items: ['all', '2017-2010', '2009-2000', "90's", "80's", 'others']
    };

    $scope.price = {
        current: 'all',
        items: ['all', '0-50', '50-100', '100-500', '500-']
    };

    $scope.filterMovie = function (t, n) {
        var category = $scope.getCategoryIndex(t);
        var type = 0;
        switch (category) {
            case 0: //genre
                $scope.genre.current = n;
                type = $scope.getGenreIndex(n);
                break;
            case 1: //country
                $scope.country.current = n;
                type = $scope.getCountryIndex(n);
                break;
            case 2: //year
                $scope.year.current = n;
                type = $scope.getYearIndex(n);
                break;
            case 3: //price
                $scope.price.current = n;
                type = $scope.getPriceIndex(n);
                break;
        }

        $http.post("/api/filtermovie", { category: category, type: type }).then(function (response) {
            $scope.movies = response.data;

            $scope.loadPageInfo($scope.movies.Pages);
        });
    };

    $scope.loadPageInfo = function (pageInfo) {
        $http.post("/paginate", { total: pageInfo.TotalItems, itemPerPage: pageInfo.ItemPerPage, page: pageInfo.CurrentPage }).then(function (response) {
            $scope.paginateInfo = response.data;

            var html = $compile($scope.paginateInfo)($scope);

            angular.element(document.getElementById('movie_page_info')).html(html);
        });
    }

    $scope.query = function (e) {
        e.preventDefault();
        
        $scope.genre.current = 'all';
        $scope.country.current = 'all';
        $scope.year.current = 'all';
        $scope.price.current = 'all';

        $http.post("/api/query", {keywords:$scope.keywords}).then(function(response){
            $scope.movies = response.data;

            $scope.loadPageInfo($scope.movies.Pages);
        });
    };

    $scope.subscribe = function (e) {
        e.preventDefault();
        $scope.email = "";
        $scope.showSubscribeSuccess = true;
    };

    $scope.paginateMovie = function (page) {
        $http.post("/api/paginate", { page: page }).then(function (response) {
            $scope.movies = response.data;

            $scope.loadPageInfo($scope.movies.Pages);
        });
    };

    $scope.getCategoryIndex = function (g) {
        switch (g) {
            case "genre":
                return 0;
            case "country":
                return 1;
            case "year":
                return 2;
            default:
                return 3;
        }
    };

    $scope.getGenreIndex = function (g) {
        switch (g) {
            case 'action':
                return 1;
            case 'adventure':
                return 2;
            case 'fantasy':
                return 3;
            default:
                return 0;
        }
    };

    $scope.getCountryIndex = function (g) {
        switch (g) {
            case 'USA':
                return 1;
            default:
                return 0;
        }
    };

    $scope.getYearIndex = function (g) {
        switch (g) {
            case '2017-2010':
                return 1;
            case '2009-2000':
                return 2;
            case "90's":
                return 3;
            case "80's":
                return 4;
            case 'others':
                return 5;
            default:
                return 0;
        }
    };

    $scope.getPriceIndex = function (g) {
        switch (g) {
            case '0-50':
                return 1;
            case '50-100':
                return 2;
            case '100-500':
                return 3;
            case '500-':
                return 4;
            default:
                return 0;
        }
    };

    $scope.updateBadge = function () {
        $http.get("/cart/badge").then(function (response) {
            $scope.cartBadge = response.data.badge;
        });
    }

    $scope.loadMovies = function () {
        $scope.loading = true;
        $http.get("/api").then(function (response) {
            if (response.data.Movies.length > 0) {
                $scope.loading = false;
                $scope.movies = response.data;
                $scope.loadPageInfo($scope.movies.Pages);
            }
            else
            {
                $timeout($scope.loadMovies, 5000);
            }
        });
    };

    $scope.loadMovies();

    $scope.update = function () {
        $http.get("/api/updatesign").then(function (response) {
            if (response.data.count > 0) {
                $scope.loadMovies();

                $timeout($scope.update, 10000);
            }
            else {
                $scope.loadMovies();
            }
        });
    };

    $timeout($scope.update, 10000);

    $scope.$on("hideSearch", function (e, data) {
        $scope.canSearch = false;
    });

    $scope.updateBadge();
}]);

app.controller("DetailController", ['$scope', function ($scope) {
    $scope.$emit("hideSearch", "hide");
}]);

app.controller("CartController", ['$scope', function ($scope) {
    $scope.$emit("hideSearch", "hide");
}]);

app.controller("CheckoutController", ['$scope', function ($scope) {
    $scope.$emit("hideSearch", "hide");
}]);

app.controller("ShippingController", ['$scope', function ($scope) {
    $scope.$emit("hideSearch", "hide");
}]);
