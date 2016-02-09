
TaskListController.$inject = ['$scope', '$http', 'DataStored'];
function TaskListController($scope, $http, DataStored) {
    DataStored.TaskList().then(function (tasks) {
        $scope.Tasks = tasks;
        console.log(tasks);
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
    });
  
}