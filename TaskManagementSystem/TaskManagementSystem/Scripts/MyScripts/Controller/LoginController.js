
LoginController.$inject = ['$scope', '$location', '$http','$rootScope', 'LoginFactory', 'LoginService','$window'];
function LoginController($scope, $location, $http, $rootScope, LoginFactory, LoginService, $window) {
    
    $scope.Iserror = false;
    $scope.IsSuccess = false;
    $scope.LoginErrorMessage = " ";
    $scope.LoginMessage = "Please  enter correct username and password";
    $scope.IsLoggedIn = false; 
   
    $scope.Submitted = false;
    $scope.IsFormValid = false;
    $scope.User = { U_LOGIN_NAME: '', U_PASSWORD: '' }
    $scope.$watch('LoginForm.$valid', function (newVal) { $scope.IsFormValid = newVal; });
    $scope.UserLoginFunc = UserLoginFunc;

    $scope.UserLogged = false;
    function UserLoginFunc() {
        $scope.Submitted = true;
        if ($scope.IsFormValid) {
           
            LoginFactory.UserLogin($scope.User).then(function (d) {            
                if (d.data[0].Status == true) {                   
                    $scope.LoginMessage = "You are logging in..";
                    $scope.Iserror = false;                   
                    $location.path('/user' );
                      
                } else {
                    $scope.IsSuccess = true;
                    $scope.LoginErrorMessage = "There was a problem logging in, use proper email and password ";
                    $scope.Iserror = true;
                }                                                                        
            });                                   
        }
    }
}

