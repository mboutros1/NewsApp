define(['utils/appFunc', 'utils/tplManager', 'i18n!nls/lang'],
    function (appFunc, TM, i18n) {

        var timelineView = { 
            init: function () {
                appFunc.showToolbar('.views');
                $$('#ourView .pull-to-refresh-layer').show();
                hiApp.showIndicator();
            },
            getTimeline: function (data) {
                var renderData = this.renderDataFunc({
                    data: data
                });
                ko.updateThis(renderData, $$('#ourView').find('.time-line-content')[0], {
                    'feeds': {
                        key: function (d) {
                            return ko.utils.unwrapObservable(d.Id);
                        }
                    }
                });
                hiApp.hideIndicator();
                var ptrContent = $$('#ourView').find('.pull-to-refresh-content');
                ptrContent.data('scrollLoading', 'unloading');
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
                    if (data.length == 0) {
                        timelineView.showLoadResult(i18n.index.nothing_loaded);
                        return;
                    }
                    ko.updateThis({ feeds: data }, $$('#ourView').find('.time-line-content')[0]);

                } catch (e) {
                    logger.log(e.message);
                }
                hiApp.pullToRefreshDone();

            },
            infiniteTimeline: function (options) {
                options = options || {};
                ko.updateThis({ feeds: options.feeds }, $$('#ourView').find('.time-line-content')[0]);
                hiApp.hideIndicator();

            },

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