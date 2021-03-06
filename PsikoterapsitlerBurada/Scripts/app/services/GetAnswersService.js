﻿var GetAnswerService = function () {
    var likeAction = function (answerId, method, value) {
        var element = document.getElementById(answerId);
        var likeCountElement = $(element).closest("div").find("span.like");
        var likeCount = parseInt(likeCountElement.text());
        var animatedClasses = "animated bounceIn";

        $.ajax({
            method: method,
            url: "/api/like/" + answerId
        }).success(function () {
            $(element).toggleClass(animatedClasses).toggleClass("isLike");
            likeCount += value;
            likeCountElement.text(likeCount);
        });
    }

    return {
        likeAction: likeAction
    }
}();
