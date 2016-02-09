ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'data'];
function ModalInstanceCtrl($scope, $uibModalInstance, data) {
  //  MyApp.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance, data) {
        $scope.data = data;
        $scope.close = function (/*result*/) {
            $uibModalInstance.close($scope.data);
        }

    }