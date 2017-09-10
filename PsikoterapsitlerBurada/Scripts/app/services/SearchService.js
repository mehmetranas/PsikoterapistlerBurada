var SearchService = function() {
    var loadSearch = function (url, query, auth) {
       $(".js-searchList").load(url,
           { query: query }, function () {
               $(".js-loader").addClass("hide");
               setAuthState(auth);
               tooltip();
               IndexController.getFavoriteQuestions();
            });
    };

    return {
        loadSearch: loadSearch
    }
}();