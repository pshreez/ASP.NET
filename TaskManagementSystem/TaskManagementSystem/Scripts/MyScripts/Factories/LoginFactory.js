LoginFactory.$inject = ['$http', '$q', '$rootScope', '$timeout', 'LoginService'];

function LoginFactory($http, $q, $rootScope, $timeout, LoginService) {//$cookieStore,
    var fac = {};
    fac.UserLogin = function (d) {
      
        var deferred = $q.defer();
        return $http({
            url: '/User/UserLogin',
            method: 'POST',
            data: JSON.stringify(d),
            headers: { 'content-type': 'application/json' }
        }).success(function (data) {
           
            //  LoginService.setSession(data[0].User);
          //  console.log("Sessio set here");
            LoginService.setSession(data[0].User);
            console.log(LoginService.getUserInfo());
            deferred.resolve(data);
        
        }).error(function (e) { console.log(data[0].User); alert('Error!'); deferred.reject(e); });             //Failed Callback                       
        return deferred.promise;
    
    }
    /*
    fac.SetCredentials = function (d) {
       
       $rootScope.globals = {
            loggedInUser :{
                uname: U_LOGIN_NAME              
            }
       }
        
    }*/
  //  DataStored.usertype(d.U_LOGIN_NAME).then(function (usertype) {
  //      console.log(usertype);
   // });
    /*
    fac.logout= function(){
        sessionService.destroy('usr');
       // $location.path('/home');
    },
    fac.islogged=function(){
        var $checkSessionServer = $http.post('/User/CheckSession');//'data/check_session.php'
        return $checkSessionServer;
        /*
        if(sessionService.get('user')) return true;
        else return false;
        */
   // }  

   
    return fac;
}



