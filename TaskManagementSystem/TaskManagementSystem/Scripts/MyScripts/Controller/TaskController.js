


TaskController.$inject = ['$scope', '$location', '$http', 'TaskCreateFactory', 'DataStored', 'LoginService','$uibModal'];

function TaskController($scope, $location, $http, TaskCreateFactory, DataStored, LoginService, $uibModal) {

    /****************date picker *************************/

    $scope.toggleMin = function () { $scope.minDate = $scope.minDate ? null : new Date(); }; // disable date before todays
    $scope.toggleMin();

    $scope.maxDate = new Date(2020, 5, 22);                                                  // setting the maximm date available
    $scope.openStart = function ($event) {
        $scope.status.endOpened = false;
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.startOpened = true;
    };
    $scope.openEnd = function ($event) {
        $scope.status.startOpened = false;
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.endOpened = true;
    };

    $scope.status = { startOpened: false, endOpened: false };

    /****************date picker end *************************/


    /******* setting object values *********************/

    $scope.submitText = "Save";
    $scope.Submitted   = false;
    $scope.isFormValid = false;
    $scope.Task = {
        TASK_NAME          : '',
        TASK_PROJECT_ID    : '',
        TASK_USER_ID       : '',
        TASK_START_DATE    : '',
        TASK_END_DATE      : '',
        TASK_PRIORITY      : '',
        PROJECT_PHASE      : ' ',
        TASK_ESTIMATED_HOUR: '',
        TASK_DESCRIPTION   : ''
    }
 
   

    /***   getting project lists of the manager      ****/

    var Userid = LoginService.getUserInfo().Uid;                                  
    DataStored.MyProjectList(Userid).then(function (projects) {   $scope.ProjectList = projects;  });

    /***  getting start and end date of project ***************/
    $scope.ProjectDates = true;
    $scope.StartDate = '';
    $scope.EndDate   = '';
    $scope.setDate   = function (id) {
        DataStored.ProjectDetail(id).then(function (Detail) { // alert(124);
            console.log(Detail);
            $scope.StartDate = DateFormat(Detail[0].PROJECT_START_DATE);
            $scope.EndDate = DateFormat(Detail[0].PROJECT_END_DATE);
            $scope.ProjectDates = true;
        });
    }
    /***  getting start date  of task ***************/

    $scope.checkProjectStartDate = function (date) {
        var dt = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();       
        if (!((Date.parse($scope.StartDate) <= Date.parse(dt))) || !((Date.parse($scope.EndDate) >= Date.parse(dt)))) {
            //alert('Pick the date later than' + $scope.StartDate);
            $scope.Task.TASK_START_DATE = '';
            $scope.data = {
                boldTextTitle: " ",
                textAlert: "Pick the date later than" + $scope.StartDate + " and before" + $scope.EndDate,
                mode: 'warning'
            }
            var modalInstance = $uibModal.open({
                animation  : $scope.animationsEnabled,
                templateUrl: "/Task/DateError",
                controller : 'ModalInstanceCtrl',
                backdrop   : 'static',
                keyboard   : false,
                size       : 'lg',
                resolve    : {
                data      : function () {
                    return $scope.data;
                    }

                }
            });
        }
            
    }


    /***  getting end date of task ***************/

    $scope.checkProjectEndDate = function (date) {
        var dt = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
        if (!(Date.parse($scope.EndDate) >= Date.parse(dt)) || !(Date.parse($scope.StartDate) <= Date.parse(dt))) {
          //  alert('Pick the date earlier than ' + $scope.EndDate);
            $scope.Task.TASK_END_DATE = '';
            $scope.data = {
                boldTextTitle: " ",
                textAlert: "Pick the date later than" + $scope.StartDate + " and before" + $scope.EndDate,
                mode: 'warning',
                
            }
            var modalInstance = $uibModal.open({
                animation: $scope.animationsEnabled,
                templateUrl: "/Task/DateError",
                controller: 'ModalInstanceCtrl',
                backdrop: 'static',
                keyboard: false,
                size: 'lg',
                resolve: {
                    data: function () {
                        return $scope.data;
                    }

                }
            });

        }
            
    }

    function DateFormat(date) {
        var dateString = date.substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        var date = month + "/" + day + "/" + year;
        return date;
    }

    /* alerts success and failure*/
    $scope.alerts = [];
    $scope.closeAlert = function (index) {    
        $scope.alerts.splice(index, 1);
    };


    /**************************      form submission        ****************/

    $scope.$watch('TaskForm.$valid', function (newVal) { $scope.IsFormValid = newVal; console.log('isvalid') });
    $scope.CreateTaskFunc = function () {
        $scope.Submitted = true;

        if ($scope.IsFormValid) {
            TaskCreateFactory.SaveUserData($scope.Task).then(function (d) {
                if (d[0].Status == true) {
                   // $scope.alerts.push({ msg: d[0].Message, type: 'success' });
                    $scope.data = {
                        boldTextTitle: " ",
                        textAlert: d[0].Message,
                        mode: 'success',
                
                    }
                    var modalInstance = $uibModal.open({
                        animation: $scope.animationsEnabled,
                        templateUrl: "/Task/DateError",
                        controller: 'ModalInstanceCtrl',
                        backdrop: 'static',
                        keyboard: false,
                        size: 'lg',
                        resolve: {
                            data: function () {
                                return $scope.data;
                            }

                        }
                    });
                }
                    
                else {
                  //  $scope.alerts.push({ msg: d[0].Message, type: 'danger' });
                    $scope.data = {
                        boldTextTitle: " ",
                        textAlert: d[0].Message,
                        mode: 'warning',

                    }
                    var modalInstance = $uibModal.open({
                        animation: $scope.animationsEnabled,
                        templateUrl: "/Task/DateError",
                        controller: 'ModalInstanceCtrl',
                        backdrop: 'static',
                        keyboard: false,
                        size: 'lg',
                        resolve: {
                            data: function () {
                                return $scope.data;
                            }

                        }
                    });
                }
                    
                console.log(d);
            });
            ClearForm();
        }

        /************* clear the form                         **********/
        function ClearForm() {
            $scope.Task = {};
            $scope.Submitted = false;
        }

    }
   
}


