angular.module("setupProtein", [])
    .controller("setupProteinCtrl", [
        "$scope", "$http", function ($scope, $http) {

            Array.prototype.containsById = function (obj) {
                var i = this.length;
                while (i--) {
                    if (this[i].Id == obj.Id) {
                        return true;
                    }
                }
                return false;
            }

            var getAll = function () {
                $http.get("/api/WsSetup/GetAllFoodPosibilities")
                    .success(function (data) {
                        var userChosen = $scope.userSelectedFoodPosibilities;
                        $scope.allFoodPosibilities = data.filter(function(element) {
                            return !userChosen.containsById(element);
                        });
                    })
                    .error(function (data) {
                        console.log(data);
                    });
            };

            var getChosenByUser = function () {
                $http.get("/api/WsSetup/GetFoodPosibilities")
                    .success(function (data) {
                        $scope.userSelectedFoodPosibilities = data;
                        getAll();
                    })
                    .error(function (data) {
                        console.log(data);
                    });
            };

            var getChosenFoodPosibilities = function () {
                var selector = $("#user-selected-food-posibilities-wrapper");
                var updatedData = [];
                selector.children().each(function (index, element) {
                    var id = $(element).children('.id')[0].innerHTML;
                    updatedData.push({
                        'Id': id
                    });
                });

                return updatedData;
            }

            $scope.updateFoodPosibilities = function () {
                var updatedData = getChosenFoodPosibilities();
                $http.post("/api/WsSetup/PostFoodPosibilities", updatedData)
                    .error(function(data) {
                        console.log(data);
                    });
            };

            $("#all-food-posibilities-wrapper, #user-selected-food-posibilities-wrapper").sortable({
                connectWith: ".connectedSortable"
            }).disableSelection();

            getChosenByUser();
        }
    ]);