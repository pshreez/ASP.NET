

HomeMenuController.$inject = ['$scope', '$rootScope', '$http', 'DataStored', 'LoginService', '$state'];
function HomeMenuController($scope, $rootScope, $http, DataStored, LoginService, $state) {
        $scope.IsLoggedIn = LoginService.getUserLoggedInStatus();//getUserInfo
        console.log($scope.IsLoggedIn);
        $scope.$watch('IsLoggedIn', function (newVal, oldVal) {
            $scope.IsLoggedIn = LoginService.getUserLoggedInStatus();
            if ($scope.IsLoggedIn) {
                //  console.log("hello" + LoginService.getUserInfo().UserName);
                $scope.userName = LoginService.getUserInfo().UserName;
            }
            // your code here
        }); // $rootScope.$broadcast('IsLogged', $scope.IsLogged);
        $scope.logOut = function () {
            LoginService.destroy();
            $state.go('login');
        }
    }