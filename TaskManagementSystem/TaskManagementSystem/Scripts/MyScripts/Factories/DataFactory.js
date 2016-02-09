
DataStored.$inject = ['$http'];//var LoginFactory = function ($http,
function DataStored($http) {
    var factory = {};
    factory.UsersList = function () {
        return $http({
            method: 'GET',
            url: '/User/getUserList'
        }).then(function (response) {
            return response.data;
        });
    }
    factory.GetDevelopers = function () {
        return $http({
            method: 'GET',
            url: '/User/getDevelopers'
        }).then(function (response) {
            return response.data;
        });
    }
    factory.UserDetail = function (id) {
        return $http({
            method: 'GET',
            url: '/User/GetUserDetails/' + id,
        }).then(function (response) {
            console.log(response);
            console.log('Availabledata is');

            return response.data;
        });
    }
    
    factory.UserType = function (uname) {
        return $http({
            method: 'GET',
            url: '/User/GetUserType/' + uname,
        }).then(function (response) {
            console.log('Availabledata is');

            return response.data;
        });
    }
    factory.ProjectList = function () {
        return $http({
            method: 'GET',
            url: '/Project/getProjectList'
        }).then(function (response) {
            return response.data;
        });
    }

    factory.MyProjectList = function (id) {
        return $http({
            method: 'GET',
            url: '/Project/MyProjectList/' + id,
        }).then(function (response) {
            console.log('Availabledata is');

            return response.data;
        });
    }
   
    factory.ProjectNotStartedList = function (id) {
       
        return $http({
            method: 'GET',
            url: '/Project/getNotStartedProjectList/' + id,
        }).then(function (response) {
            console.log('Availabledata is');

            return response.data;
        });
    }
    factory.ProjectOnProgressList = function (id) {
        return $http({
            method: 'GET',
            url: '/Project/getOnProgressProjectList/' + id,
        }).then(function (response) {
            console.log('Availabledata is');

            return response.data;
        });
    }
    factory.ProjectCompletedList = function (id) {
        return $http({
            method: 'GET',
            url: '/Project/getCompletedProjectList/' + id,
        }).then(function (response) {
            console.log('Availabledata is');

            return response.data;
        });
    }

    factory.GetDeadlineProjects = function (id) {
        return $http({
            method: 'GET',
            url: '/Project/DeadlineProjects/' + id,
        }).then(function (response) {
            console.log('Availabledata is');

            return response.data;
        });
    }
    factory.ProjectDetail = function (id) {
        return $http({
            method: 'GET',
            url: '/Project/GetProjectDetails/' + id,
        }).then(function (response) {
            console.log('Availabledata is');

            return response.data;
        });
    }
    factory.getUpdatedTasks = function (id) {
        return $http({
            method: 'GET',
            url: '/Notification/RecentlyUpdatedTasks/'+ id
        }).then(function (response) {
            return response.data;
        });
    }
    factory.getProjectStatus = function (id) {
        return $http({
            method: 'GET',
            url: '/Notification/ProjectStatus/' + id
        }).then(function (response) {
            return response.data;
        });
    }
    factory.TaskList = function () {
        return $http({
            method: 'GET',
            url: '/Task/getTaskList'
        }).then(function (response) {
            return response.data;
        });
    }
    factory.TasksOfproject = function (id) {
        return $http({
            method: 'GET',
            url: '/Task/getTaskofproject/'+ id
        }).then(function (response) {
            return response.data;
        });
    }

    factory.TaskDetail = function (id) {
        return $http({
            method: 'GET',
            url: '/Task/GetTaskDetails/' + id,
        }).then(function (response) {
            console.log('Availabledata is');

            return response.data;
        });
    }
    factory.AssignedTaskList_d = function (id) {
        return $http({
            method: 'GET',
            url: '/Task/AssignedTaskList/' + id,
        }).then(function (response) {
            console.log('Availabledata is');

            return response.data; 
        });
    }
    factory.AssignedUsers = function (id) {
        return $http({
            method: 'GET',
            url: '/Task/GetAssignedUsers/' + id,
        }).then(function (response) {
            console.log('Availabledata is');

            return response.data; 
        });
    }
    
    factory.OrgList = function () {
        return $http({
            method: 'GET',
            url: '/Organization/getOrganizationList'
        }).then(function (response) {
            return response.data;
        });
    }
    factory.getOrgDetail = function (id) {
        return $http({
            method: 'GET',
            url: '/Organization/getOrgDetail/' +id
        }).then(function (response) {
            console.log(response);
            return response.data;
        });
    }

    factory.ViewDiscussions = function () {
        return $http({
            method: 'GET',
            url: '/Discuss/AllDiscussion' 
        }).then(function (response) {
            console.log(response);
            return response.data;
        });
    }
    
    factory.DeveloperUpcomingTaskDeadlines = function (id) {
        

        return $http({
            method: 'GET',
            url: '/Notification/UpcomingTaskDeadlines/' + id
        }).then(function (response) {
            console.log(response);
            return response.data;
        });
    }
    factory.DeveloperTaskStatus = function (id) {
        
        return $http({
            method: 'GET',
            url: '/Notification/TaskStatus/' + id
        }).then(function (response) {
            console.log(response);
            return response.data;
        });
    }
    



   
    return factory;


    //});
}
function callThisFuntion( me )
{
    alert(me);
}


