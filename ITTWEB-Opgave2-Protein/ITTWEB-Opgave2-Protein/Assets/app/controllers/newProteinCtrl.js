angular.module("newProtein", [])
    .controller("newProteinCtrl", [
        "$scope", "$http", function ($scope, $http) {

            var getList = function () {
                $http.get("/api/WsProtein/GetFoodIntakes")
                    .success(function (data, status, headers, config) {
                        $scope.foodIntakeData = calcProtein(data);
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };

            var calcProtein = function(data) {
                for (var i = 0, len = data.length; i < len; i++) {
                    data[i].Protein = (data[i].Amount) * (data[i].FoodPosibility.ProteinRatio);
                }
                return data;
            }
            var getFoodPosibilities = function () {
                $http.get("/api/WsProtein/GetFoodPosibilities")
                    .success(function (data, status, headers, config) {
                        $scope.foodPosibilities = data;
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };
            
            $scope.deleteRow = function (index) {
                var deleteId = $scope.foodIntakeData[index].Id;
                console.log(deleteId);
                $http.post("/api/WsProtein/DeleteFoodIntake", deleteId)
                    .success(function(data, status, headers, config) {
                        $scope.getList();
                    });
            };

            $scope.updateRow = function (index) {             
                var update = $scope.foodIntakeData[index];  
                console.log(update);
                $http.post("/api/WsProtein/PostFoodIntake", update)
                    .success(function (data, status, headers, config) {
                        $scope.getList();
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };

            //Get the current user's list when the page loads.
           getList();
        }
    ]);