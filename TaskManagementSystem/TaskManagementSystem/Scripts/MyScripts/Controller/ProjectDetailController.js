
ProjectDetailController.$inject = ['$location', '$scope', '$stateParams', 'DataStored'];
function ProjectDetailController($scope, $location, $stateParams, DataStored) {
    var id = $scope.pid = $stateParams.pid; console.log(id);
    $scope.Project = '';
    DataStored.ProjectDetail(id).then(function (response) {
        console.log(response);
        $scope.Project = response;
       /* $scope.Project = {
                ORG_ID:response[0].ORG_ID,
                PROJECT_DESCRIPTION: response[0].PROJECT_DESCRIPTION,
                PROJECT_END_DATE: response[0].PROJECT_END_DATE,
                PROJECT_ID:  response[0].PROJECT_ID,
                PROJECT_MANAGER: response[0].PROJECT_MANAGER,
                PROJECT_NAME: response[0].PROJECT_NAME,
                PROJECT_START_DATE: response[0].PROJECT_START_DATE,
                PROJECT_STATUS: response[0].PROJECT_STATUS,
                U_PROJECT_CREATE_DATE: response[0].U_PROJECT_CREATE_DATE
           
           
        }*/
    });
    console.log(DataStored.ProjectDetail(id));
}

