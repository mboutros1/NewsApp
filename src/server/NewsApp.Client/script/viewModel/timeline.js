define(['utils/appFunc', 'i18n!nls/lang'],
    function (appFunc, i18n) {
        var Feed = function (d) {
            this.Avatar = d.Avatar;
            this.Name = d.Name;
            this.Id = d.Id;
            this.Images = d.Images;
            this.Title = d.Title;
            this.CommentsCount = d.CommentsCount;
            this.LikesCount = d.LikesCount;
            this.IsLiked = d.IsLiked;
            this.CreateDate = d.CreateDate;
            this.open = function () {
                console.log('ddd');
            }
        };
        var tLine = {
            init: function () {
                this.data.feeds = storage('feed') || [];
                this.data = ko.mapping.fromJS(this.data, this.mapping);
            }, mapping: {
                'feeds': {
                    key: function (d) {
                        return ko.utils.unwrapObservable(d.Id);
                    },
                    create: function (d) {
                        return ko.mapping.fromJS(new Feed(d.data));
                    }
                }
            },
            data: {
                i18n: {
                    forward: i18n.timeline.forward,
                    comment: i18n.timeline.comment,
                    like: i18n.timeline.like
                },
                feeds: []
            },
            getFirstIndex: function () {
                if (this.data && this.data.feeds && this.data.feeds().length > 0 && this.data.feeds().length > 9) return this.data.feeds()[0].Id();
                return null;
            },

            getLastIndex: function () {
                if (this.data && this.data.feeds && this.data.feeds().length > 0) return this.data.feeds()[this.data.feeds().length - 1].Id();
                return null;
            },
            update: function (newData) {
                if (arguments.length == 0) {
                    this.data = ko.updateThis(this.data, $$(this.elmentid)[0]);
                }
                else if (arguments.length == 1) {
                    this.elmentid = arguments[0];
                    this.data = ko.updateThis(this.data, $$(arguments[0])[0]);
                } else {
                    if (newData.length == 0) return;
                    ko.mapping.fromJS(newData, {}, this.data.feeds);
                    storage('feed', ko.mapping.toJSON(this.data.feeds));
                }
            },
            elmentid: '',
            innerUpdate: function (newData, addIt) {
                var ndata = [];
                var d = this.data.feeds();
                for (var i = 0; i < newData.length; i++) {
                    var fnd = false;
                    for (var j = 0; j < d.length; j++) {
                        if (d[j].Id() == newData[i].Id)
                            fnd = true;
                    }
                    if (!fnd) ndata.push(newData[i]);
                }
                newData = ko.mapping.fromJS(ndata)();
                this.data.feeds.extend({ rateLimit: 50 });
                for (i = 0; i < newData.length; i++) {
                    if (addIt)
                        this.data.feeds.push(newData[i]);
                    else
                        this.data.feeds.unshift(newData[i]);
                }
                if (this.data.feeds().length < 500)
                    storage('feed', ko.mapping.toJS(this.data.feeds, { 'include': ['Name', 'Avatar', 'Id'] }));

            },
            insert: function (newData) {
                this.innerUpdate(newData, false);
            },
            add: function (newData) {
                this.innerUpdate(newData, true);
            }
        };
        tLine.init();
        return tLine;
    });