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

            function calcProtein(data) {
                for (var i = 0, len = data.length; i < len; i++) {
                    data[i].Protein = (data[i].Amount) * (data[i].FoodPosibility.ProteinRatio);
                }
                return data;
            }

            $scope.deleteRow = function (index) {
                var deleteId = $scope.foodIntakeData[index].Id;
                console.log(deleteId);
                $http.post("/api/WsProtein/DeleteFoodIntake", deleteId)
                    .success(function(data, status, headers, config) {
                        $scope.getList();
                    });
            };

            //Get the current user's list when the page loads.
           getList();
        }
    ]);