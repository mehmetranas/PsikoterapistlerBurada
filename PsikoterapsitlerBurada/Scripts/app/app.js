
var isAuth;

var setAuthState = function (auth) {
    isAuth = auth;
}

var tooltip = function () {
    if (!isAuth) {
        $('[data-toggle = "tooltip"]').tooltip();
        return;
    } else {
        $('[data-toggle = "tooltip"]').tooltip("destroy");
    }
};


var GetAnswersController = function () {
   var likeAction = function (answerId) {
        if (!isAuth) {
            return;
        }
        var element = document.getElementById(answerId);
        var likeCountElement = $(element).closest("div").find("span.like");
        var likeCount = parseInt(likeCountElement.text());
        var animatedClasses = "animated bounceIn";
        var method;
        var value;
        if ($(element).hasClass("isLike")) {
            method = "delete";
            value = -1;
        } else {
            method = "post";
            value = 1;
        }

        $.ajax({
            method: method,
            url: "/api/like/" + answerId
        }).success(function () {
            $(element).toggleClass(animatedClasses).toggleClass("isLike");
            likeCount += value;
            likeCountElement.text(likeCount);
        });
        return;
    };

    var getLikeAnswersId = function (questionId) {
        $.ajax({
            url: "/api/like/" + questionId,
            method: "Get",
            success: function (data) {
                if (data == null) return;
                data.forEach(function (id) {
                    document.getElementById(id).className += " isLike animated bounceIn";
                });
            }
        });
    }

    return {
        authState: setAuthState,
        tooltip: tooltip,
        getLikeAnswersId: getLikeAnswersId,
        likeAction: likeAction
    } 

}();

var indexController = function () {
    
    var getFavoriteQuestions = function () {
        if (!isAuth) return;
        $.ajax({
            method: "Get",
            url: "/api/favorite/",
            success: function (data) {
                data.forEach(function (id) {
                    var favoriteQuestion = $(".favorite [data-questionId ='" + id + "']");
                    favoriteQuestion[0].className += " isFavorite animated rubberBand";
                });
            }
        });
    };

    var favoriteQuestionAction = function(e) {
        var id = $(e.target).attr("data-questionId");

            $.post("/api/favorite/" + id)
                .success(function () {
                    $(e.target).toggleClass("animated rubberBand").toggleClass("isFavorite");
                });
    }

    var voteAction = function(e) {
        var voteCounter = $(e.target).closest(".vote").find(".vote-counter")[0];
        var voteDto = {
            voteAction: parseInt(e.target.getAttribute("data-vote-action")),
            questionId: $(e.target).attr("data-questionId")
        };

        $.post("/api/vote", voteDto)
            .success(function (data, status, xhr) {
                if (xhr.responseJSON && xhr.responseJSON.isVoteUp) {
                    bootbox.alert({
                        title: "Opps!",
                        message: "Daha önce bu soru için oy hakkınızı kullandınız",
                        buttons: {
                            ok: {
                                label: "Tamam"
                            }
                        }
                    });
                    return;
                }

                if (xhr.responseJSON && xhr.responseJSON.isRollBack) {
                    var elements = $(e.target).closest(".vote").find(".js-vote");
                    elements.each(function (index, el) {
                        el.style.color = "black";
                    });
                } else {
                    e.target.style.color = "goldenrod";
                }

                voteCounter.innerText = parseInt(voteCounter.innerText) + parseInt(voteDto.voteAction);
            })
            .fail(function (xhr, status) {
                console.log(xhr.message);
            });

    }

    return {
        authState: setAuthState,
        getFavoriteQuestions: getFavoriteQuestions,
        favoriteQuestionAction: favoriteQuestionAction,
        tooltip: tooltip,
        voteAction: voteAction
    }
}();


