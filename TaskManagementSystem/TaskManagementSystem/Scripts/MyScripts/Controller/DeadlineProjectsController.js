DeadlineProjectsController.$inject = ['$scope', 'DataStored', 'LoginService'];
function DeadlineProjectsController($scope, DataStored, LoginService) {


    var Userid = LoginService.getUserInfo().Uid;

    DataStored.GetDeadlineProjects(Userid).then(function (data) {
        console.log(data);
        $scope.DeadlineProjects = data;
    });

    $scope.toggleDetail = function ($index) {
       
        $scope.activePosition = $scope.activePosition == $index ? -1 : $index;
    };

    DataStored.getProjectStatus(Userid).then(function (data) {
        console.log(data);
        var DataJson = {
            "data": [data[0].completed, data[0].onProgress, data[0].notStarted],
            "labels": ["Completed", "On Progress", "Not Started"],
            "colours": ['#008000', '#C2D3F6', '#FF0000']
        };
        $scope.pieDiskData = DataJson;
       // $scope.labels = ["Completed", "On Progress", "Not Started"];
       // $scope.data = [data[0].completed, data[0].onProgress, data[0].notStarted];
        $scope.ProjectStatus = data;
    });

  //  $scope.labels = ["Download Sales", "In-Store Sales", "Mail-Order Sales", "Tele Sales", "Corporate Sales"];
  //  $scope.data = [300, 500, 100, 40, 120];

}

UpdatedTasksManagerController.$inject = ['$scope', 'DataStored', 'LoginService'];
function UpdatedTasksManagerController($scope, DataStored, LoginService) {

  

    var Userid = LoginService.getUserInfo().Uid;

    DataStored.getUpdatedTasks(Userid).then(function (data) {
        console.log(data);
        $scope.TasksInfo = data;
    });

    $scope.toggleDetail = function ($index) {
       
        $scope.activePosition = $scope.activePosition == $index ? -1 : $index;
    };
}





DeveloperHomeController.$inject = ['$scope', 'DataStored', 'LoginService'];
function DeveloperHomeController($scope, DataStored, LoginService) {


    var Userid = LoginService.getUserInfo().Uid;

    DataStored.DeveloperTaskStatus(Userid).then(function (data) {
        console.log(data);
        var DataJson = {
            "data": [data[0].completed, data[0].OnProgress, data[0].notStarted],
            "labels": ["Completed", "On Progress", "Not Started"],
            "colours": ['#008000', '#C2D3F6', '#FF0000']
        };
        $scope.pieDiskData = DataJson;
        $scope.ProjectStatus = data;
        console.log(DataJson);
    });

}