var SelectedUserService = function () {
    var submitSelectedUsers = function (selectedUsers, questionId) {

        var dialog = bootbox.dialog({
            title: 'A custom dialog with init',
            message: '<p><i class="fa fa-spin fa-spinner"></i>Sorunuz gönderiliyor...</p>'
        });

        $.ajax({
            url: "/api/selectedusers",
            type: "post",
            contentType: "application/json",
            data: JSON.stringify({ selectedUsersId: selectedUsers, questionId: questionId }),
            success: function () {
                dialog.modal("hide");
                bootbox.alert({
                    title: "Tebrikler",
                    message: "Sorunuz yola çıktı",
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