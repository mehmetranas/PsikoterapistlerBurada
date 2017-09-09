var SearchController = function() {
    var search = function (url,query,auth) {
        SearchService.loadSearch(url, query, auth);
    }

    return {
        search: search
    }
}();
