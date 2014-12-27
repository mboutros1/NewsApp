define(['utils/xhr'], function (xhr) {
    var note = {
        key:'notification',
        init: function () {
            this.list = storage(note.key) || {};
            note.list = ko.mapping.fromJS(note.list);
        },
        save: function () {
            storage(note.key, ko.mapping.toJS(note.list));
        },
        insert:function(data) {
            note.list.unshift(data);
            note.save();
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