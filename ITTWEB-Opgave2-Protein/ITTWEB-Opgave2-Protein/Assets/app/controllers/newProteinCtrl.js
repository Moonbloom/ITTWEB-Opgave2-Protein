angular.module("newProtein", [])
    .controller("newProteinCtrl", [
        "$scope", "$http", function($scope, $http) {

            $scope.getList = function() {
                $http.get("/api/WsProtein/GetFoodIntakes")
                    .success(function (data, status, headers, config) {
                        console.log("SUCCESS - " + data);
                        $scope.foodIntakeData = calcProtein(data);
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };

            function calcProtein(data) {
                for (var i = 0, len = data.length; i < len; i++) {
                    data[i].Protein = (data[i].Amount) * (data[i].FoodPosibility.ProteinRatio);
                }
                console.log(data);
                return data;
            }
            $scope.deleteRow = function (index) {
                console.log(index);
                console.log($scope.foodIntakeData[index].Id);
            };

            //Get the current user's list when the page loads.
            $scope.getList();
        }
    ]);