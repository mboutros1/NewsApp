define(['utils/xhr'], function (xhr) {
    var subs = {
        init: function () {
            this.subscription = storage('subscription') || {};
            subs.subscription = ko.mapping.fromJS(subs.subscription, {
                'Subscription': {
                    create: function (d) {
                        d.data = d.data || {};
                        d.data.update = function () {
                            var id = this.ChurchSubscriptionId(), value = !this.IsSubscribe();
                            xhr.simpleCall({
                                func: 'UpdateSubscriptions',
                                data: { userId: storage('sid'), id: id, value: value }
                            }, function (response) {
                                var item = subs.find(id);
                                if (item) item.IsSubscribe(value);
                                subs.save();
                            });
                        }
                        return ko.mapping.fromJS(d.data);
                    }
                }

            });
        },
        save: function () {
            storage('subscription', ko.mapping.toJS(subs.subscription));
        },
        refresh: function (success) {
            xhr.simpleCall({
                func: 'GetSubscriptions',
                data: {
                    UserId: storage('sid')
                }
            }, function (response) {
                ko.mapping.fromJS(response, {}, subs.subscription);
                subs.save();
                if (success) success(response);
            });
        },
        get: function (success) {
            if (subs.subscription == null || subs.subscription.Churches == null || subs.subscription.Churches().length == 0)
                subs.refresh(success);
            else
                success(subs.subscription);
        },
        find: function (id) {
            var ch = subs.subscription.Churches();
            for (var i = 0; i < ch.length; i++) {
                for (var ix = 0; ix < ch[i].Subscription().length; ix++)
                    if (ch[i].Subscription()[ix].ChurchSubscriptionId() == id)
                        return ch[i].Subscription()[ix];
            }
        },
        update: function (id, value, success) {
            xhr.simpleCall({
                func: 'UpdateSubscriptions',
                data: { userId: storage('sid'), id: id, value: value }
            }, function (response) {
                var item = subs.find(id);
                if (item) item.IsSubscribe(value);
                if (success) success(response);
            });
        }
    }

    subs.init();

    return subs;
});