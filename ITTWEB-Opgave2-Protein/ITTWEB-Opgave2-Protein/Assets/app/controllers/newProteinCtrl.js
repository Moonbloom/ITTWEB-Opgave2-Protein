angular.module("newProtein", [])
    .controller("newProteinCtrl", [
        "$scope", "$http", function($scope, $http) {

            $scope.getList = function() {
                $http.get("/api/WsProtein/GetFoodIntakes")
                    .success(function (data, status, headers, config) {
                        console.log("SUCCESS - " + data);
                        $scope.foodIntakeData = data;
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };

            //Get the current user's list when the page loads.
            $scope.getList();
        }
    ]);