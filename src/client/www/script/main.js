(function () {
    var lang = localStorage.getItem('lang') || 'en-us';
    require.config({
        locale:lang,
        paths:{
            text:'../vendors/require/text',
            user:'../script/components/user',
            logger:'../script/components/logger',
            i18n:'../vendors/require/i18n',
            Framework7:'../vendors/framework7/framework7',
            GTPL:'../page/global.tpl.html',
            GS:'services/globalService'
        },
        shim:{
            'Framework7':{exports:'Framework7'}
        }
    });

    require(['Framework7', 'router', 'i18n!nls/lang', 'utils/appFunc', 'user', 'logger'], function (Framework7, router, i18n, appFunc, user, logger) {

        var app = {
            initialize:function () {
                this.bindEvents();
            },
            bindEvents:function () {
                if (appFunc.isPhonegap()) {
                    document.addEventListener('deviceready', this.onDeviceReady, false);
                } else {
                    window.onload = this.onDeviceReady();
                }
            },
            onDeviceReady:function () {
                app.receivedEvent('deviceready');
                cordova.exec.setJsToNativeBridgeMode(cordova.exec.jsToNativeModes.XHR_NO_PAYLOAD);
                app.user.register();
            },
            user:user,
            receivedEvent:function (event) {
                switch (event) {
                    case 'deviceready':
                        app.initMainView();
                        break;
                }
            },
            initMainView:function () {
                window.$$ = Dom7;

                window.hiApp = new Framework7({
                    pushState:false,
                    popupCloseByOutside:false,
                    animateNavBackIcon:true,
                    modalTitle:i18n.global.modal_title,
                    modalButtonOk:i18n.global.modal_button_ok,
                    modalButtonCancel:i18n.global.cancel,
                    preprocess:router.preprocess
                });
                window.logger = logger;
                window.mainView = hiApp.addView('#ourView', {
                    dynamicNavbar:true
                });

                window.contatcView = hiApp.addView('#contatcView', {
                    dynamicNavbar:true
                });

                window.settingView = hiApp.addView('#settingView', {
                    dynamicNavbar:true
                });
                router.init();
            }
        };
        app.baseUrl = 'http://192.168.113.1/';
        app.baseUrl = 'http://localhost:1641/';
        window.app = app;
        app.initialize();
    });
})();
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