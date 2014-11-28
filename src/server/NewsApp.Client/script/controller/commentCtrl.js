define(['utils/appFunc', 'utils/xhr', 'view/module'], function (appFunc, xhr, VM) {
    var commentCtrl = {

        init: function (query) {

            var bindings = [{
                element: '.item-comment-btn',
                event: 'click',
                handler: function () {
                    var us = require('GS').getCurrentUser();
                    if (us.IsAnonymous)
                        mainView.loadPage('page/login.html');
                    else
                        VM.module('commentView').commentPopup({ user: us, query: query });
                }
            }]; 
            VM.module('commentView').init({
                bindings: bindings
            }); 
            this.getComments(query);
        },

        getComments: function (query) {
            xhr.simpleCall({
                func: 'GetComments',
                data: { feedId: query.id }
            }, function (response) {
                VM.module('commentView').render({
                    comments: response
                });

            });
        }

    };

    return commentCtrl;
});