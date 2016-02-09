
UserHomeController.$inject = ['$scope', '$location', '$http','DataStored'];
function UserHomeController($scope, $location, $http, DataStored) {

    DataStored.UsersList().then(function (response) {
        console.log(response);
        $scope.Users = response;
    });
}