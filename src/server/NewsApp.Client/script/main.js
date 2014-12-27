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
            GS: 'viewModel/user',
            tLine: 'viewModel/timeline',
            userSubscriptions: 'viewModel/userSubscriptions',
            note: 'viewModel/notification'
        },
        shim: {
            'Framework7': {
                exports: 'Framework7'
            },
        }
    });

    require(['Framework7', 'router', 'i18n!nls/lang', 'utils/appFunc', 'logger', 'GS','note'], function (Framework7, router, i18n, appFunc, logger, GS) {

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
        app.baseUrl = storage('url');
        if (app.baseUrl == null || app.baseUrl == '')
            app.baseUrl = "http://192.168.10.118/";
        window.thisApp = app;
        app.initialize();
        //**************Debugging Only
        //  thisApp.baseUrl = "http://localhost/"; 
        //   app.initMainView();
        //**************  
    });
})();
function cl(log) {

    logger.log(log);
}

var onNotificationGCM = function (e) {
    logger.log('notification coming');
    logger.log(e);
    try {
        if (e.foreground == false) {
          //  alert('Cold start Detected ' + JSON.stringify(e));
        }
        window.plugins.pushNotification.setApplicationIconBadgeNumber(cl, cl, e.badge);
        if (e.alert) {
            logger.log("Alert " + e.alert);
        }
        if (e.badge) {
            console.log("Badge number " + e.badge);
            var pushNotification = window.plugins.pushNotification;
            pushNotification.setApplicationIconBadgeNumber(cl, cl, e.badge);
        }
        if (e.sound) {
            console.log("Sound passed in " + e.sound);
            var snd = new Media(e.sound);
            snd.play();
        }
        if (e.payload.id) {
            require('note').insert(e.payload);
            mainView.loadPage('page/item.html?isNote=true&id=' + e.payload.id);
        }
    } catch (e) {
        logger.log(e.message);
    }
}


function storage(key, data) {
    try {
        if (key == 'clear') {
            var url = storage('url');
            localStorage.clear();
            storage('url', url);
        }
        else if (arguments.length == 2 && data == 'remove') {
            localStorage.removeItem(key);
        }
        else if (arguments.length == 1) {
            var d = localStorage.getItem(key);
            try {
                return JSON.parse(d);
            } catch (e) {
                return d;
            }
        }
        else if (arguments.length == 2) {
            if (typeof (data) == "function") {
                localStorage.setItem(key, JSON.stringify(data()));
            }
            else if (typeof (data) == 'object')
                localStorage.setItem(key, JSON.stringify(data));
            else
                localStorage.setItem(key, data);
        }
    } catch (e) {
        return null;
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