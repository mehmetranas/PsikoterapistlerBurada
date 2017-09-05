
var isAuth;

//Select users to ask question in selectedUsersToAskQuestionController
var selectedUsers = [];

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

var IndexController = function () {
    
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

var SelectUserToAskQuestionController = function () {

    var usersTable = function (id) {
        $(id).DataTable(
            {
                "language": {
                    "sDecimal": ",",
                    "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
                    "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
                    "sInfoEmpty": "Kayıt yok",
                    "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
                    "sInfoPostFix": "",
                    "sInfoThousands": ".",
                    "sLengthMenu": "Sayfada _MENU_ kayıt göster",
                    "sLoadingRecords": "Yükleniyor...",
                    "sProcessing": "İşleniyor...",
                    "sSearch": "Ara:",
                    "sZeroRecords": "Eşleşen kayıt bulunamadı",
                    "oPaginate": {
                        "sFirst": "İlk",
                        "sLast": "Son",
                        "sNext": "Sonraki",
                        "sPrevious": "Önceki"
                    },
                    "oAria": {
                        "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                        "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
                    }
                }
            });
    }

    var userSelect = function () {
        $(".js-username").click(function (select) {

            if (selectedUsers.length === 3) {
                bootbox.alert({
                    title: "Oops!",
                    message: "Üzgünüz 3 kişiden fazlasını seçemezsiniz",
                    buttons: {
                        ok: {
                            label: "Tamam"
                        }
                    }
                });
                return;
            }
            var selectButton = document.getElementById($(select.target).attr("id"));
            selectButton.innerText = "Eklendi";
            selectButton.disabled = true;
            var username = $(select.target).attr("data-username");
            var table = document.getElementById("js-selected");
            var row = table.insertRow(table.rows.length);
            var userCell = row.insertCell(0);
            var actionCell = row.insertCell(1);
            var removeUser = document.createElement("button");
            removeUser.innerText = "Çıkar";
            removeUser.setAttribute("user-id", $(select.target).attr("id"));
            removeUser.className = "btn btn-link js-remove-user";
            userCell.innerHTML = username;
            actionCell.appendChild(removeUser);
            $(row).addClass("animated pulse");
            selectedUsers.push($(select.target).attr("id"));
        });
    }

    var userRemove = function () {
        $(document).on("click",
            ".js-remove-user",
            function (remove) {
                var removeUserId = $(remove.target).attr("user-id");
                var removeButton = $(this);
                var userButton = document.getElementById(removeUserId);
                var row = removeButton.closest("tr");
                userButton.innerText = "Seç";
                userButton.disabled = false;
                $(row).addClass("animated fadeOut")
                    .one("webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend",
                    function () {
                        $(this).remove();
                    });
                selectedUsers.splice(selectedUsers.indexOf(removeUserId), 1);
            });
    }

    var sendQuestion = function (questionId) {
        $(".js-submit").click(function () {
            $.ajax({
                url: "/api/selectedusers",
                type: "post",
                contentType: "application/json",
                data: JSON.stringify({ selectedUsersId: selectedUsers, questionId: questionId }),
                success: function () {
                    bootbox.alert({
                        title: "Tebrikler",
                        message: "Sorunuz yola çıktı bile",
                        buttons: {
                            ok: {
                                label: "Tamam"
                            }
                        },
                        callback: function () {
                            window.location.href = "/User/GetMyQuestions";
                        }
                    });
                },
                error: function (data) {
                    console.log(data.responseJSON.message);
                }
            });
        });

    }

    return {
        usersTable: usersTable,
        userSelect: userSelect,
        userRemove: userRemove,
        sendQuestion: sendQuestion
}
}();


