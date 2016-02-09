
ProjectCreateFactory.$inject = ['$http', '$q', ];
function ProjectCreateFactory($http, $q) {

    var fac = {};
    fac.SaveProjectData = function (d) {
        var deferred = $q.defer();
        return $http({
            url: '/Project/CreateProject',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        }).success(function (d) {
            deferred.resolve(d);// console.log(d); 
        })                            // Success callback
          .error(function (e) { console.log(d); deferred.reject(e); });             //Failed Callback                       
        return deferred.promise;
    };
    return fac;
}
