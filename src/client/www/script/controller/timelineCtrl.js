define(['utils/appFunc', 'utils/xhr', 'view/module'], function (appFunc, xhr, VM) {

    var timelineCtrl = {
        init: function () {

            VM.module('timelineView').init();

            this.getTimeline();
        },

        bindEvent: function () {

            var bindings = [
                {
                    element: '#ourView',
                    selector: '.pull-to-refresh-content',
                    event: 'refresh',
                    handler: timelineCtrl.refreshTimeline
                },
                {
                    element: '#ourView',
                    selector: '.pull-to-refresh-content',
                    event: 'infinite',
                    handler: timelineCtrl.infiniteTimeline
                },
                {
                    element: '#ourView',
                    selector: '.refresh-click',
                    event: 'click',
                    handler: VM.module('timelineView').refreshTimelineByClick
                },
                {
                    element: document,
                    selector: 'a.open-send-popup',
                    event: 'click',
                    handler: VM.module('postView').openSendPopup
                },
                {
                    element: '#ourView',
                    selector: '.time-line-content .item-content .click-content',
                    event: 'click',
                    handler: VM.module('timelineView').openItemPage
                }
            ];

            appFunc.bindEvents(bindings);

        },

        getTimeline: function () {
            var that = this;
            xhr.simpleCall({
                func: 'GetFeed', data: { userId: app.user.userId, startAt: timelineCtrl.firstIndex }
            }, function (response) {
                timelineCtrl.firstIndex = response.data.length > 0 ? response.data[0].Id : 00;
                timelineCtrl.lastIndex = response.data.length > 0 ? response.data[response.data.length - 1].Id : 00;
                VM.module('timelineView').getTimeline(response.data);
            });
        },

        refreshTimeline: function () {
            var that = this;
            xhr.simpleCall({
                func: 'GetFeed', data: { userId: app.user.userId, startAt: timelineCtrl.firstIndex, refresh: true }
            }, function (response) {
                timelineCtrl.firstIndex = response.data.length > 0 ? response.data[0].Id : 00;
                VM.module('timelineView').refreshTimeline(response.data);
            });
        },

        infiniteTimeline: function () {
            var $dom = $$(this);
            var that = this;
            xhr.simpleCall({
                func: 'GetFeed', data: { userId: app.user.userId, startAt: timelineCtrl.lastIndex, refresh: false }
            }, function (response) {
                timelineCtrl.lastIndex = response.data.length > 0 ? response.data[response.data.length - 1].Id : 00;
                VM.module('timelineView').infiniteTimeline({
                    data: response.data,
                    $dom: $dom
                });
            });
        }
    };

    timelineCtrl.bindEvent();

    return timelineCtrl;
});