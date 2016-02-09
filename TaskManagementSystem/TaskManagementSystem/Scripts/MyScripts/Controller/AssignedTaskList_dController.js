
AssignedTaskList_dController.$inject = ['$scope', 'DataStored', 'LoginService', '$uibModal'];
function AssignedTaskList_dController($scope, DataStored, LoginService, $uibModal) {


    var userInfo = LoginService.getUserInfo();
    $scope.AssignedTasks = []
    DataStored.AssignedTaskList_d(userInfo.Uid).then(function (info) {
        console.log(info);
        $scope.availableTasks = info.length;        
        if (info != null) {
            $scope.taskAssigned = info;
            $scope.itemsPerPage = 5;
            $scope.currentPage = 0;
            $scope.range = function () {
                var rangeSize = 4;
                var ps = [];
                var start;
                start = $scope.currentPage;
                if (start > $scope.pageCount() - rangeSize) { start = $scope.pageCount() - rangeSize + 1; }
                for (var i = start; i < start + rangeSize; i++) { if (i >= 0) ps.push(i); }
                return ps;
            };
            $scope.prevPage = function () { if ($scope.currentPage > 0) { $scope.currentPage--; } };
            $scope.DisablePrevPage = function () { return $scope.currentPage === 0 ? "disabled" : ""; };
            $scope.pageCount = function () { return Math.ceil($scope.taskAssigned.length / $scope.itemsPerPage) - 1; };

            $scope.nextPage = function () { if ($scope.currentPage > $scope.pageCount()) { $scope.currentPage++; } };
            $scope.DisableNextPage = function () { return $scope.currentPage === $scope.pageCount() ? "disabled" : ""; };
            $scope.setPage = function (n) { $scope.currentPage = n; };
        }
    });

    $scope.toggleDetail = function ($index,Pid, Mid) {
        $scope.activePosition = $scope.activePosition == $index ? -1 : $index;
        DataStored.UserDetail(Mid).then(function (data) {
            $scope.ProjectManager = data;
            console.log(data);
        });
      
    }
   // console.log($scope.AssignedTasks);
    $scope.animationsEnabled = true;
    $scope.message = [];
    $scope.UpdateTask = function (taskInfo) {
        console.log(taskInfo);
        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: '/Task/UpdateTaskStatus',
            controller: 'UpdateTaskController',
            backdrop: 'static',
            keyboard: false,
            size: 'lg',
            resolve: {
                items: function () {
                    return taskInfo;
                }
               
            }
        });
        $scope.submitText = 'Update';
    }

    $scope.toggleAnimation = function () { $scope.animationsEnabled = !$scope.animationsEnabled; };
}





DeadlineTasksController.$inject = ['$scope', 'DataStored', 'LoginService', '$uibModal'];
function DeadlineTasksController($scope, DataStored, LoginService, $uibModal) {


    var userInfo = LoginService.getUserInfo();
    $scope.AssignedTasks = []
    DataStored.DeveloperUpcomingTaskDeadlines(userInfo.Uid).then(function (info) {
        $scope.availableTasks = info.length;
        if (info != null) {
            $scope.taskAssigned = info;
            $scope.itemsPerPage = 5;
            $scope.currentPage = 0;
            $scope.range = function () {
                var rangeSize = 4;
                var ps = [];
                var start;
                start = $scope.currentPage;
                if (start > $scope.pageCount() - rangeSize) { start = $scope.pageCount() - rangeSize + 1; }
                for (var i = start; i < start + rangeSize; i++) { if (i >= 0) ps.push(i); }
                return ps;
            };
            $scope.prevPage = function () { if ($scope.currentPage > 0) { $scope.currentPage--; } };
            $scope.DisablePrevPage = function () { return $scope.currentPage === 0 ? "disabled" : ""; };
            $scope.pageCount = function () { return Math.ceil($scope.taskAssigned.length / $scope.itemsPerPage) - 1; };

            $scope.nextPage = function () { if ($scope.currentPage > $scope.pageCount()) { $scope.currentPage++; } };
            $scope.DisableNextPage = function () { return $scope.currentPage === $scope.pageCount() ? "disabled" : ""; };
            $scope.setPage = function (n) { $scope.currentPage = n; };
        }
    });

    $scope.toggleDetail = function ($index, Pid, Mid) {
        $scope.activePosition = $scope.activePosition == $index ? -1 : $index;
        DataStored.UserDetail(Mid).then(function (data) {
            $scope.ProjectManager = data;
            console.log(data);
        });

    }
    // console.log($scope.AssignedTasks);
    $scope.animationsEnabled = true;
    $scope.message = [];
    $scope.UpdateTask = function (taskInfo) {
        //alert(taskInfo);
        var data = {
            ID : taskInfo.ID,
            TASK_ID: taskInfo.TASK_ID_ASSIGNED,
            TASK_HOUR_CONSUMED: taskInfo.TASK_HOUR_CONSUMED,
            USER_TASK_STATUS: taskInfo.TASK_STATUS
        };
        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: '/Task/UpdateTaskStatus',
            controller: 'UpdateTaskController',
            backdrop: 'static',
            keyboard: false,
            size: 'lg',
            resolve: {
                items: function () {
                    return data;
                }

            }
        });
        $scope.submitText = 'Update';
    }
    /*
        $scope.UpdateHours = function (taskInfo) {
          
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: '/Task/UpdateTaskHour',
                controller: 'UpdateHourTaskController',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    items: function () {
                        return taskInfo;
                    }
                }
            });
            $scope.submitText = 'Update';
        }*/
    $scope.toggleAnimation = function () { $scope.animationsEnabled = !$scope.animationsEnabled; };
}