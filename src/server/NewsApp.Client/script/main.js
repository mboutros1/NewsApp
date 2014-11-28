(function () {
    var lang = localStorage.getItem('lang') || 'en-us';
    require.config({
        locale: lang,
        baseUrl: 'script',
        paths: {
            text: '../vendors/require/text',
            logger: '../script/components/logger',
            i18n: '../vendors/require/i18n',
            Framework7: '../vendors/framework7/framework7',
            GTPL: '../page/global.tpl.html',
            GS: 'services/globalService',
            userSubscriptions: 'viewModel/userSubscriptions'
        },
        shim: {
            'Framework7': {
                exports: 'Framework7'
            },
        }
    });

    require(['Framework7', 'router', 'i18n!nls/lang', 'utils/appFunc', 'logger', 'GS'], function (Framework7, router, i18n, appFunc, logger, GS) {

        var app = {
            initialize: function () {
                this.bindEvents();
            },
            bindEvents: function () {
                if (appFunc.isPhonegap()) {
                    document.addEventListener('deviceready', this.onDeviceReady, false);
                } else {
                    window.onload = this.onDeviceReady();
                }
            },
            onDeviceReady: function () {
                app.receivedEvent('deviceready');
                //  cordova.exec.setJsToNativeBridgeMode(cordova.exec.jsToNativeModes.XHR_NO_PAYLOAD);
                if (GS) GS.register();
            },
            receivedEvent: function (event) {
                switch (event) {
                    case 'deviceready':
                        app.initMainView();
                        break;
                }
            },
            initMainView: function () {
                window.$$ = Dom7;
                window.hiApp = new Framework7({
                    pushState: false,
                    popupCloseByOutside: false,
                    animateNavBackIcon: true,
                    swipeBackPage: true,
                    modalTitle: i18n.global.modal_title,
                    modalButtonOk: i18n.global.modal_button_ok,
                    modalButtonCancel: i18n.global.cancel,
                    preprocess: router.preprocess
                });
                window.logger = logger;
                logger.addHandler(function (m) {
                    logger.displayHvrErr(m);
                });
                setTimeout(logger.runQeue, 10000);
                window.mainView = hiApp.addView('#ourView', {
                    dynamicNavbar: true
                });

                window.contatcView = hiApp.addView('#contatcView', {
                    dynamicNavbar: true
                });

                window.settingView = hiApp.addView('#settingView', {
                    dynamicNavbar: true
                });
                router.init();
            },
            //baseUrl : "http://192.168.1.7/",
            baseUrl: "http://localhost/",

        };
        //app.baseUrl = 'http://192.168.113.1/';
        ////  app.baseUrl = "http://192.168.10.118/";
        //app.baseUrl = "http://192.168.1.2/";
        //app.baseUrl = "http://192.168.1.7/";
        //   app.baseUrl = 'http://localhost:1641/';
        //app.baseUrl = 'http://localhost/';
        app.baseUrl = storage('url');
        if (app.baseUrl == null || app.baseUrl == '')
            app.baseUrl = "http://192.168.10.118/";
        window.thisApp = app;
        app.initialize();
        //**************Debugging Only
        //  thisApp.baseUrl = "http://localhost/";
        if (!window.cordova) {
            window.cordova = { platformId: 'ios' };
            thisApp.baseUrl = 'http://localhost:1641/';
            //thisApp.initMainView();
        }
        //************** 


        //   app.initMainView();

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


function storage(key, data) {
    if (key == 'clear') {
        var url = storage('url');
        localStorage.clear();
        storage('url', url);
        return;
    }
    if (arguments.length == 2 && data == 'remove') {
        localStorage.removeItem(key);
        return;
    }
    if (arguments.length == 1) {
        var d = localStorage.getItem(key);
        try {
            return JSON.parse(d);
        } catch (e) {
            return d;
        }
    }
    if (arguments.length == 2) {
        if (typeof (data) == "function") {
            localStorage.setItem(key, JSON.stringify(data()));
        } else if (typeof (data) == 'object')
            localStorage.setItem(key, JSON.stringify(data));
        else
            localStorage.setItem(key, data);

    }
}
Function.prototype.trace = function () {
    var trace = [];
    var current = this;
    while (current) {
        trace.push(current.signature());
        current = current.caller;
    }
    return trace;
}

Function.prototype.signature = function () {
    var signature = {
        name: this.getName(),
        params: [],
        toString: function () {
            var params = this.params.length > 0 ?
                "'" + this.params.join("', '") + "'" : "";
            return this.name + "(" + params + ")"
        }
    };
    if (this.arguments) {
        for (var x = 0; x < this.arguments.length; x++)
            signature.params.push(this.arguments[x]);
    }
    return signature;
}

Function.prototype.getName = function () {
    if (this.name)
        return this.name;
    var definition = this.toString().split("\n")[0];
    var exp = /^function ([^\s(]+).+/;
    if (exp.test(definition))
        return definition.split("\n")[0].replace(exp, "$1") || "anonymous";
    return "anonymous";
}