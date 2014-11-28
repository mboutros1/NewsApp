define(['userSubscriptions'], function (sub) {
    var CONFIG = null;
    var globalService = {
        init: function () {
            if (!CONFIG) {
                CONFIG = {
                    setCurrentUser: function (sid, user) {
                        var crId = storage('sid');
                        var lid = parseInt(storage('lid'));
                        if (lid > 0 && lid != crId)
                            require('utils/xhr').enqeue({ func: 'Merge', data: { oldUserId: crId, newUserid: lid }, type: 'POST' });
                        CONFIG.currentUser = user;
                        storage('user', JSON.stringify(user));
                        storage('sid', sid);
                    }, currentUser: { UserId: 0 }
                };
                if (storage('sid')) {
                    CONFIG.currentUser.UserId = storage('sid');
                }
                if (localStorage.getItem('user')) {
                    try {
                        CONFIG.currentUser = storage('user');
                    }
                    catch (e) { }
                }
                if (!CONFIG.currentUser.UserId) CONFIG.currentUser.UserId = 0;
            }
        },
        sub: sub,
        setCurrentUser: function (sid, user) {
            CONFIG.setCurrentUser(sid, user);
        },
        getCurrentUser: function () {
            return CONFIG.currentUser;
        },
        register: function () {
            var that = this;
            if (window.plugins && window.plugins.pushNotification)
                window.plugins.pushNotification.register(function (status) {
                    storage("deviceId", status);
                    that.storeToken(status);
                }, function (st) {
                    logger.log(st);
                }, { alert: true, sound: true, badge: true, ecb: 'onNotificationGCM' });

        },
        login: function (result) {
            var deviceId = storage("deviceId");
            var uid = parseInt(storage('sid'));
            result = !result ? {} : result;
            var name = result.first_name + ' ' + result.last_name;
            var x = require('utils/xhr');
            x.enqeue({
                func: 'LoginFb',
                data: {
                    userId: uid,
                    email: result.email,
                    name: name,
                    birthdate: result.birthday,
                    facebookId: result.id,
                    deviceId: deviceId
                }, method: 'POST'
            }, this.loginSuccessed);
            mainView.loadPage('index.html');
        },
        loginSuccessed: function (response) {
            storage('lid', response.UserId);
            CONFIG.setCurrentUser(response.UserId, response);
            hiApp.hidePreloader();
        },
        facebookUpdate: function () {
            var that = this;
            var fbData = JSON.parse(localStorage.getItem('fbData'));
            facebookConnectPlugin.api(fbData.id + "/?fields=id,email,birthday,first_name,last_name",
                ["user_birthday"],
                function (result) {

                    try {
                        storage('fbData', result);
                        logger.log(name);
                        that.login(result);
                    } catch (e) {
                        logger.log(e);
                    }

                }, cl);
        },
        storeToken: function (deviceId) {
            $$.ajax({
                url: thisApp.baseUrl + 'Account/Register',
                dataType: 'json',
                success: function (e) {
                    CONFIG.setCurrentUser(e.UserId, e);
                },
                contentType: 'application/json',
                type: 'POST',
                error: function (st) {
                    logger.log(st);
                },
                async: true,
                data: JSON.stringify({ userId: CONFIG.currentUser.UserId, deviceId: deviceId, deviceType: cordova.platformId })
            });
        },
        getSid: function () {
            var m = $$.parseUrlQuery(window.location.href || '');
            return m.sid || localStorage.getItem('sid');
        },
        removeCurrentUser: function () {
            CONFIG.currentUser = { UserId: 0 };
            localStorage.removeItem('user');
            localStorage.removeItem('sid');
        },

        isLogin: function () {
            return (CONFIG.currentUser && localStorage.getItem('sid'));
        }

    };

    globalService.init();

    return globalService;
});