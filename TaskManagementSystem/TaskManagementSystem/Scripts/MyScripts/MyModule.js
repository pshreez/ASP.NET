


var MyApp = angular.module('MyApp', ['ngRoute', 'ngAnimate', 'chart.js', 'ui.bootstrap', 'ui.router',
    'ui.bootstrap.datetimepicker', 'ui.bootstrap.modal', 'ui.router.tabs']);

MyApp.controller('HomeMenuController', HomeMenuController);

MyApp.factory('DataStored', DataStored);

/* login */
MyApp.controller('LoginController', LoginController);
MyApp.factory('LoginFactory', LoginFactory);
MyApp.service('LoginService', LoginService);

/* projects */
MyApp.controller('ProjectCreateController', ProjectCreateController);
MyApp.factory('ProjectCreateFactory', ProjectCreateFactory);
MyApp.controller('ProjectListController', ProjectListController);
MyApp.controller('MyProjectListViewController', MyProjectListViewController);
MyApp.controller('MyProjectListViewController', MyProjectListViewController);
    
//MyApp.controller('MyProjectEditController', MyProjectEditController);
MyApp.controller('ProjectDetailController', ProjectDetailController);
MyApp.controller('ShowAllProjectsController', ShowAllProjectsController);
MyApp.controller('ProjectNotStartedListController', ProjectNotStartedListController);
MyApp.controller('ProjectOnProgressListController', ProjectOnProgressListController);
MyApp.controller('ProjectCompletedListController', ProjectCompletedListController);
MyApp.controller('ProjectTasksListController', ProjectTasksListController); 
MyApp.controller('DeadlineProjectsController', DeadlineProjectsController);
MyApp.controller('EditProjectController', EditProjectController);


/* user */
MyApp.factory('CheckUserFactory', CheckUserFactory);
MyApp.controller('RegisterController', RegisterController);
MyApp.factory('RegisterFactory', RegisterFactory);
MyApp.controller('ModalInstanceCtrl', ModalInstanceCtrl);

/* directives */
MyApp.directive('ngUnique', ngUnique);
MyApp.directive('ngDropdownMultiselect', ngDropdownMultiselect);

/* task */
MyApp.controller('TaskController', TaskController);
MyApp.factory('TaskCreateFactory', TaskCreateFactory);
MyApp.controller('TaskDetailController', TaskDetailController);
MyApp.factory('TaskAssignFactory', TaskAssignFactory);
MyApp.controller('AssignTaskController', AssignTaskController); 
MyApp.controller('UpdatedTasksManagerController', UpdatedTasksManagerController);


/** developer ***/
MyApp.controller('TaskListController', TaskListController); 
MyApp.controller('UpdateTaskController', UpdateTaskController);
MyApp.controller('UpdateHourTaskController', UpdateHourTaskController);
MyApp.controller('AssignedTaskList_dController', AssignedTaskList_dController); DeadlineTasksController
MyApp.controller('DeadlineTasksController', DeadlineTasksController);
MyApp.controller('AssignTaskToUserController', AssignTaskToUserController); 
MyApp.controller('MyTaskEditController', MyTaskEditController); 
MyApp.controller('DeveloperHomeController', DeveloperHomeController);
/* userlist */
MyApp.controller('UserListController', UserListController);
MyApp.controller('UserDetailController', UserDetailController); 
MyApp.controller('DiscussionCreateController', DiscussionCreateController); 
MyApp.controller('ViewDiscussionController', ViewDiscussionController);
/* filters  */
MyApp.filter('jsonDate', jsonDate);
MyApp.filter('pagination', pagination);
MyApp.filter('jsonDateConvert', jsonDateConvert);

/*Menu Controls*/

MyApp.controller('MainMenuCtrl', MainMenuCtrl);
MyApp.controller('ProjectsCtrl', ProjectsCtrl);
MyApp.controller('TasksCtrl', TasksCtrl);
MyApp.controller('UserCtrl', UserCtrl);
MyApp.controller('UserHomeCtrl', UserHomeCtrl);
MyApp.controller('DiscussionCtrl', DiscussionCtrl);

MyApp.run(['$state', '$state', '$stateParams', function ($rootScope, $state, $stateParams, LoginService) {
    $rootScope.$state = $state;
    $rootScope.$stateParams = $stateParams;
    $rootScope.IsLoggedIn = false;
    /* $rootScope.$on("$routeChangeSuccess", function(userInfo) {
     console.log(userInfo);
   });
 
   $rootScope.$on("$routeChangeError", function(event, current, previous, eventObj) {
     if (eventObj.authenticated === false) {
       $location.path("/login");
     }
   });
     */
}]);
HomeMenuController.$inject = ['$scope', '$rootScope', '$http', 'DataStored', 'LoginService', '$state'];
function HomeMenuController($scope, $rootScope, $http, DataStored, LoginService, $state) {
    $scope.status = {
        isopen: false
    };

    $scope.IsLoggedIn = LoginService.getUserLoggedInStatus();                          //getUserInfo
    console.log($scope.IsLoggedIn);
    $scope.$watch('IsLoggedIn', function (newVal, oldVal) {
        $scope.IsLoggedIn = LoginService.getUserLoggedInStatus();
        if ($scope.IsLoggedIn) {                                          //  console.log("hello" + LoginService.getUserInfo().UserName);
            $scope.userName = LoginService.getUserInfo().UserName;
        }
    });                                                                   // $rootScope.$broadcast('IsLogged', $scope.IsLogged);
    $scope.logOut = function () {
        LoginService.destroy();
        $state.go('login');
    }
}

MyApp.config(function ($routeProvider, $locationProvider, $urlRouterProvider, $stateProvider) {   
    $stateProvider
        .state("home", {
            url: '',
            views: {
                'title': { template: '<title>Home page</title>' },
                'main': {

                    templateUrl: '/Home/Home'
                }
            },
            data : { pageTitle: 'Home' }
        }).state('login', {
            url: '/login',
            views: {
                'title': { template: '<title>Login</title>' },
                'main': {
                    controller: 'LoginController',
                    templateUrl: '/User/Index'
                }

            },
            data: { pageTitle: 'Login page' }
        }).state('register', {
            url: '/register',
            views: {
                'title': { template: '<title>Register</title>' },
                'main': {
                    controller: 'RegisterController',
                    templateUrl: '/User/Register'
                }
            }
        }).state('user', {
            url: '/user',
            views: {
                'title': { template: '<title>User home</title>' },
                'main': {
                    controller: 'MainMenuCtrl',
                    templateUrl: '/HomePage/mainMenu'
                }
            }
        })
        .state('user.home', {                       /*home tab */
            url: '/home',
            templateUrl: '/HomePage/UserHome',//,
            controller: 'UserHomeCtrl'
        })
        .state('user.home.mdsboard', {                 /*home tab --> dashboard  */
            url: '/mdsboard',
            templateUrl: '/Home/ManagerHome',
            //controller:'UserHomeCtrl'

        })
        .state('user.home.dsboard', {                 /*home tab --> dashboard  */
            url: '/dsboard',
            templateUrl: '/Home/DeveloperHome',
            //controller:'UserHomeCtrl'

        })
        .state('user.Projects', {                    /*projects tab */
            url: '/Projects',
            controller: 'ProjectsCtrl',
            templateUrl: '/HomePage/project'
        })
        .state('user.Tasks', {                        /*tasks tab  */
            url: '/Tasks',
            controller: 'TasksCtrl',
            templateUrl: '/HomePage/task'
        })
        .state('user.Users', {                        /*users tab  */
            url: '/Users',
            controller: 'UserCtrl',
            templateUrl: '/HomePage/Users'
        })
        .state('user.Users.vwUsers', {                /*user tab---->view users  */
            url: '/vwUsers',
            templateUrl: '/User/ViewUserList',
            controller: 'UserListController',
        })
        .state('user.Users.details', {                /*user tab---->user details  */
            url: '/details/:uid',
            templateUrl: '/User/Details',
            controller: 'UserDetailController',
        })
        .state('user.Projects.vwProjects', {          /*projects tab---->view projects  */
            url: '/vwProjects',
            templateUrl: '/Project/ViewProjectList',
            controller: 'ProjectListController',
        })
        .state('user.Projects.mProjects', {          /*projects tab---->view projects  */
            url: '/mProjects',
            templateUrl: '/Project/MyProjectListView',
            controller: 'MyProjectListViewController',
        })
        .state('user.Projects.cProjects', {          /*projects tab---->create projects */
            url: '/cProjects',
            templateUrl: '/Project/Index',
            controller: 'ProjectCreateController',
        })
        .state('user.Projects.details', {                      /*discuss tab   *///user.Projects.details
            url: '/details/:pid',
            templateUrl: '/Project/Details',
            controller: 'ProjectDetailController',
        }).state('user.Tasks.pdetails', {                      /*discuss tab   *///user.Projects.details
            url: '/pdetails/:pid',
            templateUrl: '/Project/Details',
            controller: 'ProjectDetailController',
        })
        .state('user.Tasks.mTasks', {                                         /*tasks tab---->task view  */
            url: '/mTasks',
            templateUrl: '/Task/MyCreatedTasksList',
            //  controller: 'MyCreatedTasksListController',
        }).state('user.Projects.mTasks', {                                         /*tasks tab---->task view  */
            url: '/mTasks/:pid',
            templateUrl: '/Task/ProjectTasksList',
            //  controller: 'MyCreatedTasksListController',
        })
        .state('user.Tasks.vwTasks', {                                /*tasks tab---->task view  */
            url: '/vwTasks',
            templateUrl: '/Task/ViewTaskList',
            controller: 'TaskListController',
        }).state('user.Tasks.upTasks', {                                /*tasks tab---->task view  */
            url: '/upTasks',
            templateUrl: '/Notification/UpdatedTasksManager',
            controller: 'UpdatedTasksManagerController',
        })
        .state('user.Tasks.details', {                                          /*discuss tab   *///user.Projects.details
            url: '/details/:uid',
            templateUrl: '/Task/Details',
            controller: 'TaskDetailController',
        })
        .state('user.Tasks.dTasks', {                                /*tasks tab---->task view  */
            url: '/dTasks',
            templateUrl: '/Task/AssignedTaskList_d',
            controller: 'AssignedTaskList_dController',
        })
        .state('user.Tasks.uTasks', {                                          /*discuss tab   *///user.Projects.details
            url: '/uTasks',
            templateUrl: '/Task/UpdatedTaskList_d',
            //controller: 'UpdatedTaskList_dController',
        })
        .state('user.Tasks.cTasks', {                                        /*tasks tab---->create task   */
            url: '/cTasks',
            templateUrl: '/Task/Index',
            controller: 'TaskController'
        })
        .state('user.Tasks.aTasks', {                           /*tasks tab---->create task   */
            url: '/aTasks',
            templateUrl: '/Task/AssignTask',
            controller: 'AssignTaskController'
        })
        .state('user.Discuss', {                                                        /*discuss tab   */
            url: '/discuss',
            templateUrl: '/HomePage/Discuss',
            controller: 'DiscussionCtrl',
        })
        .state('user.Discuss.recent', {                                         /*discuss tab  -->recent */
            url: '/recent',
            templateUrl: '/Discuss/ViewDiscuss',
            //  controller: 'TaskController',
        }).state('user.Discuss.cDiscuss', {                                  /*discuss tab  --->create discussions */
            url: '/cDiscuss',
            templateUrl: '/Discuss/CreateDiscuss',
            //  controller: 'TaskController',
        });

    /*$urlRouterProvider.otherwise("/");*/

});

MyApp.controller('ViewProjectTasksListController', function ($scope, $location, $stateParams, DataStored, $uibModal) {
   
        var id = $scope.pid = $stateParams.pid; console.log(id);

        DataStored.TasksOfproject(id).then(function (data) {
            $scope.Tasks = data;
        });

        $scope.toggleDetail = function ($index, TaskId) {
            $scope.activePosition = $scope.activePosition == $index ? -1 : $index;
            // alert(TaskId);
            DataStored.AssignedUsers(TaskId).then(function (data) {
                console.log(data);
                $scope.AssignedUsers = data;
            });
        }
        $scope.AssignUsers = function (TaskId, ProjectId) {
          //  alert(ProjectId + ' ' + TaskId);
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

        $scope.GoBackToMyProjects = function () {
            $location.path('/user/Projects/mProjects');
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
});


MyApp.controller('RegisterPageController', function ($scope, $uibModalInstance,$window, data) {

    $scope.data = data;
    $scope.close = function (/*result*/) {
        $uibModalInstance.close($scope.data);
        
    }

    
});



