define([],function(){
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
            $$.ajax({
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
    if (savedValue !=null) that.userId = savedValue.userId;
    if (typeof (that.userId) == 'undefined' || typeof (that.userId) != 'number')
        that.userId = 0;
    return that;
})