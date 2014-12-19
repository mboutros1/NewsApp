define(['utils/appFunc', 'utils/xhr', 'view/module', 'GS', 'tLine'], function (appFunc, xhr,
    VM, GS, tLine) {

    var timelineCtrl = {
        init: function () {
            VM.module('timelineView').init();
            tLine.update($$('#ourView').find('.time-line-content')[0]);
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
                    handler: function () {
                        if (require('GS').getCurrentUser().IsAnonymous())
                            mainView.loadPage('page/login.html');
                        else
                            VM.module('postView').openSendPopup();
                    }
                },
                 {
                     element: document,
                     selector: 'a.likePost',
                     event: 'click',
                     handler: function () {

                         var sender = $$(this);
                         var feedId = sender.attr('data-feedId');
                         var icon = sender.find('.icon');
                         xhr.simpleCall({
                             func: icon.hasClass('ios7-heart-outline') ? 'Like' : 'Dislike', method: 'POST',
                             data: { feedId: feedId, userId: GS.getCurrentUser().UserId() }
                         }, function (response) {
                             sender.find('count').text(response.Count);
                             if (icon.hasClass('ios7-heart-outline')) {
                                 icon.removeClass('ios7-heart-outline');
                                 icon.addClass('ios7-heart-liked');
                             } else {
                                 icon.removeClass('ios7-heart-liked');
                                 icon.addClass('ios7-heart-outline');
                             }
                         });
                     }
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
            var user = GS.getCurrentUser();
            console.log('get the feed ' + user.UserId());
            var that = this;
            var deviceId = storage("deviceId");
            var platForm = 'DK';
            if (typeof (cordova) != 'undefined' && typeof (cordova.platformId) != 'undefined')
                platForm = 'DK';
            xhr.simpleCall({
                func: 'GetInitFeed', method: 'POST', data: {
                    UserId: user.UserId(),
                    StartAt: timelineCtrl.firstIndex, DeviceId: deviceId, DeviceType: platForm
                }
            }, function (response) {
                GS.setCurrentUser(response.User.UserId, response.User);
                timelineCtrl.firstIndex = response.data.length > 0 ? response.data[0].Id : 00;
                timelineCtrl.lastIndex = response.data.length > 0 ? response.data[response.data.length - 1].Id : 00;
                tLine.update(response.data,$$('#ourView').find('.time-line-content')[0]);

             });
        },

        refreshTimeline: function () {
            var user = GS.getCurrentUser();
            xhr.simpleCall({
                func: 'GetFeed', error: function () {
                    hiApp.pullToRefreshDone();
                }, data: {
                    UserId: user.UserId(), StartAt: timelineCtrl.firstIndex,
                    Refresh: true
                }
            }, function (response) {
                timelineCtrl.firstIndex = response.data.length > 0 ? response.data[0].Id : 00;
                tLine.update(response.data, $$('#ourView').find('.time-line-content')[0]);
                //VM.module('timelineView').refreshTimeline(response.data);
            });
        },

        infiniteTimeline: function () {
            var $dom = $$(this);
            var user = GS.getCurrentUser();
            if (timelineCtrl.inprogress) return;
            timelineCtrl.inprogress = true;
            console.log('loading infinite line');
            hiApp.showIndicator();
            xhr.simpleCall({
                error: function () {
                    timelineCtrl.inprogress = false;
                }, func: 'GetFeed', data: { UserId: user.UserId(), StartAt: timelineCtrl.lastIndex, Refresh: false }
            }, function (response) {
                timelineCtrl.lastIndex = response.data.length > 0 ? response.data[response.data.length - 1].Id : 00;
                tLine.add(response.data);
                //VM.module('timelineView').infiniteTimeline({
                //    feeds: response.data,
                //    $dom: $dom
                //});
                timelineCtrl.inprogress = false;
            });
        }
    };

    timelineCtrl.bindEvent();

    return timelineCtrl;
});