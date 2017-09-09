var NotificationController = function () {

    var getNotifications = function () {
        if (!isAuth) return;
        NotificationService.getNotifications();
    }
    var notificationClose = function (e) {
        NotificationService.notificationClose(e);
    }
    var notificationRead = function (e) {
        NotificationService.notificationRead(e);
    }

    return {
        getNotifications: getNotifications,
        notificationClose: notificationClose,
        notificationRead: notificationRead
    }

}();
