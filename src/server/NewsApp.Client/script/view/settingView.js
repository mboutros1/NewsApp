define(['utils/appFunc', 'utils/tplManager', 'GS', 'i18n!nls/lang'], function (appFunc, TM, GS, i18n) {

    /* global $CONFIG */

    var settingView = {

        init: function (params) {
            appFunc.bindEvents(params.bindings);
        },

        renderSetting: function (user) {
            var renderData = ko.mapping.fromJS({
                Avatar: user.Avatar,
                Name: user.Name || "", 
                i18nNickName: i18n.setting.nickname,
                i18nPoints: i18n.setting.points,
                feedBack: i18n.setting.feed_back,
                subscriptions: i18n.setting.subscriptions,
                about: i18n.setting.about,
                language: i18n.global.language,
                loginOut: i18n.setting.login_out,
                baseUrl: thisApp.baseUrl,
                logOut: function () {
                    hiApp.confirm(i18n.setting.confirm_logout, function () {
                        GS.removeCurrentUser();
                        mainView.loadPage('page/login.html');
                        hiApp.showTab('#ourView');
                    });
                },
                checkVersion: function () {
                    var version = $CONFIG.version;
                    var releaseTime = $CONFIG.release_time;
                    hiApp.alert(i18n.setting.current_version + ' V' + version + '<br/>[ ' + releaseTime + ' ]');
                }
            });
            ko.updateThis(renderData, $$('#settingView .page[data-page="setting"]')[0]);
            var bindings = [{
                element: '.logout-button',
                event: 'click',
                handler: settingView.logOut
            }, {
                element: '#settingView .update-button',
                event: 'click',
                handler: settingView.checkVersion
            }];

            appFunc.bindEvents(bindings);

            hiApp.hideIndicator();
        },

        //logOut: function () {
        //    hiApp.confirm(i18n.setting.confirm_logout, function () {
        //        GS.removeCurrentUser();

        //        mainView.loadPage('page/login.html');
        //        hiApp.showTab('#ourView');
        //    });
        //},

        checkVersion: function () {
            var version = $CONFIG.version;
            var releaseTime = $CONFIG.release_time;
            hiApp.alert(i18n.setting.current_version + ' V' + version + '<br/>[ ' + releaseTime + ' ]');
        }

    };

    return settingView;
});