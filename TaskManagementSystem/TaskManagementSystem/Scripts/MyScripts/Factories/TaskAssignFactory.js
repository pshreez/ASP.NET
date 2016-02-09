TaskAssignFactory.$inject = ['$http'];
function TaskAssignFactory($http) {
     var fac = {};
     fac.SaveAssignTask = function (d) {

        var deferred = $q.defer();
        $http({
            url: '/Task/AssignTaskAction',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        }).success(function (d) {                                               // Success callback
            deferred.resolve(d);
            console.log(d);
        })
           .error(function (e) { console.log('error--->'); console.log(d); deferred.reject(e); });             //Failed Callback
        return deferred.promise;
    };
    return fac;
}