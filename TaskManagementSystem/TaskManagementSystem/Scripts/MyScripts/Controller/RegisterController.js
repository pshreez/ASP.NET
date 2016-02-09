
RegisterController.$inject = ['$q', '$http', '$scope', 'RegisterFactory', '$uibModal','$window'];
function RegisterController($q, $http, $scope, RegisterFactory, $uibModal,$window) {
    $scope.RegisterMessage = "This is the register page of the user ";
    $scope.submitText = "Save";

    $scope.Submitted = false;
    $scope.isFormValid = false;
    $scope.removeTagOnBackspace = function (event) {
        if (event.keyCode === 8) {

        }
    };

    $scope.RegisterMessage = '';
    $scope.RegisterErrorMessage = '';
    $scope.Iserror = false;
    $scope.IsSuccess = true;

    $scope.User = { U_FIRST_NAME: '', U_LAST_NAME: '', U_LOGIN_NAME: '', U_EMAIL: '', U_PASSWORD: '', U_POSITION: '', };
    $scope.$watch('RegisterForm.$valid', function (newValue) { $scope.isFormValid = newValue; console.log(123); });

    $scope.UserRegisterFunc = function () {                             //Save Data
        $scope.Submitted = true;
        if ($scope.submitText == 'Save') {

            if ($scope.isFormValid) {

                $scope.submitText = 'Please Wait...';
                RegisterFactory.SaveUserData($scope.User).then(function (d) {       // alert(d[0].Username);  ClearForm();
                    if (d[0].Status == true) {
                       /* $scope.RegisterMessage = d[0].Message;
                        ClearForm();                                                         ////clear form here
                        $scope.submitText = "Save";
                        $scope.RegisterMessage = '';*/
                        $scope.IsSuccess = true;
                        $scope.data = {
                            boldTextTitle: " ",
                            textAlert: d[0].Message,
                            mode: 'success'
                        }
                        var modalInstance = $uibModal.open({
                            animation: $scope.animationsEnabled,
                            templateUrl: "/User/RegisterPage",
                            controller: 'RegisterPageController',
                            backdrop: 'static',
                            keyboard: false,
                            size: 'lg',
                            resolve: {
                                data: function () {
                                    return $scope.data;
                                }

                            }
                        }); $window.location.href = '#/login';
                    } else {
                        ClearForm();
                        $scope.RegisterErrorMessage = d[0].Message;
                        $scope.Iserror = true;

                    }
                });
            } else { $scope.RegisterMessage = "This is the register page of the user "; }
        }
    }
    function ClearForm() {
        $scope.User = {};
        $scope.RegisterForm.$setPristine();
        $scope.submitted = false;
    }

}

