var NotificationService = function() {

    var getNotifications = function() {
        $.getJSON("/api/notification",
            function (notifications) {
                if (notifications.length > 0) {
                    $("li.notification").removeClass("hide");
                    $(".js-notification-count").text(notifications.length)
                        .removeClass("hide")
                        .addClass("animated bounce");
                    $(".notification").popover({
                        html: true,
                        placement: "bottom",
                        content: function () {
                            var template = $("#notification-template").html();
                            var content = _.template(template);
                            return content({ notifications: notifications });
                        }
                    });
                }
            });

    }

    var notificationClose = function(e) {
        var notificationId = $(e.target).attr("data-notification");
        var element = $(e.target);
        $.post("/api/readnotification/" + notificationId)
            .success(function () {
                element.closest("li").addClass("animated fadeOut")
                    .one('webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend',
                        function () {
                            var count = parseInt($(".js-notification-count").text());
                            count--;
                            if (count === 0) {
                                $(".js-notification-count").text("").addClass("hide");
                                $(".notification").popover("destroy");
                                $("li.notification").addClass("hide");
                                return;
                            }
                            element.closest("li").remove();
                            $(".js-notification-count").text(count);
                        });
            });
    }

    var notificationRead = function(e) {
        var notificationId = $(e.target).attr("data-notification");
        $.post("/api/readnotification/" + notificationId);
    };

    return {
        getNotifications: getNotifications,
        notificationClose: notificationClose,
        notificationRead: notificationRead
    }
}();