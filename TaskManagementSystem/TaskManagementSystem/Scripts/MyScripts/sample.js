/*
  var factory = {}; 

  factory.method1 = function() {
          //..
      }

  factory.method2 = function() {
          //..
      }

  return factory;
  return {
         /* ProjectList: function () {
              return $http({ method: 'GET', url: '/Project/getProjectList' }).then(function (response) {
                  return response.data;
              });
          },
  UsersList: function () {
      return $http({
          method: 'GET',
          url: '/Home/result'
      }).then(function (response) {
          return response.data;
      });
  }
  }*/

/*  $scope.TextMessage = "List of users ";
       $http({
               method: 'GET',
               url: '/Home/result'
           }).success(function (data, status, headers, config) {
                $scope.Users = data;
           }).error(function(data, status, headers, config) {
           alert(data);
           });

      */


$http({ method: 'GET', url: '/Project/getProjectList' }).success(function (data, status, headers, config) {
    $scope.ProjectList = data;
}).error(function (data, status, headers, config) { //alert(data);$scope.OrgList = $scope.OrgList[0];
    $scope.ProjectList = { PROJECT_ID: 1, PROJECT_NAME: "No data available" }
});

$http({ method: 'GET', url: '/Organization/getOrganizationList' }).success(function (data, status, headers, config) {
    $scope.OrgList = data; //console.log(data);
    //   $scope.object.OrgList = $scope.OrgList[1];
}).error(function (data, status, headers, config) { //alert(data);$scope.OrgList = $scope.OrgList[0];
    $scope.OrgList = { ORG_ID: 1, ORG_NAME: "No data available" }
});


/*  $http({ method: 'POST', url: '/Project/CreateProject', data: JSON.stringify($scope.Project), headers: { 'content-type': 'application/json' } })
                       .success(function (data, status, headers, config) {
                           $scope.ErrorMessage = "Project is created";
                       }) .error(function (data, status, headers, config) {
                       $scope.ErrorMessage = "Project cannot be created here";
                   });*/

//ng-init="model=(myDate | date:'yyyy-MM-dd')"<input type="date" ng-init="model=(myDate | date:'yyyy-MM-dd')" ng-model="model" />


//register

@section Scripts{
    <script src="~/Scripts/MyScripts/LoginUser.js"></script>
<script src="~/Scripts/MyScripts/DataFactory.js"></script>
<script>
    MyApp.controller('RegisterController', function ($scope, RegistrationService) {
        $scope.RegisterMessage = "This is the register page of the user ";
        $scope.submitText = "Save";
        $scope.submitted = false;
        $scope.isFormValid = false;
        $scope.unique = true;
        $scope.removeTagOnBackspace = function (event) {
            if (event.keyCode === 8) {
                $scope.unique = true;
            }
        };
        $scope.User = { U_FIRST_NAME : '', U_LAST_NAME: '', U_LOGIN_NAME: '',U_EMAIL: '',U_PASSWORD: '', U_POSITION: '', };
        $scope.$watch('RegisterForm.$valid', function (newValue) { $scope.isFormValid = newValue;  });
        $scope.UserRegisterFunc = function () {                             //Save Data

            if ($scope.submitText == 'Save') {
                $scope.submitted = true;
                if ($scope.isFormValid) {

                    $scope.submitText = 'Please Wait...';
                    RegistrationService.SaveUserData($scope.User).then(function (d) {       // alert(d[0].Username);  ClearForm();
                        if (d[0].Status == true) {
                            $scope.RegisterMessage = d[0].Message;
                            ClearForm();                                                         ////clear form here
                            $scope.submitText = "Save";
                        } else {
                            ClearForm();
                            $scope.RegisterMessage = "The form is not submitted ";
                        }
                    });
                } else { $scope.RegisterMessage = "This is the register page of the user ";  }
            }
        }
        function ClearForm() {
            $scope.User = {};
            $scope.RegisterForm.$setPristine();
            $scope.submitted = false;
        }
    });

    MyApp.factory('RegistrationService', function ($http,$q) { //helps you run functions asynchronously, and use their return values (or exceptions) when they are done processing.
        var fac = {};
        fac.SaveUserData = function (d) {
            var deferred = $q.defer();
            $http({
                url: '/User/RegisterUser',
                method: 'POST',
                data: JSON.stringify(d),
                headers: {'content-type' : 'application/json'}
            }).success(function (d) { deferred.resolve(d); })                            // Success callback
               .error(function (e) { alert('Error!'); deferred.reject(e); });             //Failed Callback
            return deferred.promise;
        };
        return fac;
    });
    MyApp.factory('CheckUserService', ['$q', '$http', CheckUserService])
         .directive('ngUnique', ['CheckUserService', isUniqueDirective]);
   
   
    function isUniqueDirective(CheckUserService) {                   // MyApp.directive('ngUnique', ['CheckUserService', function (CheckUserService) {
        return {
            require: 'ngModel',
            link: function (scope, elem, attrs, ngModel) {

                elem.bind(' blur', function (evt) {//on
                    var currentValue = elem.val();
                    var username = { "U_LOGIN_NAME": currentValue }
                    CheckUserService.checkUniqueValue(username)
                      .then(function (unique) {  console.log(unique);
                          if (unique == true)  ngModel.$setValidity('unique', false); // shows the username already taken                    
                      }).catch(function () {
                          ngModel.$setValidity('unique', false);
                      });
                });
            }
        }
    }
    // }]);
        


    function CheckUserService($q, $http) {// MyApp.factory('CheckUserService', function ($http,$q) {
        var fac={};
        var deferred = $q.defer();
        fac.checkUniqueValue = function (  value) {
          
            $http({
                url: '/Home/IsUserAvailable',
                method: 'POST',
                data: JSON.stringify(value),
                headers: {'content-type' : 'application/json'}
            }).success(function (d) {
                deferred.resolve(d); //alert("Success");
            })                            // Success callback
          .error(function (e) { alert("Error"); deferred.reject(e); });             //Failed Callback
            return deferred.promise;
        };
        return fac;
    }// });


    



    /* 
    
    angular.module('app', [])
       .service('userService', ['$q', '$http', UserService])
       .directive('uniqueUsername', ['userService', UniqueUsernameDirective]);
    
    
    
    //aysnchronous greet
    function asyncGreet(name) {
  var deferred = $q.defer();

  setTimeout(function() {
    deferred.notify('About to greet ' + name + '.');

    if (okToGreet(name)) {
      deferred.resolve('Hello, ' + name + '!');
    } else {
      deferred.reject('Greeting ' + name + ' is not allowed.');
    }
  }, 1000);

  return deferred.promise;
}

var promise = asyncGreet('Robin Hood');
promise.then(function(greeting) {
  alert('Success: ' + greeting);
}, function(reason) {
  alert('Failed: ' + reason);
}, function(update) {
  alert('Got notification: ' + update);
});*/
    /* var compareTo = function () {
         return {
             require: "ngModel",
             scope: {
                 otherModelValue: "=compareTo"
             },
             link: function (scope, element, attributes, ngModel) {
 
                 ngModel.$validators.compareTo = function (modelValue) {
                     return modelValue == scope.otherModelValue;
                 };
 
                 scope.$watch("otherModelValue", function () {
                     ngModel.$validate();
                 });
             }
         };
     };
     module.directive("compareTo", compareTo);*/
    /*  MyApp.directive('pwCheck', [function () {
       return {
           require: 'ngModel',
           link: function (scope, elem, attrs, ctrl) {
               var firstPassword = '#' + attrs.pwCheck;
               elem.add(firstPassword).on('keyup', function () {
                   scope.$apply(function () {
                       var v = elem.val()===$(firstPassword).val();
                       ctrl.$setValidity('pwmatch', v);
                   });
               });
           }
       }
   }]);*/
    </script>

}
