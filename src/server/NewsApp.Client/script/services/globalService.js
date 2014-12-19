define(['userSubscriptions'], function (sub) {
    var CONFIG = null;
    var globalService = {
        init: function () {
            if (!CONFIG) {
                CONFIG = {
                    validateUser: function (user) {
                        var crId = storage('sid');
                        var lid = parseInt(storage('lid'));
                        if (lid > 0 && lid != crId) {
                            require('utils/xhr').enqeue({ func: 'Merge', method: 'POST', data: { oldUserId: crId, newUserid: lid } });
                            crId = lid;
                            user.UserId = crId;
                        }
                        storage('sid', user.UserId);
                        return user;
                    },
                    setCurrentUser: function (sid, user) {
                        user = CONFIG.validateUser(user);
                        CONFIG.currentUser = user;
                        storage('user', user);
                    }, currentUser: { UserId: 0 }
                };
                if (storage('sid')) {
                    CONFIG.currentUser.UserId = storage('sid');
                }
                var u = storage('user');
                if (u)
                    CONFIG.currentUser = CONFIG.validateUser(u);

                var fb = storage('fbData');
                if (fb && parseInt(fb.id) > 0 && CONFIG.currentUser.Avatar && CONFIG.currentUser.Avatar.indexOf('FB:') == -1)
                    CONFIG.currentUser.Avatar = "FB:" + fb.id;
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
                    if (storage("deviceId") != status) {
                        storage("deviceId", status);
                        that.storeToken(status);
                    }
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
            x.simpleCall({
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
        },
        loginSuccessed: function (response) {
            if (CONFIG.currentUser && CONFIG.currentUser.UserId != response.UserId)
                storage('lid', response.UserId);
            CONFIG.setCurrentUser(response.UserId, response);
            mainView.loadPage('index.html');
            hiApp.hidePreloader();
        },
        facebookUpdate: function () {
            var that = this;
            var fbData = storage('fbData');
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
                    if (CONFIG.currentUser && CONFIG.currentUser.UserId != e.UserId)
                        storage('sid', e.UserId);
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
            return m.sid || storage('sid');
        },
        removeCurrentUser: function () {
            CONFIG.currentUser = { UserId: 0 };
            storage('user','remove');
            storage('sid','remove');
        },

        isLogin: function () {
            return (CONFIG.currentUser && storage('sid'));
        }

    };

    globalService.init();

    return globalService;
});