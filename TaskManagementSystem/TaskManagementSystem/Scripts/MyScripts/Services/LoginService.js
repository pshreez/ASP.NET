
 
LoginService.$inject = ['$http'];
function LoginService($http) {
    this.UserName = null;
    this.Role = null;
    this.IsLoggedIn = false;
    this.Uid = null;
    this.userInfo = {};
    this.IsLoggedIn = false;
    this.setSession = function (User) {

        this.Uid = User.U_ID;
        this.UserName = User.U_LOGIN_NAME;
        this.Role = User.U_POSITION;
        this.IsLoggedIn = true;
    };
    this.UserType = function () {
        return this.Role;
    }

    this.getUserLoggedInStatus = function () {
        return this.IsLoggedIn;
    };
    this.destroy = function (UserName) {
        this.Uid = null;
        this.UserName = null;
        this.userRole = null;
        this.IsLoggedIn = false;
    };
    this.getUserInfo = function () {
        this.userInfo = {
            Uid: this.Uid,
            UserName: this.UserName,
            Role: this.Role,
            IsLoggedIn: this.IsLoggedIn
        }
        return this.userInfo;
    };

}