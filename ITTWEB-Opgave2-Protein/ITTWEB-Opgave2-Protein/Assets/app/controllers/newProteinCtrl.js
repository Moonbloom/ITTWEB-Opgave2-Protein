angular.module("newProtein", [])
    .controller("newProteinCtrl", [
        "$scope", "$http", function ($scope, $http) {

            var calcProtein = function (data) {
                for (var i = 0, len = data.length; i < len; i++) {
                    calcEachProtein(data[i]);
                }

                return data;
            }

            var getList = function () {
                $http.get("/api/WsProtein/GetFoodIntakes")
                    .success(function (data, status, headers, config) {
                        console.log(data);
                        $scope.foodIntakeData = calcProtein(data);
                        calcDailySummary();
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };

            var getUserPref = function () {
                $http.get("/api/WsAccount/GetUserPreferences")
                .success(function (data, status, headers, config) {
                    $scope.userData = data;
                    getList();
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

            var calcDailySummary = function () {
                $scope.dailySummary = {};
                $scope.dailySummary.total = calcDailyTotal();
                $scope.dailySummary.daily = calcDailyDaily();
                $scope.dailySummary.percentage = calcDailyPercentage();
                $scope.dailySummary.left = calcDailyLeft();
            }

            var calcDailyTotal = function () {
                var data = $scope.foodIntakeData;
                var totalProtein = 0;
                for (var i = 0; i < data.length; i++) {
                    if (data[i].Protein) {
                        totalProtein += parseFloat(data[i].Protein);
                    }
                }
                return totalProtein.toFixed(2);
            }

            var calcDailyDaily = function () {
                var user = $scope.userData;
                switch (user.UserType.Id) {
                    case 1:
                        return parseFloat(user.Weight) * 0.8;
                    case 2:
                        return parseFloat(user.Weight) * 1.5;
                    case 3:
                        return parseFloat(user.Weight) * 2.0;
                    default:
                        return 0;
                }
            }

            var calcDailyPercentage = function () {
                var total = parseFloat($scope.dailySummary.total);
                var daily = parseFloat($scope.dailySummary.daily);
                if (total === 0 || daily === 0) {
                    return 0;
                }

                return ((total / daily) * 100).toFixed(2);
            }

            var calcDailyLeft = function () {
                var total = $scope.dailySummary.total;
                var daily = $scope.dailySummary.daily;
                return (daily - total).toFixed(2);
            }

            var calcEachProtein = function (data) {
                if (data.Amount && data.FoodPosibility) {
                    return data.Protein = ((data.Amount) * (data.FoodPosibility.ProteinRatio)).toFixed(2);
                } else {
                    return data.Protein = "";
                }
            }

            $scope.deleteRow = function (index) {
                var deleteId = $scope.foodIntakeData[index].Id;
                $http.post("/api/WsProtein/DeleteFoodIntake", deleteId)
                    .success(function (data, status, headers, config) {
                        getList();
                    });
            };

            $scope.updateFoodIntake = function (index) {
                var update = $scope.foodIntakeData[index];
                $http.post("/api/WsProtein/UpdateFoodIntake", update)
                    .success(function (data, status, headers, config) {
                        getList();
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };

            $scope.postFoodIntake = function () {
                var post = $scope.newFoodIntake;
                post.FoodPosibilityId = post.FoodPosibility.Id;
                $http.post("/api/WsProtein/PostFoodIntake", post)
                    .success(function (data, status, headers, config) {
                        $scope.newFoodIntake = null;
                        getList();
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };

            $scope.calcEachProteinTable = function (data) {
                calcEachProtein(data);
                calcDailySummary();
            }

            getFoodPosibilities();
            getUserPref();
        }
    ]);