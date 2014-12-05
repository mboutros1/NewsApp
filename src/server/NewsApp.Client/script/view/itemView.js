define(['utils/appFunc', 'i18n!nls/lang', 'utils/tplManager'], function (appFunc, i18n, TM) {

    var itemView = {

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
        }

    };

    return itemView;
});