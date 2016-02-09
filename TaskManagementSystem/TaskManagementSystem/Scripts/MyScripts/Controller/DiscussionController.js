



DiscussionCreateController.$inject = ['$scope', '$location', '$http', '$rootScope', 'LoginService', '$window'];
function DiscussionCreateController($scope, $location, $http, $rootScope, LoginService, $window) {



    $scope.Submitted = false;
    $scope.IsFormValid = false;
    $scope.submitText = "Post";
    $scope.Discussion = {
        DISCUSS_TITLE: '',
        DISCUSSION_TEXT: ''
    }
    $scope.alerts = [];
    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };

    $scope.$watch('DiscussionForm.$valid', function (newVal) { $scope.IsFormValid = newVal; console.log('isvalid') });
    /** posting discussion ***/
    $scope.DiscussionTaskFunc = function(){
        $scope.Submitted = true;
        if ($scope.IsFormValid) {

            $http({
                url: '/Discuss/DiscussionCreate',
                method: 'POST',
                data: JSON.stringify($scope.Discussion),
                headers: { 'content-type': 'application/json' }
            }).success(function (data) {
                if (data[0].ProjectStatus == true)
                    $scope.alerts.push({ msg: data[0].Message, type: 'success' });
                else
                    $scope.alerts.push({ msg: data[0].Message, type: 'danger' });
                $scope.Discussion = {
                    DISCUSS_TITLE: '',
                    DISCUSSION_TEXT: ''
                }
            }).error(function (e) { console.log(e); $scope.alerts.push({ msg: 'Error', type: 'danger' }); });             //Failed Callback                       
        }
    }



}




ViewDiscussionController.$inject = ['$scope', '$location', '$http', '$rootScope', 'LoginService', 'DataStored'];
function ViewDiscussionController($scope, $location, $http, $rootScope, LoginService, DataStored)
{
    $scope.ReplyTextArea = false;
    $scope.HideButton = true;
    DataStored.ViewDiscussions().then(function (data) {
        $scope.Discussions = data;
    });
    $scope.getUser = function (id) {
         // $scope.activePosition = $scope.activePosition == $index ? -1 : $index;
        DataStored.UserDetail(id).then(function (userData) {
            console.log(userData);
            $scope.UserFullName = userData[0];
        });
    }

    $scope.ReplyDiscussion = function (id) {
      //  alert(id);
        $scope.ReplyTextArea = true;
        $scope.HideButton = false;

    }
    $scope.DiscussionThread= {
        D_THREAD_TEXT    : '',
        D_THREAD_ROOT_ID: '' 
    }

    $scope.alerts = [];
    $scope.closeAlert = function (index) {
        $scope.alerts.splice(index, 1);
    };
    $scope.ReplySubmit = function (id) {
        $scope.DiscussionThread.D_THREAD_ROOT_ID = id;
        $http({
            url: '/Discuss/ReplyDiscussion',
            method: 'POST',
            data: JSON.stringify($scope.DiscussionThread),
            headers: { 'content-type': 'application/json' }
        }).success(function (data) {
            if (data[0].ProjectStatus == true)
            {
                $scope.ReplyTextArea = false;
                $scope.HideButton = true;
                $scope.alerts.push({ msg: data[0].Message, type: 'success' });
                $scope.DiscussionThread = {
                    D_THREAD_TEXT: '',
                    D_THREAD_ROOT_ID: ''
                }
            }
                
            else {
                $scope.alerts.push({ msg: data[0].Message, type: 'danger' });
                $scope.ReplyTextArea = false;
                $scope.HideButton = true;
                $scope.DiscussionThread = {
                    D_THREAD_TEXT: '',
                    D_THREAD_ROOT_ID: ''
                }
            }
               
            $scope.Discussion = {
                DISCUSS_TITLE: '',
                DISCUSSION_TEXT: ''
            }
        }).error(function (e) { console.log(e); $scope.alerts.push({ msg: 'Error', type: 'danger' }); });

    }


}