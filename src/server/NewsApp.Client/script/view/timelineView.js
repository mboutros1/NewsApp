define(['utils/appFunc', 'utils/tplManager', 'i18n!nls/lang'],
    function (appFunc, TM, i18n) {
        var timelineView = {
            init: function () {
                appFunc.showToolbar('.views');
                $$('#ourView .pull-to-refresh-layer').show();
                hiApp.showIndicator();
            },
            getTimeline: function (data) {
                try {
                    var ptrContent = $$('#ourView').find('.pull-to-refresh-content');
                    ptrContent.data('scrollLoading', 'unloading');
                } catch (e) {
                    logger.log(e.message);
                } finally {
                    hiApp.hideIndicator();
                    hiApp.pullToRefreshDone();
                }
            },
            renderDataFunc: function (options) {
                options = options || {};
                var renderData = {
                    i18n: {
                        forward: i18n.timeline.forward,
                        comment: i18n.timeline.comment,
                        like: i18n.timeline.like
                    },
                    feeds: options.data
                };
                return renderData;
            },
            refreshItemTime: function () {
                $$('#ourView').find('.item-header .detail .create-time').each(function () {
                    var nowTime = appFunc.timeFormat($$(this).data('time'));
                    $$(this).html(nowTime);
                });
            },
            refreshTimeline: function (data) {
                try {
                    if (data.length == 0)
                        timelineView.showLoadResult(i18n.index.nothing_loaded);
                } catch (e) {
                    logger.log(e.message);
                } finally {
                    hiApp.pullToRefreshDone();
                } 
            },
            //infiniteTimeline: function (options) {
            //    try {
            //        options = options || {};
            //        var kos = ko.dataFor($$('#ourView').find('.time-line-content')[0]);
            //        kos.feeds.extend({ rateLimit: 50 });
            //        var newFeeds = ko.mapping.fromJS(options.feeds)();
            //        for (var i = 0; i < newFeeds.length; i++)
            //            kos.feeds.push(newFeeds[i]);
            //    } catch (e) {
            //        logger.log(e.message);
            //    } finally {
            //        hiApp.hideIndicator();
            //    }
            //}, 
            refreshTimelineByClick: function () {
                setTimeout(function () {
                    $$('#ourView .refresh-click ').find('i').addClass('ios7-reloading');
                }, 350);
                $$('#ourView .pull-to-refresh-content').scrollTop(0, 300);
                hiApp.pullToRefreshTrigger('#ourview');
            }, 
            showLoadResult: function (text) {
                logger.displayHvrErr(text);
            },
            openItemPage: function (e) {
                if (e.target.nodeName !== 'DIV') {
                    return false;
                }
                var itemId = $$(this).parents('.item-content').data('id');
                mainView.loadPage('page/item.html?id=' + itemId);
            }

        };

        return timelineView;
    });