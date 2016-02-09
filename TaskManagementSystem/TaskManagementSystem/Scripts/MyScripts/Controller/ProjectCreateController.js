
ProjectCreateController.$inject = ['$scope', '$location', '$http', 'ProjectCreateFactory', 'DataStored','$window','$uibModal'];
function ProjectCreateController($scope, $location, $http, ProjectCreateFactory, DataStored, $window,$uibModal) {
   
   
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




    $scope.ProjectMessage = "Create a project here";
    $scope.submitText = "Save";
    $scope.Submitted = false;

    $scope.isFormValid = false;

    $scope.Project = {
        PROJECT_NAME: '',
        PROJECT_START_DATE: '',
        PROJECT_END_DATE: '',
        ORG_ID: '',
        PROJECT_DESCRIPTION: ''

    }

    $scope.alerts = [];
    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };
    DataStored.OrgList().then(function (response) { $scope.OrgList = response; });
    $scope.$watch('ProjectForm.$valid', function (newVal) { $scope.IsFormValid = newVal; });
    $scope.CreateProjectFunc = function () {
        $scope.Submitted = true;
        if ($scope.IsFormValid) {
            ProjectCreateFactory.SaveProjectData($scope.Project).then(function (d) {
                console.log(d);
                if (d.data[0].ProjectStatus == true) {
                  //  $scope.alerts.push({ msg: d.data[0].Message, type: 'success' });
                    $scope.SuccessMessage = d.data[0].Message;
                    console.log("Project created");
                    // $window.location.href = '#/user/Projects/cProjects';
                    $scope.data = {
                        boldTextTitle: " ",
                        textAlert: d.data[0].Message,
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
                } else {
                    
                  //  $scope.alerts.push({ msg: d.data[0].Message, type: 'danger' });                   
                 //   console.log("project Already exist created");
                    $scope.data = {
                        boldTextTitle: " ",
                        textAlert: d.data[0].Message,
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
                ClearForm();

            });
        }
    }

    function ClearForm() {
        $scope.Project = {};
        $scope.ProjectForm.$setPristine();
        $scope.submitted = false;
    }

}

