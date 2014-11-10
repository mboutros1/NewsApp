define([], function () {
    var CONFIG = null;
    var globalService = {
        init: function () {
            if (!CONFIG) {
                CONFIG = {
                    setCurrentUser: function (sid, user) {
                        CONFIG.currentUser = user;
                        localStorage.setItem('user', JSON.stringify(user));
                        localStorage.setItem('sid', sid);
                    }, currentUser: { UserId: 0 }
                };
                if (localStorage.getItem('sid')) {
                    CONFIG.currentUser.UserId = localStorage.getItem('sid');
                }
                if (localStorage.getItem('user')) {
                    try {
                        CONFIG.currentUser = JSON.parse(localStorage.getItem('user'));
                    }
                    catch (e) { }
                }
                if (!CONFIG.currentUser.UserId) CONFIG.currentUser.UserId = 0;
            }
        },

        setCurrentUser: function (sid, user) {
            CONFIG.setCurrentUser(sid, user);
        },
        getCurrentUser: function () {
            return CONFIG.currentUser;
        },
        register: function () {
            var that = this;
            window.plugins.pushNotification.register(function (status) {
                localStorage.setItem("deviceId", status);
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
            require('utils/xhr').simpleCall({
                func: 'LoginFb',
                data: {
                    userId: uid,
                    email: result.email,
                    name: name,
                    birthdate: result.birthday,
                    facebookId: result.id,
                    deviceId: deviceId
                }, method: 'POST'
            }, function (response) {
                CONFIG.setCurrentUser(response.UserId, response);
                mainView.loadPage('index.html');
                hiApp.hidePreloader();
            });
        },
        facebookUpdate: function () {
            var fbData = JSON.parse(localStorage.getItem('fbData'));
            facebookConnectPlugin.api(fbData.id + "/?fields=id,email,birthday,first_name,last_name",
                ["user_birthday"],
                function (result) {

                    try {
                        storage('fbData', result);
                        logger.log(name);
                        this.login(result);
                    } catch (e) {
                        logger.log(e);
                    }

                }, cl);
        },
        storeToken: function (deviceId) {
            $$.ajax({
                url: app.baseUrl + 'Home/Register',
                dataType: 'json',
                success: function (e) {
                    CONFIG.setCurrentUser(e.UserId, e);
                    localStorage.setItem('sid', CONFIG.currentUser.UserId);
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