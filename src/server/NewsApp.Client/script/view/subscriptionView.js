define(['utils/appFunc', 'utils/tplManager', 'i18n!nls/lang'], function (appFunc, TM, i18n) {

    var subscriptionView = {

        init: function () {
            appFunc.showToolbar('.views');
            hiApp.showIndicator();
        },
        i18next: function (content) {
            var renderData = {
                subscription: i18n.setting.subscriptions,
            };
            var output = TM.renderTpl(content, renderData);
            return output;
        },
        getSubscriptions: function (data) {
            var renderData = this.renderDataFunc({
                data: data
            });
            ko.updateThis(renderData, $$('.subscription-page').find('.content-block')[0]);
            hiApp.hideIndicator();
        },
        renderDataFunc: function (options) {
            options = options || {};
            return {
                i18n: {
                    subscription: i18n.setting.subscriptions,
                },
                churches: options.data.Churches,
            };
        }, 
        setSubscription: function (options) {
            options = options || {};
            var renderData = {
                i18n: {
                    forward: i18n.timeline.forward,
                    comment: i18n.timeline.comment,
                    like: i18n.timeline.like
                },
                weibo: options.data,
                finalText: function () {
                    return appFunc.matchUrl(this.Title);
                },
                time: function () {
                    return appFunc.timeFormat(this.CreateDate);
                }
            };
            return renderData;
        },


        showLoadResult: function (text) {
            setTimeout(function () {
                $$('#ourView .load-result').html(text).css('opacity', '1').transition(1000);

                setTimeout(function () {
                    $$('#ourView .load-result').css('opacity', '0').transition(1000);
                }, 2100);
            }, 400);
        },

        openItemPage: function (e) {
            if (e.target.nodeName !== 'DIV') {
                return false;
            }
            var itemId = $$(this).parents('.item-content').data('id');
            mainView.loadPage('page/item.html?id=' + itemId);
        }

    };

    return subscriptionView;
});
