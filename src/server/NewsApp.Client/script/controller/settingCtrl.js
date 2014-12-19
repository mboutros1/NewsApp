define(['utils/appFunc', 'utils/xhr', 'view/module', 'GS'], function (appFunc, xhr, VM, GS) {

    var settingCtrl = {

        init: function () {

            var bindings = [{
                element: '#settingView',
                event: 'show',
                handler: settingCtrl.renderSetting
            }];
            VM.module('settingView').init({
                bindings: bindings
            });

        },

        renderSetting: function () {
            if ($$('#settingView .page-content')[0])
                return;
            hiApp.showIndicator();
            VM.module('settingView').renderSetting(GS.getCurrentUser());
        }

    };

    return settingCtrl;
});