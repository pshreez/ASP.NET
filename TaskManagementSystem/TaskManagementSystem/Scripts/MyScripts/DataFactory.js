MyApp.factory('DataStored', function ($http) {
    var factory = {};
    factory.UsersList = function () {
                  return $http({ 
                      method: 'GET',
                      url: '/User/getUserList'
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
    factory.ProjectList = function () {
                    return $http({
                        method: 'GET',
                        url: '/Project/getProjectList'
                    }).then(function (response) {
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
    factory.TaskList = function () {
                    return $http({
                        method: 'GET',
                        url: '/Task/getTaskList'
                    }).then(function (response) {
                        return response.data;
                    });
    }
    factory.UserDetail = function (id) {
                    return $http({
                        method: 'GET',
                        url: '/User/GetUserDetails/' + id,                    
                    }).then(function (response) {
                        return response.data;
                    });
    }
    return factory;
    

});

function callThisFuntion( me )
{
    alert(me);
}


