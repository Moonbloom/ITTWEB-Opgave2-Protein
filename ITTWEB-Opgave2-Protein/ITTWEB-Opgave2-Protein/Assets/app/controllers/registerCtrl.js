angular.module("register", [])
    .controller("registerCtrl", [
        "$scope", "$http", function($scope, $http) {
            $scope.register = function() {
                var params = {
                    Email: $scope.username,
                    Password: $scope.password1,
                    ConfirmPassword: $scope.password2,
                    Weight: $scope.weight,
                    UserTypeId: $scope.selectedUserType.Id
                };

                console.log(params);

                $http.post("/api/Account/Register", params)
                    .success(function(data, status, headers, config) {
                        $scope.successMessage = "Registration Complete. You can now sign in with your new account.";
                        $scope.showErrorMessage = false;
                        $scope.showSuccessMessage = true;
                    })
                    .error(function(data, status, headers, config) {
                        if (angular.isArray(data))
                            $scope.errorMessages = data;
                        else
                            $scope.errorMessages = new Array(data.replace(/["']{1}/gi, ""));

                        $scope.showSuccessMessage = false;
                        $scope.showErrorMessage = true;
                    });
            };

            $http.get("/api/WsProtein/GetUserTypes")
                .success(function(data, status, headers, config) {
                    $scope.availableUserTypes = data;

                    $scope.selectedUserType = data[0];
                });

            $scope.showAlert = false;
            $scope.showSuccess = false;
        }
    ]);