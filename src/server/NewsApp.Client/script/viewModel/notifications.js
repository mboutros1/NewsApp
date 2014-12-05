define(['utils/xhr'], function (xhr) {
    var note = {
        key:'notification',
        init: function () {
            this.list = storage(note.key) || {};
            note.list = ko.mapping.fromJS(note.list, {
                'Subscription': {
                    create: function (d) {
                        d.data = d.data || {};
                        d.data.update = function () {
                            var id = this.ChurchSubscriptionId(), value = !this.IsSubscribe();
                            xhr.simpleCall({
                                func: 'UpdateSubscriptions',
                                data: { userId: storage('sid'), id: id, value: value }
                            }, function (response) {
                                var item = note.find(id);
                                if (item) item.IsSubscribe(value);
                                note.save();
                            });
                        }
                        return ko.mapping.fromJS(d.data);
                    }
                }

            });
        },
        save: function () {
            storage(note.key, ko.mapping.toJS(note.list));
        },
        refresh: function (success) {
            xhr.simpleCall({
                func: 'GetNotification',
                data: {
                    UserId: storage('sid')
                }
            }, function (response) {
                ko.mapping.fromJS(response, {}, note.list);
                note.save();
                if (success) success(response);
            });
        },  
        seen: function (ids) {
            xhr.simpleCall({
                func: 'Seen',
                data: { userId: storage('sid'), id: id }
            }, function (response) {
                var item = note.find(id);
                if (item) item.IsSubscribe(value);
                if (success) success(response);
            });
        }
    }

    note.init();

    return note;
});