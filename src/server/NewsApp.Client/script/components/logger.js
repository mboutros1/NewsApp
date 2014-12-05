define([], function () {
    var toCleanJson = function (obj, noNull, options) {
        if (obj.__ko_mapping__)
            return ko.mapping.toJSON(obj, options);
        return noNull ? JSON.stringify(obj, noNullStringify, 2) : JSON.stringify(obj);
    }
    var noNullStringify = function (key, value) {

        if (value == null || value == 0 || value == "0")
            return null;
        if ($.isArray(value) && value.length == 0) return null;
        return value == {} ? null : value;
    }
    var that = {
        enableJavascriptDebugging: true,
        errorUrl: "",
        lineBreak: '\r\n',
        jsErrorUrl: "",
        devMode: true,
        log: function (log) {
            var result = $$("#result");
            if (typeof log == 'object') log = JSON.stringify(log);
            result.html(result.html() + "<br/>" + log);
            console.log(log);
            this.list.push(log);
            storage("logs", that.list);

        },
        clear: function () {
            localStorage.removeItem("logs");
            this.list = [];
        },
        addHandler: function (handler) {
            if (this.enableJavascriptDebugging) {
                window.onerror = function (message, url, line) {
                    handler(message, arguments.callee.trace());
                };
                if (RegExp("Safari|Opera").test(navigator.userAgent)
                    && !RegExp("Chrome").test(navigator.userAgent)) {
                    var originalError = window.Error;
                    window.Error = function () {
                        if (arguments.length > 0)
                            handler(arguments[0], arguments.callee.trace());
                        return originalError.apply(this, arguments);
                    };
                }
            }
        },
        getErrorMsgStack: function (xhr, st, obj, errObj) {
            var errorMessage = errObj.stack;
            if (obj) errorMessage += 'caller: ' + toCleanJson(obj) + this.lineBreak;
            var eAll = (window.location && window.location.pathname) ? window.location.pathname + this.lineBreak : '';
            eAll += st + this.lineBreak + errorMessage.replace(/\\/g, "|").replace(/}/g, "|").replace(/{}/g, "|");
            return eAll;
        },
        enableJSErrorLog: function () {
            return this.enableJavascriptDebugging && this.errorUrl != '' && this.jsErrorUrl != '';
        },
        getError: function (err, st, obj, logtype) {
            var value, errObj = this.getErrorObject(err);
            if (typeof err == 'string')
                value = {
                    message: err, stack: err, original: err
                }
            else
                value = {
                    url: errObj.url,
                    message: (errObj.message + st).replace(/\\/g, "|").replace(/}/g, "|").replace(/{}/g, "|"),
                    stack: this.getErrorMsgStack(err, st, obj, errObj), original: err,
                    statusCode: errObj.statusCode
                };
            if (logtype)
                logger.logError(value.stack, 'SVC_500');
            if (!value.message || value.message == 'undefined') {
                var url = value.url || obj.url;
                if (url && url != '') {
                    var indexOf = url.indexOf("://");
                    if (indexOf > -1) {
                        url = url.substr(indexOf + 3);
                        indexOf = url.indexOf("/");
                        if (indexOf > -1) url = url.substr(indexOf + 1);
                    }
                    value.url = url;
                    value.data = value.data || obj.data;
                    value.message = "[XHR]:Failed " + value.url + (value.data ? (" " + JSON.stringify(value.data)) : '');
                } else
                    value.message = '';
            }
            return value;
        },
        getErrorObject: function (xhr) {
            if (typeof xhr.responseText == 'undefined')
                if (xhr.readyState < 4) xhr.responseText = "Connection error";
                else if (typeof xhr.stack != 'undefined' && typeof xhr.message != 'undefined') {
                    return xhr;
                }
            var returnDefault = function (xhr) {
                return {
                    url: xhr.url,
                    data: xhr.data,
                    message: response,
                    stack: response,
                    original: xhr
                }
            }
            var response = xhr.responseText;
            if (response == "") return returnDefault(xhr); 
            try {
                response = JSON.parse(response);
                return response;
            } catch (e) {
                return returnDefault(xhr);
            }
        }, displayHvrErr: function (text) {
            if ((typeof text == 'undefined') || text == '') return;
            $$('.load-result').html(text).css('opacity', '1').transition(500);
            setTimeout(function () {
                $$('.load-result').css('opacity', '0').transition(1500);
            }, 3100);
        }, runQeue: function () {
            var xhr = require('utils/xhr');
            if (xhr.qeue && xhr.qeue.length > 0) {
                xhr.fireQeue();
                console.log('Quee running');
            }
            setTimeout(logger.runQeue, 10000);
        }
    }
    that.list = [];
    var savedValue = JSON.parse(localStorage.getItem("logs"));
    if (Array.isArray(savedValue))
        that.list = savedValue;
    console.log('log created');
    return that;
})

