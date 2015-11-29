angular.module("todoManager", [])
    .controller("todoManagerCtrl", [
        "$scope", "$http", function($scope, $http) {

            $scope.getList = function() {
                $http.get("/api/WsTodo/GetUserTodoItems")
                    .success(function(data, status, headers, config) {
                        $scope.todoList = data;
                    });
            };

            $scope.postItem = function() {
                var item = {
                    Task: $scope.newTaskText
                };

                if ($scope.newTaskText !== "") {
                    $http.post("/api/WsTodo/PostTodoItem", item)
                        .success(function(data, status, headers, config) {
                            $scope.newTaskText = "";
                            $scope.getList();
                        });
                }
            };

            $scope.complete = function(index) {
                $http.post("/api/WsTodo/CompleteTodoItem/" + $scope.todoList[index].Id)
                    .success(function(data, status, headers, config) {
                        $scope.getList();
                    });
            };

            $scope.delete = function (index) {
                $http.post("/api/WsTodo/DeleteTodoItem/" + $scope.todoList[index].Id)
                    .success(function(data, status, headers, config) {
                        $scope.getList();
                    });
            };

            //Get the current user's list when the page loads.
            $scope.getList();
        }
    ]);