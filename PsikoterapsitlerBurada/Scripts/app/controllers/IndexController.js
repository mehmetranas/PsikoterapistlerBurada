﻿var IndexController = function () {

    var getFavoriteQuestions = function () {
        if (!isAuth) return;
        FavoriteQuestionService.getFavoriteQuestions();
    };

    var favoriteQuestionAction = function (e) {
        var id = $(e.target).attr("data-questionId");
        FavoriteQuestionService.signAsFavoriteQuestion(id, e);
    }

    var voteAction = function (e) {
        var voteCounter = $(e.target).closest(".vote").find(".vote-counter")[0];
        var voteDto = {
            voteAction: parseInt(e.target.getAttribute("data-vote-action")),
            questionId: $(e.target).attr("data-questionId")
        };

        VoteService.vote(voteDto, voteCounter,e);
    }

    $(".favorite span").click(function (e) {
        if (!isAuth) return;
        favoriteQuestionAction(e);
    });

    $(".js-vote").click(function (e) {
        if (!isAuth) return;
        voteAction(e);
    });

    return {
        getFavoriteQuestions: getFavoriteQuestions
    }
}();
