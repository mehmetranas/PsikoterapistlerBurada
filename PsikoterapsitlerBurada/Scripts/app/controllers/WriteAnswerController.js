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

    return {
        create: create
    }
}();