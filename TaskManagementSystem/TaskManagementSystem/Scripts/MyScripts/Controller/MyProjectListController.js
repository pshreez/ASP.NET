

MyProjectListViewController.$inject = ['$scope', 'LoginService', 'DataStored'];
function MyProjectListViewController($scope, LoginService, DataStored) {  
   
    $scope.templates = [{
        name: 'Show All ',
        url: '/Project/ShowAllProjects'
    
    },
    {
        name: 'Not Started',
        url: '/Project/NotStartedProjectList'
     
    }, {
        name: 'On Progress',
        url: '/Project/OnProgressProjectList'
     
    },
    {
        name: 'Completed',
        url: '/Project/CompletedProjectList'
    
    }];
    $scope.template = $scope.templates[0];      
}


ShowAllProjectsController.$inject = ['$scope', 'LoginService', 'DataStored', '$uibModal'];
function ShowAllProjectsController($scope, LoginService, DataStored,$uibModal) {


    var Userid = LoginService.getUserInfo().Uid;
    $scope.toggleDetail = function ($index, ProjectId, OrgId) {

        $scope.activePosition = $scope.activePosition == $index ? -1 : $index;

        DataStored.TasksOfproject(ProjectId).then(function (d) {
            console.log(d);
            if (d != null)
                $scope.Tasks = d;
            else
                $scope.NoTasks = 'No tasks Available ';
        });

        DataStored.getOrgDetail(OrgId).then(function (Org) {
            console.log(Org);
            $scope.OrgName = Org.ORG_NAME;
        });

        $scope.EditProject = function (Project) {
         //   alert('You want to edit the project' + Project);
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: "/Project/EditProject",
                controller: 'EditProjectController',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    data: function () {
                        return Project;
                    }

                }
            });
        }
    };

    DataStored.MyProjectList(Userid).then(function (projects) {
        console.log(projects);
        $scope.availableProjects = projects.length;
        if (projects != null) {
            $scope.MyProjects = projects;
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
            $scope.pageCount = function () { return Math.ceil($scope.MyProjects.length / $scope.itemsPerPage) - 1; };

            $scope.nextPage = function () { if ($scope.currentPage > $scope.pageCount()) { $scope.currentPage++; } };
            $scope.DisableNextPage = function () { return $scope.currentPage === $scope.pageCount() ? "disabled" : ""; };
            $scope.setPage = function (n) { $scope.currentPage = n; };
        } else {
            $scope.NoProjects = " ** No Projects Available **";
        }


    });
   

}


/***********  Not Started  ***********************/
ProjectNotStartedListController.$inject = ['$scope', 'LoginService', 'DataStored','$uibModal'];
function ProjectNotStartedListController($scope, LoginService, DataStored, $uibModal) {
   
    var Userid = LoginService.getUserInfo().Uid;
    $scope.toggleDetail = function ($index, ProjectId, OrgId) {

        $scope.activePosition = $scope.activePosition == $index ? -1 : $index;

        DataStored.TasksOfproject(ProjectId).then(function (d) {
            console.log(d);
            if (d != null)
                $scope.Tasks = d;
            else
                $scope.NoTasks = 'No tasks Available ';
        });

        DataStored.getOrgDetail(OrgId).then(function (Org) {
            console.log(Org);
            $scope.OrgName = Org.ORG_NAME;
        });

        $scope.EditProject = function (Project) {
           // alert('You want to edit the project' + Project);
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: "/Project/EditProject",
                controller: 'EditProjectController',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    data: function () {
                        return Project;
                    }

                }
            });
        }
    };
    DataStored.ProjectNotStartedList(Userid).then(function (projects) {
        console.log(projects);
        $scope.availableProjects = projects.length;
        if (projects != null) {
            $scope.MyProjects = projects;
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
            $scope.pageCount = function () { return Math.ceil($scope.MyProjects.length / $scope.itemsPerPage) - 1; };

            $scope.nextPage = function () { if ($scope.currentPage > $scope.pageCount()) { $scope.currentPage++; } };
            $scope.DisableNextPage = function () { return $scope.currentPage === $scope.pageCount() ? "disabled" : ""; };
            $scope.setPage = function (n) { $scope.currentPage = n; };
        } else {
            $scope.NoProjects = " ** No Projects Available **";
        }
    });
}


ProjectOnProgressListController.$inject = ['$scope', 'LoginService', 'DataStored', '$uibModal'];
function ProjectOnProgressListController($scope, LoginService, DataStored,$uibModal) {

    var Userid = LoginService.getUserInfo().Uid;
    $scope.toggleDetail = function ($index, ProjectId, OrgId) {

        $scope.activePosition = $scope.activePosition == $index ? -1 : $index;

        DataStored.TasksOfproject(ProjectId).then(function (d) {
            console.log(d);
            if (d != null)
                $scope.Tasks = d;
            else
                $scope.NoTasks = 'No tasks Available ';
        });
        DataStored.getOrgDetail(OrgId).then(function (Org) {
            console.log(Org);
            $scope.OrgName = Org.ORG_NAME;
        });

        $scope.EditProject = function (Project) {
           // alert('You want to edit the project' + Project);
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: "/Project/EditProject",
                controller: 'EditProjectController',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    data: function () {
                        return Project;
                    }

                }
            });
        }
    };
    DataStored.ProjectOnProgressList(Userid).then(function (projects) {
        console.log(projects);
        $scope.availableProjects = projects.length;
        if (projects != null) {
            $scope.MyProjects = projects;
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
            $scope.pageCount = function () { return Math.ceil($scope.MyProjects.length / $scope.itemsPerPage) - 1; };

            $scope.nextPage = function () { if ($scope.currentPage > $scope.pageCount()) { $scope.currentPage++; } };
            $scope.DisableNextPage = function () { return $scope.currentPage === $scope.pageCount() ? "disabled" : ""; };
            $scope.setPage = function (n) { $scope.currentPage = n; };
        } else {
            $scope.NoProjects = " ** No Projects Available **";
        }
    });
}

ProjectCompletedListController.$inject = ['$scope', 'LoginService', 'DataStored', '$uibModal'];
function ProjectCompletedListController($scope, LoginService, DataStored,$uibModal) {

    
    var Userid = LoginService.getUserInfo().Uid;
    $scope.toggleDetail = function ($index, ProjectId, OrgId) {

        $scope.activePosition = $scope.activePosition == $index ? -1 : $index;

        DataStored.TasksOfproject(ProjectId).then(function (d) {
            console.log(d);
            if (d != null)
                $scope.Tasks = d;
            else
                $scope.NoTasks = 'No tasks Available ';
        });
        DataStored.getOrgDetail(OrgId).then(function (Org) {
            console.log(Org);
            $scope.OrgName = Org.ORG_NAME;
        });

        $scope.EditProject = function (Project) {
          //  alert('You want to edit the project' + Project);
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: "/Project/EditProject",
                controller: 'EditProjectController',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    data: function () {
                        return Project;
                    }

                }
            });
        }
    };
    DataStored.ProjectCompletedList(Userid).then(function (projects) {
        console.log(projects);
        $scope.availableProjects = projects.length;
        if (projects != null) {
            $scope.MyProjects = projects;
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
            $scope.pageCount = function () { return Math.ceil($scope.MyProjects.length / $scope.itemsPerPage) - 1; };

            $scope.nextPage = function () { if ($scope.currentPage > $scope.pageCount()) { $scope.currentPage++; } };
            $scope.DisableNextPage = function () { return $scope.currentPage === $scope.pageCount() ? "disabled" : ""; };
            $scope.setPage = function (n) { $scope.currentPage = n; };
        } else {
            $scope.NoProjects = " ** No Projects Available **";
        }
    });

}






