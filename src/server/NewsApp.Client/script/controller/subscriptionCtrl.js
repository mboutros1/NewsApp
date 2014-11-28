define(['utils/appFunc', 'utils/xhr', 'view/module', 'GS'], function (appFunc, xhr, VM, GS) {

    var subscriptionCtrl = {
        init: function () {
            VM.module('subscriptionView').init();
            GS.sub.get(function (response) {
                VM.module('subscriptionView').getSubscriptions(response);
            });
        },
        bindEvent: function () {
            var bindings = [
            ];
            appFunc.bindEvents(bindings);
        },
    };

    subscriptionCtrl.bindEvent();

    return subscriptionCtrl;
});