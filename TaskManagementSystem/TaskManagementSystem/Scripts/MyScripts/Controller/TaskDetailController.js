TaskDetailController.$inject = ['$scope', '$location', '$stateParams', 'DataStored'];
function TaskDetailController($scope, $location, $stateParams, DataStored) {
    var id = $scope.uid = $stateParams.uid; console.log(id);
    DataStored.TaskDetail(id).then(function (response) {
        console.log(response[0].PROJECT_PHASE);
        $scope.Task = {

            PROJECT_PHASE: response[0].PROJECT_PHASE,
            TASK_DESCRIPTION: response[0].TASK_DESCRIPTION,
            TASK_END_DATE: response[0].TASK_END_DATE,
            TASK_ESTIMATED_HOUR: response[0].TASK_ESTIMATED_HOUR,
            TASK_HOUR_CONSUMED: response[0].TASK_HOUR_CONSUMED,
            TASK_ID: response[0].TASK_ID,
            TASK_NAME: response[0].TASK_NAME,
            TASK_PRIORITY: response[0].TASK_PRIORITY,
            TASK_PROJECT_ID: response[0].TASK_PRIORITY,
            TASK_START_DATE: response[0].TASK_START_DATE,
            TASK_STATUS: response[0].TASK_STATUS,
            TASK_USER_ID: response[0].TASK_USER_ID
        }
    });
    console.log($scope.Task);
}