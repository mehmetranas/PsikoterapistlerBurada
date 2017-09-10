var SelectUserToAskQuestionController = function () {
    var selectedUsers = [];

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

    var userSelect = function (e) {

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

        $(".js-submit").attr("disabled", false);
        var selectButton = document.getElementById($(e.target).attr("id"));
        selectButton.innerText = "Eklendi";
        selectButton.disabled = true;
        var username = $(e.target).attr("data-username");
        var table = document.getElementById("js-selected");
        var row = table.insertRow(table.rows.length);
        var userCell = row.insertCell(0);
        var actionCell = row.insertCell(1);
        var removeUser = document.createElement("button");
        removeUser.innerText = "Çıkar";
        removeUser.setAttribute("user-id", $(e.target).attr("id"));
        removeUser.className = "btn btn-link js-remove-user";
        userCell.innerHTML = username;
        actionCell.appendChild(removeUser);
        $(row).addClass("animated pulse");
        selectedUsers.push($(e.target).attr("id"));
    }

    var userRemove = function (remove) {
        
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
        if (selectedUsers.length === 0)
            $(".js-submit").attr("disabled", true);
    }

    var sendUsers = function (questionId) {
        SelectedUserService.submitSelectedUsers(selectedUsers, questionId);
    };

    return {
        usersTable: usersTable,
        userSelect: userSelect,
        userRemove: userRemove,
        sendUsers: sendUsers
    }
}();
