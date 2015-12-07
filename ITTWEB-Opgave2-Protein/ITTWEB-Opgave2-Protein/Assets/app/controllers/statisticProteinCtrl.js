angular.module("statisticProtein", [])
    .controller("statisticProteinCtrl", [
        "$scope", "$http", function($scope, $http) {

            $scope.getStatistic = function (Time) {
                $http.get("/api/WsStatistic/GetStatistic", Time) //send start dato med
                    .success(function (data, status, headers, config) {
                        $scope.rawData = calcProtein(data);
                        //Do stuff with it 
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
            };
        }
    ]);