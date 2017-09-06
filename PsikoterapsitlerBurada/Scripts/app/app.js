
var isAuth;

var setAuthState = function(auth) {
    isAuth = auth;
};

var tooltip = function () {
    if (!isAuth) {
        $('[data-toggle = "tooltip"]').tooltip();
        return;
    } else {
        $('[data-toggle = "tooltip"]').tooltip("destroy");
    }
};





