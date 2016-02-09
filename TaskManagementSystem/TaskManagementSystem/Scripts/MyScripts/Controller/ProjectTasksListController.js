
ProjectTasksListController.$inject = ['$http', '$scope',  '$uibModal', 'DataStored','LoginService'];
function ProjectTasksListController($http, $scope, $uibModal, DataStored, LoginService) {

    //MyApp.controller('ProjectTasksListController', function ($scope, DataStored, LoginService, $uibModal) {

    $scope.ProjectList = [];
    var Userid = LoginService.getUserInfo().Uid;
    DataStored.MyProjectList(Userid).then(function (projects) {
        $scope.ProjectList = projects;

    });


    $scope.toggleDetail = function ($index, TaskId) {
        $scope.activePosition = $scope.activePosition == $index ? -1 : $index;
        // alert(TaskId);
        DataStored.AssignedUsers(TaskId).then(function (data) {
            console.log(data);
            $scope.AssignedUsers = data;
        });
    }
    $scope.OnselectProject = function (ProjectId) {
        DataStored.TasksOfproject(ProjectId).then(function (data) {
            $scope.availableTasks = data.length;
            if (data != null) {
                $scope.Tasks = data;

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
                $scope.pageCount = function () { return Math.ceil($scope.Tasks.length / $scope.itemsPerPage) - 1; };

                $scope.nextPage = function () { if ($scope.currentPage > $scope.pageCount()) { $scope.currentPage++; } };
                $scope.DisableNextPage = function () { return $scope.currentPage === $scope.pageCount() ? "disabled" : ""; };
                $scope.setPage = function (n) { $scope.currentPage = n; };
            } else {

                console.log('No data available');
            }


        });
    }

    $scope.AssignUsers = function (TaskId, ProjectId) {
        //alert(ProjectId + ' ' + TaskId);
        $scope.Assigndata = {
            PROJECT_ID: ProjectId,
            TASK_ID_ASSIGNED: TaskId
        }
        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: "/Task/AssignTaskToUser",
            controller: 'AssignTaskToUserController',
            backdrop: 'static',
            keyboard: false,
            size: 'lg',
            resolve: {
                data: function () {
                    return $scope.Assigndata;
                }

            }
        });
    }

    $scope.EditTask = function (Task) {
        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: "/Task/EditTask",
            controller: 'MyTaskEditController',
            backdrop: 'static',
            keyboard: false,
            size: 'lg',
            resolve: {
                data: function () {
                    return Task;
                }

            }
        });
    }

}
