
ngUnique.$inject = ['$http','CheckUserFactory'];
function ngUnique($http, CheckUserFactory) {                   
    var toId = {};
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, elem, attrs, Ctrl) {
            Ctrl.$setValidity('unique', true);
          
            elem.bind('blur keyup keydown', function (evt) {                                           
                var username = { "U_LOGIN_NAME": elem.val() }             
                  $http.post("/User/IsUserAvailable", JSON.stringify(username)).success(function (data) {    
                        Ctrl.$setValidity('unique', !data);
                    }).error(function(data, status, headers, cfg) {
                        Ctrl.$setValidity('unique', false);
                    });
                });          
        }
    }
}

