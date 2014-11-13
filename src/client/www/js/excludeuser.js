function User() {

    var that = {
        register: function () {
            window.plugins.pushNotification.register(function (status) {
                app.last = status;
                app.user.storeToken(status);
            }, function (st) {
                logger.log(st);
            }, { alert: true, sound: true, badge: true, ecb: 'onNotificationGCM' });

        },
        storeToken: function (deviceId) {
            $.ajax({
                url: app.baseUrl + 'Home/Register',
                dataType: 'json',
                success: function (e) {
                    app.user.userId = parseInt(e);
                    app.user.save();
                },
                contentType: 'application/json',
                type: 'POST',
                error: function (st) {
                    logger.log(st);
                },
                async: true,
                data: JSON.stringify({ userId: app.user.userId, deviceId: deviceId, deviceType: cordova.platformId })
            });
        }, save: function () {
            localStorage.setItem('user', JSON.stringify(this));
        }
    };
    var savedValue = JSON.parse(localStorage.getItem('user'));
    that = $.extend(this, that);
    that = $.extend(this, savedValue);
    if (typeof (this.userId) == 'undefined' || typeof (this.userId) != 'number')
        that.userId = 0;
    return that;
}
function Logger() {
    var that = {
        log: function (log) {
            var result = $("#result");
            if (typeof log == 'object') log = JSON.stringify(log);
            result.html(result.html() + "<br/>" + log);
            console.log(log);
            this.list.push(log);
            localStorage.setItem("logs", JSON.stringify(that.list));

        },
        clear: function () {
            localStorage.removeItem("logs");
            this.list = [];
        }
    }
    that.list = [];
    var savedValue = JSON.parse(localStorage.getItem("logs"));
    if (Array.isArray(savedValue))
        that.list = savedValue;
    that = $.extend(this, that);
    console.log('log created');
    return that;
}
function cl(log) {
    logger.log(log);
}

var onNotificationGCM = function (e) {
    logger.log('notification coming');
    logger.log(e);
    window.plugins.pushNotification.setApplicationIconBadgeNumber(cl, cl, e.badge);
    if (e.alert) {
        logger.log("Alert " + e.alert);
        alert(e.alert);
    }
    if (e.badge) {
        console.log("Badge number " + e.badge);
        var pushNotification = window.plugins.pushNotification;
        pushNotification.setApplicationIconBadgeNumber(cl, cl, e.badge);
    }
    if (e.sound) {
        console.log("Sound passed in " + e.sound);
        //var snd = new Media(e.sound);
        //snd.play();
    }
}
var logger = new Logger();