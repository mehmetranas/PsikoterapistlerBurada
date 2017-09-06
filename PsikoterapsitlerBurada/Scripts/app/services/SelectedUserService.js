var SelectedUserService = function () {
    var submitSelectedUsers = function (selectedUsers, questionId) {
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
    }

    return {
        submitSelectedUsers: submitSelectedUsers
    }
}();