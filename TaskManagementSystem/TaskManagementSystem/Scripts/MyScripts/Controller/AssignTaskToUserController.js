

AssignTaskToUserController.$inject = ['$http', '$scope', '$uibModalInstance', '$uibModal', 'DataStored', 'data'];
function AssignTaskToUserController($http, $scope, $uibModalInstance, $uibModal, DataStored, data) {

    //MyApp.controller('AssignTaskToUserController', function ($http, $scope, $uibModalInstance, $uibModal, DataStored, data) {
    console.log(data);
    $scope.Usermodel = [];

    DataStored.GetDevelopers().then(function (Users) {
        console.log(Users);
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

    $scope.TaskAssign = {
        PROJECT_ID: data.PROJECT_ID,
        TASK_ID_ASSIGNED: data.TASK_ID_ASSIGNED,
        TASK_USER_ID: $scope.Usermodel
    }

    $scope.alerts = [];
    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };
    $scope.AssignTaskToUserFunc = function () {


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
                console.log(d);
                if (d[0].AssignStatus == true)
                    $scope.alerts.push({ msg: d[0].Message, type: 'success' });
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

        $scope.SubmitText = 'Assign';
        //console.log($scope.data);
    }


    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
}