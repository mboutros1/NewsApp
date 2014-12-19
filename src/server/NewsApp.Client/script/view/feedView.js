define(['utils/appFunc', 'i18n!nls/lang', 'utils/tplManager'], function (appFunc, i18n, TM) {

    var feedView = {

        init: function (params) {
            appFunc.hideToolbar('.views');
            appFunc.bindEvents(params.bindings);

            var id = params.query.id;
            this.getItem(id, params.item || null);
            return params.item;
        },

        getItem: function (id, passedItem) {
            var item;
            if (!passedItem) {
                var $this = $$('.time-line-content .item-content[data-id="' + id + '"]');
                item = {
                    Id: $this.data('id'),
                    Name: $this.find('.item-header .detail .nickname').html(),
                    Avatar: $this.find('.item-header .avatar>img').attr('src'),
                    CreateDate: $this.find('.item-header .detail .create-time').text(),
                    Title: $this.find('.item-subtitle').html(),
                    Images: $this.find('item-image>img').attr('src')
                };

                if ($this.find('.item-image img')[0])
                    item.image = $this.find('.item-image img').attr('src');

            } else
                item = passedItem;
            ko.updateThis(item, $$('#itemContent')[0]);
            //var output = TM.renderTplById('itemTemplate', item);
            //$$('#itemContent').html(output);
        },

        i18next: function (content) {
            var renderData = {
                back: i18n.global.back,
                title: i18n.item.title,
                comment: i18n.timeline.comment,
                forward: i18n.timeline.forward
            };

            var output = TM.renderTpl(content, renderData);

            return output;
        },
        commentPopup: function (params) {
            var renderData = {
                cancel: i18n.global.cancel,
                comment: i18n.timeline.comment,
                send: i18n.global.send, feedId: params.query.id,
                avatar: appFunc.formatAvatar(this.Avatar)
            };

            if (params.user.Name) {
                renderData.title = i18n.comment.reply_comment;
                renderData.placeholder = i18n.comment.reply + '@' + params.user.Name + ':';
            } else {
                renderData.title = i18n.timeline.comment;
                renderData.placeholder = i18n.comment.placeholder;
            }

            var output = TM.renderTplById('commentPopupTemplate', renderData);
            hiApp.popup($$.trim(output));

            var bindings = [{
                element: '#commentBtn',
                event: 'click',
                handler: feedView.sendComment
            }];

            appFunc.bindEvents(bindings);
        },
        sendComment: function () {
            var text = $$('#commentText').val();
            var that = $$(this);
            if (appFunc.getCharLength(text) < 4) {
                hiApp.alert(i18n.index.err_text_too_short);
                return false;
            }
            hiApp.showPreloader(i18n.comment.commenting);
            xhr.simpleCall({
                func: 'Comment', method: 'POST', data: {
                    feedId: that.attr('data-feed-id'),
                    userId: GS.getCurrentUser().UserId(), comment: text
                }
            },
                         function (response) {
                             ko.updateThis({ comments: response.Comments }, $$(feedView.elementId)[0]);
                             hiApp.hidePreloader();
                             hiApp.closeModal('.comment-popup');
                             //Refresh comment content
                         });

        },
        elementId: '#commentContent',
        render: function (params) {
            var renderData = {
                comments: params.comments,
                emptyComment: i18n.comment.empty_comment,
                rtime: function () {
                    return appFunc.timeFormat(this.time);
                }
            };
            ko.updateThis(renderData, $$(feedView.elementId)[0]);
            var bindings = [{
                element: '#commentContent .comment-item',
                event: 'click',
                handler: feedView.createActionSheet
            }];
            appFunc.bindEvents(bindings);
        },

        createActionSheet: function () {
            var replyName = $$(this).find('.comment-detail .name').html();
            var buttons1 = [
                {
                    text: i18n.comment.reply_comment,
                    bold: true,
                    onClick: function () {
                        feedView.commentPopup({ name: replyName });
                    }
                },
                {
                    text: i18n.comment.copy_comment,
                    bold: true
                }
            ];
            var buttons2 = [
                {
                    text: i18n.global.cancel,
                    color: 'red'
                }
            ];

            var groups = [buttons1, buttons2];
            hiApp.actions(groups);
        }

    };

    return feedView;
});