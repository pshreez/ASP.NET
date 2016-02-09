

UpdateTaskController.$inject = ['$scope', '$http', '$uibModalInstance','$uibModal','items'];
function UpdateTaskController($scope, $http, $uibModalInstance,$uibModal, items) {
    console.log(items);
    $scope.Taskassign = {
        ID                : items.ID,
        TASK_ID           : items.TASK_ID,
        TASK_STATUS       : items.USER_TASK_STATUS,
        TASK_HOUR_CONSUMED: items.TASK_HOUR_CONSUMED
    }
   
    $scope.Submitted = false;
    $scope.IsFormValid = false;
    $scope.submitText = 'Update';
    $scope.isDisabled = false;
    $scope.$watch('UpdateTaskStatusForm.$valid', function (newVal) { $scope.IsFormValid = newVal; });
    $scope.UpdateTaskStatusFunc = function () {
        console.log(2);
        if ($scope.IsFormValid) {
            console.log($scope.Taskassign);
            $scope.Submitted = true;
            console.log($scope.Task);
            $http({
                method: 'POST',
                url: '/Task/UpdateTaskStatusAction',
                data: JSON.stringify($scope.Taskassign),
                headers: { 'content-type': 'application/json' }
            }).success(function (d) {
                if (d[0].Status == true) {
                  
                    $uibModalInstance.dismiss('cancel');
                  
                    $scope.data = {
                        boldTextTitle: " ",
                        textAlert: d[0].Message,
                        mode: 'success',

                    }
                    var modalInstance = $uibModal.open({
                        animation: $scope.animationsEnabled,
                        templateUrl: "/Task/DateError",
                        controller: 'ModalInstanceCtrl',
                        backdrop: 'static',
                        keyboard: false,
                        size: 'lg',
                        resolve: {
                            data: function () {
                                return $scope.data;
                            }
                        }
                    });
                }
                else {
                    $scope.submitText = "Not Saved";
                  //  $scope.alerts.push({ msg: d[0].Message, type: 'danger' });
                    $uibModalInstance.dismiss('cancel');

                    $scope.data = {
                        boldTextTitle: " ",
                        textAlert: d[0].Message,
                        mode: 'warning',

                    }
                    var modalInstance = $uibModal.open({
                        animation: $scope.animationsEnabled,
                        templateUrl: "/Task/DateError",
                        controller: 'ModalInstanceCtrl',
                        backdrop: 'static',
                        keyboard: false,
                        size: 'lg',
                        resolve: {
                            data: function () {
                                return $scope.data;
                            }
                        }
                    });
                }
            }).error(function (e) { console.log(d); });
        }

     

    }

    $scope.cancel = function () {
        $scope.isDisabled = true;
        $uibModalInstance.dismiss('cancel');
    };
}



UpdateHourTaskController.$inject = ['$scope', '$http', '$uibModalInstance', 'items'];
function UpdateHourTaskController($scope, $http, $uibModalInstance, items) {
    console.log(items);
    $scope.Task = {
        ID: items.ID,
        TASK_ID_ASSIGNED :items.TASK_ID_ASSIGNED,
        TASK_ESTIMATED_HOUR: ' '
    }
    
    $scope.Submitted = false;
    $scope.IsFormValid = false;
    $scope.submitText = 'Update';
    $scope.$watch('UpdateTaskHourForm.$valid', function (newVal) { $scope.IsFormValid = newVal;  });
    $scope.UpdateTaskHourFunc = function () {
        console.log(2);
        if ($scope.IsFormValid) {
            console.log(3);
            $scope.Submitted = true;
            console.log($scope.Task);
            $http({
                method: 'POST',
                url: '/Task/UpdateTaskHourAction',
                data: JSON.stringify($scope.Task),
                headers: { 'content-type': 'application/json' }
            }).then(function (response) {
                console.log(response);
                $scope.TaskHour = response.data[0].Message;                           
               $uibModalInstance.dismiss('cancel');     
               
            });
        }
      
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}
