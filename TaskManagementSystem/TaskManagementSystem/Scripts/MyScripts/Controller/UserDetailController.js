
UserDetailController.$inject = ['$scope', '$location', '$stateParams', 'DataStored'];
function UserDetailController($scope, $location, $stateParams, DataStored) {
        var id = $scope.uid = $stateParams.uid; console.log(id);
        DataStored.UserDetail(id).then(function (response) {
            console.log(response[0].U_EMAIL);
            $scope.User = {
                U_EMAIL: response[0].U_EMAIL,
                U_FIRST_NAME: response[0].U_FIRST_NAME,
                U_ID: response[0].U_ID,
                U_LAST_NAME: response[0].U_LAST_NAME,
                U_LOGIN_NAME: response[0].U_LOGIN_NAME,
                U_PASSWORD: response[0].U_PASSWORD,
                U_POSITION: response[0].U_POSITION,
            }
        });
    }
