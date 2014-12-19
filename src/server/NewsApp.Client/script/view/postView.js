define(['utils/appFunc',
        'i18n!nls/lang',
        'utils/tplManager',
        'components/geolocation',
        'components/camera', 'utils/xhr', 'GS'], function (appFunc, i18n, TM, geo, camera, xhr, GS) {

            var postView = {

                openSendPopup: function () {
                    var sub = GS.sub.get();

                    var renderData = ko.mapping.fromJS({
                        cancel: i18n.global.cancel,
                        send: i18n.global.send,
                        senTweet: i18n.index.sen_tweet,
                        sendPlaceholder: i18n.index.send_placeholder,
                        loadingGeo: i18n.geo.loading_geo,
                        text: "",
                        churches: sub.Churches,
                        selectedChurch: sub.Churches()[0],
                        selectedSubscription: null,
                        postMsg: function () {
                            var text = renderData.text();
                            if (appFunc.getCharLength(text) < 4) {
                                hiApp.alert(i18n.index.err_text_too_short);
                                return;
                            }
                            if (renderData.selectedSubscription() == null) {
                                hiApp.alert(i18n.index.err_text_no_subscription);
                                return;
                            }
                            var imgSrc = $$('#uploadPicPreview>img').attr('src');
                            if (imgSrc.length > 4) {
                                camera.startUpload(imgSrc);
                            } else {

                                hiApp.showPreloader(i18n.index.sending); 
                                xhr.enqeue({
                                    func: 'Post', method: 'POST',
                                    data: {
                                        UserId: GS.getCurrentUser().UserId(), Body: text,
                                        ChurchSubscriptionId: renderData.selectedSubscription().ChurchSubscriptionId()
                                    }
                                }, 'hiApp.pullToRefreshTrigger()');

                                setTimeout(function () {
                                    hiApp.hidePreloader();
                                    hiApp.closeModal('.send-popup'); 
                                }, 1300);
                            }
                        }

                    });
                    var output = $$("#sendPopupTemplate").html();
                    renderData.text('fsdfasdfs');
                    hiApp.popup($$.trim(output));
                    $$(".send-popup ").on('opened', function () {
                        ko.updateThis(renderData, $$(".send-popup ")[0]);
                    });
                    var bindings = [{
                        element: 'div.message-tools .get-position',
                        event: 'click',
                        handler: geo.catchGeoInfo
                    }, {
                        element: '#geoInfo',
                        event: 'click',
                        handler: geo.cleanGeo
                    }, {
                        element: 'div.message-tools .image-upload',
                        event: 'click',
                        handler: camera.getPicture
                    }];

                    appFunc.bindEvents(bindings);
                },

                clearSendPopup: function () {
                    $$('#messageText').val('');
                    $$('#uploadPicPreview>img').attr('src', '');
                    $$('#uploadPicPreview').hide();
                }
            };

            return postView;
        });