var WriteAnswerController = function() {
    var create = function() {
        $(".js-send-button").click(function (e) {
            var questionId = e.target.id;
            var answerDto = {
                QuestionId: questionId,
                AnswerText: $(".js-answer-text").val()
            }
            WriteAnswerService.post(answerDto);
        });
    }

    var checkValidation = function (e) {
        var value = $(e.target).val();
        if (value)
            $(".js-send-button").removeAttr("disabled");
        else
            $(".js-send-button").attr("disabled", true);
    }

    return {
        create: create,
        checkValidation: checkValidation
    }
}();