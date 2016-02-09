CheckUserFactory.$inject = ['$q', '$http'];
function CheckUserFactory($q, $http) {
    var fac = {};
    var deferred = $q.defer();
    fac.checkUniqueValue = function (value) {

        $http({
            url: '/User/IsUserAvailable',
            method: 'POST',
            data: JSON.stringify(value),
            headers: { 'content-type': 'application/json' }
        }).success(function (d) {
            deferred.resolve(d);                          //console.log(d); //alert("Success");
        })                                               // Success callback
      .error(function (e) {
          alert("Error"); deferred.reject(e);             //console.log(e);
      });                                                 //Failed Callback
        return deferred.promise;
    };
    return fac;
}
