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


            //xhr.simpleCall({
            //    func: 'GetUserInfo', data: GS.getCurrentUser(), method: 'POST'
            //}, function (response) {
            //    var user = response;
            //    GS.setCurrentUser(user.UserId, user);
            //    VM.module('settingView').renderSetting(GS.getCurrentUser());

            //    //VM.module('settingView').renderSetting(user);
            //});
        }

    };

    return settingCtrl;
});