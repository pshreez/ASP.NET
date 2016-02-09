


MyTaskEditController.$inject = ['$http', '$scope', '$uibModalInstance', '$uibModal', 'DataStored', 'data', 'LoginService'];

function MyTaskEditController($http, $scope, $uibModalInstance, $uibModal, DataStored, data, LoginService) {

   // MyApp.controller('MyTaskEditController', function ($http, $scope, $uibModalInstance, $uibModal, DataStored, data, LoginService) {
        console.log(data);

        /***             date picker ******************/
        $scope.toggleMin = function () { $scope.minDate = $scope.minDate ? null : new Date(); };// disable date before todays
        $scope.toggleMin();
        $scope.maxDate = new Date(2020, 5, 22);  // setting the maximm date available
        $scope.openStart = function ($event) {
            $scope.status.endOpened = false;
            $event.preventDefault();
            $event.stopPropagation(); $scope.status.startOpened = true;
        };
        $scope.openEnd = function ($event) {
            $scope.status.startOpened = false;
            $event.preventDefault();
            $event.stopPropagation(); $scope.status.endOpened = true;
        };
        $scope.status = { startOpened: false, endOpened: false };

        $scope.submitText = 'Save';

    
        function DateFormat(date) {
            var dateString = date.substr(6);
            var currentTime = new Date(parseInt(dateString));
            var month = currentTime.getMonth() + 1;
            var day = currentTime.getDate();
            var year = currentTime.getFullYear();
            var date = month + "/" + day + "/" + year;
            return date;
        }
        var startDate = new Date(DateFormat(data.TASK_START_DATE));
        var endDate = new Date(DateFormat(data.TASK_END_DATE));

        /** setting values to the form fields **************/
        $scope.Task = {
            TASK_ID            : data.TASK_ID,
            TASK_PROJECT_ID    : data.TASK_PROJECT_ID,
            PROJECT_PHASE      : data.PROJECT_PHASE,
            TASK_DESCRIPTION   : data.TASK_DESCRIPTION,
            TASK_START_DATE    : startDate,
            TASK_END_DATE      : endDate,
            TASK_ESTIMATED_HOUR: data.TASK_ESTIMATED_HOUR,       
            TASK_NAME          : data.TASK_NAME,
            TASK_PRIORITY      : data.TASK_PRIORITY,
            TASK_PROJECT_ID    : data.TASK_PROJECT_ID,        
            TASK_STATUS        : data.TASK_STATUS
        }
        /***   getting project lists of the manager      ****/

        var Userid = LoginService.getUserInfo().Uid;
        DataStored.MyProjectList(Userid).then(function (projects) { $scope.ProjectList = projects; });

        /***  getting start and end date of project ***************/

        $scope.ProjectDates = false;
        $scope.StartDate = '';
        $scope.EndDate = '';
        $scope.setDate = function (id) {
            DataStored.ProjectDetail(id).then(function (Detail) { // alert(124);
                console.log(Detail);
                $scope.StartDate = DateFormat(Detail[0].PROJECT_START_DATE);
                $scope.EndDate = DateFormat(Detail[0].PROJECT_END_DATE);
                $scope.ProjectDates = true;
            });
        }

        DataStored.ProjectDetail($scope.Task.TASK_PROJECT_ID).then(function (Detail) { // alert(124);
            console.log(Detail);
            $scope.StartDate = DateFormat(Detail[0].PROJECT_START_DATE);
            $scope.EndDate = DateFormat(Detail[0].PROJECT_END_DATE);
            $scope.ProjectDates = true;
        });


        /***  getting start date  of task ***************/

        $scope.checkProjectStartDate = function (date) {
            var dt = (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
            if (!((Date.parse($scope.StartDate) <= Date.parse(dt))) || !((Date.parse($scope.EndDate) >= Date.parse(dt)))) {
              //  alert('Pick the date later than' + $scope.StartDate);
                $scope.Task.TASK_START_DATE = '';
                $scope.data = {
                    boldTextTitle: " ",
                    textAlert: "Pick the date later than" + $scope.StartDate + " and before" + $scope.EndDate,
                    mode: 'warning'
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

        $scope.Submitted = false;
        $scope.submitText = "Save";
        $scope.isFormValid = false;
        $scope.isDisabled = false;
        $scope.$watch('TaskEditForm.$valid', function (newVal) { $scope.IsFormValid = newVal; });

        $scope.EditTaskFunc = function () {

            $scope.Submitted = true;
            if ($scope.IsFormValid) {
             //   alert(1234);
                $scope.submitText = "Saving";
                $http({
                    url: '/Task/TaskEditAction',
                    method: 'POST',
                    data: JSON.stringify($scope.Task),
                    headers: { 'content-type': 'application/json' }
                }).success(function (d) {
                    console.log(d);
                    if (d[0].Status == true) {
                        $scope.submitText = "Saved";
                        $uibModalInstance.dismiss('cancel');
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
                        });                        //  $location('/user/Tasks/mTasks');
                    }
                    else {
                        $scope.submitText = "Not Saved";
                        $scope.alerts.push({ msg: d[0].Message, type: 'danger' });
                        $uibModalInstance.dismiss('cancel');
                    }console.log(d);
                })                           
             .error(function (e) { console.log(d); deferred.reject(e); });             //Failed Callback                       

            }
        }

        /**         cancelling form            ****/
        $scope.cancel = function () {
            $scope.isDisabled = true;
            $uibModalInstance.dismiss('cancel');
        };
    }











