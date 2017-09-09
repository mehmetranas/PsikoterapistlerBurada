var WriteAnswerService = function() {

    var post = function(answerDto) {
        $.post("/api/answer", answerDto)
            .success(function() {
                bootbox.alert({
                    size: "small",
                    title: "Tebrikler",
                    message: "Cevabınız yolda",
                    callback: function() {
                        $(".js-answer-text").val("");
                        window.location.href = "/User/GetQustionAskedToMe";
                    }
                });
            });

    };

    return {
      post: post  
    }
}();