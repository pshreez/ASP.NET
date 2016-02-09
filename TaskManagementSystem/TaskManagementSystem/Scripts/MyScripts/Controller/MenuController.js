
var MainMenuCtrl = ['$rootScope', '$state', '$scope', '$stateParams', 'LoginService', function ($rootScope, $state, $scope, $stateParams, LoginService) {

    var userInfo = LoginService.getUserInfo(); console.log(userInfo.Role);
    $scope.initialise = function () {
        $scope.go = function (state) {  $state.go(state);  };
        $scope.tabs = [ {
               title: 'Home',
               route: 'user.home',
               url: 'user/home',
               active: true,
               disabled: false,
               template: "Views/HomePage/Userhome.cshtml"
           }, {
              title: 'Projects',
              route: 'user.Projects',
              url: 'user/Projects',
              active: false,
              disabled: false,            
          }, {
              title: 'Tasks',
              route: 'user.Tasks',
              url: 'user/Tasks',
              active: false,
              disabled: false,            
          }, {
              title: 'Users',
              route: 'user.Users',
              url: 'user/Users',
              active: false,
              disabled: false,             
          }, {
             title: 'Discussions',
             route: 'user.Discuss',
             url: 'user/Discuss',
             active: false,
             disabled: false
         }];
    }; $scope.initialise();
}];


var UserHomeCtrl = ['$rootScope', '$scope', '$stateParams', 'LoginService', function ($rootScope, $scope, $stateParams,LoginService) {
   
    if (LoginService.UserType() == 'manager')
    {
        $scope.tabs = [{
            title: 'Dashboard',
            route: 'user.home.mdsboard',
            url: 'user/home/mdsboard/:test',
            active: true
        },
        ];
    }
       
    else {
        $scope.tabs = [{
            title: 'Dashboard',
            route: 'user.home.dsboard',
            url: 'user/home/dsboard/:test',
            active: true
        },
        ];
    }
    
   
}];
var ProjectsCtrl = ['$rootScope', '$scope', '$stateParams', 'LoginService', function ($rootScope, $scope,$stateParams, LoginService) {

   
    console.log("testing :" + LoginService.UserType());
    if (LoginService.UserType() == 'manager')

        $scope.tabs = [{
            title: 'My Projects',
            route: 'user.Projects.mProjects',
            url: 'user/Projects/mProjects/:test',
            active: true
        },{
            title: 'View all ',
            route: 'user.Projects.vwProjects',
            url: 'user/Projects/vwProjects/:test',
            active: false
        },
         {
             title: 'Create ',
             route: 'user.Projects.cProjects',
             url: 'user/Projects/cProjects/:test',
             active: false
         }];
       
    else
        $scope.tabs = [{
            title: 'View all',
            route: 'user.Projects.vwProjects',
            url: 'user/Projects/vwProjects/:test',
            active: true
        }];
       
    
}];

var TasksCtrl = ['$rootScope', '$scope', '$stateParams','LoginService', function ($rootScope, $scope,$stateParams, LoginService) {

    if (LoginService.UserType() == 'manager')
        $scope.tabs = [{
            title: 'My tasks',
            route: 'user.Tasks.mTasks',
            url: 'user/Tasks/mTasks/:test',
            active: true,
            disabled: false
        },
          {
              title: 'Create',
              route: 'user.Tasks.cTasks',
              url: 'user/Tasks/cTasks',
              active: false,
              disabled: false
          }, {
              title: 'Assign',
              route: 'user.Tasks.aTasks',
              url: 'user/Tasks/aTasks',
              active: false,
              disabled: false
          }, {
              title: 'Recently updated',
              route: 'user.Tasks.upTasks',
              url: 'user/Tasks/upTasks',
              active: false,
              disabled: false
          }
        ];
    else
        $scope.tabs = [{
            title: 'My Tasks',
            route: 'user.Tasks.dTasks',
            url: 'user/Tasks/dTasks/:test',
            active: true,
            disabled: false
        },{
            title: 'Task Deadlines',
            route: 'user.Tasks.uTasks',
            url: 'user/Tasks/ uTasks/:test',
            active: true,
            disabled: false
        }];
   
}];

var UserCtrl = ['$rootScope', '$scope', '$stateParams','LoginService', function ($rootScope, $scope,$stateParams, LoginService) {

    $scope.initialise = function () {
        $scope.tabs = [{
            title: 'View Users',
            route: 'user.Users.vwUsers',
            url: 'user/Users/vwUsers/:test',
            // controller: 'MainMenuCtrl',
            active: true,
            disabled: false
        }
        ];
    };
  //  console.log($scope.tabs);
    $scope.initialise();
}];

var DiscussionCtrl = ['$rootScope', '$scope', '$stateParams','LoginService', function ($rootScope, $scope,$stateParams, LoginService) {
    $scope.initialise = function () {
        $scope.tabs = [{
            title: ' Recent ',
            route: 'user.Discuss.recent',
            url: 'user/discuss/recent/:test',
            active: true
        },
          {
              title: 'Create',
              route: 'user.Discuss.cDiscuss',
              url: 'user/discuss/cDiscuss/:test',
              active: false
          }];
    };
    $scope.initialise();
}];