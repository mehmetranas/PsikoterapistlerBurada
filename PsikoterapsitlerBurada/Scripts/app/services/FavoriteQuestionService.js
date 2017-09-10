
var FavoriteQuestionService = function() {

    var getFavoriteQuestions = function() {
        $.ajax({
            method: "Get",
            url: "/api/favorite/",
            success: function (data) {
                data.forEach(function (id) {
                    var favoriteQuestion = $(".favorite [data-questionId ='" + id + "']");
                    if(favoriteQuestion[0])
                    favoriteQuestion[0].className += " isFavorite animated rubberBand";
                });
            }
        });

    }

    var signAsFavoriteQuestion = function (id, e) {
        console.log(e);
        $.post("/api/favorite/" + id)
            .success(function () {
                $(e.target).toggleClass("animated rubberBand").toggleClass("isFavorite");
            });
    }
    return {
        getFavoriteQuestions: getFavoriteQuestions,
        signAsFavoriteQuestion: signAsFavoriteQuestion
    }
}();