
RegisterFactory.$inject = ['$q', '$http']; 
function RegisterFactory($q, $http) {

    var fac = {};
    fac.SaveUserData = function (d) {
        var deferred = $q.defer();
        $http({
            url: '/User/RegisterUser',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        }).success(function (d) { deferred.resolve(d); })                            // Success callback
           .error(function (e) { alert('Error!'); deferred.reject(e); });             //Failed Callback
        return deferred.promise;
    };
    return fac;
}














