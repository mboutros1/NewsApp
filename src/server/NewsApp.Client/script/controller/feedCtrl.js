define(['utils/appFunc', 'view/module', 'utils/xhr', 'view/feedView'], function (appFunc, VM, xhr,feedView) {

    var itemCtrl = {
        init: function (query) {
            var bindings = [
                {
                    element: '.back2home',
                    event: 'click',
                    handler: VM.module('appView').showToolbar
                }, {
                    element: '.item-comment-btn',
                    event: 'click',
                    handler: function () {
                        var us = require('GS').getCurrentUser();
                        if (us.IsAnonymous())
                            mainView.loadPage('page/login.html');
                        else
                            feedView.commentPopup({ user: us, query: query });
                    }
                }
            ];

            feedView.init({
                bindings: bindings,
                query: query, item: query.item
            });
            this.getComments(query);
        },
        getComments: function (query) {
            if (query.item) {
                feedView.render({
                    comments: query.item.Comments
                });
            } else
                xhr.simpleCall({
                    func: 'GetComments',
                    data: { feedId: query.id }
                }, function (response) {
                    feedView.render({
                        comments: response
                    });

                });
        }
    };

    return itemCtrl;
});