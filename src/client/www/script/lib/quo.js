﻿/* QuoJS v2.3.6 - 2013/5/13
  http://quojs.tapquo.com
  Copyright (c) 2013 Javi Jimenez Villar (@soyjavi) - Licensed MIT */
(function () {
    var e;
    e = function () {
        var e, t, n;
        t = [];
        e = function (t, r) {
            var i;
            if (!t) {
                return n()
            } else if (e.toType(t) === "function") {
                return e(document).ready(t)
            } else {
                i = e.getDOMObject(t, r);
                return n(i, t)
            }
        };
        n = function (e, r) {
            e = e || t;
            e.__proto__ = n.prototype;
            e.selector = r || "";
            return e
        };
        e.extend = function (e) {
            Array.prototype.slice.call(arguments, 1).forEach(function (t) {
                var n, r;
                r = [];
                for (n in t) {
                    r.push(e[n] = t[n])
                }
                return r
            });
            return e
        };
        n.prototype = e.fn = {};
        return e
    }();
    window.Quo = e;
    "$$" in window || (window.$$ = e)
}).call(this);
(function () {
    (function (e) {
        var t, n, r, i, u, a, o, s, c, f, l;
        t = { TYPE: "GET", MIME: "json" };
        r = { script: "text/javascript, application/javascript", json: "application/json", xml: "application/xml, text/xml", html: "text/html", text: "text/plain" };
        n = 0;
        e.ajaxSettings = {
            type: t.TYPE, async: true, success: {}, error: {}, context: null, dataType: t.MIME, headers: {}, xhr: function () {
                return new window.XMLHttpRequest
            }, crossDomain: false, timeout: 0
        };
        e.ajax = function (n) {
            var r, o, f, h;
            f = e.mix(e.ajaxSettings, n);
            if (f.type === t.TYPE) {
                f.url += e.serializeParameters(f.data, "?")
            } else {
                f.data = e.serializeParameters(f.data)
            }
            if (i(f.url)) {
                return e.jsonp(f)
            }
            h = f.xhr();
            h.onreadystatechange = function () {
                if (h.readyState === 4) {
                    clearTimeout(r);
                    return c(h, f)
                }
            };
            h.open(f.type, f.url, f.async);
            s(h, f);
            if (f.timeout > 0) {
                r = setTimeout(function () {
                    return l(h, f)
                }, f.timeout)
            }
            try {
                h.send(f.data)
            } catch (d) {
                o = d;
                h = o;
                a("Resource not found", h, f)
            }
            if (f.async) {
                return h
            } else {
                return u(h, f)
            }
        };
        e.jsonp = function (t) {
            var r, i, u, a;
            if (t.async) {
                i = "jsonp" + ++n;
                u = document.createElement("script");
                a = {
                    abort: function () {
                        e(u).remove();
                        if (i in window) {
                            return window[i] = {}
                        }
                    }
                };
                r = void 0;
                window[i] = function (n) {
                    clearTimeout(r);
                    e(u).remove();
                    delete window[i];
                    return f(n, a, t)
                };
                u.src = t.url.replace(RegExp("=\\?"), "=" + i);
                e("head").append(u);
                if (t.timeout > 0) {
                    r = setTimeout(function () {
                        return l(a, t)
                    }, t.timeout)
                }
                return a
            } else {
                return console.error("QuoJS.ajax: Unable to make jsonp synchronous call.")
            }
        };
        e.get = function (t, n, r, i) {
            return e.ajax({ url: t, data: n, success: r, dataType: i })
        };
        e.post = function (e, t, n, r) {
            return o("POST", e, t, n, r)
        };
        e.put = function (e, t, n, r) {
            return o("PUT", e, t, n, r)
        };
        e["delete"] = function (e, t, n, r) {
            return o("DELETE", e, t, n, r)
        };
        e.json = function (n, r, i) {
            return e.ajax({ url: n, data: r, success: i, dataType: t.MIME })
        };
        e.serializeParameters = function (e, t) {
            var n, r;
            if (t == null) {
                t = ""
            }
            r = t;
            for (n in e) {
                if (e.hasOwnProperty(n)) {
                    if (r !== t) {
                        r += "&"
                    }
                    r += "" + encodeURIComponent(n) + "=" + encodeURIComponent(e[n])
                }
            }
            if (r === t) {
                return ""
            } else {
                return r
            }
        };
        c = function (e, t) {
            if (e.status >= 200 && e.status < 300 || e.status === 0) {
                if (t.async) {
                    f(u(e, t), e, t)
                }
            } else {
                a("QuoJS.ajax: Unsuccesful request", e, t)
            }
        };
        f = function (e, t, n) {
            n.success.call(n.context, e, t)
        };
        a = function (e, t, n) {
            n.error.call(n.context, e, t, n)
        };
        s = function (e, t) {
            var n;
            if (t.contentType) {
                t.headers["Content-Type"] = t.contentType
            }
            if (t.dataType) {
                t.headers["Accept"] = r[t.dataType]
            }
            for (n in t.headers) {
                e.setRequestHeader(n, t.headers[n])
            }
        };
        l = function (e, t) {
            e.onreadystatechange = {};
            e.abort();
            a("QuoJS.ajax: Timeout exceeded", e, t)
        };
        o = function (t, n, r, i, u) {
            return e.ajax({ type: t, url: n, data: r, success: i, dataType: u, contentType: "application/x-www-form-urlencoded" })
        };
        u = function (e, n) {
            var r, i;
            i = e.responseText;
            if (i) {
                if (n.dataType === t.MIME) {
                    try {
                        i = JSON.parse(i)
                    } catch (u) {
                        r = u;
                        i = r;
                        a("QuoJS.ajax: Parse Error", e, n)
                    }
                } else {
                    if (n.dataType === "xml") {
                        i = e.responseXML
                    }
                }
            }
            return i
        };
        return i = function (e) {
            return RegExp("=\\?").test(e)
        }
    })(Quo)
}).call(this);
(function () {
    (function (e) {
        var t, n, r, i, u, a, o, s;
        t = [];
        i = Object.prototype;
        r = /^\s*<(\w+|!)[^>]*>/;
        u = document.createElement("table");
        a = document.createElement("tr");
        n = { tr: document.createElement("tbody"), tbody: u, thead: u, tfoot: u, td: a, th: a, "*": document.createElement("div") };
        e.toType = function (e) {
            return i.toString.call(e).match(/\s([a-z|A-Z]+)/)[1].toLowerCase()
        };
        e.isOwnProperty = function (e, t) {
            return i.hasOwnProperty.call(e, t)
        };
        e.getDOMObject = function (t, n) {
            var i, u, a;
            i = null;
            u = [1, 9, 11];
            a = e.toType(t);
            if (a === "array") {
                i = o(t)
            } else if (a === "string" && r.test(t)) {
                i = e.fragment(t.trim(), RegExp.$1);
                t = null
            } else if (a === "string") {
                i = e.query(document, t);
                if (n) {
                    if (i.length === 1) {
                        i = e.query(i[0], n)
                    } else {
                        i = e.map(function () {
                            return e.query(i, n)
                        })
                    }
                }
            } else if (u.indexOf(t.nodeType) >= 0 || t === window) {
                i = [t];
                t = null
            }
            return i
        };
        e.map = function (t, n) {
            var r, i, u, a;
            a = [];
            r = void 0;
            i = void 0;
            if (e.toType(t) === "array") {
                r = 0;
                while (r < t.length) {
                    u = n(t[r], r);
                    if (u != null) {
                        a.push(u)
                    }
                    r++
                }
            } else {
                for (i in t) {
                    u = n(t[i], i);
                    if (u != null) {
                        a.push(u)
                    }
                }
            }
            return s(a)
        };
        e.each = function (t, n) {
            var r, i;
            r = void 0;
            i = void 0;
            if (e.toType(t) === "array") {
                r = 0;
                while (r < t.length) {
                    if (n.call(t[r], r, t[r]) === false) {
                        return t
                    }
                    r++
                }
            } else {
                for (i in t) {
                    if (n.call(t[i], i, t[i]) === false) {
                        return t
                    }
                }
            }
            return t
        };
        e.mix = function () {
            var t, n, r, i, u;
            r = {};
            t = 0;
            i = arguments.length;
            while (t < i) {
                n = arguments[t];
                for (u in n) {
                    if (e.isOwnProperty(n, u) && n[u] !== undefined) {
                        r[u] = n[u]
                    }
                }
                t++
            }
            return r
        };
        e.fragment = function (t, r) {
            var i;
            if (r == null) {
                r = "*"
            }
            if (!(r in n)) {
                r = "*"
            }
            i = n[r];
            i.innerHTML = "" + t;
            return e.each(Array.prototype.slice.call(i.childNodes), function () {
                return i.removeChild(this)
            })
        };
        e.fn.map = function (t) {
            return e.map(this, function (e, n) {
                return t.call(e, n, e)
            })
        };
        e.fn.instance = function (e) {
            return this.map(function () {
                return this[e]
            })
        };
        e.fn.filter = function (t) {
            return e([].filter.call(this, function (n) {
                return n.parentNode && e.query(n.parentNode, t).indexOf(n) >= 0
            }))
        };
        e.fn.forEach = t.forEach;
        e.fn.indexOf = t.indexOf;
        o = function (e) {
            return e.filter(function (e) {
                return e !== void 0 && e !== null
            })
        };
        return s = function (e) {
            if (e.length > 0) {
                return [].concat.apply([], e)
            } else {
                return e
            }
        }
    })(Quo)
}).call(this);
(function () {
    (function (e) {
        e.fn.attr = function (t, n) {
            if (this.length === 0) {
                null
            }
            if (e.toType(t) === "string" && n === void 0) {
                return this[0].getAttribute(t)
            } else {
                return this.each(function () {
                    return this.setAttribute(t, n)
                })
            }
        };
        e.fn.removeAttr = function (e) {
            return this.each(function () {
                return this.removeAttribute(e)
            })
        };
        e.fn.data = function (e, t) {
            return this.attr("data-" + e, t)
        };
        e.fn.removeData = function (e) {
            return this.removeAttr("data-" + e)
        };
        e.fn.val = function (t) {
            if (e.toType(t) === "string") {
                return this.each(function () {
                    return this.value = t
                })
            } else {
                if (this.length > 0) {
                    return this[0].value
                } else {
                    return null
                }
            }
        };
        e.fn.show = function () {
            return this.style("display", "block")
        };
        e.fn.hide = function () {
            return this.style("display", "none")
        };
        e.fn.height = function () {
            var e;
            e = this.offset();
            return e.height
        };
        e.fn.width = function () {
            var e;
            e = this.offset();
            return e.width
        };
        e.fn.offset = function () {
            var e;
            e = this[0].getBoundingClientRect();
            return { left: e.left + window.pageXOffset, top: e.top + window.pageYOffset, width: e.width, height: e.height }
        };
        return e.fn.remove = function () {
            return this.each(function () {
                if (this.parentNode != null) {
                    return this.parentNode.removeChild(this)
                }
            })
        }
    })(Quo)
}).call(this);
(function () {
    (function (e) {
        var t, n, r, i, u, a, o;
        r = null;
        t = /WebKit\/([\d.]+)/;
        n = { Android: /(Android)\s+([\d.]+)/, ipad: /(iPad).*OS\s([\d_]+)/, iphone: /(iPhone\sOS)\s([\d_]+)/, Blackberry: /(BlackBerry|BB10|Playbook).*Version\/([\d.]+)/, FirefoxOS: /(Mozilla).*Mobile[^\/]*\/([\d\.]*)/, webOS: /(webOS|hpwOS)[\s\/]([\d.]+)/ };
        e.isMobile = function () {
            r = r || u();
            return r.isMobile && r.os.name !== "firefoxOS"
        };
        e.environment = function () {
            r = r || u();
            return r
        };
        e.isOnline = function () {
            return navigator.onLine
        };
        u = function () {
            var e, t;
            t = navigator.userAgent;
            e = {};
            e.browser = i(t);
            e.os = a(t);
            e.isMobile = !!e.os;
            e.screen = o();
            return e
        };
        i = function (e) {
            var n;
            n = e.match(t);
            if (n) {
                return n[0]
            } else {
                return e
            }
        };
        a = function (e) {
            var t, r, i;
            t = null;
            for (r in n) {
                i = e.match(n[r]);
                if (i) {
                    t = { name: r === "iphone" || r === "ipad" ? "ios" : r, version: i[2].replace("_", ".") };
                    break
                }
            }
            return t
        };
        return o = function () {
            return { width: window.innerWidth, height: window.innerHeight }
        }
    })(Quo)
}).call(this);
(function () {
    (function (e) {
        var t, n, r, i, u, a, o, s, c, f, l, h;
        t = 1;
        i = {};
        r = { preventDefault: "isDefaultPrevented", stopImmediatePropagation: "isImmediatePropagationStopped", stopPropagation: "isPropagationStopped" };
        n = { touchstart: "mousedown", touchmove: "mousemove", touchend: "mouseup", touch: "click", doubletap: "dblclick", orientationchange: "resize" };
        u = /complete|loaded|interactive/;
        e.fn.on = function (t, n, r) {
            if (n === "undefined" || e.toType(n) === "function") {
                return this.bind(t, n)
            } else {
                return this.delegate(n, t, r)
            }
        };
        e.fn.off = function (t, n, r) {
            if (n === "undefined" || e.toType(n) === "function") {
                return this.unbind(t, n)
            } else {
                return this.undelegate(n, t, r)
            }
        };
        e.fn.ready = function (t) {
            if (u.test(document.readyState)) {
                return t(e)
            } else {
                return e.fn.addEvent(document, "DOMContentLoaded", function () {
                    return t(e)
                })
            }
        };
        e.Event = function (e, t) {
            var n, r;
            n = document.createEvent("Events");
            n.initEvent(e, true, true, null, null, null, null, null, null, null, null, null, null, null, null);
            if (t) {
                for (r in t) {
                    n[r] = t[r]
                }
            }
            return n
        };
        e.fn.bind = function (e, t) {
            return this.each(function () {
                l(this, e, t)
            })
        };
        e.fn.unbind = function (e, t) {
            return this.each(function () {
                h(this, e, t)
            })
        };
        e.fn.delegate = function (t, n, r) {
            return this.each(function (i, u) {
                l(u, n, r, t, function (n) {
                    return function (r) {
                        var i, o;
                        o = e(r.target).closest(t, u).get(0);
                        if (o) {
                            i = e.extend(a(r), { currentTarget: o, liveFired: u });
                            return n.apply(o, [i].concat([].slice.call(arguments, 1)))
                        }
                    }
                })
            })
        };
        e.fn.undelegate = function (e, t, n) {
            return this.each(function () {
                h(this, t, n, e)
            })
        };
        e.fn.trigger = function (t, n, r) {
            if (e.toType(t) === "string") {
                t = e.Event(t, n)
            }
            if (r != null) {
                t.originalEvent = r
            }
            return this.each(function () {
                this.dispatchEvent(t)
            })
        };
        e.fn.addEvent = function (e, t, n) {
            if (e.addEventListener) {
                return e.addEventListener(t, n, false)
            } else if (e.attachEvent) {
                return e.attachEvent("on" + t, n)
            } else {
                return e["on" + t] = n
            }
        };
        e.fn.removeEvent = function (e, t, n) {
            if (e.removeEventListener) {
                return e.removeEventListener(t, n, false)
            } else if (e.detachEvent) {
                return e.detachEvent("on" + t, n)
            } else {
                return e["on" + t] = null
            }
        };
        l = function (t, n, r, u, a) {
            var c, l, h, d;
            n = s(n);
            h = f(t);
            l = i[h] || (i[h] = []);
            c = a && a(r, n);
            d = { event: n, callback: r, selector: u, proxy: o(c, r, t), delegate: c, index: l.length };
            l.push(d);
            return e.fn.addEvent(t, d.event, d.proxy)
        };
        h = function (t, n, r, u) {
            var a;
            n = s(n);
            a = f(t);
            return c(a, n, r, u).forEach(function (n) {
                delete i[a][n.index];
                return e.fn.removeEvent(t, n.event, n.proxy)
            })
        };
        f = function (e) {
            return e._id || (e._id = t++)
        };
        s = function (t) {
            var r;
            r = e.isMobile() ? t : n[t];
            return r || t
        };
        o = function (e, t, n) {
            var r;
            t = e || t;
            r = function (e) {
                var r;
                r = t.apply(n, [e].concat(e.data));
                if (r === false) {
                    e.preventDefault()
                }
                return r
            };
            return r
        };
        c = function (e, t, n, r) {
            return (i[e] || []).filter(function (e) {
                return e && (!t || e.event === t) && (!n || e.callback === n) && (!r || e.selector === r)
            })
        };
        return a = function (t) {
            var n;
            n = e.extend({ originalEvent: t }, t);
            e.each(r, function (e, r) {
                n[e] = function () {
                    this[r] = function () {
                        return true
                    };
                    return t[e].apply(t, arguments)
                };
                return n[r] = function () {
                    return false
                }
            });
            return n
        }
    })(Quo)
}).call(this);
(function () {
    (function ($$) {
        var CURRENT_TOUCH, EVENT, FIRST_TOUCH, GESTURE, GESTURES, HOLD_DELAY, TAPS, TOUCH_TIMEOUT, _angle, _capturePinch, _captureRotation, _cleanGesture, _distance, _fingersPosition, _getTouches, _hold, _isSwipe, _listenTouches, _onTouchEnd, _onTouchMove, _onTouchStart, _parentIfText, _swipeDirection, _trigger;
        TAPS = null;
        EVENT = void 0;
        GESTURE = {};
        FIRST_TOUCH = [];
        CURRENT_TOUCH = [];
        TOUCH_TIMEOUT = void 0;
        HOLD_DELAY = 650;
        GESTURES = ["touch", "tap", "singleTap", "doubleTap", "hold", "swipe", "swiping", "swipeLeft", "swipeRight", "swipeUp", "swipeDown", "rotate", "rotating", "rotateLeft", "rotateRight", "pinch", "pinching", "pinchIn", "pinchOut", "drag", "dragLeft", "dragRight", "dragUp", "dragDown"];
        GESTURES.forEach(function (e) {
            $$.fn[e] = function (t) {
                var n;
                n = e === "touch" ? "touchend" : e;
                return $$(document.body).delegate(this.selector, n, t)
            };
            return this
        });
        $$(document).ready(function () {
            return _listenTouches()
        });
        _listenTouches = function () {
            var e;
            e = $$(document.body);
            e.bind("touchstart", _onTouchStart);
            e.bind("touchmove", _onTouchMove);
            e.bind("touchend", _onTouchEnd);
            return e.bind("touchcancel", _cleanGesture)
        };
        _onTouchStart = function (e) {
            var t, n, r, i;
            EVENT = e;
            r = Date.now();
            t = r - (GESTURE.last || r);
            TOUCH_TIMEOUT && clearTimeout(TOUCH_TIMEOUT);
            i = _getTouches(e);
            n = i.length;
            FIRST_TOUCH = _fingersPosition(i, n);
            GESTURE.el = $$(_parentIfText(i[0].target));
            GESTURE.fingers = n;
            GESTURE.last = r;
            if (!GESTURE.taps) {
                GESTURE.taps = 0
            }
            GESTURE.taps++;
            if (n === 1) {
                if (n >= 1) {
                    GESTURE.gap = t > 0 && t <= 250
                }
                return setTimeout(_hold, HOLD_DELAY)
            } else if (n === 2) {
                GESTURE.initial_angle = parseInt(_angle(FIRST_TOUCH), 10);
                GESTURE.initial_distance = parseInt(_distance(FIRST_TOUCH), 10);
                GESTURE.angle_difference = 0;
                return GESTURE.distance_difference = 0
            }
        };
        _onTouchMove = function (e) {
            var t, n, r;
            EVENT = e;
            if (GESTURE.el) {
                r = _getTouches(e);
                t = r.length;
                if (t === GESTURE.fingers) {
                    CURRENT_TOUCH = _fingersPosition(r, t);
                    n = _isSwipe(e);
                    if (n) {
                        GESTURE.prevSwipe = true
                    }
                    if (n || GESTURE.prevSwipe === true) {
                        _trigger("swiping")
                    }
                    if (t === 2) {
                        _captureRotation();
                        _capturePinch();
                        e.preventDefault()
                    }
                } else {
                    _cleanGesture()
                }
            }
            return true
        };
        _isSwipe = function (e) {
            var t, n, r;
            t = false;
            if (CURRENT_TOUCH[0]) {
                n = Math.abs(FIRST_TOUCH[0].x - CURRENT_TOUCH[0].x) > 30;
                r = Math.abs(FIRST_TOUCH[0].y - CURRENT_TOUCH[0].y) > 30;
                t = GESTURE.el && (n || r)
            }
            return t
        };
        _onTouchEnd = function (e) {
            var t, n, r, i, u;
            EVENT = e;
            _trigger("touch");
            if (GESTURE.fingers === 1) {
                if (GESTURE.taps === 2 && GESTURE.gap) {
                    _trigger("doubleTap");
                    _cleanGesture()
                } else if (_isSwipe() || GESTURE.prevSwipe) {
                    _trigger("swipe");
                    u = _swipeDirection(FIRST_TOUCH[0].x, CURRENT_TOUCH[0].x, FIRST_TOUCH[0].y, CURRENT_TOUCH[0].y);
                    _trigger("swipe" + u);
                    _cleanGesture()
                } else {
                    _trigger("tap");
                    if (GESTURE.taps === 1) {
                        TOUCH_TIMEOUT = setTimeout(function () {
                            _trigger("singleTap");
                            return _cleanGesture()
                        }, 100)
                    }
                }
            } else {
                t = false;
                if (GESTURE.angle_difference !== 0) {
                    _trigger("rotate", { angle: GESTURE.angle_difference });
                    i = GESTURE.angle_difference > 0 ? "rotateRight" : "rotateLeft";
                    _trigger(i, { angle: GESTURE.angle_difference });
                    t = true
                }
                if (GESTURE.distance_difference !== 0) {
                    _trigger("pinch", { angle: GESTURE.distance_difference });
                    r = GESTURE.distance_difference > 0 ? "pinchOut" : "pinchIn";
                    _trigger(r, { distance: GESTURE.distance_difference });
                    t = true
                }
                if (!t && CURRENT_TOUCH[0]) {
                    if (Math.abs(FIRST_TOUCH[0].x - CURRENT_TOUCH[0].x) > 10 || Math.abs(FIRST_TOUCH[0].y - CURRENT_TOUCH[0].y) > 10) {
                        _trigger("drag");
                        n = _swipeDirection(FIRST_TOUCH[0].x, CURRENT_TOUCH[0].x, FIRST_TOUCH[0].y, CURRENT_TOUCH[0].y);
                        _trigger("drag" + n)
                    }
                }
                _cleanGesture()
            }
            return EVENT = void 0
        };
        _fingersPosition = function (e, t) {
            var n, r;
            r = [];
            n = 0;
            e = e[0].targetTouches ? e[0].targetTouches : e;
            while (n < t) {
                r.push({ x: e[n].pageX, y: e[n].pageY });
                n++
            }
            return r
        };
        _captureRotation = function () {
            var angle, diff, i, symbol;
            angle = parseInt(_angle(CURRENT_TOUCH), 10);
            diff = parseInt(GESTURE.initial_angle - angle, 10);
            if (Math.abs(diff) > 20 || GESTURE.angle_difference !== 0) {
                i = 0;
                symbol = GESTURE.angle_difference < 0 ? "-" : "+";
                while (Math.abs(diff - GESTURE.angle_difference) > 90 && i++ < 10) {
                    eval("diff " + symbol + "= 180;")
                }
                GESTURE.angle_difference = parseInt(diff, 10);
                return _trigger("rotating", { angle: GESTURE.angle_difference })
            }
        };
        _capturePinch = function () {
            var e, t;
            t = parseInt(_distance(CURRENT_TOUCH), 10);
            e = GESTURE.initial_distance - t;
            if (Math.abs(e) > 10) {
                GESTURE.distance_difference = e;
                return _trigger("pinching", { distance: e })
            }
        };
        _trigger = function (e, t) {
            if (GESTURE.el) {
                t = t || {};
                if (CURRENT_TOUCH[0]) {
                    t.iniTouch = GESTURE.fingers > 1 ? FIRST_TOUCH : FIRST_TOUCH[0];
                    t.currentTouch = GESTURE.fingers > 1 ? CURRENT_TOUCH : CURRENT_TOUCH[0]
                }
                return GESTURE.el.trigger(e, t, EVENT)
            }
        };
        _cleanGesture = function (e) {
            FIRST_TOUCH = [];
            CURRENT_TOUCH = [];
            GESTURE = {};
            return clearTimeout(TOUCH_TIMEOUT)
        };
        _angle = function (e) {
            var t, n, r;
            t = e[0];
            n = e[1];
            r = Math.atan((n.y - t.y) * -1 / (n.x - t.x)) * (180 / Math.PI);
            if (r < 0) {
                return r + 180
            } else {
                return r
            }
        };
        _distance = function (e) {
            var t, n;
            t = e[0];
            n = e[1];
            return Math.sqrt((n.x - t.x) * (n.x - t.x) + (n.y - t.y) * (n.y - t.y)) * -1
        };
        _getTouches = function (e) {
            if ($$.isMobile()) {
                return e.touches
            } else {
                return [e]
            }
        };
        _parentIfText = function (e) {
            if ("tagName" in e) {
                return e
            } else {
                return e.parentNode
            }
        };
        _swipeDirection = function (e, t, n, r) {
            var i, u;
            i = Math.abs(e - t);
            u = Math.abs(n - r);
            if (i >= u) {
                if (e - t > 0) {
                    return "Left"
                } else {
                    return "Right"
                }
            } else {
                if (n - r > 0) {
                    return "Up"
                } else {
                    return "Down"
                }
            }
        };
        return _hold = function () {
            if (GESTURE.last && Date.now() - GESTURE.last >= HOLD_DELAY) {
                _trigger("hold");
                return GESTURE.taps = 0
            }
        }
    })(Quo)
}).call(this);
(function () {
    (function (e) {
        e.fn.text = function (t) {
            if (t || e.toType(t) === "number") {
                return this.each(function () {
                    return this.textContent = t
                })
            } else {
                return this[0].textContent
            }
        };
        e.fn.html = function (t) {
            var n;
            n = e.toType(t);
            if (t || n === "number" || n === "string") {
                return this.each(function () {
                    var e, r, i, u;
                    if (n === "string" || n === "number") {
                        return this.innerHTML = t
                    } else {
                        this.innerHTML = null;
                        if (n === "array") {
                            u = [];
                            for (r = 0, i = t.length; r < i; r++) {
                                e = t[r];
                                u.push(this.appendChild(e))
                            }
                            return u
                        } else {
                            return this.appendChild(t)
                        }
                    }
                })
            } else {
                return this[0].innerHTML
            }
        };
        e.fn.append = function (t) {
            var n;
            n = e.toType(t);
            return this.each(function () {
                var e = this;
                if (n === "string") {
                    return this.insertAdjacentHTML("beforeend", t)
                } else if (n === "array") {
                    return t.each(function (t, n) {
                        return e.appendChild(n)
                    })
                } else {
                    return this.appendChild(t)
                }
            })
        };
        e.fn.prepend = function (t) {
            var n;
            n = e.toType(t);
            return this.each(function () {
                var e = this;
                if (n === "string") {
                    return this.insertAdjacentHTML("afterbegin", t)
                } else if (n === "array") {
                    return t.each(function (t, n) {
                        return e.insertBefore(n, e.firstChild)
                    })
                } else {
                    return this.insertBefore(t, this.firstChild)
                }
            })
        };
        e.fn.replaceWith = function (t) {
            var n;
            n = e.toType(t);
            this.each(function () {
                var e = this;
                if (this.parentNode) {
                    if (n === "string") {
                        return this.insertAdjacentHTML("beforeBegin", t)
                    } else if (n === "array") {
                        return t.each(function (t, n) {
                            return e.parentNode.insertBefore(n, e)
                        })
                    } else {
                        return this.parentNode.insertBefore(t, this)
                    }
                }
            });
            return this.remove()
        };
        return e.fn.empty = function () {
            return this.each(function () {
                return this.innerHTML = null
            })
        }
    })(Quo)
}).call(this);
(function () {
    (function (e) {
        var t, n, r, i, u, a;
        r = "parentNode";
        t = /^\.([\w-]+)$/;
        n = /^#[\w\d-]+$/;
        i = /^[\w-]+$/;
        e.query = function (e, r) {
            var u;
            r = r.trim();
            if (t.test(r)) {
                u = e.getElementsByClassName(r.replace(".", ""))
            } else if (i.test(r)) {
                u = e.getElementsByTagName(r)
            } else if (n.test(r) && e === document) {
                u = e.getElementById(r.replace("#", ""));
                if (!u) {
                    u = []
                }
            } else {
                u = e.querySelectorAll(r)
            }
            if (u.nodeType) {
                return [u]
            } else {
                return Array.prototype.slice.call(u)
            }
        };
        e.fn.find = function (t) {
            var n;
            if (this.length === 1) {
                n = Quo.query(this[0], t)
            } else {
                n = this.map(function () {
                    return Quo.query(this, t)
                })
            }
            return e(n)
        };
        e.fn.parent = function (e) {
            var t;
            t = e ? a(this) : this.instance(r);
            return u(t, e)
        };
        e.fn.siblings = function (e) {
            var t;
            t = this.map(function (e, t) {
                return Array.prototype.slice.call(t.parentNode.children).filter(function (e) {
                    return e !== t
                })
            });
            return u(t, e)
        };
        e.fn.children = function (e) {
            var t;
            t = this.map(function () {
                return Array.prototype.slice.call(this.children)
            });
            return u(t, e)
        };
        e.fn.get = function (e) {
            if (e === undefined) {
                return this
            } else {
                return this[e]
            }
        };
        e.fn.first = function () {
            return e(this[0])
        };
        e.fn.last = function () {
            return e(this[this.length - 1])
        };
        e.fn.closest = function (t, n) {
            var r, i;
            i = this[0];
            r = e(t);
            if (!r.length) {
                i = null
            }
            while (i && r.indexOf(i) < 0) {
                i = i !== n && i !== document && i.parentNode
            }
            return e(i)
        };
        e.fn.each = function (e) {
            this.forEach(function (t, n) {
                return e.call(t, n, t)
            });
            return this
        };
        a = function (t) {
            var n;
            n = [];
            while (t.length > 0) {
                t = e.map(t, function (e) {
                    if ((e = e.parentNode) && e !== document && n.indexOf(e) < 0) {
                        n.push(e);
                        return e
                    }
                })
            }
            return n
        };
        return u = function (t, n) {
            if (n === undefined) {
                return e(t)
            } else {
                return e(t).filter(n)
            }
        }
    })(Quo)
}).call(this);
(function () {
    (function (e) {
        var t, n, r;
        t = ["-webkit-", "-moz-", "-ms-", "-o-", ""];
        e.fn.addClass = function (e) {
            return this.each(function () {
                if (!r(e, this.className)) {
                    this.className += " " + e;
                    return this.className = this.className.trim()
                }
            })
        };
        e.fn.removeClass = function (e) {
            return this.each(function () {
                if (!e) {
                    return this.className = ""
                } else {
                    if (r(e, this.className)) {
                        return this.className = this.className.replace(e, " ").replace(/\s+/g, " ").trim()
                    }
                }
            })
        };
        e.fn.toggleClass = function (e) {
            return this.each(function () {
                if (r(e, this.className)) {
                    return this.className = this.className.replace(e, " ")
                } else {
                    this.className += " " + e;
                    return this.className = this.className.trim()
                }
            })
        };
        e.fn.hasClass = function (e) {
            return r(e, this[0].className)
        };
        e.fn.style = function (e, t) {
            if (t) {
                return this.each(function () {
                    return this.style[e] = t
                })
            } else {
                return this[0].style[e] || n(this[0], e)
            }
        };
        e.fn.css = function (e, t) {
            return this.style(e, t)
        };
        e.fn.vendor = function (e, n) {
            var r, i, u, a;
            a = [];
            for (i = 0, u = t.length; i < u; i++) {
                r = t[i];
                a.push(this.style("" + r + e, n))
            }
            return a
        };
        r = function (e, t) {
            var n;
            n = t.split(/\s+/g);
            return n.indexOf(e) >= 0
        };
        return n = function (e, t) {
            return document.defaultView.getComputedStyle(e, "")[t]
        }
    })(Quo)
}).call(this);