var GetAnswersController = function () {

    var likeAction = function (e) {
        if (!isAuth || $(e.target).attr("data-answer-userId") === $(e.target).attr("data-userId")) {
            return;
        }

        var answerId = $(e.target).attr("id");
        if ($(e.target).hasClass("isLike"))
            LikeService.likeAction(answerId, "DELETE", -1);
        else
            LikeService.likeAction(answerId, "POST", 1);
    };

    var getLikeAnswersId = function (questionId) {
        LikeService.getLikes(questionId);
    }

    return {
        getLikeAnswersId: getLikeAnswersId,
        likeAction: likeAction
    }
}();
