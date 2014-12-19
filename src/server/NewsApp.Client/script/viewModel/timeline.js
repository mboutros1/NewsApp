define(['utils/appFunc', 'i18n!nls/lang'],
    function (appFunc, i18n) {
        var tLine = {
            init: function () {
                this.data.feeds = storage('feed');
                this.data = ko.mapping.fromJS(this.data, {
                    'feeds': {
                        key: function (d) {
                            return ko.utils.unwrapObservable(d.Id);
                        },
                        create: function (d) {
                            d.data.open = function () {
                                console.log(arguments);
                            }
                            return ko.mapping.fromJS(d.data);
                        }
                    }
                });
            },
            data: {
                i18n: {
                    forward: i18n.timeline.forward,
                    comment: i18n.timeline.comment,
                    like: i18n.timeline.like
                },
                feeds: []
            },
            update: function (newData, elementId) {
                if (arguments.length == 1) {
                    ko.updateThis(this.data, arguments[0]);
                } else {
                    ko.updateThis({ feeds: newData }, elementId);
                    storage('feed', ko.mapping.toJS(this.data.feeds)); 
                } 
            },
            add: function (newData) {
                newData = ko.mapping.fromJS(newData);
                this.data.feeds.extend({ rateLimit: 50 });
                for (var i = 0; i < newData().length; i++)
                    this.data.feeds.push(newData()[i]);

            }
        };
        tLine.init();
        return tLine;
    });