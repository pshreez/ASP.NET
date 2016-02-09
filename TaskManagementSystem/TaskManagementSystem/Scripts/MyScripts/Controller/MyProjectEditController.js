

EditProjectController.$inject = ['$http','$scope', '$uibModalInstance','$uibModal', 'DataStored', 'data'];

function EditProjectController($http,$scope, $uibModalInstance,$uibModal, DataStored, data) {

    console.log(data);

    /***             date picker ******************/
    //$scope.toggleMin = function () { $scope.minDate = $scope.minDate ? null : new Date(); };// disable date before todays
   // $scope.toggleMin();
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

    /***            putting data onto form ******************/

    function DateFormat(date) {
        var dateString = date.substr(6);
        var currentTime = new Date(parseInt(dateString));
        var month = currentTime.getMonth() + 1;
        var day = currentTime.getDate();
        var year = currentTime.getFullYear();
        var date = month + "/" + day + "/" + year;
        return date;
    }
    var startDate = new Date(DateFormat(data.PROJECT_START_DATE));
    var endDate = new Date(DateFormat(data.PROJECT_END_DATE));
    $scope.Project = {
        PROJECT_ID: data.PROJECT_ID,
        PROJECT_NAME: data.PROJECT_NAME,
        PROJECT_START_DATE: startDate,
        PROJECT_END_DATE: endDate,
        ORG_ID : data.ORG_ID,
        PROJECT_STATUS: data.PROJECT_STATUS,
        PROJECT_DESCRIPTION: data.PROJECT_DESCRIPTION
    }
    $scope.submitText = "Save";
    $scope.Submitted = false;

    $scope.alerts = [];
    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };
    $scope.isFormValid = false;
    DataStored.OrgList().then(function (response) { $scope.OrgList = response; });
    $scope.$watch('ProjectEditForm.$valid', function (newVal) { $scope.IsFormValid = newVal; });
    $scope.isDisabled = false;
    $scope.EditProjectFunc = function () {
        $scope.Submitted = true;
        if ($scope.IsFormValid) {
            $scope.submitText = "Saving";
            $http({
                url: '/Project/EditProjectAction',
                method: 'POST',
                data: JSON.stringify($scope.Project),
                headers: { 'content-type': 'application/json' }
            }).success(function (d) {
                
                if (d[0].Status == true){
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
                    });//  $location('/user/Projects/mProjects');
                }                  
                else {
                    $scope.submitText = "Not Saved";
                    $scope.alerts.push({ msg: d[0].Message, type: 'danger' });
                    $uibModalInstance.dismiss('cancel');
                }                   
                console.log(d);// console.log(d); 
            })                            // Success callback
         .error(function (e) { console.log(d);  });             //Failed Callback                       
           
        }
    }

    $scope.cancel = function () {
        $scope.isDisabled = true;
        $uibModalInstance.dismiss('cancel');
    };
}
