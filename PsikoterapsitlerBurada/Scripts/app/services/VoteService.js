var VoteService = function () {

    var vote = function (voteDto, voteCounter,e) {
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
        vote: vote
    }

}();