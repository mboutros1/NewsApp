define(['utils/appFunc',
        'i18n!nls/lang',
        'components/networkStatus'], function (appFunc, i18n, networkStatus) {
            var xhr = {
                search: function (code, array) {
                    for (var i = 0; i < array.length; i++) {
                        if (array[i].code === code) {
                            return array[i];
                        }
                    }
                    return false;
                },
                showLoadResult: function (text, st) {
                    hiApp.hideIndicator();
                    hiApp.hidePreloader();
                    var msg = "";
                    if ((text.status < 200 || text.status > 300) && text.status != 304)
                        msg = "No Internet Connection! ";
                    if (logger.devMode || msg == '')
                        msg += logger.getError(text, st, this).message;
                    cl(msg);
                    logger.displayHvrErr(msg);
                    if (this.original.error) this.original.error();
                },
                getRequestURL: function (options) {
                    var baseUrl = thisApp.baseUrl;
                    var query = options.query || {};
                    var func = options.func || '';
                    var url = "";
                    switch (options.func) {
                        case 'user_login':
                        case 'LoginFb':
                        case 'Merge':
                        case 'GetUserInfo':
                            url = baseUrl + 'Account/';
                            if (options.func == 'user_login') func = 'Login';
                            break;
                        case "Comment":
                        case "Like":
                        case "Dislike":
                        case "Post":
                        case "GetFeed":
                        case "GetInitFeed":
                        case "GetComments":
                            url = baseUrl + 'NewsFeed/';
                            break;
                        case "GetSubscriptions":
                        case "UpdateSubscriptions":
                            url = baseUrl + 'Church/';
                            break;
                        default:
                            url = baseUrl + 'Home/';
                            break;
                    }
                    var apiServer = url + func;
                    (appFunc.isEmpty(query) ? '' : '?');
                    var name;
                    for (name in query) {
                        apiServer += name + '=' + query[name] + '&';
                    }
                    return apiServer.replace(/&$/gi, '');
                },
                simpleCall: function (options, callback) {
                    options = options || {};
                    options.data = options.data ? options.data : '';

                    //If you access your server api ,please user `post` method.
                    options.method = options.method || 'GET';
                    //options.method = options.method || 'POST';
                    if (appFunc.isPhonegap()) {
                        //Check network connection
                        try {
                            var network = networkStatus.checkConnection();
                            if (network === 'NoNetwork') {

                                hiApp.alert(i18n.error.no_network, function () {
                                    hiApp.hideIndicator();
                                    hiApp.hidePreloader();
                                });
                                return false;
                            }
                        } catch (e) {

                        }

                    }
                    options.url = xhr.getRequestURL(options);
                    logger.log('[XHR]' + options.url);
                    var thisXhr = $$.ajax({
                        url: options.url,
                        method: options.method,
                        original: options,
                        crossDomain: true,
                        data: options.data,
                        error: this.showLoadResult,
                        complete: options.complete,
                        success: function (data) {
                            thisXhr.isgood = true;
                            data = data ? JSON.parse(data) : '';
                            var codes = [
                                                         { code: 10000, message: 'Your session is invalid, please login again', path: '/' },
                                                         { code: 10001, message: 'Unknown error,please login again', path: 'tpl/login.html' },
                                                         { code: 20001, message: 'User name or password does not match', path: '/' }
                            ];

                            var codeLevel = xhr.search(data.err_code, codes);

                            if (!codeLevel) {
                                try {
                                    (typeof (callback) === 'function') ? callback(data) : '';

                                } catch (ex) {
                                    logger.log(ex.message);
                                } finally {
                                    hiApp.hideIndicator();
                                    hiApp.hidePreloader();
                                }

                            } else {

                                hiApp.alert(codeLevel.message, function () {
                                    if (codeLevel.path !== '/')
                                        mainView.loadPage(codeLevel.path);
                                    hiApp.hideIndicator();
                                    hiApp.hidePreloader();
                                });
                            }
                        }
                    });
                    setTimeout(function () {
                        if (thisXhr.isgood) return;
                        thisXhr.abort();
                        xhr.original = options;
                        xhr.showLoadResult(thisXhr);
                    }, 10000);

                },
                enqeue: function (options, success) {
                    var lst = (this.qeue || storage('qeue')) || [];
                    options.funcName = success;
                    var found = false;
                    var str = JSON.stringify(options);
                    for (var i = 0; i < lst.length; i++) {
                        if (str == JSON.stringify(lst[1])) found = true;
                    }
                    if (found) return;
                    lst.push(options);
                    storage('qeue', lst);
                    this.qeue = lst;
                }, fireQeue: function () {
                    var lst = (this.qeue || storage('qeue')) || [];
                    if (lst.length == 0) return;
                    var that = this;
                    var doItem = function (option) {
                        option.async = true;
                        that.simpleCall(option, function (response) {
                            if (typeof (option.funcName) == 'string') {
                                option.funcName = option.funcName + '(' + JSON.stringify(response) + ')';
                                eval(option.funcName);
                            }
                            lst.pop(option);
                            storage('qeue', lst);
                            that.qeue = lst;
                        });
                    }
                    for (var i = lst.length - 1; i == 0; i--) {
                        doItem(lst[i]);
                    }
                }
            };

            return xhr;
        });
//$$.ajaxPrefilter(function (options, originalOptions, jqXHR) {

//    // retry not set or less than 2 : retry not requested
//    if (!originalOptions.retryMax || !originalOptions.retryMax >= 2) return;
//    // no timeout was setup
//    if (!originalOptions.timeout > 0) return;

//    if (originalOptions.retryCount) {
//        // increment retry count each time
//        originalOptions.retryCount++;
//    } else {
//        // init the retry count if not set
//        originalOptions.retryCount = 1;
//        // copy original error callback on first time
//        originalOptions._error = originalOptions.error;
//    };

//    // overwrite error handler for current request
//    options.error = function (_jqXHR, _textStatus, _errorThrown) {
//        // retry max was exhausted or it is not a timeout error
//        if (originalOptions.retryCount >= originalOptions.retryMax || _textStatus != 'timeout') {
//            // call original error handler if any
//            if (originalOptions._error) originalOptions._error(_jqXHR, _textStatus, _errorThrown);
//            return;
//        };
//        // Call AJAX again with original options
//        $$.ajax(originalOptions);
//    };
//});