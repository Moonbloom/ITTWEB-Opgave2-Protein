angular.module("newProtein", [])
    .controller("newProteinCtrl", [
        "$scope", "$http", function ($scope, $http) {

            var calcProtein = function (data) {
                for (var i = 0, len = data.length; i < len; i++) {
                    $scope.calcEachProtein(data[i]);
                }
                return data;
            }

            var getList = function () {
                $http.get("/api/WsProtein/GetFoodIntakes")
                    .success(function (data, status, headers, config) {
                        $scope.foodIntakeData = calcProtein(data);
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };

            var getFoodPosibilities = function () {
                $http.get("/api/WsProtein/GetFoodPosibilities")
                    .success(function (data, status, headers, config) {
                        $scope.foodPosibilities = data;
                        $scope.selectedFoodPos = data[0];
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };
            
            $scope.deleteRow = function (index) {
                var deleteId = $scope.foodIntakeData[index].Id;
                $http.post("/api/WsProtein/DeleteFoodIntake", deleteId)
                    .success(function(data, status, headers, config) {
                        getList();
                    });
            };

            $scope.updateRow = function (index) {             
                var update = $scope.foodIntakeData[index];  
                $http.post("/api/WsProtein/PostFoodIntake", update)
                    .success(function (data, status, headers, config) {
                        getList();
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };

            
            $scope.calcEachProtein = function (data) {
                if (data.Amount && data.FoodPosibility) {
                    return data.Protein = (data.Amount) * (data.FoodPosibility.ProteinRatio);
                }
                else
                    return data.Protein = "-";
                
            }

            //Get the current user's list when the page loads.
            getList();
            getFoodPosibilities();
        }
    ]);