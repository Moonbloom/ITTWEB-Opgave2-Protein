﻿angular.module("statisticProtein", [])
    .controller("statisticProteinCtrl", [
        "$scope", "$http", function ($scope, $http) {

            $scope.getStatistic = function (selectedTime) {
                if (!selectedTime) {
                    $scope.rawData = null;
                    $scope.average = null;
                    return;
                }
                $http.get('/api/WsStatistic/GetStatistic', { params: { time: selectedTime } })
                .success(function (data, status) {
                    $scope.rawData = calcProtein(data);
                    for (var i = 0, len = $scope.rawData.length; i < len; i++) {
                        calcDailySummary($scope.rawData[i]);
                    }
                    updateTotal();
                });
            };

            var calcProtein = function (data) {
                for (var i = 0, len = data.length; i < len; i++) {
                    for (var j = 0, len2 = data[i].length; j < len2; j++) {
                        data[i][j].Protein = ((data[i][j].Amount) * (data[i][j].FoodPosibility.ProteinRatio)).toFixed(2);
                    }
                }
                return data;
            };

            //data is an array of intakes with protein calculated
            var calcDailySummary = function (data) {
                data.summary = {};
                data.summary.date = (data[0].Date).slice(0, 10);
                data.summary.total = calcDailyTotal(data);
                data.summary.daily = calcDailyDaily();
                data.summary.percentage = calcDailyPercentage(data);
                data.summary.left = calcDailyLeft(data);
            };

            var calcDailyTotal = function (data) {
                var totalProtein = 0;
                for (var i = 0; i < data.length; i++) {
                    if (data[i].Protein) {
                        totalProtein += parseFloat(data[i].Protein);
                    }
                }
                return totalProtein.toFixed(2);
            };

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
            };

            var calcDailyPercentage = function (data) {
                var total = parseFloat(data.summary.total);
                var daily = parseFloat(data.summary.daily);
                if (total === 0 || daily === 0) {
                    return 0;
                }

                return ((total / daily) * 100).toFixed(2);
            };

            var calcDailyLeft = function (data) {
                var total = data.summary.total;
                var daily = data.summary.daily;
                return (daily - total).toFixed(2);
            };

            var getUserPref = function () {
                $http.get("/api/WsAccount/GetUserPreferences")
                    .success(function (data, status, headers, config) {
                        $scope.userData = data;
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };

            var updateTotal = function (data) {
                $scope.average = {};
                var total = 0;
                var daily = 0;
                var left = 0;
                for (var i = 0, len = $scope.rawData.length; i < len; i++) {
                    total += parseFloat($scope.rawData[i].summary.total);
                    daily += parseFloat($scope.rawData[i].summary.daily);
                    left += parseFloat($scope.rawData[i].summary.left);
                }

                $scope.average.total = total.toFixed(2);
                $scope.average.daily = daily.toFixed(2);
                $scope.average.left = left.toFixed(2);

                if (total === 0 || daily === 0) {
                    $scope.average.percentage = 0;
                } else {
                    $scope.average.percentage = ((total / daily) * 100).toFixed(2);
                }
            };

            getUserPref();
        }
    ]);