
AssignTaskController.$inject = ['$scope', '$http', 'DataStored', 'LoginService', '5SecondNotificationBar'];
function AssignTaskController($scope, $http, DataStored, LoginService, TaskAssignFactory, NotificationBar) {
       
    $scope.SubmitText = 'Assign';
    $scope.isFormValid = false;
    $scope.Submitted = false;
    $scope.Notask = false;
    $scope.NoUserSelected = false;
    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };
    
    $scope.Usermodel = [];
    if ($scope.Usermodel.length == 0) {
         $scope.NoUserSelected = true;
    }
    else{
        $scope.NoUserSelected = false;
    }
    DataStored.GetDevelopers().then(function (Users) {
        $scope.Users = Users;
        for (i = 0; i < Users.length; i++) {          
                $scope.Users[i] = {
                    id: Users[i].U_ID,
                    label: Users[i].U_FIRST_NAME + ' ' + Users[i].U_LAST_NAME + ' - ' + Users[i].U_LOGIN_NAME
                }               
        }
        $scope.UsersList = $scope.Users;       
    });

    
    $scope.UserDropdownsettings = {                          /*drop down setting */
        scrollableHeight: '200px',
        scrollable: true,
        enableSearch: true
    };
    var removeItem = function (items, item) {                             /*remove the user */
        for (var index = 0; index < items.length; index++) {
            if (item.Id == items[index].Id) {
                item.IsSelected = false;
                items.splice(index, 1);
                break;
            }
        }
    };
    $scope.removeFirstItem = function (item) { removeItem($scope.Usermodel, item); };
    $scope.FirstSelectedItems = [];


    var Userid = LoginService.getUserInfo().Uid;
    DataStored.MyProjectList(Userid).then(function (Projects) {
        $scope.ProjectList = Projects;
    });
    $scope.GetTasks = function () {
        $scope.TASK_ID = null;
        $scope.Tasks = 'No Tasks';
        DataStored.TasksOfproject($scope.TaskAssign.PROJECT_ID).then(function (Tasks) {
            if (Tasks == '')
                $scope.Notask = true;
            else {
                $scope.Tasks = Tasks;
                $scope.Notask = false;
            }
                              
        });
    }


    $scope.TaskAssign = {
        PROJECT_ID : '',
        TASK_ID_ASSIGNED : '',
        TASK_USER_ID: $scope.Usermodel
    }

    $scope.$watch('AssignTaskForm.$valid', function (newVal) { $scope.IsFormValid = newVal; console.log('isvalid') });
    $scope.AssignTaskFunc = function () {
       
        if ($scope.IsFormValid) {                                                                                              //  console.log($scope.TaskAssign);
            $scope.data = {};
            for (var i in $scope.TaskAssign.TASK_USER_ID) {
                                                                                                 //  console.log($scope.TaskAssign.TASK_USER_ID[i].id);
                var j = 0;
                $scope.alerts = [];
                $scope.data[j] = {
                    PROJECT_ID: $scope.TaskAssign.PROJECT_ID,
                    TASK_ID_ASSIGNED: $scope.TaskAssign.TASK_ID_ASSIGNED,
                    TASK_USER_ID: $scope.TaskAssign.TASK_USER_ID[i].id
                }
                $scope.SubmitText = 'Assigning..';
                $http({
                    url: '/Task/AssignTaskAction',
                    method: 'POST',
                    data: JSON.stringify($scope.data[j]),
                    headers: { 'content-type': 'application/json' }
                }).success(function (d) {

                    if (d[0].AssignStatus == true)
                        $scope.alerts.push({ msg: d[0].Message ,type: 'success' });
                    else
                        $scope.alerts.push({ msg: d[0].Message, type: 'danger' });
                    console.log(d);
                })
                   .error(function (e) {
                       console.log('error--->');
                       $scope.alerts.push({ msg: d.Message + 'couldnot assign the task' });
                   });
                i++; j++;
            }
            ClearForm();
            $scope.SubmitText = 'Assign';
        }                                                                                                   //console.log($scope.data);
    }

    $scope.SubmitText = 'Assign';

    function ClearForm() {
        $scope.TaskAssign = {};
        $scope.AssignTaskForm.$setPristine();
        $scope.Submitted = false;
    }
}
