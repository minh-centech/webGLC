﻿! function(a, b) {
    "object" == typeof exports && "object" == typeof module ? module.exports = b() : "function" == typeof define && define.amd ? define([], b) : "object" == typeof exports ? exports.jwplayer = b() : a.jwplayer = b()
}(this, function() {
    return function(a) {
        function b(c) {
            if (d[c]) return d[c].exports;
            var e = d[c] = {
                exports: {},
                id: c,
                loaded: !1
            };
            return a[c].call(e.exports, e, e.exports, b), e.loaded = !0, e.exports
        }
        var c = window.webpackJsonpjwplayer;
        window.webpackJsonpjwplayer = function(d, f) {
            for (var g, h, i = 0, j = []; i < d.length; i++) h = d[i], e[h] && j.push.apply(j, e[h]), e[h] = 0;
            for (g in f) a[g] = f[g];
            for (c && c(d, f); j.length;) j.shift().call(null, b)
        };
        var d = {},
            e = {
                0: 0
            };
        return b.e = function(a, c) {
            if (0 === e[a]) return c.call(null, b);
            if (void 0 !== e[a]) e[a].push(c);
            else {
                e[a] = [c];
                var d = document.getElementsByTagName("head")[0],
                    f = document.createElement("script");
                f.type = "text/javascript", f.charset = "utf-8", f.async = !0, f.src = b.p + "" + ({
                    1: "polyfills.promise",
                    2: "polyfills.base64",
                    3: "provider.youtube",
                    4: "provider.dashjs",
                    5: "provider.shaka",
                    6: "provider.cast"
                }[a] || a) + ".js", d.appendChild(f)
            }
        }, b.m = a, b.c = d, b.p = "", b(0)
    }([function(a, b, c) {
        a.exports = c(40)
    }, , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , , function(a, b, c) {
        var d, e;
        d = [c(41), c(174), c(45)], e = function(a, b, c) {
            return window.jwplayer ? window.jwplayer : c.extend(a, b)
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(42), c(48), c(168)], e = function(a, b) {
            return c.p = b.loadFrom(), a.selectPlayer
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(43), c(98), c(45)], e = function(a, b, c) {
            var d = a.selectPlayer,
                e = function() {
                    var a = d.apply(this, arguments);
                    return a ? a : {
                        registerPlugin: function(a, c, d) {
                            "jwpsrv" !== a && b.registerPlugin(a, c, d)
                        }
                    }
                };
            return c.extend(a, {
                selectPlayer: e
            })
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(44), c(45), c(86), c(84), c(80), c(98)], e = function(a, b, c, d, e, f) {
            function g(a) {
                var f = a.getName().name;
                if (!b.find(e, b.matches({
                        name: f
                    }))) {
                    if (!b.isFunction(a.supports)) throw {
                        message: "Tried to register a provider with an invalid object"
                    };
                    e.unshift({
                        name: f,
                        supports: a.supports
                    })
                }
                var g = function() {};
                g.prototype = c, a.prototype = new g, d[f] = a
            }
            var h = [],
                i = 0,
                j = function(b) {
                    var c, d;
                    return b ? "string" == typeof b ? (c = k(b), c || (d = document.getElementById(b))) : "number" == typeof b ? c = h[b] : b.nodeType && (d = b, c = k(d.id)) : c = h[0], c ? c : d ? l(new a(d, m)) : {
                        registerPlugin: f.registerPlugin
                    }
                },
                k = function(a) {
                    for (var b = 0; b < h.length; b++)
                        if (h[b].id === a) return h[b];
                    return null
                },
                l = function(a) {
                    return i++, a.uniqueId = i, h.push(a), a
                },
                m = function(a) {
                    for (var b = h.length; b--;)
                        if (h[b].uniqueId === a.uniqueId) {
                            h.splice(b, 1);
                            break
                        }
                },
                n = {
                    selectPlayer: j,
                    registerProvider: g,
                    availableProviders: e,
                    registerPlugin: f.registerPlugin
                };
            return j.api = n, n
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(46), c(62), c(47), c(48), c(61), c(60), c(45), c(63), c(165), c(166), c(167), c(59)], e = function(a, b, c, d, e, f, g, h, i, j, k, l) {
            var m = function(f, m) {
                var n, o = this,
                    p = !1,
                    q = {};
                g.extend(this, c), this.utils = d, this._ = g, this.Events = c, this.version = l, this.trigger = function(a, b) {
                    return b = g.isObject(b) ? g.extend({}, b) : {}, b.type = a, window.jwplayer && window.jwplayer.debug ? c.trigger.call(o, a, b) : c.triggerSafe.call(o, a, b)
                }, this.dispatchEvent = this.trigger, this.removeEventListener = this.off.bind(this);
                var r = function() {
                    n = new h(f), i(o, n), j(o, n), n.on(a.JWPLAYER_PLAYLIST_ITEM, function() {
                        q = {}
                    }), n.on(a.JWPLAYER_MEDIA_META, function(a) {
                        g.extend(q, a.metadata)
                    }), n.on(a.JWPLAYER_READY, function(a) {
                        p = !0, s.tick("ready"), a.setupTime = s.between("setup", "ready")
                    }), n.on("all", o.trigger)
                };
                r(), k(this), this.id = f.id;
                var s = this._qoe = new e;
                s.tick("init");
                var t = function() {
                    p = !1, q = {}, o.off(), n && n.off(), n && n.playerDestroy && n.playerDestroy()
                };
                return this.getPlugin = function(a) {
                    return o.plugins && o.plugins[a]
                }, this.addPlugin = function(a, b) {
                    this.plugins = this.plugins || {}, this.plugins[a] = b, this.onReady(b.addToPlayer), b.resize && this.onResize(b.resizeHandler)
                }, this.setup = function(a) {
                    return s.tick("setup"), t(), r(), d.foreach(a.events, function(a, b) {
                        var c = o[a];
                        "function" == typeof c && c.call(o, b)
                    }), a.id = o.id, n.setup(a, this), o
                }, this.qoe = function() {
                    var b = n.getItemQoe(),
                        c = s.between("setup", "ready"),
                        d = b.between(a.JWPLAYER_MEDIA_PLAY_ATTEMPT, a.JWPLAYER_MEDIA_FIRST_FRAME);
                    return {
                        setupTime: c,
                        firstFrame: d,
                        player: s.dump(),
                        item: b.dump()
                    }
                }, this.getContainer = function() {
                    return n.getContainer ? n.getContainer() : f
                }, this.getMeta = this.getItemMeta = function() {
                    return q
                }, this.getPlaylistItem = function(a) {
                    if (!d.exists(a)) return n._model.get("playlistItem");
                    var b = o.getPlaylist();
                    return b ? b[a] : null
                }, this.getRenderingMode = function() {
                    return "html5"
                }, this.load = function(a) {
                    var b = this.getPlugin("vast") || this.getPlugin("googima");
                    return b && b.destroy(), n.load(a), o
                }, this.play = function(a, c) {
                    if (g.isBoolean(a) || (c = a), c || (c = {
                            reason: "external"
                        }), a === !0) return n.play(c), o;
                    if (a === !1) return n.pause(), o;
                    switch (a = o.getState()) {
                        case b.PLAYING:
                        case b.BUFFERING:
                            n.pause();
                            break;
                        default:
                            n.play(c)
                    }
                    return o
                }, this.pause = function(a) {
                    return g.isBoolean(a) ? this.play(!a) : this.play()
                }, this.createInstream = function() {
                    return n.createInstream()
                }, this.castToggle = function() {
                    n && n.castToggle && n.castToggle()
                }, this.playAd = this.pauseAd = d.noop, this.remove = function() {
                    return m(o), o.trigger("remove"), t(), o
                }, this
            };
            return m
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [], e = function() {
            var a = {},
                b = Array.prototype,
                c = Object.prototype,
                d = Function.prototype,
                e = b.slice,
                f = b.concat,
                g = c.toString,
                h = c.hasOwnProperty,
                i = b.map,
                j = b.reduce,
                k = b.forEach,
                l = b.filter,
                m = b.every,
                n = b.some,
                o = b.indexOf,
                p = Array.isArray,
                q = Object.keys,
                r = d.bind,
                s = function(a) {
                    return a instanceof s ? a : this instanceof s ? void 0 : new s(a)
                },
                t = s.each = s.forEach = function(b, c, d) {
                    if (null == b) return b;
                    if (k && b.forEach === k) b.forEach(c, d);
                    else if (b.length === +b.length) {
                        for (var e = 0, f = b.length; f > e; e++)
                            if (c.call(d, b[e], e, b) === a) return
                    } else
                        for (var g = s.keys(b), e = 0, f = g.length; f > e; e++)
                            if (c.call(d, b[g[e]], g[e], b) === a) return; return b
                };
            s.map = s.collect = function(a, b, c) {
                var d = [];
                return null == a ? d : i && a.map === i ? a.map(b, c) : (t(a, function(a, e, f) {
                    d.push(b.call(c, a, e, f))
                }), d)
            };
            var u = "Reduce of empty array with no initial value";
            s.reduce = s.foldl = s.inject = function(a, b, c, d) {
                var e = arguments.length > 2;
                if (null == a && (a = []), j && a.reduce === j) return d && (b = s.bind(b, d)), e ? a.reduce(b, c) : a.reduce(b);
                if (t(a, function(a, f, g) {
                        e ? c = b.call(d, c, a, f, g) : (c = a, e = !0)
                    }), !e) throw new TypeError(u);
                return c
            }, s.find = s.detect = function(a, b, c) {
                var d;
                return v(a, function(a, e, f) {
                    return b.call(c, a, e, f) ? (d = a, !0) : void 0
                }), d
            }, s.filter = s.select = function(a, b, c) {
                var d = [];
                return null == a ? d : l && a.filter === l ? a.filter(b, c) : (t(a, function(a, e, f) {
                    b.call(c, a, e, f) && d.push(a)
                }), d)
            }, s.reject = function(a, b, c) {
                return s.filter(a, function(a, d, e) {
                    return !b.call(c, a, d, e)
                }, c)
            }, s.compact = function(a) {
                return s.filter(a, s.identity)
            }, s.every = s.all = function(b, c, d) {
                c || (c = s.identity);
                var e = !0;
                return null == b ? e : m && b.every === m ? b.every(c, d) : (t(b, function(b, f, g) {
                    return (e = e && c.call(d, b, f, g)) ? void 0 : a
                }), !!e)
            };
            var v = s.some = s.any = function(b, c, d) {
                c || (c = s.identity);
                var e = !1;
                return null == b ? e : n && b.some === n ? b.some(c, d) : (t(b, function(b, f, g) {
                    return e || (e = c.call(d, b, f, g)) ? a : void 0
                }), !!e)
            };
            s.size = function(a) {
                return null == a ? 0 : a.length === +a.length ? a.length : s.keys(a).length
            }, s.after = function(a, b) {
                return function() {
                    return --a < 1 ? b.apply(this, arguments) : void 0
                }
            }, s.before = function(a, b) {
                var c;
                return function() {
                    return --a > 0 && (c = b.apply(this, arguments)), 1 >= a && (b = null), c
                }
            };
            var w = function(a) {
                return null == a ? s.identity : s.isFunction(a) ? a : s.property(a)
            };
            s.sortedIndex = function(a, b, c, d) {
                c = w(c);
                for (var e = c.call(d, b), f = 0, g = a.length; g > f;) {
                    var h = f + g >>> 1;
                    c.call(d, a[h]) < e ? f = h + 1 : g = h
                }
                return f
            };
            var v = s.some = s.any = function(b, c, d) {
                c || (c = s.identity);
                var e = !1;
                return null == b ? e : n && b.some === n ? b.some(c, d) : (t(b, function(b, f, g) {
                    return e || (e = c.call(d, b, f, g)) ? a : void 0
                }), !!e)
            };
            s.contains = s.include = function(a, b) {
                return null == a ? !1 : (a.length !== +a.length && (a = s.values(a)), s.indexOf(a, b) >= 0)
            }, s.where = function(a, b) {
                return s.filter(a, s.matches(b))
            }, s.findWhere = function(a, b) {
                return s.find(a, s.matches(b))
            }, s.max = function(a, b, c) {
                if (!b && s.isArray(a) && a[0] === +a[0] && a.length < 65535) return Math.max.apply(Math, a);
                var d = -(1 / 0),
                    e = -(1 / 0);
                return t(a, function(a, f, g) {
                    var h = b ? b.call(c, a, f, g) : a;
                    h > e && (d = a, e = h)
                }), d
            }, s.difference = function(a) {
                var c = f.apply(b, e.call(arguments, 1));
                return s.filter(a, function(a) {
                    return !s.contains(c, a)
                })
            }, s.without = function(a) {
                return s.difference(a, e.call(arguments, 1))
            }, s.indexOf = function(a, b, c) {
                if (null == a) return -1;
                var d = 0,
                    e = a.length;
                if (c) {
                    if ("number" != typeof c) return d = s.sortedIndex(a, b), a[d] === b ? d : -1;
                    d = 0 > c ? Math.max(0, e + c) : c
                }
                if (o && a.indexOf === o) return a.indexOf(b, c);
                for (; e > d; d++)
                    if (a[d] === b) return d;
                return -1
            };
            var x = function() {};
            s.bind = function(a, b) {
                var c, d;
                if (r && a.bind === r) return r.apply(a, e.call(arguments, 1));
                if (!s.isFunction(a)) throw new TypeError;
                return c = e.call(arguments, 2), d = function() {
                    if (!(this instanceof d)) return a.apply(b, c.concat(e.call(arguments)));
                    x.prototype = a.prototype;
                    var f = new x;
                    x.prototype = null;
                    var g = a.apply(f, c.concat(e.call(arguments)));
                    return Object(g) === g ? g : f
                }
            }, s.partial = function(a) {
                var b = e.call(arguments, 1);
                return function() {
                    for (var c = 0, d = b.slice(), e = 0, f = d.length; f > e; e++) d[e] === s && (d[e] = arguments[c++]);
                    for (; c < arguments.length;) d.push(arguments[c++]);
                    return a.apply(this, d)
                }
            }, s.once = s.partial(s.before, 2), s.memoize = function(a, b) {
                var c = {};
                return b || (b = s.identity),
                    function() {
                        var d = b.apply(this, arguments);
                        return s.has(c, d) ? c[d] : c[d] = a.apply(this, arguments)
                    }
            }, s.delay = function(a, b) {
                var c = e.call(arguments, 2);
                return setTimeout(function() {
                    return a.apply(null, c)
                }, b)
            }, s.defer = function(a) {
                return s.delay.apply(s, [a, 1].concat(e.call(arguments, 1)))
            }, s.throttle = function(a, b, c) {
                var d, e, f, g = null,
                    h = 0;
                c || (c = {});
                var i = function() {
                    h = c.leading === !1 ? 0 : s.now(), g = null, f = a.apply(d, e), d = e = null
                };
                return function() {
                    var j = s.now();
                    h || c.leading !== !1 || (h = j);
                    var k = b - (j - h);
                    return d = this, e = arguments, 0 >= k ? (clearTimeout(g), g = null, h = j, f = a.apply(d, e), d = e = null) : g || c.trailing === !1 || (g = setTimeout(i, k)), f
                }
            }, s.keys = function(a) {
                if (!s.isObject(a)) return [];
                if (q) return q(a);
                var b = [];
                for (var c in a) s.has(a, c) && b.push(c);
                return b
            }, s.invert = function(a) {
                for (var b = {}, c = s.keys(a), d = 0, e = c.length; e > d; d++) b[a[c[d]]] = c[d];
                return b
            }, s.defaults = function(a) {
                return t(e.call(arguments, 1), function(b) {
                    if (b)
                        for (var c in b) void 0 === a[c] && (a[c] = b[c])
                }), a
            }, s.extend = function(a) {
                return t(e.call(arguments, 1), function(b) {
                    if (b)
                        for (var c in b) a[c] = b[c]
                }), a
            }, s.pick = function(a) {
                var c = {},
                    d = f.apply(b, e.call(arguments, 1));
                return t(d, function(b) {
                    b in a && (c[b] = a[b])
                }), c
            }, s.omit = function(a) {
                var c = {},
                    d = f.apply(b, e.call(arguments, 1));
                for (var g in a) s.contains(d, g) || (c[g] = a[g]);
                return c
            }, s.clone = function(a) {
                return s.isObject(a) ? s.isArray(a) ? a.slice() : s.extend({}, a) : a
            }, s.isArray = p || function(a) {
                return "[object Array]" == g.call(a)
            }, s.isObject = function(a) {
                return a === Object(a)
            }, t(["Arguments", "Function", "String", "Number", "Date", "RegExp"], function(a) {
                s["is" + a] = function(b) {
                    return g.call(b) == "[object " + a + "]"
                }
            }), s.isArguments(arguments) || (s.isArguments = function(a) {
                return !(!a || !s.has(a, "callee"))
            }), s.isFunction = function(a) {
                return "function" == typeof a
            }, s.isFinite = function(a) {
                return isFinite(a) && !isNaN(parseFloat(a))
            }, s.isNaN = function(a) {
                return s.isNumber(a) && a != +a
            }, s.isBoolean = function(a) {
                return a === !0 || a === !1 || "[object Boolean]" == g.call(a)
            }, s.isNull = function(a) {
                return null === a
            }, s.isUndefined = function(a) {
                return void 0 === a
            }, s.has = function(a, b) {
                return h.call(a, b)
            }, s.identity = function(a) {
                return a
            }, s.constant = function(a) {
                return function() {
                    return a
                }
            }, s.property = function(a) {
                return function(b) {
                    return b[a]
                }
            }, s.propertyOf = function(a) {
                return null == a ? function() {} : function(b) {
                    return a[b]
                }
            }, s.matches = function(a) {
                return function(b) {
                    if (b === a) return !0;
                    for (var c in a)
                        if (a[c] !== b[c]) return !1;
                    return !0
                }
            }, s.now = Date.now || function() {
                return (new Date).getTime()
            }, s.result = function(a, b) {
                if (null != a) {
                    var c = a[b];
                    return s.isFunction(c) ? c.call(a) : c
                }
            };
            var y = 0;
            return s.uniqueId = function(a) {
                var b = ++y + "";
                return a ? a + b : b
            }, s
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [], e = function() {
            var a = {
                    DRAG: "drag",
                    DRAG_START: "dragStart",
                    DRAG_END: "dragEnd",
                    CLICK: "click",
                    DOUBLE_CLICK: "doubleClick",
                    TAP: "tap",
                    DOUBLE_TAP: "doubleTap",
                    OVER: "over",
                    MOVE: "move",
                    OUT: "out"
                },
                b = {
                    COMPLETE: "complete",
                    ERROR: "error",
                    JWPLAYER_AD_CLICK: "adClick",
                    JWPLAYER_AD_COMPANIONS: "adCompanions",
                    JWPLAYER_AD_COMPLETE: "adComplete",
                    JWPLAYER_AD_ERROR: "adError",
                    JWPLAYER_AD_IMPRESSION: "adImpression",
                    JWPLAYER_AD_META: "adMeta",
                    JWPLAYER_AD_PAUSE: "adPause",
                    JWPLAYER_AD_PLAY: "adPlay",
                    JWPLAYER_AD_SKIPPED: "adSkipped",
                    JWPLAYER_AD_TIME: "adTime",
                    JWPLAYER_CAST_AD_CHANGED: "castAdChanged",
                    JWPLAYER_MEDIA_COMPLETE: "complete",
                    JWPLAYER_READY: "ready",
                    JWPLAYER_MEDIA_SEEK: "seek",
                    JWPLAYER_MEDIA_BEFOREPLAY: "beforePlay",
                    JWPLAYER_MEDIA_BEFORECOMPLETE: "beforeComplete",
                    JWPLAYER_MEDIA_BUFFER_FULL: "bufferFull",
                    JWPLAYER_DISPLAY_CLICK: "displayClick",
                    JWPLAYER_PLAYLIST_COMPLETE: "playlistComplete",
                    JWPLAYER_CAST_SESSION: "cast",
                    JWPLAYER_MEDIA_ERROR: "mediaError",
                    JWPLAYER_MEDIA_FIRST_FRAME: "firstFrame",
                    JWPLAYER_MEDIA_PLAY_ATTEMPT: "playAttempt",
                    JWPLAYER_MEDIA_LOADED: "loaded",
                    JWPLAYER_MEDIA_SEEKED: "seeked",
                    JWPLAYER_SETUP_ERROR: "setupError",
                    JWPLAYER_ERROR: "error",
                    JWPLAYER_PLAYER_STATE: "state",
                    JWPLAYER_CAST_AVAILABLE: "castAvailable",
                    JWPLAYER_MEDIA_BUFFER: "bufferChange",
                    JWPLAYER_MEDIA_TIME: "time",
                    JWPLAYER_MEDIA_TYPE: "mediaType",
                    JWPLAYER_MEDIA_VOLUME: "volume",
                    JWPLAYER_MEDIA_MUTE: "mute",
                    JWPLAYER_MEDIA_META: "meta",
                    JWPLAYER_MEDIA_LEVELS: "levels",
                    JWPLAYER_MEDIA_LEVEL_CHANGED: "levelsChanged",
                    JWPLAYER_CONTROLS: "controls",
                    JWPLAYER_FULLSCREEN: "fullscreen",
                    JWPLAYER_RESIZE: "resize",
                    JWPLAYER_PLAYLIST_ITEM: "playlistItem",
                    JWPLAYER_PLAYLIST_LOADED: "playlist",
                    JWPLAYER_AUDIO_TRACKS: "audioTracks",
                    JWPLAYER_AUDIO_TRACK_CHANGED: "audioTrackChanged",
                    JWPLAYER_LOGO_CLICK: "logoClick",
                    JWPLAYER_CAPTIONS_LIST: "captionsList",
                    JWPLAYER_CAPTIONS_CHANGED: "captionsChanged",
                    JWPLAYER_PROVIDER_CHANGED: "providerChanged",
                    JWPLAYER_PROVIDER_FIRST_FRAME: "providerFirstFrame",
                    JWPLAYER_USER_ACTION: "userAction",
                    JWPLAYER_PROVIDER_CLICK: "providerClick",
                    JWPLAYER_VIEW_TAB_FOCUS: "tabFocus",
                    JWPLAYER_CONTROLBAR_DRAGGING: "scrubbing",
                    JWPLAYER_INSTREAM_CLICK: "instreamClick"
                };
            return b.touchEvents = a, b
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45)], e = function(a) {
            var b = [],
                c = b.slice,
                d = {
                    on: function(a, b, c) {
                        if (!f(this, "on", a, [b, c]) || !b) return this;
                        this._events || (this._events = {});
                        var d = this._events[a] || (this._events[a] = []);
                        return d.push({
                            callback: b,
                            context: c
                        }), this
                    },
                    once: function(b, c, d) {
                        if (!f(this, "once", b, [c, d]) || !c) return this;
                        var e = this,
                            g = a.once(function() {
                                e.off(b, g), c.apply(this, arguments)
                            });
                        return g._callback = c, this.on(b, g, d)
                    },
                    off: function(b, c, d) {
                        var e, g, h, i, j, k, l, m;
                        if (!this._events || !f(this, "off", b, [c, d])) return this;
                        if (!b && !c && !d) return this._events = void 0, this;
                        for (i = b ? [b] : a.keys(this._events), j = 0, k = i.length; k > j; j++)
                            if (b = i[j], h = this._events[b]) {
                                if (this._events[b] = e = [], c || d)
                                    for (l = 0, m = h.length; m > l; l++) g = h[l], (c && c !== g.callback && c !== g.callback._callback || d && d !== g.context) && e.push(g);
                                e.length || delete this._events[b]
                            }
                        return this
                    },
                    trigger: function(a) {
                        if (!this._events) return this;
                        var b = c.call(arguments, 1);
                        if (!f(this, "trigger", a, b)) return this;
                        var d = this._events[a],
                            e = this._events.all;
                        return d && g(d, b, this), e && g(e, arguments, this), this
                    },
                    triggerSafe: function(a) {
                        if (!this._events) return this;
                        var b = c.call(arguments, 1);
                        if (!f(this, "trigger", a, b)) return this;
                        var d = this._events[a],
                            e = this._events.all;
                        return d && h(d, b, this), e && h(e, arguments, this), this
                    }
                },
                e = /\s+/,
                f = function(a, b, c, d) {
                    if (!c) return !0;
                    if ("object" == typeof c) {
                        for (var f in c) a[b].apply(a, [f, c[f]].concat(d));
                        return !1
                    }
                    if (e.test(c)) {
                        for (var g = c.split(e), h = 0, i = g.length; i > h; h++) a[b].apply(a, [g[h]].concat(d));
                        return !1
                    }
                    return !0
                },
                g = function(a, b, c) {
                    var d, e = -1,
                        f = a.length,
                        g = b[0],
                        h = b[1],
                        i = b[2];
                    switch (b.length) {
                        case 0:
                            for (; ++e < f;)(d = a[e]).callback.call(d.context || c);
                            return;
                        case 1:
                            for (; ++e < f;)(d = a[e]).callback.call(d.context || c, g);
                            return;
                        case 2:
                            for (; ++e < f;)(d = a[e]).callback.call(d.context || c, g, h);
                            return;
                        case 3:
                            for (; ++e < f;)(d = a[e]).callback.call(d.context || c, g, h, i);
                            return;
                        default:
                            for (; ++e < f;)(d = a[e]).callback.apply(d.context || c, b);
                            return
                    }
                },
                h = function(a, b, c) {
                    for (var d, e = -1, f = a.length; ++e < f;) try {
                        (d = a[e]).callback.apply(d.context || c, b)
                    } catch (g) {}
                };
            return d
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(51), c(45), c(52), c(53), c(55), c(49), c(56), c(50), c(57), c(60)], e = function(a, b, c, d, e, f, g, h, i, j) {
            var k = {};
            return k.log = function() {
                window.console && ("object" == typeof console.log ? console.log(Array.prototype.slice.call(arguments, 0)) : console.log.apply(console, arguments))
            }, k.between = function(a, b, c) {
                return Math.max(Math.min(a, c), b)
            }, k.foreach = function(a, b) {
                var c, d;
                for (c in a) "function" === k.typeOf(a.hasOwnProperty) ? a.hasOwnProperty(c) && (d = a[c], b(c, d)) : (d = a[c], b(c, d))
            }, k.indexOf = b.indexOf, k.noop = function() {}, k.seconds = a.seconds, k.prefix = a.prefix, k.suffix = a.suffix, b.extend(k, f, h, c, g, d, e, i, j), k
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(50)], e = function(a, b) {
            function c(a) {
                return /^(?:(?:https?|file)\:)?\/\//.test(a)
            }

            function d(b) {
                return a.some(b, function(a) {
                    return "parsererror" === a.nodeName
                })
            }
            var e = {};
            return e.getAbsolutePath = function(a, d) {
                if (b.exists(d) || (d = document.location.href), b.exists(a)) {
                    if (c(a)) return a;
                    var e, f = d.substring(0, d.indexOf("://") + 3),
                        g = d.substring(f.length, d.indexOf("/", f.length + 1));
                    if (0 === a.indexOf("/")) e = a.split("/");
                    else {
                        var h = d.split("?")[0];
                        h = h.substring(f.length + g.length + 1, h.lastIndexOf("/")), e = h.split("/").concat(a.split("/"))
                    }
                    for (var i = [], j = 0; j < e.length; j++) e[j] && b.exists(e[j]) && "." !== e[j] && (".." === e[j] ? i.pop() : i.push(e[j]));
                    return f + g + "/" + i.join("/")
                }
            }, e.getScriptPath = a.memoize(function(a) {
                for (var b = document.getElementsByTagName("script"), c = 0; c < b.length; c++) {
                    var d = b[c].src;
                    if (d && d.indexOf(a) >= 0) return d.substr(0, d.indexOf(a))
                }
                return ""
            }), e.parseXML = function(a) {
                var b = null;
                try {
                    "DOMParser" in window ? (b = (new window.DOMParser).parseFromString(a, "text/xml"), (d(b.childNodes) || b.childNodes && d(b.childNodes[0].childNodes)) && (b = null)) : (b = new window.ActiveXObject("Microsoft.XMLDOM"), b.async = "false", b.loadXML(a))
                } catch (c) {}
                return b
            }, e.serialize = function(a) {
                if (void 0 === a) return null;
                if ("string" == typeof a && a.length < 6) {
                    var b = a.toLowerCase();
                    if ("true" === b) return !0;
                    if ("false" === b) return !1;
                    if (!isNaN(Number(a)) && !isNaN(parseFloat(a))) return Number(a)
                }
                return a
            }, e.parseDimension = function(a) {
                return "string" == typeof a ? "" === a ? 0 : a.lastIndexOf("%") > -1 ? a : parseInt(a.replace("px", ""), 10) : a
            }, e.timeFormat = function(a, b) {
                if (0 >= a && !b) return "00:00";
                var c = 0 > a ? "-" : "";
                a = Math.abs(a);
                var d = Math.floor(a / 3600),
                    e = Math.floor((a - 3600 * d) / 60),
                    f = Math.floor(a % 60);
                return c + (d ? d + ":" : "") + (10 > e ? "0" : "") + e + ":" + (10 > f ? "0" : "") + f
            }, e.adaptiveType = function(a) {
                if (0 !== a) {
                    var b = -120;
                    if (b >= a) return "DVR";
                    if (0 > a || a === 1 / 0) return "LIVE"
                }
                return "VOD"
            }, e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45)], e = function(a) {
            var b = {};
            return b.exists = function(a) {
                switch (typeof a) {
                    case "string":
                        return a.length > 0;
                    case "object":
                        return null !== a;
                    case "undefined":
                        return !1
                }
                return !0
            }, b.isHTTPS = function() {
                return 0 === window.location.href.indexOf("https")
            }, b.isRtmp = function(a, b) {
                return 0 === a.indexOf("rtmp") || "rtmp" === b
            }, b.isYouTube = function(a, b) {
                return "youtube" === b || /^(http|\/\/).*(youtube\.com|youtu\.be)\/.+/.test(a)
            }, b.youTubeID = function(a) {
                var b = /v[=\/]([^?&]*)|youtu\.be\/([^?]*)|^([\w-]*)$/i.exec(a);
                return b ? b.slice(1).join("").replace("?", "") : ""
            }, b.typeOf = function(b) {
                if (null === b) return "null";
                var c = typeof b;
                return "object" === c && a.isArray(b) ? "array" : c
            }, b
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45)], e = function(a) {
            function b(a) {
                return a.indexOf("(format=m3u8-") > -1 ? "m3u8" : !1
            }
            var c = function(a) {
                    return a.replace(/^\s+|\s+$/g, "")
                },
                d = function(a, b, c) {
                    for (a = "" + a, c = c || "0"; a.length < b;) a = c + a;
                    return a
                },
                e = function(a, b) {
                    for (var c = 0; c < a.attributes.length; c++)
                        if (a.attributes[c].name && a.attributes[c].name.toLowerCase() === b.toLowerCase()) return a.attributes[c].value.toString();
                    return ""
                },
                f = function(a) {
                    if (!a || "rtmp" === a.substr(0, 4)) return "";
                    var c = b(a);
                    return c ? c : (a = a.substring(a.lastIndexOf("/") + 1, a.length).split("?")[0].split("#")[0], a.lastIndexOf(".") > -1 ? a.substr(a.lastIndexOf(".") + 1, a.length).toLowerCase() : void 0)
                },
                g = function(a) {
                    var b = parseInt(a / 3600),
                        c = parseInt(a / 60) % 60,
                        e = a % 60;
                    return d(b, 2) + ":" + d(c, 2) + ":" + d(e.toFixed(3), 6)
                },
                h = function(b) {
                    if (a.isNumber(b)) return b;
                    b = b.replace(",", ".");
                    var c = b.split(":"),
                        d = 0;
                    return "s" === b.slice(-1) ? d = parseFloat(b) : "m" === b.slice(-1) ? d = 60 * parseFloat(b) : "h" === b.slice(-1) ? d = 3600 * parseFloat(b) : c.length > 1 ? (d = parseFloat(c[c.length - 1]), d += 60 * parseFloat(c[c.length - 2]), 3 === c.length && (d += 3600 * parseFloat(c[c.length - 3]))) : d = parseFloat(b), d
                },
                i = function(b, c) {
                    return a.map(b, function(a) {
                        return c + a
                    })
                },
                j = function(b, c) {
                    return a.map(b, function(a) {
                        return a + c
                    })
                };
            return {
                trim: c,
                pad: d,
                xmlAttribute: e,
                extension: f,
                hms: g,
                seconds: h,
                suffix: j,
                prefix: i
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45)], e = function(a) {
            function b(a) {
                return function() {
                    return d(a)
                }
            }
            var c = {},
                d = a.memoize(function(a) {
                    var b = navigator.userAgent.toLowerCase();
                    return null !== b.match(a)
                }),
                e = c.isInt = function(a) {
                    return parseFloat(a) % 1 === 0
                };
            c.isFlashSupported = function() {
                var a = c.flashVersion();
                return a && a >= 11.2
            }, c.isFF = b(/firefox/i), c.isIPod = b(/iP(hone|od)/i), c.isIPad = b(/iPad/i), c.isSafari602 = b(/Macintosh.*Mac OS X 10_8.*6\.0\.\d* Safari/i), c.isOSX = b(/Mac OS X/i), c.isEdge = b(/\sedge\/\d+/i);
            var f = c.isIETrident = function(a) {
                    return c.isEdge() ? !0 : a ? (a = parseFloat(a).toFixed(1), d(new RegExp("trident/.+rv:\\s*" + a, "i"))) : d(/trident/i)
                },
                g = c.isMSIE = function(a) {
                    return a ? (a = parseFloat(a).toFixed(1), d(new RegExp("msie\\s*" + a, "i"))) : d(/msie/i)
                },
                h = b(/chrome/i);
            c.isChrome = function() {
                return h() && !c.isEdge()
            }, c.isIE = function(a) {
                return a ? (a = parseFloat(a).toFixed(1), a >= 11 ? f(a) : g(a)) : g() || f()
            }, c.isSafari = function() {
                return d(/safari/i) && !d(/chrome/i) && !d(/chromium/i) && !d(/android/i)
            };
            var i = c.isIOS = function(a) {
                return d(a ? new RegExp("iP(hone|ad|od).+\\s(OS\\s" + a + "|.*\\sVersion/" + a + ")", "i") : /iP(hone|ad|od)/i)
            };
            c.isAndroidNative = function(a) {
                return j(a, !0)
            };
            var j = c.isAndroid = function(a, b) {
                return b && d(/chrome\/[123456789]/i) && !d(/chrome\/18/) ? !1 : a ? (e(a) && !/\./.test(a) && (a = "" + a + "."), d(new RegExp("Android\\s*" + a, "i"))) : d(/Android/i)
            };
            return c.isMobile = function() {
                return i() || j()
            }, c.isIframe = function() {
                return window.frameElement && "IFRAME" === window.frameElement.nodeName
            }, c.flashVersion = function() {
                if (c.isAndroid()) return 0;
                var a, b = navigator.plugins;
                if (b && (a = b["Shockwave Flash"], a && a.description)) return parseFloat(a.description.replace(/\D+(\d+\.?\d*).*/, "$1"));
                if ("undefined" != typeof window.ActiveXObject) {
                    try {
                        if (a = new window.ActiveXObject("ShockwaveFlash.ShockwaveFlash")) return parseFloat(a.GetVariable("$version").split(" ")[1].replace(/\s*,\s*/, "."))
                    } catch (d) {
                        return 0
                    }
                    return a
                }
                return 0
            }, c
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(51), c(45), c(54)], e = function(a, b, c) {
            var d = {};
            d.createElement = function(a) {
                var b = document.createElement("div");
                return b.innerHTML = a, b.firstChild
            }, d.styleDimension = function(a) {
                return a + (a.toString().indexOf("%") > 0 ? "" : "px")
            };
            var e = function(a) {
                    return b.isString(a.className) ? a.className.split(" ") : []
                },
                f = function(b, c) {
                    c = a.trim(c), b.className !== c && (b.className = c)
                };
            return d.classList = function(a) {
                return a.classList ? a.classList : e(a)
            }, d.hasClass = c.hasClass, d.addClass = function(a, c) {
                var d = e(a),
                    g = b.isArray(c) ? c : c.split(" ");
                b.each(g, function(a) {
                    b.contains(d, a) || d.push(a)
                }), f(a, d.join(" "))
            }, d.removeClass = function(a, c) {
                var d = e(a),
                    g = b.isArray(c) ? c : c.split(" ");
                f(a, b.difference(d, g).join(" "))
            }, d.replaceClass = function(a, b, c) {
                var d = a.className || "";
                b.test(d) ? d = d.replace(b, c) : c && (d += " " + c), f(a, d)
            }, d.toggleClass = function(a, c, e) {
                var f = d.hasClass(a, c);
                e = b.isBoolean(e) ? e : !f, e !== f && (e ? d.addClass(a, c) : d.removeClass(a, c))
            }, d.emptyElement = function(a) {
                for (; a.firstChild;) a.removeChild(a.firstChild)
            }, d.addStyleSheet = function(a) {
                var b = document.createElement("link");
                b.rel = "stylesheet", b.href = a, document.getElementsByTagName("head")[0].appendChild(b)
            }, d.empty = function(a) {
                if (a)
                    for (; a.childElementCount > 0;) a.removeChild(a.children[0])
            }, d.bounds = function(a) {
                var b = {
                    left: 0,
                    right: 0,
                    width: 0,
                    height: 0,
                    top: 0,
                    bottom: 0
                };
                if (!a || !document.body.contains(a)) return b;
                var c = a.getBoundingClientRect(a),
                    d = window.pageYOffset,
                    e = window.pageXOffset;
                return c.width || c.height || c.left || c.top ? (b.left = c.left + e, b.right = c.right + e, b.top = c.top + d, b.bottom = c.bottom + d, b.width = c.right - c.left, b.height = c.bottom - c.top, b) : b
            }, d
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [], e = function() {
            return {
                hasClass: function(a, b) {
                    var c = " " + b + " ";
                    return 1 === a.nodeType && (" " + a.className + " ").replace(/[\t\r\n\f]/g, " ").indexOf(c) >= 0
                }
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(51)], e = function(a) {
            function b(a) {
                a = a.split("-");
                for (var b = 1; b < a.length; b++) a[b] = a[b].charAt(0).toUpperCase() + a[b].slice(1);
                return a.join("")
            }

            function c(b, c, d) {
                if ("" === c || void 0 === c || null === c) return "";
                var e = d ? " !important" : "";
                return "string" == typeof c && isNaN(c) ? /png|gif|jpe?g/i.test(c) && c.indexOf("url") < 0 ? "url(" + c + ")" : c + e : 0 === c || "z-index" === b || "opacity" === b ? "" + c + e : /color/i.test(b) ? "#" + a.pad(c.toString(16).replace(/^0x/i, ""), 6) + e : Math.ceil(c) + "px" + e
            }
            var d, e = {},
                f = function(a, b) {
                    d || (d = document.createElement("style"), d.type = "text/css", document.getElementsByTagName("head")[0].appendChild(d));
                    var c = a + JSON.stringify(b).replace(/"/g, ""),
                        f = document.createTextNode(c);
                    e[a] && d.removeChild(e[a]), e[a] = f, d.appendChild(f)
                },
                g = function(a, d) {
                    if (void 0 !== a && null !== a) {
                        void 0 === a.length && (a = [a]);
                        var e, f = {};
                        for (e in d) f[e] = c(e, d[e]);
                        for (var g = 0; g < a.length; g++) {
                            var h, i = a[g];
                            if (void 0 !== i && null !== i)
                                for (e in f) h = b(e), i.style[h] !== f[e] && (i.style[h] = f[e])
                        }
                    }
                },
                h = function(a) {
                    for (var b in e) b.indexOf(a) >= 0 && (d.removeChild(e[b]), delete e[b])
                },
                i = function(a, b) {
                    g(a, {
                        transform: b,
                        webkitTransform: b,
                        msTransform: b,
                        mozTransform: b,
                        oTransform: b
                    })
                },
                j = function(a, b) {
                    var c = "rgb";
                    a ? (a = String(a).replace("#", ""), 3 === a.length && (a = a[0] + a[0] + a[1] + a[1] + a[2] + a[2])) : a = "000000";
                    var d = [parseInt(a.substr(0, 2), 16), parseInt(a.substr(2, 2), 16), parseInt(a.substr(4, 2), 16)];
                    return void 0 !== b && 100 !== b && (c += "a", d.push(b / 100)), c + "(" + d.join(",") + ")"
                };
            return {
                css: f,
                style: g,
                clearCss: h,
                transform: i,
                hexToRgba: j
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(49)], e = function(a, b) {
            function c(a) {
                a.onload = null, a.onprogress = null, a.onreadystatechange = null, a.onerror = null, "abort" in a && a.abort()
            }

            function d(b, d) {
                return function(e) {
                    var f = e.currentTarget || d.xhr;
                    if (clearTimeout(d.timeoutId), d.retryWithoutCredentials && d.xhr.withCredentials) {
                        c(f);
                        var g = a.extend({}, d, {
                            xhr: null,
                            withCredentials: !1,
                            retryWithoutCredentials: !1
                        });
                        return void l(g)
                    }
                    d.onerror(b, d.url, f)
                }
            }

            function e(a) {
                return function(b) {
                    var c = b.currentTarget || a.xhr;
                    if (4 === c.readyState) {
                        if (clearTimeout(a.timeoutId), c.status >= 400) {
                            var d;
                            return d = 404 === c.status ? "File not found" : "" + c.status + "(" + c.statusText + ")", a.onerror(d, a.url, c)
                        }
                        if (200 === c.status) return f(a)(b)
                    }
                }
            }

            function f(a) {
                return function(c) {
                    var d = c.currentTarget || a.xhr;
                    if (clearTimeout(a.timeoutId), a.responseType) {
                        if ("json" === a.responseType) return g(d, a)
                    } else {
                        var e, f = d.responseXML;
                        if (f) try {
                            e = f.firstChild
                        } catch (i) {}
                        if (f && e) return h(d, f, a);
                        if (j && d.responseText && !f && (f = b.parseXML(d.responseText), f && f.firstChild)) return h(d, f, a);
                        if (a.requireValidXML) return void a.onerror("Invalid XML", a.url, d)
                    }
                    a.oncomplete(d)
                }
            }

            function g(b, c) {
                if (!b.response || a.isString(b.response) && '"' !== b.responseText.substr(1)) try {
                    b = a.extend({}, b, {
                        response: JSON.parse(b.responseText)
                    })
                } catch (d) {
                    return void c.onerror("Invalid JSON", c.url, b)
                }
                return c.oncomplete(b)
            }

            function h(b, c, d) {
                var e = c.documentElement;
                return d.requireValidXML && ("parsererror" === e.nodeName || e.getElementsByTagName("parsererror").length) ? void d.onerror("Invalid XML", d.url, b) : (b.responseXML || (b = a.extend({}, b, {
                    responseXML: c
                })), d.oncomplete(b))
            }
            var i = function() {},
                j = !1,
                k = function(a) {
                    var b = document.createElement("a"),
                        c = document.createElement("a");
                    b.href = location.href;
                    try {
                        return c.href = a, c.href = c.href, b.protocol + "//" + b.host != c.protocol + "//" + c.host
                    } catch (d) {}
                    return !0
                },
                l = function(b, g, h, l) {
                    a.isObject(b) && (l = b, b = l.url);
                    var m, n = a.extend({
                        xhr: null,
                        url: b,
                        withCredentials: !1,
                        retryWithoutCredentials: !1,
                        timeout: 6e4,
                        timeoutId: -1,
                        oncomplete: g || i,
                        onerror: h || i,
                        mimeType: l && !l.responseType ? "text/xml" : "",
                        requireValidXML: !1,
                        responseType: l && l.plainText ? "text" : ""
                    }, l);
                    if ("XDomainRequest" in window && k(b)) m = n.xhr = new window.XDomainRequest, m.onload = f(n), m.ontimeout = m.onprogress = i, j = !0;
                    else {
                        if (!("XMLHttpRequest" in window)) return void n.onerror("", b);
                        m = n.xhr = new window.XMLHttpRequest, m.onreadystatechange = e(n)
                    }
                    var o = d("Error loading file", n);
                    m.onerror = o, "overrideMimeType" in m ? n.mimeType && m.overrideMimeType(n.mimeType) : j = !0;
                    try {
                        b = b.replace(/#.*$/, ""), m.open("GET", b, !0)
                    } catch (p) {
                        return o(p), m
                    }
                    if (n.responseType) try {
                        m.responseType = n.responseType
                    } catch (p) {}
                    n.timeout && (n.timeoutId = setTimeout(function() {
                        c(m), n.onerror("Timeout", b, m)
                    }, n.timeout));
                    try {
                        n.withCredentials && "withCredentials" in m && (m.withCredentials = !0), m.send()
                    } catch (p) {
                        o(p)
                    }
                    return m
                };
            return {
                ajax: l,
                crossdomain: k
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(58), c(45), c(50), c(49), c(59)], e = function(a, b, c, d, e) {
            var f = {};
            return f.repo = b.memoize(function() {
                var b = e.split("+")[0],
                    d = a.repo + b + "/";
                return c.isHTTPS() ? d.replace(/^http:/, "https:") : d
            }), f.versionCheck = function(a) {
                var b = ("0" + a).split(/\W/),
                    c = e.split(/\W/),
                    d = parseFloat(b[0]),
                    f = parseFloat(c[0]);
                return d > f ? !1 : !(d === f && parseFloat("0" + b[1]) > parseFloat(c[1]))
            }, f.isSDK = function(a) {
                return !(!a.analytics || !a.analytics.sdkplatform)
            }, f.loadFrom = function() {
                return f.repo()
            }, f
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [], e = function() {
            return {
                repo: "http://ssl.p.jwpcdn.com/player/v/",
                SkinsIncluded: ["seven"],
                SkinsLoadable: ["beelden", "bekle", "five", "glow", "roundster", "six", "stormtrooper", "vapor"],
                dvrSeekLimit: -25
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [], e = function() {
            return "7.3.5+commercial_v7-3-5.80.commercial.5a924f.jwplayer.ad873d.analytics.5a0154.vast.0300bb.googima.e8ba93.plugin-sharing.08a279.plugin-related.909f55.plugin-gapro.0374cd"
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [], e = function() {
            var a = function(a, c, d) {
                    if (c = c || this, d = d || [], window.jwplayer && window.jwplayer.debug) return a.apply(c, d);
                    try {
                        return a.apply(c, d)
                    } catch (e) {
                        return new b(a.name, e)
                    }
                },
                b = function(a, b) {
                    this.name = a, this.message = b.message || b.toString(), this.error = b
                };
            return {
                tryCatch: a,
                Error: b
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45)], e = function(a) {
            var b = function() {
                var b = {},
                    c = {},
                    d = {},
                    e = {};
                return {
                    start: function(c) {
                        b[c] = a.now(), d[c] = d[c] + 1 || 1
                    },
                    end: function(d) {
                        if (b[d]) {
                            var e = a.now() - b[d];
                            c[d] = c[d] + e || e
                        }
                    },
                    dump: function() {
                        return {
                            counts: d,
                            sums: c,
                            events: e
                        }
                    },
                    tick: function(b, c) {
                        e[b] = c || a.now()
                    },
                    between: function(a, b) {
                        return e[b] && e[a] ? e[b] - e[a] : -1
                    }
                }
            };
            return b
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [], e = function() {
            return {
                BUFFERING: "buffering",
                IDLE: "idle",
                COMPLETE: "complete",
                PAUSED: "paused",
                PLAYING: "playing",
                ERROR: "error",
                LOADING: "loading",
                STALLED: "stalled"
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(64), c(81), c(158)], e = function(a, b, d) {
            var e = a.prototype.setup;
            return a.prototype.setup = function(a, f) {
                e.apply(this, arguments);
                var g = this._model.get("edition"),
                    h = b(g),
                    i = this._model.get("cast"),
                    j = this;
                h("casting") && i && i.appid && c.e(6, function(a) {
                    var b = c(159);
                    j._castController = new b(j, j._model), j.castToggle = j._castController.castToggle.bind(j._castController)
                });
                var k = d.setup();
                this.once("ready", k.onReady, this), f.getAdBlock = k.checkAdBlock
            }, a
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(73), c(115), c(74), c(45), c(93), c(111), c(77), c(114), c(65), c(48), c(116), c(47), c(76), c(62), c(46), c(156)], e = function(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p) {
            function q(a) {
                return function() {
                    var b = Array.prototype.slice.call(arguments, 0);
                    this.eventsQueue.push([a, b])
                }
            }

            function r(a) {
                return a === n.LOADING || a === n.STALLED ? n.BUFFERING : a
            }
            var s = function(a) {
                this.originalContainer = this.currentContainer = a, this.eventsQueue = [], d.extend(this, l), this._model = new g
            };
            return s.prototype = {
                play: q("play"),
                pause: q("pause"),
                setVolume: q("setVolume"),
                setMute: q("setMute"),
                seek: q("seek"),
                stop: q("stop"),
                load: q("load"),
                playlistNext: q("playlistNext"),
                playlistPrev: q("playlistPrev"),
                playlistItem: q("playlistItem"),
                setFullscreen: q("setFullscreen"),
                setCurrentCaptions: q("setCurrentCaptions"),
                setCurrentQuality: q("setCurrentQuality"),
                setup: function(g, l) {
                    function p() {
                        U.mediaModel.on("change:state", function(a, b) {
                            var c = r(b);
                            U.set("state", c)
                        })
                    }

                    function q() {
                        X = null, D(U.get("item")), U.on("change:state", m, this), U.on("change:castState", function(a, b) {
                            aa.trigger(o.JWPLAYER_CAST_SESSION, b)
                        }), U.on("change:fullscreen", function(a, b) {
                            aa.trigger(o.JWPLAYER_FULLSCREEN, {
                                fullscreen: b
                            })
                        }), U.on("itemReady", function() {
                            aa.trigger(o.JWPLAYER_PLAYLIST_ITEM, {
                                index: U.get("item"),
                                item: U.get("playlistItem")
                            })
                        }), U.on("change:playlist", function(a, b) {
                            b.length && aa.trigger(o.JWPLAYER_PLAYLIST_LOADED, {
                                playlist: b
                            })
                        }), U.on("change:volume", function(a, b) {
                            aa.trigger(o.JWPLAYER_MEDIA_VOLUME, {
                                volume: b
                            })
                        }), U.on("change:mute", function(a, b) {
                            aa.trigger(o.JWPLAYER_MEDIA_MUTE, {
                                mute: b
                            })
                        }), U.on("change:controls", function(a, b) {
                            aa.trigger(o.JWPLAYER_CONTROLS, {
                                controls: b
                            })
                        }), U.on("change:scrubbing", function(a, b) {
                            b ? y() : w()
                        }), U.on("change:captionsList", function(a, b) {
                            aa.trigger(o.JWPLAYER_CAPTIONS_LIST, {
                                tracks: b,
                                track: Q()
                            })
                        }), U.mediaController.on("all", aa.trigger.bind(aa)), V.on("all", aa.trigger.bind(aa)), this.showView(V.element()), window.addEventListener("beforeunload", function() {
                            X && X.destroy(), U && U.destroy()
                        }), d.defer(s)
                    }

                    function s() {
                        for (aa.trigger(o.JWPLAYER_READY, {
                                setupTime: 0
                            }), aa.trigger(o.JWPLAYER_PLAYLIST_LOADED, {
                                playlist: U.get("playlist")
                            }), aa.trigger(o.JWPLAYER_PLAYLIST_ITEM, {
                                index: U.get("item"),
                                item: U.get("playlistItem")
                            }), aa.trigger(o.JWPLAYER_CAPTIONS_LIST, {
                                tracks: U.get("captionsList"),
                                track: U.get("captionsIndex")
                            }), U.get("autostart") && w({
                                reason: "autostart"
                            }); aa.eventsQueue.length > 0;) {
                            var a = aa.eventsQueue.shift(),
                                b = a[0],
                                c = a[1] || [];
                            aa[b].apply(aa, c)
                        }
                    }

                    function t(a) {
                        switch (U.get("state") === n.ERROR && U.set("state", n.IDLE), x(!0), U.get("autostart") && U.once("itemReady", w), typeof a) {
                            case "string":
                                u(a);
                                break;
                            case "object":
                                var b = C(a);
                                b && D(0);
                                break;
                            case "number":
                                D(a)
                        }
                    }

                    function u(a) {
                        var b = new i;
                        b.on(o.JWPLAYER_PLAYLIST_LOADED, function(a) {
                            t(a.playlist)
                        }), b.on(o.JWPLAYER_ERROR, function(a) {
                            a.message = "Error loading playlist: " + a.message, this.triggerError(a)
                        }, this), b.load(a)
                    }

                    function v() {
                        var a = aa._instreamAdapter && aa._instreamAdapter.getState();
                        return d.isString(a) ? a : U.get("state")
                    }

                    function w(a) {
                        var b;
                        if (a && U.set("playReason", a.reason), U.get("state") !== n.ERROR) {
                            var c = aa._instreamAdapter && aa._instreamAdapter.getState();
                            if (d.isString(c)) return l.pauseAd(!1);
                            if (U.get("state") === n.COMPLETE && (x(!0), D(0)), !$ && ($ = !0, aa.trigger(o.JWPLAYER_MEDIA_BEFOREPLAY, {
                                    playReason: U.get("playReason")
                                }), $ = !1, Z)) return Z = !1, void(Y = null);
                            if (z()) {
                                if (0 === U.get("playlist").length) return !1;
                                b = j.tryCatch(function() {
                                    U.loadVideo()
                                })
                            } else U.get("state") === n.PAUSED && (b = j.tryCatch(function() {
                                U.playVideo()
                            }));
                            return b instanceof j.Error ? (aa.triggerError(b), Y = null, !1) : !0
                        }
                    }

                    function x(a) {
                        U.off("itemReady", w);
                        var b = !a;
                        Y = null;
                        var c = j.tryCatch(function() {
                            U.stopVideo()
                        }, aa);
                        return c instanceof j.Error ? (aa.triggerError(c), !1) : (b && (_ = !0), $ && (Z = !0), !0)
                    }

                    function y() {
                        Y = null;
                        var a = aa._instreamAdapter && aa._instreamAdapter.getState();
                        if (d.isString(a)) return l.pauseAd(!0);
                        switch (U.get("state")) {
                            case n.ERROR:
                                return !1;
                            case n.PLAYING:
                            case n.BUFFERING:
                                var b = j.tryCatch(function() {
                                    ba().pause()
                                }, this);
                                if (b instanceof j.Error) return aa.triggerError(b), !1;
                                break;
                            default:
                                $ && (Z = !0)
                        }
                        return !0
                    }

                    function z() {
                        var a = U.get("state");
                        return a === n.IDLE || a === n.COMPLETE || a === n.ERROR
                    }

                    function A(a) {
                        U.get("state") !== n.ERROR && (U.get("scrubbing") || U.get("state") === n.PLAYING || w(!0), ba().seek(a))
                    }

                    function B(a, b) {
                        x(!0), D(a), w(b)
                    }

                    function C(a) {
                        var b = h(a);
                        return b = h.filterPlaylist(b, U.getProviders(), U.get("androidhls"), U.get("drm"), U.get("preload")), U.set("playlist", b), d.isArray(b) && 0 !== b.length ? !0 : (aa.triggerError({
                            message: "Error loading playlist: No playable sources found"
                        }), !1)
                    }

                    function D(a) {
                        var b = U.get("playlist");
                        a = (a + b.length) % b.length, U.set("item", a), U.set("playlistItem", b[a]), U.setActiveItem(b[a])
                    }

                    function E(a) {
                        B(U.get("item") - 1, a || {
                            reason: "external"
                        })
                    }

                    function F(a) {
                        B(U.get("item") + 1, a || {
                            reason: "external"
                        })
                    }

                    function G() {
                        if (z()) {
                            if (_) return void(_ = !1);
                            Y = G;
                            var a = U.get("item");
                            return a === U.get("playlist").length - 1 ? void(U.get("repeat") ? F({
                                reason: "repeat"
                            }) : (U.set("state", n.COMPLETE), aa.trigger(o.JWPLAYER_PLAYLIST_COMPLETE, {}))) : void F({
                                reason: "playlist"
                            })
                        }
                    }

                    function H(a) {
                        ba().setCurrentQuality(a)
                    }

                    function I() {
                        return ba() ? ba().getCurrentQuality() : -1
                    }

                    function J() {
                        return this._model ? this._model.getConfiguration() : void 0
                    }

                    function K() {
                        if (this._model.mediaModel) return this._model.mediaModel.get("visualQuality");
                        var a = L();
                        if (a) {
                            var b = I(),
                                c = a[b];
                            if (c) return {
                                level: d.extend({
                                    index: b
                                }, c),
                                mode: "",
                                reason: ""
                            }
                        }
                        return null
                    }

                    function L() {
                        return ba() ? ba().getQualityLevels() : null
                    }

                    function M(a) {
                        ba() && ba().setCurrentAudioTrack(a)
                    }

                    function N() {
                        return ba() ? ba().getCurrentAudioTrack() : -1
                    }

                    function O() {
                        return ba() ? ba().getAudioTracks() : null
                    }

                    function P(a) {
                        U.persistVideoSubtitleTrack(a), aa.trigger(o.JWPLAYER_CAPTIONS_CHANGED, {
                            tracks: R(),
                            track: a
                        })
                    }

                    function Q() {
                        return W.getCurrentIndex()
                    }

                    function R() {
                        return W.getCaptionsList()
                    }

                    function S() {
                        var a = U.getVideo();
                        if (a) {
                            var b = a.detachMedia();
                            if (b instanceof HTMLVideoElement) return b
                        }
                        return null
                    }

                    function T() {
                        var a = j.tryCatch(function() {
                            U.getVideo().attachMedia()
                        });
                        return a instanceof j.Error ? void j.log("Error calling _attachMedia", a) : void("function" == typeof Y && Y())
                    }
                    var U, V, W, X, Y, Z, $ = !1,
                        _ = !1,
                        aa = this,
                        ba = function() {
                            return U.getVideo()
                        },
                        ca = new a(g);
                    U = this._model.setup(ca), V = this._view = new k(l, U), W = new f(l, U), X = new e(l, U, V, C), X.on(o.JWPLAYER_READY, q, this), X.on(o.JWPLAYER_SETUP_ERROR, this.setupError, this), U.mediaController.on(o.JWPLAYER_MEDIA_COMPLETE, function() {
                        d.defer(G)
                    }), U.mediaController.on(o.JWPLAYER_MEDIA_ERROR, this.triggerError, this), U.on("change:flashBlocked", function(a, b) {
                        if (!b) return void this._model.set("errorEvent", void 0);
                        var c = !!a.get("flashThrottle"),
                            d = {
                                message: c ? "Click to run Flash" : "Flash plugin failed to load"
                            };
                        c || this.trigger(o.JWPLAYER_ERROR, d), this._model.set("errorEvent", d)
                    }, this), p(), U.on("change:mediaModel", p), this.play = w, this.pause = y, this.seek = A, this.stop = x, this.load = t, this.playlistNext = F, this.playlistPrev = E, this.playlistItem = B, this.setCurrentCaptions = P, this.setCurrentQuality = H, this.detachMedia = S, this.attachMedia = T, this.getCurrentQuality = I, this.getQualityLevels = L, this.setCurrentAudioTrack = M, this.getCurrentAudioTrack = N, this.getAudioTracks = O, this.getCurrentCaptions = Q, this.getCaptionsList = R, this.getVisualQuality = K, this.getConfig = J, this.getState = v, this.setVolume = U.setVolume, this.setMute = U.setMute, this.getProvider = function() {
                        return U.get("provider")
                    }, this.getWidth = function() {
                        return U.get("containerWidth")
                    }, this.getHeight = function() {
                        return U.get("containerHeight")
                    }, this.getContainer = function() {
                        return this.currentContainer
                    }, this.resize = V.resize, this.getSafeRegion = V.getSafeRegion, this.setCues = V.addCues, this.setFullscreen = function(a) {
                        d.isBoolean(a) || (a = !U.get("fullscreen")), U.set("fullscreen", a), this._instreamAdapter && this._instreamAdapter._adModel && this._instreamAdapter._adModel.set("fullscreen", a)
                    }, this.addButton = function(a, b, c, e, f) {
                        var g = {
                                img: a,
                                tooltip: b,
                                callback: c,
                                id: e,
                                btnClass: f
                            },
                            h = U.get("dock");
                        h = h ? h.slice(0) : [], h = d.reject(h, d.matches({
                            id: g.id
                        })), h.push(g), U.set("dock", h)
                    }, this.removeButton = function(a) {
                        var b = U.get("dock") || [];
                        b = d.reject(b, d.matches({
                            id: a
                        })), U.set("dock", b)
                    }, this.checkBeforePlay = function() {
                        return $
                    }, this.getItemQoe = function() {
                        return U._qoeItem
                    }, this.setControls = function(a) {
                        d.isBoolean(a) || (a = !U.get("controls")), U.set("controls", a);
                        var b = U.getVideo();
                        b && b.setControls(a)
                    }, this.playerDestroy = function() {
                        this.stop(), this.showView(this.originalContainer), V && V.destroy(), U && U.destroy(), X && (X.destroy(), X = null)
                    }, this.isBeforePlay = this.checkBeforePlay, this.isBeforeComplete = function() {
                        return U.getVideo().checkComplete()
                    }, this.createInstream = function() {
                        return this.instreamDestroy(), this._instreamAdapter = new c(this, U, V), this._instreamAdapter
                    }, this.skipAd = function() {
                        this._instreamAdapter && this._instreamAdapter.skipAd()
                    }, this.instreamDestroy = function() {
                        aa._instreamAdapter && aa._instreamAdapter.destroy()
                    }, b(l, this), X.start()
                },
                showView: function(a) {
                    (document.documentElement.contains(this.currentContainer) || (this.currentContainer = document.getElementById(this._model.get("id")), this.currentContainer)) && (this.currentContainer.parentElement && this.currentContainer.parentElement.replaceChild(a, this.currentContainer), this.currentContainer = a)
                },
                triggerError: function(a) {
                    this._model.set("errorEvent", a), this._model.set("state", n.ERROR), this._model.once("change:state", function() {
                        this._model.set("errorEvent", void 0)
                    }, this), this.trigger(o.JWPLAYER_ERROR, a)
                },
                setupError: function(a) {
                    var b = a.message,
                        c = j.createElement(p(this._model.get("id"), this._model.get("skin"), b)),
                        e = this._model.get("width"),
                        f = this._model.get("height");
                    j.style(c, {
                        width: e.toString().indexOf("%") > 0 ? e : e + "px",
                        height: f.toString().indexOf("%") > 0 ? f : f + "px"
                    }), this.showView(c);
                    var g = this;
                    d.defer(function() {
                        g.trigger(o.JWPLAYER_SETUP_ERROR, {
                            message: b
                        })
                    })
                }
            }, s
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(66), c(67), c(48), c(46), c(47), c(45)], e = function(a, b, c, d, e, f) {
            var g = function() {
                function g(e) {
                    var g = c.tryCatch(function() {
                        var c, g = e.responseXML ? e.responseXML.childNodes : null,
                            h = "";
                        if (g) {
                            for (var k = 0; k < g.length && (h = g[k], 8 === h.nodeType); k++);
                            "xml" === a.localName(h) && (h = h.nextSibling), "rss" === a.localName(h) && (c = b.parse(h))
                        }
                        if (!c) try {
                            c = JSON.parse(e.responseText), f.isArray(c) || (c = c.playlist)
                        } catch (l) {
                            return void i("Not a valid RSS/JSON feed")
                        }
                        j.trigger(d.JWPLAYER_PLAYLIST_LOADED, {
                            playlist: c
                        })
                    });
                    g instanceof c.Error && i()
                }

                function h(a) {
                    i("Playlist load error: " + a)
                }

                function i(a) {
                    j.trigger(d.JWPLAYER_ERROR, {
                        message: a ? a : "Error loading file"
                    })
                }
                var j = f.extend(this, e);
                this.load = function(a) {
                    c.ajax(a, g, h)
                }, this.destroy = function() {
                    this.off()
                }
            };
            return g
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(51)], e = function(a) {
            return {
                localName: function(a) {
                    return a ? a.localName ? a.localName : a.baseName ? a.baseName : "" : ""
                },
                textContent: function(b) {
                    return b ? b.textContent ? a.trim(b.textContent) : b.text ? a.trim(b.text) : "" : ""
                },
                getChildNode: function(a, b) {
                    return a.childNodes[b]
                },
                numChildren: function(a) {
                    return a.childNodes ? a.childNodes.length : 0
                }
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(51), c(66), c(68), c(69), c(70)], e = function(a, b, c, d, e) {
            function f(b) {
                for (var f = {}, h = 0; h < b.childNodes.length; h++) {
                    var i = b.childNodes[h],
                        k = j(i);
                    if (k) switch (k.toLowerCase()) {
                        case "enclosure":
                            f.file = a.xmlAttribute(i, "url");
                            break;
                        case "title":
                            f.title = g(i);
                            break;
                        case "guid":
                            f.mediaid = g(i);
                            break;
                        case "pubdate":
                            f.date = g(i);
                            break;
                        case "description":
                            f.description = g(i);
                            break;
                        case "link":
                            f.link = g(i);
                            break;
                        case "category":
                            f.tags ? f.tags += g(i) : f.tags = g(i)
                    }
                }
                return f = d(b, f), f = c(b, f), new e(f)
            }
            var g = b.textContent,
                h = b.getChildNode,
                i = b.numChildren,
                j = b.localName,
                k = {};
            return k.parse = function(a) {
                for (var b = [], c = 0; c < i(a); c++) {
                    var d = h(a, c),
                        e = j(d).toLowerCase();
                    if ("channel" === e)
                        for (var g = 0; g < i(d); g++) {
                            var k = h(d, g);
                            "item" === j(k).toLowerCase() && b.push(f(k))
                        }
                }
                return b
            }, k
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(66), c(51), c(48)], e = function(a, b, c) {
            var d = "jwplayer",
                e = function(e, f) {
                    for (var g = [], h = [], i = b.xmlAttribute, j = "default", k = "label", l = "file", m = "type", n = 0; n < e.childNodes.length; n++) {
                        var o = e.childNodes[n];
                        if (o.prefix === d) {
                            var p = a.localName(o);
                            "source" === p ? (delete f.sources, g.push({
                                file: i(o, l),
                                "default": i(o, j),
                                label: i(o, k),
                                type: i(o, m)
                            })) : "track" === p ? (delete f.tracks, h.push({
                                file: i(o, l),
                                "default": i(o, j),
                                kind: i(o, "kind"),
                                label: i(o, k)
                            })) : (f[p] = c.serialize(a.textContent(o)), "file" === p && f.sources && delete f.sources)
                        }
                        f[l] || (f[l] = f.link)
                    }
                    if (g.length)
                        for (f.sources = [], n = 0; n < g.length; n++) g[n].file.length > 0 && (g[n][j] = "true" === g[n][j], g[n].label.length || delete g[n].label, f.sources.push(g[n]));
                    if (h.length)
                        for (f.tracks = [], n = 0; n < h.length; n++) h[n].file.length > 0 && (h[n][j] = "true" === h[n][j], h[n].kind = h[n].kind.length ? h[n].kind : "captions", h[n].label.length || delete h[n].label, f.tracks.push(h[n]));
                    return f
                };
            return e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(66), c(51), c(48)], e = function(a, b, c) {
            var d = b.xmlAttribute,
                e = a.localName,
                f = a.textContent,
                g = a.numChildren,
                h = "media",
                i = function(a, b) {
                    function j(a) {
                        var b = {
                            zh: "Chinese",
                            nl: "Dutch",
                            en: "English",
                            fr: "French",
                            de: "German",
                            it: "Italian",
                            ja: "Japanese",
                            pt: "Portuguese",
                            ru: "Russian",
                            es: "Spanish"
                        };
                        return b[a] ? b[a] : a
                    }
                    var k, l, m = "tracks",
                        n = [];
                    for (l = 0; l < g(a); l++)
                        if (k = a.childNodes[l], k.prefix === h) {
                            if (!e(k)) continue;
                            switch (e(k).toLowerCase()) {
                                case "content":
                                    d(k, "duration") && (b.duration = c.seconds(d(k, "duration"))), g(k) > 0 && (b = i(k, b)), d(k, "url") && (b.sources || (b.sources = []), b.sources.push({
                                        file: d(k, "url"),
                                        type: d(k, "type"),
                                        width: d(k, "width"),
                                        label: d(k, "label")
                                    }));
                                    break;
                                case "title":
                                    b.title = f(k);
                                    break;
                                case "description":
                                    b.description = f(k);
                                    break;
                                case "guid":
                                    b.mediaid = f(k);
                                    break;
                                case "thumbnail":
                                    b.image || (b.image = d(k, "url"));
                                    break;
                                case "player":
                                    break;
                                case "group":
                                    i(k, b);
                                    break;
                                case "subtitle":
                                    var o = {};
                                    o.file = d(k, "url"), o.kind = "captions", d(k, "lang").length > 0 && (o.label = j(d(k, "lang"))), n.push(o)
                            }
                        }
                    for (b.hasOwnProperty(m) || (b[m] = []), l = 0; l < n.length; l++) b[m].push(n[l]);
                    return b
                };
            return i
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(71), c(72)], e = function(a, b, c) {
            var d = {
                    sources: [],
                    tracks: []
                },
                e = function(e) {
                    e = e || {}, a.isArray(e.tracks) || delete e.tracks;
                    var f = a.extend({}, d, e);
                    a.isObject(f.sources) && !a.isArray(f.sources) && (f.sources = [b(f.sources)]), a.isArray(f.sources) && 0 !== f.sources.length || (e.levels ? f.sources = e.levels : f.sources = [b(e)]);
                    for (var g = 0; g < f.sources.length; g++) {
                        var h = f.sources[g];
                        if (h) {
                            var i = h["default"];
                            i ? h["default"] = "true" === i.toString() : h["default"] = !1, f.sources[g].label || (f.sources[g].label = g.toString()), f.sources[g] = b(f.sources[g])
                        }
                    }
                    return f.sources = a.compact(f.sources), a.isArray(f.tracks) || (f.tracks = []), a.isArray(f.captions) && (f.tracks = f.tracks.concat(f.captions), delete f.captions), f.tracks = a.compact(a.map(f.tracks, c)), f
                };
            return e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(51), c(45)], e = function(a, b, c) {
            var d = {
                    "default": !1
                },
                e = function(e) {
                    if (e && e.file) {
                        var f = c.extend({}, d, e);
                        f.file = b.trim("" + f.file);
                        var g = /^[^\/]+\/(?:x-)?([^\/]+)$/;
                        if (a.isYouTube(f.file) ? f.type = "youtube" : a.isRtmp(f.file) ? f.type = "rtmp" : g.test(f.type) ? f.type = f.type.replace(g, "$1") : f.type || (f.type = b.extension(f.file)), f.type) {
                            switch (f.type) {
                                case "m3u8":
                                case "vnd.apple.mpegurl":
                                    f.type = "hls";
                                    break;
                                case "dash+xml":
                                    f.type = "dash";
                                    break;
                                case "smil":
                                    f.type = "rtmp";
                                    break;
                                case "m4a":
                                    f.type = "aac"
                            }
                            return c.each(f, function(a, b) {
                                "" === a && delete f[b]
                            }), f
                        }
                    }
                };
            return e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45)], e = function(a) {
            var b = {
                    kind: "captions",
                    "default": !1
                },
                c = function(c) {
                    return c && c.file ? a.extend({}, b, c) : void 0
                };
            return c
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(45)], e = function(a, b) {
            function d(c) {
                b.each(c, function(b, d) {
                    c[d] = a.serialize(b)
                })
            }

            function e(a) {
                return a.slice && "px" === a.slice(-2) && (a = a.slice(0, -2)), a
            }

            function f(b, c) {
                if (-1 === c.toString().indexOf("%")) return 0;
                if ("string" != typeof b || !a.exists(b)) return 0;
                if (/^\d*\.?\d+%$/.test(b)) return b;
                var d = b.indexOf(":");
                if (-1 === d) return 0;
                var e = parseFloat(b.substr(0, d)),
                    f = parseFloat(b.substr(d + 1));
                return 0 >= e || 0 >= f ? 0 : f / e * 100 + "%"
            }
            var g = {
                    autostart: !1,
                    controls: !0,
                    displaytitle: !0,
                    displaydescription: !0,
                    mobilecontrols: !1,
                    repeat: !1,
                    castAvailable: !1,
                    skin: "seven",
                    stretching: "uniform",
                    mute: !1,
                    volume: 90,
                    width: 480,
                    height: 270
                },
                h = function(h) {
                    var i = b.extend({}, (window.jwplayer || {}).defaults, h);
                    d(i);
                    var j = b.extend({}, g, i);
                    if ("." === j.base && (j.base = a.getScriptPath("jwplayer.js")), j.base = (j.base || a.loadFrom()).replace(/\/?$/, "/"), c.p = j.base, j.width = e(j.width), j.height = e(j.height), j.flashplayer = j.flashplayer || a.getScriptPath("jwplayer.js") + "jwplayer.flash.swf", "http:" === window.location.protocol && (j.flashplayer = j.flashplayer.replace("https", "http")), j.aspectratio = f(j.aspectratio, j.width), b.isObject(j.skin) && (j.skinUrl = j.skin.url, j.skinColorInactive = j.skin.inactive, j.skinColorActive = j.skin.active, j.skinColorBackground = j.skin.background, j.skin = b.isString(j.skin.name) ? j.skin.name : g.skin), b.isString(j.skin) && j.skin.indexOf(".xml") > 0 && (console.log("JW Player does not support XML skins, please update your config"), j.skin = j.skin.replace(".xml", "")), j.aspectratio || delete j.aspectratio, !j.playlist) {
                        var k = b.pick(j, ["title", "description", "type", "mediaid", "image", "file", "sources", "tracks", "preload"]);
                        j.playlist = [k]
                    }
                    return j
                };
            return h
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(75), c(92), c(46), c(62), c(48), c(47), c(45)], e = function(a, b, c, d, e, f, g) {
            function h(c) {
                var d = c.get("provider").name || "";
                return d.indexOf("flash") >= 0 ? b : a
            }
            var i = {
                    skipoffset: null,
                    tag: null
                },
                j = function(a, b, f) {
                    function j(a, b) {
                        b = b || {}, u.tag && !b.tag && (b.tag = u.tag), this.trigger(a, b)
                    }

                    function k(a) {
                        s._adModel.set("duration", a.duration), s._adModel.set("position", a.position)
                    }

                    function l(a) {
                        if (m && t + 1 < m.length) {
                            s._adModel.set("state", "buffering"), b.set("skipButton", !1), t++;
                            var d, e = m[t];
                            n && (d = n[t]), this.loadItem(e, d)
                        } else a.type === c.JWPLAYER_MEDIA_COMPLETE && (j.call(this, a.type, a), this.trigger(c.JWPLAYER_PLAYLIST_COMPLETE, {})), this.destroy()
                    }
                    var m, n, o, p, q, r = h(b),
                        s = new r(a, b),
                        t = 0,
                        u = {},
                        v = g.bind(function(a) {
                            a = a || {}, a.hasControls = !!b.get("controls"), this.trigger(c.JWPLAYER_INSTREAM_CLICK, a), s && s._adModel && (s._adModel.get("state") === d.PAUSED ? a.hasControls && s.instreamPlay() : s.instreamPause())
                        }, this),
                        w = g.bind(function() {
                            s && s._adModel && s._adModel.get("state") === d.PAUSED && b.get("controls") && (a.setFullscreen(), a.play())
                        }, this);
                    this.type = "instream", this.init = function() {
                        o = b.getVideo(), p = b.get("position"), q = b.get("playlist")[b.get("item")], s.on("all", j, this), s.on(c.JWPLAYER_MEDIA_TIME, k, this), s.on(c.JWPLAYER_MEDIA_COMPLETE, l, this), s.init(), o.detachMedia(), b.mediaModel.set("state", d.BUFFERING), a.checkBeforePlay() || 0 === p && !o.checkComplete() ? (p = 0, b.set("preInstreamState", "instream-preroll")) : o && o.checkComplete() || b.get("state") === d.COMPLETE ? b.set("preInstreamState", "instream-postroll") : b.set("preInstreamState", "instream-midroll");
                        var g = b.get("state");
                        return g !== d.PLAYING && g !== d.BUFFERING || o.pause(), f.setupInstream(s._adModel), s._adModel.set("state", d.BUFFERING), f.clickHandler().setAlternateClickHandlers(e.noop, null), this.setText("Loading ad"), this
                    }, this.loadItem = function(a, d) {
                        if (e.isAndroid(2.3)) return void this.trigger({
                            type: c.JWPLAYER_ERROR,
                            message: "Error loading instream: Cannot play instream on Android 2.3"
                        });
                        g.isArray(a) && (m = a, n = d, a = m[t], n && (d = n[t])), this.trigger(c.JWPLAYER_PLAYLIST_ITEM, {
                            index: t,
                            item: a
                        }), u = g.extend({}, i, d), s.load(a), this.addClickHandler();
                        var f = a.skipoffset || u.skipoffset;
                        f && (s._adModel.set("skipMessage", u.skipMessage), s._adModel.set("skipText", u.skipText), s._adModel.set("skipOffset", f), b.set("skipButton", !0))
                    }, this.applyProviderListeners = function(a) {
                        s.applyProviderListeners(a), this.addClickHandler()
                    }, this.play = function() {
                        s.instreamPlay()
                    }, this.pause = function() {
                        s.instreamPause()
                    }, this.hide = function() {
                        s.hide()
                    }, this.addClickHandler = function() {
                        f.clickHandler().setAlternateClickHandlers(v, w), s.on(c.JWPLAYER_MEDIA_META, this.metaHandler, this)
                    }, this.skipAd = function(a) {
                        var b = c.JWPLAYER_AD_SKIPPED;
                        this.trigger(b, a), l.call(this, {
                            type: b
                        })
                    }, this.metaHandler = function(a) {
                        a.width && a.height && f.resizeMedia()
                    }, this.destroy = function() {
                        if (this.off(), b.set("skipButton", !1), s) {
                            f.clickHandler() && f.clickHandler().revertAlternateClickHandlers(), s.instreamDestroy(), f.destroyInstream(), s = null, a.attachMedia();
                            var c = b.get("preInstreamState");
                            switch (c) {
                                case "instream-preroll":
                                case "instream-midroll":
                                    var h = g.extend({}, q);
                                    h.starttime = p, b.loadVideo(h), e.isMobile() && b.mediaModel.get("state") === d.BUFFERING && o.setState(d.BUFFERING), o.play();
                                    break;
                                case "instream-postroll":
                                case "instream-idle":
                                    o.stop()
                            }
                        }
                    }, this.getState = function() {
                        return s && s._adModel ? s._adModel.get("state") : !1
                    }, this.setText = function(a) {
                        f.setAltText(a ? a : "")
                    }, this.hide = function() {
                        f.useExternalControls()
                    }
                };
            return g.extend(j.prototype, f), j
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(47), c(76), c(46), c(62), c(77)], e = function(a, b, c, d, e, f) {
            var g = function(g, h) {
                function i(b) {
                    var e = b || m.getVideo();
                    if (n !== e) {
                        if (n = e, !e) return;
                        e.off(), e.on("all", function(b, c) {
                            c = a.extend({}, c, {
                                type: b
                            }), this.trigger(b, c)
                        }, o), e.on(d.JWPLAYER_MEDIA_BUFFER_FULL, l), e.on(d.JWPLAYER_PLAYER_STATE, j), e.attachMedia(), e.volume(h.get("volume")), e.mute(h.get("mute")), m.on("change:state", c, o)
                    }
                }

                function j(a) {
                    switch (a.newstate) {
                        case e.PLAYING:
                            m.set("state", a.newstate);
                            break;
                        case e.PAUSED:
                            m.set("state", a.newstate)
                    }
                }

                function k(a) {
                    h.trigger(a.type, a), o.trigger(d.JWPLAYER_FULLSCREEN, {
                        fullscreen: a.jwstate
                    })
                }

                function l() {
                    m.getVideo().play()
                }
                var m, n, o = a.extend(this, b);
                return g.on(d.JWPLAYER_FULLSCREEN, function(a) {
                    this.trigger(d.JWPLAYER_FULLSCREEN, a)
                }, o), this.init = function() {
                    m = (new f).setup({
                        id: h.get("id"),
                        volume: h.get("volume"),
                        fullscreen: h.get("fullscreen"),
                        mute: h.get("mute")
                    }), m.on("fullscreenchange", k), this._adModel = m
                }, o.load = function(a) {
                    m.set("item", 0), m.set("playlistItem", a), m.setActiveItem(a), i(), m.off(d.JWPLAYER_ERROR), m.on(d.JWPLAYER_ERROR, function(a) {
                        this.trigger(d.JWPLAYER_ERROR, a)
                    }, o), m.loadVideo(a)
                }, o.applyProviderListeners = function(a) {
                    i(a), a.off(d.JWPLAYER_ERROR), a.on(d.JWPLAYER_ERROR, function(a) {
                        this.trigger(d.JWPLAYER_ERROR, a)
                    }, o), h.on("change:volume", function(a, b) {
                        n.volume(b)
                    }, o), h.on("change:mute", function(a, b) {
                        n.mute(b)
                    }, o)
                }, this.instreamDestroy = function() {
                    m && (m.off(), this.off(), n && (n.detachMedia(), n.off(), m.getVideo() && n.destroy()), m = null, g.off(null, null, this), g = null)
                }, o.instreamPlay = function() {
                    m.getVideo() && m.getVideo().play(!0)
                }, o.instreamPause = function() {
                    m.getVideo() && m.getVideo().pause(!0)
                }, o
            };
            return g
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(62)], e = function(a) {
            function b(b) {
                return b === a.COMPLETE || b === a.ERROR ? a.IDLE : b
            }
            return function(a, c, d) {
                if (c = b(c), d = b(d), c !== d) {
                    var e = c.replace(/(?:ing|d)$/, ""),
                        f = {
                            type: e,
                            newstate: c,
                            oldstate: d,
                            reason: a.mediaModel.get("state")
                        };
                    "play" === e && (f.playReason = a.get("playReason")), this.trigger(e, f)
                }
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(78), c(89), c(90), c(45), c(47), c(91), c(46), c(62)], e = function(a, b, c, d, e, f, g, h, i) {
            var j = ["volume", "mute", "captionLabel", "qualityLabel"],
                k = function() {
                    function g(a, b) {
                        switch (a) {
                            case "flashThrottle":
                                var c = "resume" !== b.state;
                                this.set("flashThrottle", c), this.set("flashBlocked", c);
                                break;
                            case "flashBlocked":
                                return void this.set("flashBlocked", !0);
                            case "flashUnblocked":
                                return void this.set("flashBlocked", !1);
                            case "volume":
                            case "mute":
                                return void this.set(a, b[a]);
                            case h.JWPLAYER_MEDIA_TYPE:
                                this.mediaModel.set("mediaType", b.mediaType);
                                break;
                            case h.JWPLAYER_PLAYER_STATE:
                                return void this.mediaModel.set("state", b.newstate);
                            case h.JWPLAYER_MEDIA_BUFFER:
                                this.set("buffer", b.bufferPercent);
                            case h.JWPLAYER_MEDIA_META:
                                var d = b.duration;
                                e.isNumber(d) && (this.mediaModel.set("duration", d), this.set("duration", d));
                                break;
                            case h.JWPLAYER_MEDIA_BUFFER_FULL:
                                this.mediaModel.get("playAttempt") ? this.playVideo() : this.mediaModel.on("change:playAttempt", function() {
                                    this.playVideo()
                                }, this);
                                break;
                            case h.JWPLAYER_MEDIA_TIME:
                                this.mediaModel.set("position", b.position), this.set("position", b.position), e.isNumber(b.duration) && (this.mediaModel.set("duration", b.duration), this.set("duration", b.duration));
                                break;
                            case h.JWPLAYER_PROVIDER_CHANGED:
                                this.set("provider", m.getName());
                                break;
                            case h.JWPLAYER_MEDIA_LEVELS:
                                this.setQualityLevel(b.currentQuality, b.levels), this.mediaModel.set("levels", b.levels);
                                break;
                            case h.JWPLAYER_MEDIA_LEVEL_CHANGED:
                                this.setQualityLevel(b.currentQuality, b.levels), this.persistQualityLevel(b.currentQuality, b.levels);
                                break;
                            case h.JWPLAYER_AUDIO_TRACKS:
                                this.setCurrentAudioTrack(b.currentTrack, b.tracks), this.mediaModel.set("audioTracks", b.tracks);
                                break;
                            case h.JWPLAYER_AUDIO_TRACK_CHANGED:
                                this.setCurrentAudioTrack(b.currentTrack, b.tracks);
                                break;
                            case "subtitlesTrackChanged":
                                this.setVideoSubtitleTrack(b.currentTrack, b.tracks);
                                break;
                            case "visualQuality":
                                var f = e.extend({}, b);
                                this.mediaModel.set("visualQuality", f)
                        }
                        var g = e.extend({}, b, {
                            type: a
                        });
                        this.mediaController.trigger(a, g)
                    }
                    var k, m, n = this,
                        o = a.noop;
                    this.mediaController = e.extend({}, f), this.mediaModel = new l, d.model(this), this.set("mediaModel", this.mediaModel), this.setup = function(b) {
                        var d = new c;
                        return d.track(j, this), e.extend(this.attributes, d.getAllItems(), b, {
                            item: 0,
                            state: i.IDLE,
                            flashBlocked: !1,
                            fullscreen: !1,
                            compactUI: !1,
                            scrubbing: !1,
                            duration: 0,
                            position: 0,
                            buffer: 0
                        }), a.isMobile() && !b.mobileSdk && this.set("autostart", !1), this.updateProviders(), this
                    }, this.getConfiguration = function() {
                        return e.omit(this.clone(), ["mediaModel"])
                    }, this.updateProviders = function() {
                        k = new b(this.getConfiguration())
                    }, this.setQualityLevel = function(a, b) {
                        a > -1 && b.length > 1 && "youtube" !== m.getName().name && this.mediaModel.set("currentLevel", parseInt(a))
                    }, this.persistQualityLevel = function(a, b) {
                        var c = b[a] || {},
                            d = c.label;
                        this.set("qualityLabel", d)
                    }, this.setCurrentAudioTrack = function(a, b) {
                        a > -1 && b.length > 0 && a < b.length && this.mediaModel.set("currentAudioTrack", parseInt(a))
                    }, this.onMediaContainer = function() {
                        var a = this.get("mediaContainer");
                        o.setContainer(a)
                    }, this.changeVideoProvider = function(a) {
                        this.off("change:mediaContainer", this.onMediaContainer), m && (m.off(null, null, this), m.getContainer() && m.remove()), o = new a(n.get("id"), n.getConfiguration());
                        var b = this.get("mediaContainer");
                        b ? o.setContainer(b) : this.once("change:mediaContainer", this.onMediaContainer), this.set("provider", o.getName()), -1 === o.getName().name.indexOf("flash") && (this.set("flashThrottle", void 0), this.set("flashBlocked", !1)), m = o, m.volume(n.get("volume")), m.mute(n.get("mute")), m.on("all", g, this)
                    }, this.destroy = function() {
                        this.off(), m && (m.off(null, null, this), m.destroy())
                    }, this.getVideo = function() {
                        return m
                    }, this.setFullscreen = function(a) {
                        a = !!a, a !== n.get("fullscreen") && n.set("fullscreen", a)
                    }, this.chooseProvider = function(a) {
                        return k.choose(a).provider
                    }, this.setActiveItem = function(a) {
                        this.mediaModel.off(), this.mediaModel = new l, this.set("mediaModel", this.mediaModel);
                        var b = a && a.sources && a.sources[0];
                        if (void 0 !== b) {
                            var c = this.chooseProvider(b);
                            if (!c) throw new Error("No suitable provider found");
                            o instanceof c || n.changeVideoProvider(c), o.init && o.init(a), this.trigger("itemReady", a)
                        }
                    }, this.getProviders = function() {
                        return k
                    }, this.resetProvider = function() {
                        o = null
                    }, this.setVolume = function(a) {
                        a = Math.round(a), n.set("volume", a), m && m.volume(a);
                        var b = 0 === a;
                        b !== n.get("mute") && n.setMute(b)
                    }, this.setMute = function(b) {
                        if (a.exists(b) || (b = !n.get("mute")), n.set("mute", b), m && m.mute(b), !b) {
                            var c = Math.max(10, n.get("volume"));
                            this.setVolume(c)
                        }
                    }, this.loadVideo = function(a) {
                        if (this.mediaModel.set("playAttempt", !0), this.mediaController.trigger(h.JWPLAYER_MEDIA_PLAY_ATTEMPT, {
                                playReason: this.get("playReason")
                            }), !a) {
                            var b = this.get("item");
                            a = this.get("playlist")[b]
                        }
                        this.set("position", a.starttime || 0), this.set("duration", a.duration || 0), m.load(a)
                    }, this.stopVideo = function() {
                        m && m.stop()
                    }, this.playVideo = function() {
                        m.play()
                    }, this.persistCaptionsTrack = function() {
                        var a = this.get("captionsTrack");
                        a ? this.set("captionLabel", a.label) : this.set("captionLabel", "Off")
                    }, this.setVideoSubtitleTrack = function(a, b) {
                        this.set("captionsIndex", a), a && b && a <= b.length && b[a - 1].data && this.set("captionsTrack", b[a - 1]), m && m.setSubtitlesTrack && m.setSubtitlesTrack(a)
                    }, this.persistVideoSubtitleTrack = function(a) {
                        this.setVideoSubtitleTrack(a), this.persistCaptionsTrack()
                    }
                },
                l = k.MediaModel = function() {
                    this.set("state", i.IDLE)
                };
            return e.extend(k.prototype, g), e.extend(l.prototype, g), k
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(79)], e = function(a) {
            return a.prototype.providerSupports = function(a, b) {
                return a.supports(b, this.config.edition)
            }, a
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(80), c(84), c(45)], e = function(a, b, c) {
            function d(b) {
                this.providers = a.slice(), this.config = b || {}, "flash" === this.config.primary && f(this.providers, "html5", "flash")
            }

            function e(a, b) {
                for (var c = 0; c < a.length; c++)
                    if (a[c].name === b) return c;
                return -1
            }

            function f(a, b, c) {
                var d = e(a, b),
                    f = e(a, c),
                    g = a[d];
                a[d] = a[f], a[f] = g
            }
            return c.extend(d.prototype, {
                providerSupports: function(a, b) {
                    return a.supports(b)
                },
                choose: function(a) {
                    a = c.isObject(a) ? a : {};
                    for (var d = this.providers.length, e = 0; d > e; e++) {
                        var f = this.providers[e];
                        if (this.providerSupports(f, a)) {
                            var g = d - e - 1;
                            return {
                                priority: g,
                                name: f.name,
                                type: a.type,
                                provider: b[f.name]
                            }
                        }
                    }
                    return null
                }
            }), d
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(81), c(45), c(82)], e = function(a, b, c, d) {
            function e(c, d) {
                var e = b(d);
                if (!e("dash")) return !1;
                if (c.drm && !e("drm")) return !1;
                if (!window.MediaSource) return !1;
                if (!a.isChrome() && !a.isIETrident(11)) return !1;
                var f = c.file || "";
                return "dash" === c.type || "mpd" === c.type || f.indexOf(".mpd") > -1 || f.indexOf("mpd-time-csf") > -1
            }
            var f = c.find(d, c.matches({
                    name: "flash"
                })),
                g = f.supports;
            return f.supports = function(c, d) {
                if (!a.isFlashSupported()) return !1;
                var e = c && c.type;
                if ("hls" === e || "m3u8" === e) {
                    var f = b(d);
                    return f("hls")
                }
                return g.apply(this, arguments)
            }, d.push({
                name: "dashjs",
                supports: c.constant(!1)
            }), d.push({
                name: "shaka",
                supports: e
            }), d
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45)], e = function(a) {
            var b = "free",
                c = "premium",
                d = "enterprise",
                e = "ads",
                f = "unlimited",
                g = "trial",
                h = {
                    setup: [b, c, d, e, f, g],
                    dash: [c, d, e, f, g],
                    drm: [d, e, f, g],
                    hls: [c, e, d, f, g],
                    ads: [e, f, g],
                    casting: [c, d, e, f, g],
                    jwpsrv: [b, c, d, e, g]
                },
                i = function(b) {
                    return function(c) {
                        return a.contains(h[c], b)
                    }
                };
            return i
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(45), c(83)], e = function(a, b, c) {
            function d(b) {
                if ("hls" === b.type)
                    if (b.androidhls !== !1) {
                        var c = a.isAndroidNative;
                        if (c(2) || c(3) || c("4.0")) return !1;
                        if (a.isAndroid()) return !0
                    } else if (a.isAndroid()) return !1;
                return null
            }
            var e = [{
                name: "youtube",
                supports: function(b) {
                    return a.isYouTube(b.file, b.type)
                }
            }, {
                name: "html5",
                supports: function(b) {
                    var e = {
                            aac: "audio/mp4",
                            mp4: "video/mp4",
                            f4v: "video/mp4",
                            m4v: "video/mp4",
                            mov: "video/mp4",
                            mp3: "audio/mpeg",
                            mpeg: "audio/mpeg",
                            ogv: "video/ogg",
                            ogg: "video/ogg",
                            oga: "video/ogg",
                            vorbis: "video/ogg",
                            webm: "video/webm",
                            f4a: "video/aac",
                            m3u8: "application/vnd.apple.mpegurl",
                            m3u: "application/vnd.apple.mpegurl",
                            hls: "application/vnd.apple.mpegurl"
                        },
                        f = b.file,
                        g = b.type,
                        h = d(b);
                    if (null !== h) return h;
                    if (a.isRtmp(f, g)) return !1;
                    if (!e[g]) return !1;
                    if (c.canPlayType) {
                        var i = c.canPlayType(e[g]);
                        return !!i
                    }
                    return !1
                }
            }, {
                name: "flash",
                supports: function(c) {
                    var d = {
                            flv: "video",
                            f4v: "video",
                            mov: "video",
                            m4a: "video",
                            m4v: "video",
                            mp4: "video",
                            aac: "video",
                            f4a: "video",
                            mp3: "sound",
                            mpeg: "sound",
                            smil: "rtmp"
                        },
                        e = b.keys(d);
                    if (!a.isFlashSupported()) return !1;
                    var f = c.file,
                        g = c.type;
                    return a.isRtmp(f, g) ? !0 : b.contains(e, g)
                }
            }];
            return e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [], e = function() {
            return document.createElement("video")
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(85), c(87)], e = function(a, b) {
            var c = {
                html5: a,
                flash: b
            };
            return c
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(55), c(48), c(45), c(46), c(62), c(86), c(47)], e = function(a, b, c, d, e, f, g) {
            function h(a, c) {
                b.foreach(a, function(a, b) {
                    c.addEventListener(a, b, !1)
                })
            }

            function i(a, c) {
                b.foreach(a, function(a, b) {
                    c.removeEventListener(a, b, !1)
                })
            }

            function j(a, b, c) {
                "addEventListener" in a ? a.addEventListener(b, c) : a["on" + b] = c
            }

            function k(a, b, c) {
                a && ("removeEventListener" in a ? a.removeEventListener(b, c) : a["on" + b] = null)
            }

            function l(a) {
                if ("hls" === a.type)
                    if (a.androidhls !== !1) {
                        var c = b.isAndroidNative;
                        if (c(2) || c(3) || c("4.0")) return !1;
                        if (b.isAndroid()) return !0
                    } else if (b.isAndroid()) return !1;
                return null
            }

            function m(m, y) {
                function z() {
                    na(Ua.audioTracks), ra(Ua.textTracks)
                }

                function A(a) {
                    Ca.trigger("click", a)
                }

                function B() {
                    Ia && !Ka && (I(H()), F(da(), za, ya))
                }

                function C() {
                    Ia && F(da(), za, ya)
                }

                function D() {
                    n(Ga), Ea = !0, Ia && (Ca.state === e.STALLED ? Ca.setState(e.PLAYING) : Ca.state === e.PLAYING && (Ga = setTimeout(ca, o)), Ka && Ua.duration === 1 / 0 && 0 === Ua.currentTime || (I(H()), G(Ua.currentTime), F(da(), za, ya), Ca.state === e.PLAYING && (Ca.trigger(d.JWPLAYER_MEDIA_TIME, {
                        position: za,
                        duration: ya
                    }), E())))
                }

                function E() {
                    var a = Sa.level;
                    if (a.width !== Ua.videoWidth || a.height !== Ua.videoHeight) {
                        if (a.width = Ua.videoWidth, a.height = Ua.videoHeight, ua(), !a.width || !a.height) return;
                        Sa.reason = Sa.reason || "auto", Sa.mode = "hls" === Ba[Ja].type ? "auto" : "manual", Sa.bitrate = 0, a.index = Ja, a.label = Ba[Ja].label, Ca.trigger("visualQuality", Sa),
                            Sa.reason = ""
                    }
                }

                function F(a, b, c) {
                    a === Ha && c === ya || (Ha = a, Ca.trigger(d.JWPLAYER_MEDIA_BUFFER, {
                        bufferPercent: 100 * a,
                        position: b,
                        duration: c
                    }))
                }

                function G(a) {
                    0 > ya && (a = -(_() - a)), za = a
                }

                function H() {
                    var a = Ua.duration,
                        b = _();
                    if (a === 1 / 0 && b) {
                        var c = b - Ua.seekable.start(0);
                        c !== 1 / 0 && c > 120 && (a = -c)
                    }
                    return a
                }

                function I(a) {
                    ya = a, Fa && a && a !== 1 / 0 && Ca.seek(Fa)
                }

                function J() {
                    var a = H();
                    Ka && a === 1 / 0 && (a = 0), Ca.trigger(d.JWPLAYER_MEDIA_META, {
                        duration: a,
                        height: Ua.videoHeight,
                        width: Ua.videoWidth
                    }), I(a)
                }

                function K() {
                    Ia && (Ea = !0, M())
                }

                function L() {
                    Ia && (Ua.muted && (Ua.muted = !1, Ua.muted = !0), ua(), J())
                }

                function M() {
                    Aa || (Aa = !0, Ca.trigger(d.JWPLAYER_MEDIA_BUFFER_FULL))
                }

                function N() {
                    Ca.setState(e.PLAYING), Ua.hasAttribute("hasplayed") || Ua.setAttribute("hasplayed", ""), Ca.trigger(d.JWPLAYER_PROVIDER_FIRST_FRAME, {})
                }

                function O() {
                    Ca.state !== e.COMPLETE && Ua.currentTime !== Ua.duration && Ca.setState(e.PAUSED)
                }

                function P() {
                    Ka || Ua.paused || Ua.ended || Ca.state !== e.LOADING && Ca.state !== e.ERROR && (Ca.seeking || Ca.setState(e.STALLED))
                }

                function Q() {
                    Ia && (b.log("Error playing media: %o %s", Ua.error, Ua.src || xa.file), Ca.trigger(d.JWPLAYER_MEDIA_ERROR, {
                        message: "Error loading media: File could not be played"
                    }))
                }

                function R(a) {
                    var d;
                    return "array" === b.typeOf(a) && a.length > 0 && (d = c.map(a, function(a, b) {
                        return {
                            label: a.label || b
                        }
                    })), d
                }

                function S(a) {
                    Ba = a, Ja = T(a);
                    var b = R(a);
                    b && Ca.trigger(d.JWPLAYER_MEDIA_LEVELS, {
                        levels: b,
                        currentQuality: Ja
                    })
                }

                function T(a) {
                    var b = Math.max(0, Ja),
                        c = y.qualityLabel;
                    if (a)
                        for (var d = 0; d < a.length; d++)
                            if (a[d]["default"] && (b = d), c && a[d].label === c) return d;
                    return Sa.reason = "initial choice", Sa.level = {
                        width: 0,
                        height: 0
                    }, b
                }

                function U() {
                    return r || s
                }

                function V(a, c, d) {
                    xa = Ba[Ja], Fa = 0, n(Ga);
                    var f = document.createElement("source");
                    f.src = xa.file;
                    var g = Ua.src !== f.src;
                    g || U() ? (ya = c, W(d), Ua.load()) : (0 === a && 0 !== Ua.currentTime && (Fa = -1, Ca.seek(a)), Ua.play()), za = Ua.currentTime, r && (M(), Ua.paused || Ca.state === e.PLAYING || Ca.setState(e.LOADING)), b.isIOS() && Ca.getFullScreen() && (Ua.controls = !0), a > 0 && Ca.seek(a)
                }

                function W(a) {
                    if (Na = null, Oa = null, Qa = -1, Pa = -1, Ra = -1, Sa.reason || (Sa.reason = "initial choice", Sa.level = {
                            width: 0,
                            height: 0
                        }), Ea = !1, Aa = !1, Ka = l(xa), Ua.src = xa.file, xa.preload && Ua.setAttribute("preload", xa.preload), a && a.tracks) {
                        var c = b.isIOS() && !b.isSDK(y);
                        c && Y(a.tracks)
                    }
                }

                function X() {
                    Ua && (Ua.removeAttribute("src"), !q && Ua.load && Ua.load())
                }

                function Y(a) {
                    for (; Ua.firstChild;) Ua.removeChild(Ua.firstChild);
                    Z(a)
                }

                function Z(a) {
                    if (a) {
                        Ua.setAttribute("crossorigin", "anonymous");
                        for (var b = 0; b < a.length; b++)
                            if (-1 !== a[b].file.indexOf(".vtt") && /subtitles|captions|descriptions|chapters|metadata/.test(a[b].kind)) {
                                var c = document.createElement("track");
                                c.src = a[b].file, c.kind = a[b].kind, c.srclang = a[b].language || "", c.label = a[b].label, c.mode = "disabled", Ua.appendChild(c)
                            }
                    }
                }

                function $() {
                    for (var a = Ua.seekable ? Ua.seekable.length : 0, b = 1 / 0; a--;) b = Math.min(b, Ua.seekable.start(a));
                    return b
                }

                function _() {
                    for (var a = Ua.seekable ? Ua.seekable.length : 0, b = 0; a--;) b = Math.max(b, Ua.seekable.end(a));
                    return b
                }

                function aa() {
                    Ca.seeking = !1, Ca.trigger(d.JWPLAYER_MEDIA_SEEKED)
                }

                function ba() {
                    Ca.trigger("volume", {
                        volume: Math.round(100 * Ua.volume)
                    }), Ca.trigger("mute", {
                        mute: Ua.muted
                    })
                }

                function ca() {
                    Ua.currentTime === za && P()
                }

                function da() {
                    var a = Ua.buffered,
                        c = Ua.duration;
                    return !a || 0 === a.length || 0 >= c || c === 1 / 0 ? 0 : b.between(a.end(a.length - 1) / c, 0, 1)
                }

                function ea() {
                    if (Ia && Ca.state !== e.IDLE && Ca.state !== e.COMPLETE) {
                        if (n(Ga), Ja = -1, La = !0, Ca.trigger(d.JWPLAYER_MEDIA_BEFORECOMPLETE), !Ia) return;
                        fa()
                    }
                }

                function fa() {
                    n(Ga), Ca.setState(e.COMPLETE), La = !1, Ca.trigger(d.JWPLAYER_MEDIA_COMPLETE)
                }

                function ga(a) {
                    Ma = !0, ma(a), b.isIOS() && (Ua.controls = !1)
                }

                function ha() {
                    var a = -1,
                        b = 0;
                    if (Na)
                        for (b; b < Na.length; b++)
                            if ("showing" === Na[b].mode) {
                                a = b;
                                break
                            }
                    sa(a + 1)
                }

                function ia() {
                    for (var a = -1, b = 0; b < Ua.audioTracks.length; b++)
                        if (Ua.audioTracks[b].enabled) {
                            a = b;
                            break
                        }
                    oa(a)
                }

                function ja(a) {
                    ka(a.currentTarget.activeCues)
                }

                function ka(a) {
                    if (a && a.length && Ra !== a[0].startTime) {
                        var b = {
                                TIT2: "title",
                                TT2: "title",
                                WXXX: "url",
                                TPE1: "artist",
                                TP1: "artist",
                                TALB: "album",
                                TAL: "album"
                            },
                            d = function(a, b) {
                                var c, d, e, f, g, h;
                                for (c = "", e = a.length, d = b || 0; e > d;) switch (f = a[d++], f >> 4) {
                                    case 0:
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                    case 5:
                                    case 6:
                                    case 7:
                                        c += String.fromCharCode(f);
                                        break;
                                    case 12:
                                    case 13:
                                        g = a[d++], c += String.fromCharCode((31 & f) << 6 | 63 & g);
                                        break;
                                    case 14:
                                        g = a[d++], h = a[d++], c += String.fromCharCode((15 & f) << 12 | (63 & g) << 6 | (63 & h) << 0)
                                }
                                return c
                            },
                            e = function(a, b) {
                                var c, d, e;
                                for (c = "", e = a.length, d = b || 0; e > d;) 254 === a[d] && 255 === a[d + 1] || (c += String.fromCharCode((a[d] << 8) + a[d + 1])), d += 2;
                                return c
                            },
                            f = c.reduce(a, function(a, f) {
                                if (!("value" in f) && "data" in f && f.data instanceof ArrayBuffer) {
                                    var g = f,
                                        h = new Uint8Array(g.data);
                                    f = {
                                        value: {
                                            key: "",
                                            data: ""
                                        }
                                    };
                                    for (var i = 10; 14 > i && i < h.length && 0 !== h[i];) f.value.key += String.fromCharCode(h[i]), i++;
                                    var j = h[20];
                                    1 === j || 2 === j ? f.value.data = e(h, 21) : f.value.data = d(h, 21)
                                }
                                if (b.hasOwnProperty(f.value.key) && (a[b[f.value.key]] = f.value.data), f.value.info) {
                                    var k = a[f.value.key];
                                    c.isObject(k) || (k = {}, a[f.value.key] = k), k[f.value.info] = f.value.data
                                } else a[f.value.key] = f.value.data;
                                return a
                            }, {});
                        Ra = a[0].startTime, Ca.trigger("meta", {
                            metadataTime: Ra,
                            metadata: f
                        })
                    }
                }

                function la(a) {
                    Ma = !1, ma(a), b.isIOS() && (Ua.controls = !1)
                }

                function ma(a) {
                    Ca.trigger("fullscreenchange", {
                        target: a.target,
                        jwstate: Ma
                    })
                }

                function na(a) {
                    if (Oa = null, a) {
                        if (a.length) {
                            for (var b = 0; b < a.length; b++)
                                if (a[b].enabled) {
                                    Qa = b;
                                    break
                                } - 1 === Qa && (Qa = 0, a[Qa].enabled = !0), Oa = c.map(a, function(a) {
                                var b = {
                                    name: a.label || a.language,
                                    language: a.language
                                };
                                return b
                            })
                        }
                        j(a, "change", ia), Oa && Ca.trigger("audioTracks", {
                            currentTrack: Qa,
                            tracks: Oa
                        })
                    }
                }

                function oa(a) {
                    Ua && Ua.audioTracks && Oa && a > -1 && a < Ua.audioTracks.length && a !== Qa && (Ua.audioTracks[Qa].enabled = !1, Qa = a, Ua.audioTracks[Qa].enabled = !0, Ca.trigger("audioTrackChanged", {
                        currentTrack: Qa,
                        tracks: Oa
                    }))
                }

                function pa() {
                    return Oa || []
                }

                function qa() {
                    return Qa
                }

                function ra(a) {
                    if (Na = null, a) {
                        if (a.length) {
                            var b = 0,
                                c = a.length;
                            for (b; c > b; b++) "metadata" === a[b].kind ? (a[b].oncuechange = ja, a[b].mode = "showing") : "subtitles" !== a[b].kind && "captions" !== a[b].kind || (a[b].mode = "disabled", Na || (Na = []), Na.push(a[b]))
                        }
                        j(a, "change", ha), Na && Na.length && Ca.trigger("subtitlesTracks", {
                            tracks: Na
                        })
                    }
                }

                function sa(a) {
                    Na && Pa !== a - 1 && (Pa > -1 && Pa < Na.length ? Na[Pa].mode = "disabled" : c.each(Na, function(a) {
                        a.mode = "disabled"
                    }), a > 0 && a <= Na.length ? (Pa = a - 1, Na[Pa].mode = "showing") : Pa = -1, Ca.trigger("subtitlesTrackChanged", {
                        currentTrack: Pa + 1,
                        tracks: Na
                    }))
                }

                function ta() {
                    return Pa
                }

                function ua() {
                    if ("hls" === Ba[0].type) {
                        var a = "video";
                        0 === Ua.videoWidth && (a = "audio"), Ca.trigger("mediaType", {
                            mediaType: a
                        })
                    }
                }

                function va() {
                    Na && Na[Pa] && (Na[Pa].mode = "disabled")
                }
                this.state = e.IDLE, this.seeking = !1, c.extend(this, g), this.trigger = function(a, b) {
                    return Ia ? g.trigger.call(this, a, b) : void 0
                }, this.setState = function(a) {
                    return Ia ? f.setState.call(this, a) : void 0
                };
                var wa, xa, ya, za, Aa, Ba, Ca = this,
                    Da = {
                        click: A,
                        durationchange: B,
                        ended: ea,
                        error: Q,
                        loadeddata: z,
                        loadedmetadata: L,
                        canplay: K,
                        playing: N,
                        progress: C,
                        pause: O,
                        seeked: aa,
                        timeupdate: D,
                        volumechange: ba,
                        webkitbeginfullscreen: ga,
                        webkitendfullscreen: la
                    },
                    Ea = !1,
                    Fa = 0,
                    Ga = -1,
                    Ha = -1,
                    Ia = !0,
                    Ja = -1,
                    Ka = null,
                    La = !1,
                    Ma = !1,
                    Na = null,
                    Oa = null,
                    Pa = -1,
                    Qa = -1,
                    Ra = -1,
                    Sa = {
                        level: {}
                    },
                    Ta = document.getElementById(m),
                    Ua = Ta ? Ta.querySelector("video") : void 0;
                Ua = Ua || document.createElement("video"), Ua.className = "jw-video jw-reset", h(Da, Ua), v || (Ua.controls = !0, Ua.controls = !1), Ua.setAttribute("x-webkit-airplay", "allow"), Ua.setAttribute("webkit-playsinline", ""), this.stop = function() {
                    n(Ga), Ia && (X(), b.isIETrident() && Ua.pause(), Ja = -1, this.setState(e.IDLE))
                }, this.destroy = function() {
                    i(Da, Ua), k(Ua.audioTracks, "change", ia), k(Ua.textTracks, "change", ha), this.remove(), this.off()
                }, this.init = function(a) {
                    Ia && (Ba = a.sources, Ja = T(a.sources), a.sources.length && "hls" !== a.sources[0].type && this.sendMediaType(a.sources), xa = Ba[Ja], za = a.starttime || 0, ya = a.duration || 0, Sa.reason = "", W(a))
                }, this.load = function(a) {
                    Ia && (S(a.sources), a.sources.length && "hls" !== a.sources[0].type && this.sendMediaType(a.sources), r && !Ua.hasAttribute("hasplayed") || Ca.setState(e.LOADING), V(a.starttime || 0, a.duration || 0, a))
                }, this.play = function() {
                    return Ca.seeking ? (Ca.setState(e.LOADING), void Ca.once(d.JWPLAYER_MEDIA_SEEKED, Ca.play)) : void Ua.play()
                }, this.pause = function() {
                    n(Ga), Ua.pause(), this.setState(e.PAUSED)
                }, this.seek = function(a) {
                    if (Ia)
                        if (0 > a && (a += $() + _()), 0 === Fa && this.trigger(d.JWPLAYER_MEDIA_SEEK, {
                                position: Ua.currentTime,
                                offset: a
                            }), Ea || (Ea = !!_()), Ea) {
                            Fa = 0;
                            try {
                                Ca.seeking = !0, Ua.currentTime = a
                            } catch (b) {
                                Ca.seeking = !1, Fa = a
                            }
                        } else Fa = a, t && Ua.paused && Ua.play()
                }, this.volume = function(a) {
                    a = b.between(a / 100, 0, 1), Ua.volume = a
                }, this.mute = function(a) {
                    Ua.muted = !!a
                }, this.checkComplete = function() {
                    return La
                }, this.detachMedia = function() {
                    return n(Ga), va(), Ia = !1, Ua
                }, this.attachMedia = function() {
                    Ia = !0, Ea = !1, this.seeking = !1, Ua.loop = !1, La && fa()
                }, this.setContainer = function(a) {
                    wa = a, a.appendChild(Ua)
                }, this.getContainer = function() {
                    return wa
                }, this.remove = function() {
                    X(), n(Ga), Ja = -1, wa === Ua.parentNode && wa.removeChild(Ua)
                }, this.setVisibility = function(b) {
                    b = !!b, b || u ? a.style(wa, {
                        visibility: "visible",
                        opacity: 1
                    }) : a.style(wa, {
                        visibility: "",
                        opacity: 0
                    })
                }, this.resize = function(b, c, d) {
                    if (!(b && c && Ua.videoWidth && Ua.videoHeight)) return !1;
                    var e = {
                        objectFit: ""
                    };
                    if ("uniform" === d) {
                        var f = b / c,
                            g = Ua.videoWidth / Ua.videoHeight;
                        Math.abs(f - g) < .09 && (e.objectFit = "fill", d = "exactfit")
                    }
                    var h = p || u || v || w;
                    if (h) {
                        var i = -Math.floor(Ua.videoWidth / 2 + 1),
                            j = -Math.floor(Ua.videoHeight / 2 + 1),
                            k = Math.ceil(100 * b / Ua.videoWidth) / 100,
                            l = Math.ceil(100 * c / Ua.videoHeight) / 100;
                        "none" === d ? k = l = 1 : "fill" === d ? k = l = Math.max(k, l) : "uniform" === d && (k = l = Math.min(k, l)), e.width = Ua.videoWidth, e.height = Ua.videoHeight, e.top = e.left = "50%", e.margin = 0, a.transform(Ua, "translate(" + i + "px, " + j + "px) scale(" + k.toFixed(2) + ", " + l.toFixed(2) + ")")
                    }
                    return a.style(Ua, e), !1
                }, this.setFullscreen = function(a) {
                    if (a = !!a) {
                        var c = b.tryCatch(function() {
                            var a = Ua.webkitEnterFullscreen || Ua.webkitEnterFullScreen;
                            a && a.apply(Ua)
                        });
                        return c instanceof b.Error ? !1 : Ca.getFullScreen()
                    }
                    var d = Ua.webkitExitFullscreen || Ua.webkitExitFullScreen;
                    return d && d.apply(Ua), a
                }, Ca.getFullScreen = function() {
                    return Ma || !!Ua.webkitDisplayingFullscreen
                }, this.setCurrentQuality = function(a) {
                    if (Ja !== a && (a = parseInt(a, 10), a >= 0 && Ba && Ba.length > a)) {
                        Ja = a, Sa.reason = "api", Sa.level = {
                            width: 0,
                            height: 0
                        }, this.trigger(d.JWPLAYER_MEDIA_LEVEL_CHANGED, {
                            currentQuality: a,
                            levels: R(Ba)
                        }), y.qualityLabel = Ba[a].label;
                        var b = Ua.currentTime || 0,
                            c = Ua.duration || 0;
                        0 >= c && (c = ya), Ca.setState(e.LOADING), V(b, c)
                    }
                }, this.getCurrentQuality = function() {
                    return Ja
                }, this.getQualityLevels = function() {
                    return R(Ba)
                }, this.getName = function() {
                    return {
                        name: x
                    }
                }, this.setCurrentAudioTrack = oa, this.getAudioTracks = pa, this.getCurrentAudioTrack = qa, this.setSubtitlesTrack = sa, this.getSubtitlesTrack = ta
            }
            var n = window.clearTimeout,
                o = 256,
                p = b.isIE(),
                q = b.isMSIE(),
                r = b.isMobile(),
                s = b.isSafari(),
                t = b.isFF(),
                u = b.isAndroidNative(),
                v = b.isIOS(7),
                w = b.isIOS(8),
                x = "html5",
                y = function() {};
            return y.prototype = f, m.prototype = new y, m
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(46), c(62), c(45)], e = function(a, b, c, d) {
            var e = a.noop,
                f = d.constant(!1),
                g = {
                    supports: f,
                    play: e,
                    load: e,
                    stop: e,
                    volume: e,
                    mute: e,
                    seek: e,
                    resize: e,
                    remove: e,
                    destroy: e,
                    setVisibility: e,
                    setFullscreen: f,
                    getFullscreen: e,
                    getContainer: e,
                    setContainer: f,
                    getName: e,
                    getQualityLevels: e,
                    getCurrentQuality: e,
                    setCurrentQuality: e,
                    getAudioTracks: e,
                    getCurrentAudioTrack: e,
                    setCurrentAudioTrack: e,
                    checkComplete: e,
                    setControls: e,
                    attachMedia: e,
                    detachMedia: e,
                    setState: function(a) {
                        var d = this.state || c.IDLE;
                        this.state = a, a !== d && this.trigger(b.JWPLAYER_PLAYER_STATE, {
                            newstate: a
                        })
                    },
                    sendMediaType: function(a) {
                        var c = a[0].type,
                            d = "oga" === c || "aac" === c || "mp3" === c || "mpeg" === c || "vorbis" === c;
                        this.trigger(b.JWPLAYER_MEDIA_TYPE, {
                            mediaType: d ? "audio" : "video"
                        })
                    }
                };
            return g
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(45), c(46), c(62), c(88), c(86), c(47)], e = function(a, b, c, d, e, f, g) {
            function h(a) {
                return a + "_swf_" + k++
            }

            function i(b) {
                var c = document.createElement("a");
                c.href = b.flashplayer;
                var d = c.hostname === window.location.host;
                return a.isChrome() && !d
            }

            function j(j, k) {
                function l(a) {
                    if (G)
                        for (var b = 0; b < a.length; b++) {
                            var c = a[b];
                            if (c.bitrate) {
                                var d = Math.round(c.bitrate / 1e3);
                                c.label = m(d)
                            }
                        }
                }

                function m(a) {
                    var b = G[a];
                    if (!b) {
                        for (var c = 1 / 0, d = G.bitrates.length; d--;) {
                            var e = Math.abs(G.bitrates[d] - a);
                            if (e > c) break;
                            c = e
                        }
                        b = G.labels[G.bitrates[d + 1]], G[a] = b
                    }
                    return b
                }

                function n() {
                    var a = k.hlslabels;
                    if (!a) return null;
                    var b = {},
                        c = [];
                    for (var d in a) {
                        var e = parseFloat(d);
                        if (!isNaN(e)) {
                            var f = Math.round(e);
                            b[f] = a[d], c.push(f)
                        }
                    }
                    return 0 === c.length ? null : (c.sort(function(a, b) {
                        return a - b
                    }), {
                        labels: b,
                        bitrates: c
                    })
                }

                function o() {
                    v = setTimeout(function() {
                        g.trigger.call(D, "flashBlocked")
                    }, 4e3), s.once("embedded", function() {
                        q(), g.trigger.call(D, "flashUnblocked")
                    }, D)
                }

                function p() {
                    q(), o()
                }

                function q() {
                    clearTimeout(v), window.removeEventListener("focus", p)
                }
                var r, s, t, u = null,
                    v = -1,
                    w = !1,
                    x = -1,
                    y = null,
                    z = -1,
                    A = null,
                    B = !0,
                    C = !1,
                    D = this,
                    E = function() {
                        return s && s.__ready
                    },
                    F = function() {
                        s && s.triggerFlash.apply(s, arguments)
                    },
                    G = n();
                b.extend(this, g, {
                    init: function(a) {
                        a.preload && "none" !== a.preload && !k.autostart && (u = a)
                    },
                    load: function(a) {
                        u = a, w = !1, this.setState(d.LOADING), F("load", a), a.sources.length && "hls" !== a.sources[0].type && this.sendMediaType(a.sources)
                    },
                    play: function() {
                        F("play")
                    },
                    pause: function() {
                        F("pause"), this.setState(d.PAUSED)
                    },
                    stop: function() {
                        F("stop"), x = -1, u = null, this.setState(d.IDLE)
                    },
                    seek: function(a) {
                        F("seek", a)
                    },
                    volume: function(a) {
                        if (b.isNumber(a)) {
                            var c = Math.min(Math.max(0, a), 100);
                            E() && F("volume", c)
                        }
                    },
                    mute: function(a) {
                        E() && F("mute", a)
                    },
                    setState: function() {
                        return f.setState.apply(this, arguments)
                    },
                    checkComplete: function() {
                        return w
                    },
                    attachMedia: function() {
                        B = !0, w && (this.setState(d.COMPLETE), this.trigger(c.JWPLAYER_MEDIA_COMPLETE), w = !1)
                    },
                    detachMedia: function() {
                        return B = !1, null
                    },
                    getSwfObject: function(a) {
                        var b = a.getElementsByTagName("object")[0];
                        return b ? (b.off(null, null, this), b) : e.embed(k.flashplayer, a, h(j), k.wmode)
                    },
                    getContainer: function() {
                        return r
                    },
                    setContainer: function(e) {
                        if (r !== e) {
                            r = e, s = this.getSwfObject(e), document.hasFocus() ? o() : window.addEventListener("focus", p), s.once("ready", function() {
                                q(), s.once("pluginsLoaded", function() {
                                    s.queueCommands = !1, F("setupCommandQueue", s.__commandQueue), s.__commandQueue = []
                                });
                                var a = b.extend({}, k),
                                    d = s.triggerFlash("setup", a);
                                d === s ? s.__ready = !0 : this.trigger(c.JWPLAYER_MEDIA_ERROR, d), u && F("init", u)
                            }, this);
                            var f = [c.JWPLAYER_MEDIA_META, c.JWPLAYER_MEDIA_ERROR, c.JWPLAYER_MEDIA_SEEK, c.JWPLAYER_MEDIA_SEEKED, "subtitlesTracks", "subtitlesTrackChanged", "subtitlesTrackData", "mediaType"],
                                h = [c.JWPLAYER_MEDIA_BUFFER, c.JWPLAYER_MEDIA_TIME],
                                j = [c.JWPLAYER_MEDIA_BUFFER_FULL];
                            s.on(c.JWPLAYER_MEDIA_LEVELS, function(a) {
                                l(a.levels), x = a.currentQuality, y = a.levels, this.trigger(a.type, a)
                            }, this), s.on(c.JWPLAYER_MEDIA_LEVEL_CHANGED, function(a) {
                                l(a.levels), x = a.currentQuality, y = a.levels, this.trigger(a.type, a)
                            }, this), s.on(c.JWPLAYER_AUDIO_TRACKS, function(a) {
                                z = a.currentTrack, A = a.tracks, this.trigger(a.type, a)
                            }, this), s.on(c.JWPLAYER_AUDIO_TRACK_CHANGED, function(a) {
                                z = a.currentTrack, A = a.tracks, this.trigger(a.type, a)
                            }, this), s.on(c.JWPLAYER_PLAYER_STATE, function(a) {
                                var b = a.newstate;
                                b !== d.IDLE && this.setState(b)
                            }, this), s.on(h.join(" "), function(a) {
                                "Infinity" === a.duration && (a.duration = 1 / 0), this.trigger(a.type, a)
                            }, this), s.on(f.join(" "), function(a) {
                                this.trigger(a.type, a)
                            }, this), s.on(j.join(" "), function(a) {
                                this.trigger(a.type)
                            }, this), s.on(c.JWPLAYER_MEDIA_BEFORECOMPLETE, function(a) {
                                w = !0, this.trigger(a.type), B === !0 && (w = !1)
                            }, this), s.on(c.JWPLAYER_MEDIA_COMPLETE, function(a) {
                                w || (this.setState(d.COMPLETE), this.trigger(a.type))
                            }, this), s.on("visualQuality", function(a) {
                                a.reason = a.reason || "api", this.trigger("visualQuality", a), this.trigger(c.JWPLAYER_PROVIDER_FIRST_FRAME, {})
                            }, this), s.on(c.JWPLAYER_PROVIDER_CHANGED, function(a) {
                                t = a.message, this.trigger(c.JWPLAYER_PROVIDER_CHANGED, a)
                            }, this), s.on(c.JWPLAYER_ERROR, function(b) {
                                a.log("Error playing media: %o %s", b.code, b.message, b), this.trigger(c.JWPLAYER_MEDIA_ERROR, b)
                            }, this), i(k) && s.on("throttle", function(a) {
                                q(), "resume" === a.state ? g.trigger.call(D, "flashThrottle", a) : v = setTimeout(function() {
                                    g.trigger.call(D, "flashThrottle", a)
                                }, 250)
                            }, this)
                        }
                    },
                    remove: function() {
                        x = -1, y = null, e.remove(s)
                    },
                    setVisibility: function(a) {
                        a = !!a, r.style.opacity = a ? 1 : 0
                    },
                    resize: function(a, b, c) {
                        c && F("stretch", c)
                    },
                    setControls: function(a) {
                        F("setControls", a)
                    },
                    setFullscreen: function(a) {
                        C = a, F("fullscreen", a)
                    },
                    getFullScreen: function() {
                        return C
                    },
                    setCurrentQuality: function(a) {
                        F("setCurrentQuality", a)
                    },
                    getCurrentQuality: function() {
                        return x
                    },
                    setSubtitlesTrack: function(a) {
                        F("setSubtitlesTrack", a)
                    },
                    getName: function() {
                        return t ? {
                            name: "flash_" + t
                        } : {
                            name: "flash"
                        }
                    },
                    getQualityLevels: function() {
                        return y || u.sources
                    },
                    getAudioTracks: function() {
                        return A
                    },
                    getCurrentAudioTrack: function() {
                        return z
                    },
                    setCurrentAudioTrack: function(a) {
                        F("setCurrentAudioTrack", a)
                    },
                    destroy: function() {
                        q(), this.remove(), s && (s.off(), s = null), r = null, u = null, this.off()
                    }
                }), this.trigger = function(a, b) {
                    return B ? g.trigger.call(this, a, b) : void 0
                }
            }
            var k = 0,
                l = function() {};
            return l.prototype = f, j.prototype = new l, j
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(47), c(45)], e = function(a, b, c) {
            function d(a, b, c) {
                var d = document.createElement("param");
                d.setAttribute("name", b), d.setAttribute("value", c), a.appendChild(d)
            }

            function e(e, f, i, j) {
                var k;
                if (j = j || "opaque", a.isMSIE()) {
                    var l = document.createElement("div");
                    f.appendChild(l), l.outerHTML = '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="100%" height="100%" id="' + i + '" name="' + i + '" tabindex="0"><param name="movie" value="' + e + '"><param name="allowfullscreen" value="true"><param name="allowscriptaccess" value="always"><param name="wmode" value="' + j + '"><param name="bgcolor" value="' + h + '"><param name="menu" value="false"></object>';
                    for (var m = f.getElementsByTagName("object"), n = m.length; n--;) m[n].id === i && (k = m[n])
                } else k = document.createElement("object"), k.setAttribute("type", "application/x-shockwave-flash"), k.setAttribute("data", e), k.setAttribute("width", "100%"), k.setAttribute("height", "100%"), k.setAttribute("bgcolor", h), k.setAttribute("id", i), k.setAttribute("name", i), d(k, "allowfullscreen", "true"), d(k, "allowscriptaccess", "always"), d(k, "wmode", j), d(k, "menu", "false"), f.appendChild(k, f);
                return k.className = "jw-swf jw-reset", k.style.display = "block", k.style.position = "absolute", k.style.left = 0, k.style.right = 0, k.style.top = 0, k.style.bottom = 0, c.extend(k, b), k.queueCommands = !0, k.triggerFlash = function(b) {
                    var d = this;
                    if ("setup" !== b && d.queueCommands || !d.__externalCall) {
                        for (var e = d.__commandQueue, f = e.length; f--;) e[f][0] === b && e.splice(f, 1);
                        return e.push(Array.prototype.slice.call(arguments)), d
                    }
                    var h = Array.prototype.slice.call(arguments, 1),
                        i = a.tryCatch(function() {
                            if (h.length) {
                                for (var a = h.length; a--;) "object" == typeof h[a] && c.each(h[a], g);
                                var e = JSON.stringify(h);
                                d.__externalCall(b, e)
                            } else d.__externalCall(b)
                        });
                    return i instanceof a.Error && (console.error(b, i), "setup" === b) ? (i.name = "Failed to setup flash", i) : d
                }, k.__commandQueue = [], k
            }

            function f(a) {
                a && a.parentNode && (a.style.display = "none", a.parentNode.removeChild(a))
            }

            function g(a, b, c) {
                a instanceof window.HTMLElement && delete c[b]
            }
            var h = "#000000";
            return {
                embed: e,
                remove: f
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(48)], e = function(a, b) {
            function c(a) {
                return "jwplayer." + a
            }

            function d() {
                return a.reduce(this.persistItems, function(a, d) {
                    var e = j[c(d)];
                    return e && (a[d] = b.serialize(e)), a
                }, {})
            }

            function e(a, b) {
                try {
                    j[c(a)] = b
                } catch (d) {
                    i && i.debug && console.error(d)
                }
            }

            function f() {
                a.each(this.persistItems, function(a) {
                    j.removeItem(c(a))
                })
            }

            function g() {}

            function h(b, c) {
                this.persistItems = b, a.each(this.persistItems, function(a) {
                    c.on("change:" + a, function(b, c) {
                        e(a, c)
                    })
                })
            }
            var i = window.jwplayer,
                j = {
                    removeItem: b.noop
                };
            try {
                j = window.localStorage
            } catch (k) {}
            return a.extend(g.prototype, {
                getAllItems: d,
                track: h,
                clear: f
            }), g
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(61), c(46), c(45)], e = function(a, b, c) {
            function d(a) {
                a.mediaController.off(b.JWPLAYER_MEDIA_PLAY_ATTEMPT, a._onPlayAttempt), a.mediaController.off(b.JWPLAYER_PROVIDER_FIRST_FRAME, a._triggerFirstFrame), a.mediaController.off(b.JWPLAYER_MEDIA_TIME, a._onTime)
            }

            function e(a) {
                d(a), a._triggerFirstFrame = c.once(function() {
                    var c = a._qoeItem;
                    c.tick(b.JWPLAYER_MEDIA_FIRST_FRAME);
                    var e = c.between(b.JWPLAYER_MEDIA_PLAY_ATTEMPT, b.JWPLAYER_MEDIA_FIRST_FRAME);
                    a.mediaController.trigger(b.JWPLAYER_MEDIA_FIRST_FRAME, {
                        loadTime: e
                    }), d(a)
                }), a._onTime = g(a._triggerFirstFrame), a._onPlayAttempt = function() {
                    a._qoeItem.tick(b.JWPLAYER_MEDIA_PLAY_ATTEMPT)
                }, a.mediaController.on(b.JWPLAYER_MEDIA_PLAY_ATTEMPT, a._onPlayAttempt), a.mediaController.once(b.JWPLAYER_PROVIDER_FIRST_FRAME, a._triggerFirstFrame), a.mediaController.on(b.JWPLAYER_MEDIA_TIME, a._onTime)
            }

            function f(c) {
                function d(c, d, f) {
                    c._qoeItem && f && c._qoeItem.end(f.get("state")), c._qoeItem = new a, c._qoeItem.tick(b.JWPLAYER_PLAYLIST_ITEM), c._qoeItem.start(d.get("state")), e(c), d.on("change:state", function(a, b, d) {
                        c._qoeItem.end(d), c._qoeItem.start(b)
                    })
                }
                c.on("change:mediaModel", d)
            }
            var g = function(a) {
                var b = Number.MIN_VALUE;
                return function(c) {
                    c.position > b && a(), b = c.position
                }
            };
            return {
                model: f
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(47)], e = function(a, b) {
            var c = a.extend({
                get: function(a) {
                    return this.attributes = this.attributes || {}, this.attributes[a]
                },
                set: function(a, b) {
                    if (this.attributes = this.attributes || {}, this.attributes[a] !== b) {
                        var c = this.attributes[a];
                        this.attributes[a] = b, this.trigger("change:" + a, this, b, c)
                    }
                },
                clone: function() {
                    return a.clone(this.attributes)
                }
            }, b);
            return c
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(47), c(77), c(76), c(46), c(62), c(48), c(45)], e = function(a, b, c, d, e, f, g) {
            var h = function(a, d) {
                this.model = d, this._adModel = (new b).setup({
                    id: d.get("id"),
                    volume: d.get("volume"),
                    fullscreen: d.get("fullscreen"),
                    mute: d.get("mute")
                }), this._adModel.on("change:state", c, this);
                var e = a.getContainer();
                this.swf = e.querySelector("object")
            };
            return h.prototype = g.extend({
                init: function() {
                    if (f.isChrome()) {
                        var a = -1,
                            b = !1;
                        this.swf.on("throttle", function(c) {
                            if (clearTimeout(a), "resume" === c.state) b && (b = !1, this.instreamPlay());
                            else {
                                var d = this;
                                a = setTimeout(function() {
                                    d._adModel.get("state") === e.PLAYING && (b = !0, d.instreamPause())
                                }, 250)
                            }
                        }, this)
                    }
                    this.swf.on("instream:state", function(a) {
                        switch (a.newstate) {
                            case e.PLAYING:
                                this._adModel.set("state", a.newstate);
                                break;
                            case e.PAUSED:
                                this._adModel.set("state", a.newstate)
                        }
                    }, this).on("instream:time", function(a) {
                        this._adModel.set("position", a.position), this._adModel.set("duration", a.duration), this.trigger(d.JWPLAYER_MEDIA_TIME, a)
                    }, this).on("instream:complete", function(a) {
                        this.trigger(d.JWPLAYER_MEDIA_COMPLETE, a)
                    }, this).on("instream:error", function(a) {
                        this.trigger(d.JWPLAYER_MEDIA_ERROR, a)
                    }, this), this.swf.triggerFlash("instream:init"), this.applyProviderListeners = function(a) {
                        this.model.on("change:volume", function(b, c) {
                            a.volume(c)
                        }, this), this.model.on("change:mute", function(b, c) {
                            a.mute(c)
                        }, this)
                    }
                },
                instreamDestroy: function() {
                    this._adModel && (this.off(), this.swf.off(null, null, this), this.swf.triggerFlash("instream:destroy"), this.swf = null, this._adModel.off(), this._adModel = null, this.model = null)
                },
                load: function(a) {
                    this.swf.triggerFlash("instream:load", a)
                },
                instreamPlay: function() {
                    this.swf.triggerFlash("instream:play")
                },
                instreamPause: function() {
                    this.swf.triggerFlash("instream:pause")
                }
            }, a), h
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(94), c(47), c(45), c(46)], e = function(a, b, c, d) {
            var e = function(b, e, f, g) {
                function h() {
                    m("Setup Timeout Error", "Setup took longer than " + q + " seconds to complete.")
                }

                function i() {
                    c.each(p, function(a) {
                        a.complete !== !0 && a.running !== !0 && null !== b && k(a.depends) && (a.running = !0, j(a))
                    })
                }

                function j(a) {
                    var c = function(b) {
                        b = b || {}, l(a, b)
                    };
                    a.method(c, e, b, f, g)
                }

                function k(a) {
                    return c.all(a, function(a) {
                        return p[a].complete
                    })
                }

                function l(a, b) {
                    "error" === b.type ? m(b.msg, b.reason) : "complete" === b.type ? (clearTimeout(n), o.trigger(d.JWPLAYER_READY)) : (a.complete = !0, i())
                }

                function m(a, b) {
                    clearTimeout(n), o.trigger(d.JWPLAYER_SETUP_ERROR, {
                        message: a + ": " + b
                    }), o.destroy()
                }
                var n, o = this,
                    p = a.getQueue(),
                    q = 30;
                this.start = function() {
                    n = setTimeout(h, 1e3 * q), i()
                }, this.destroy = function() {
                    clearTimeout(n), this.off(), p.length = 0, b = null, e = null, f = null
                }
            };
            return e.prototype = b, e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(95), c(81), c(80), c(45), c(48), c(57), c(97)], e = function(a, b, d, e, f, g, h) {
            function i(a, b, c) {
                if (b) {
                    var d = b.client;
                    delete b.client, /\.(js|swf)$/.test(d || "") || (d = g.repo() + c), a[d] = b
                }
            }

            function j(a, c) {
                var d = e.clone(c.get("plugins")) || {},
                    f = c.get("edition"),
                    h = b(f),
                    j = /^(vast|googima)$/,
                    k = /\.(js|swf)$/,
                    l = g.repo(),
                    m = c.get("advertising");
                if (h("ads") && m && (k.test(m.client) ? d[m.client] = m : j.test(m.client) && (d[l + m.client + ".js"] = m), delete m.client), h("jwpsrv")) {
                    var n = c.get("analytics");
                    e.isObject(n) || (n = {}), i(d, n, "jwpsrv.js")
                }
                i(d, c.get("ga"), "gapro.js"), i(d, c.get("sharing"), "sharing.js"), i(d, c.get("related"), "related.js"), c.set("plugins", d), a()
            }

            function k(b, d) {
                var e = d.get("key") || window.jwplayer && window.jwplayer.key,
                    i = new a(e),
                    j = i.edition();
                if (d.set("key", e), d.set("edition", j), "unlimited" === j) {
                    var k = f.getScriptPath("jwplayer.js");
                    if (!k) return void h.error(b, "Error setting up player", "Could not locate jwplayer.js script tag");
                    c.p = k, f.repo = g.repo = g.loadFrom = function() {
                        return k
                    }
                }
                d.updateProviders(), "invalid" === j ? h.error(b, "Error setting up player", (void 0 === e ? "Missing" : "Invalid") + " license key") : b()
            }

            function l(a, b, d) {
                "dashjs" === a ? c.e(4, function(a) {
                    var e = c(107);
                    e.register(window.jwplayer), d.updateProviders(), b()
                }) : c.e(5, function(a) {
                    var e = c(109);
                    e.register(window.jwplayer), d.updateProviders(), b()
                })
            }

            function m(a, b) {
                var c = b.get("playlist"),
                    f = b.get("edition"),
                    g = b.get("dash");
                e.contains(["shaka", "dashjs"], g) || (g = "shaka");
                var h = e.where(d, {
                        name: g
                    })[0].supports,
                    i = e.some(c, function(a) {
                        return h(a, f)
                    });
                i ? l(g, a, b) : a()
            }

            function n() {
                var a = h.getQueue();
                return a.LOAD_DASH = {
                    method: m,
                    depends: ["CHECK_KEY", "FILTER_PLAYLIST"]
                }, a.CHECK_KEY = {
                    method: k,
                    depends: ["LOADED_POLYFILLS"]
                }, a.FILTER_PLUGINS = {
                    method: j,
                    depends: ["CHECK_KEY"]
                }, a.FILTER_PLAYLIST.depends.push("CHECK_KEY"), a.LOAD_PLUGINS.depends.push("FILTER_PLUGINS"), a.SETUP_VIEW.depends.push("CHECK_KEY"), a.SEND_READY.depends.push("LOAD_DASH"), a
            }
            return {
                getQueue: n
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(96), c(81)], e = function(a, b, c) {
            var d = "invalid",
                e = "RnXcsftYjWRDA^Uy",
                f = function(f) {
                    function g(f) {
                        a.exists(f) || (f = "");
                        try {
                            f = b.decrypt(f, e);
                            var g = f.split("/");
                            h = g[0], "pro" === h && (h = "premium");
                            var k = c(h);
                            if (g.length > 2 && k("setup")) {
                                i = g[1];
                                var l = parseInt(g[2]);
                                l > 0 && (j = new Date, j.setTime(l))
                            } else h = d
                        } catch (m) {
                            h = d
                        }
                    }
                    var h, i, j;
                    this.edition = function() {
                        return j && j.getTime() < (new Date).getTime() ? d : h
                    }, this.token = function() {
                        return i
                    }, this.expiration = function() {
                        return j
                    }, g(f)
                };
            return f
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [], e = function() {
            var a = function(a) {
                    return window.atob(a)
                },
                b = function(a) {
                    return unescape(encodeURIComponent(a))
                },
                c = function(a) {
                    try {
                        return decodeURIComponent(escape(a))
                    } catch (b) {
                        return a
                    }
                },
                d = function(a) {
                    for (var b = new Array(Math.ceil(a.length / 4)), c = 0; c < b.length; c++) b[c] = a.charCodeAt(4 * c) + (a.charCodeAt(4 * c + 1) << 8) + (a.charCodeAt(4 * c + 2) << 16) + (a.charCodeAt(4 * c + 3) << 24);
                    return b
                },
                e = function(a) {
                    for (var b = new Array(a.length), c = 0; c < a.length; c++) b[c] = String.fromCharCode(255 & a[c], a[c] >>> 8 & 255, a[c] >>> 16 & 255, a[c] >>> 24 & 255);
                    return b.join("")
                };
            return {
                decrypt: function(f, g) {
                    if (f = String(f), g = String(g), 0 == f.length) return "";
                    for (var h, i, j = d(a(f)), k = d(b(g).slice(0, 16)), l = j.length, m = j[l - 1], n = j[0], o = 2654435769, p = Math.floor(6 + 52 / l), q = p * o; 0 != q;) {
                        i = q >>> 2 & 3;
                        for (var r = l - 1; r >= 0; r--) m = j[r > 0 ? r - 1 : l - 1], h = (m >>> 5 ^ n << 2) + (n >>> 3 ^ m << 4) ^ (q ^ n) + (k[3 & r ^ i] ^ m), n = j[r] -= h;
                        q -= o
                    }
                    var s = e(j);
                    return s = s.replace(/\0+$/, ""), c(s)
                }
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(98), c(65), c(101), c(58), c(45), c(48), c(46)], e = function(a, b, d, e, f, g, h) {
            function i() {
                var a = {
                    LOAD_PROMISE_POLYFILL: {
                        method: j,
                        depends: []
                    },
                    LOAD_BASE64_POLYFILL: {
                        method: k,
                        depends: []
                    },
                    LOADED_POLYFILLS: {
                        method: l,
                        depends: ["LOAD_PROMISE_POLYFILL", "LOAD_BASE64_POLYFILL"]
                    },
                    LOAD_PLUGINS: {
                        method: m,
                        depends: ["LOADED_POLYFILLS"]
                    },
                    INIT_PLUGINS: {
                        method: n,
                        depends: ["LOAD_PLUGINS", "SETUP_VIEW"]
                    },
                    LOAD_YOUTUBE: {
                        method: v,
                        depends: ["FILTER_PLAYLIST"]
                    },
                    LOAD_SKIN: {
                        method: u,
                        depends: ["LOADED_POLYFILLS"]
                    },
                    LOAD_PLAYLIST: {
                        method: p,
                        depends: ["LOADED_POLYFILLS"]
                    },
                    FILTER_PLAYLIST: {
                        method: q,
                        depends: ["LOAD_PLAYLIST"]
                    },
                    SETUP_VIEW: {
                        method: w,
                        depends: ["LOAD_SKIN"]
                    },
                    SEND_READY: {
                        method: x,
                        depends: ["INIT_PLUGINS", "LOAD_YOUTUBE", "SETUP_VIEW"]
                    }
                };
                return a
            }

            function j(a) {
                window.Promise ? a() : c.e(1, function(b) {
                    c(104), a()
                })
            }

            function k(a) {
                window.btoa && window.atob ? a() : c.e(2, function(b) {
                    c(105), a()
                })
            }

            function l(a) {
                a()
            }

            function m(b, c) {
                z = a.loadPlugins(c.get("id"), c.get("plugins")), z.on(h.COMPLETE, b), z.on(h.ERROR, f.partial(o, b)), z.load()
            }

            function n(a, b, c) {
                z.setupPlugins(c, b), a()
            }

            function o(a, b) {
                y(a, "Could not load plugin", b.message)
            }

            function p(a, c) {
                var d = c.get("playlist");
                f.isString(d) ? (A = new b, A.on(h.JWPLAYER_PLAYLIST_LOADED, function(b) {
                    c.set("playlist", b.playlist), a()
                }), A.on(h.JWPLAYER_ERROR, f.partial(r, a)), A.load(d)) : a()
            }

            function q(a, b, c, d, e) {
                var f = b.get("playlist"),
                    g = e(f);
                g ? a() : r(a)
            }

            function r(a, b) {
                b && b.message ? y(a, "Error loading playlist", b.message) : y(a, "Error loading player", "No playable sources found")
            }

            function s(a, b) {
                return f.contains(e.SkinsLoadable, a) ? b + "skins/" + a + ".css" : void 0
            }

            function t(a) {
                for (var b = document.styleSheets, c = 0, d = b.length; d > c; c++)
                    if (b[c].href === a) return !0;
                return !1
            }

            function u(a, b) {
                var c = b.get("skin"),
                    g = b.get("skinUrl");
                if (f.contains(e.SkinsIncluded, c)) return void a();
                if (g || (g = s(c, b.get("base"))), f.isString(g) && !t(g)) {
                    b.set("skin-loading", !0);
                    var i = !0,
                        j = new d(g, i);
                    j.addEventListener(h.COMPLETE, function() {
                        b.set("skin-loading", !1)
                    }), j.addEventListener(h.ERROR, function() {
                        b.set("skin", "seven"), b.set("skin-loading", !1)
                    }), j.load()
                }
                f.defer(function() {
                    a()
                })
            }

            function v(a, b) {
                var d = b.get("playlist"),
                    e = f.some(d, function(a) {
                        var b = g.isYouTube(a.file, a.type);
                        if (b && !a.image) {
                            var c = a.file,
                                d = g.youTubeID(c);
                            a.image = "//i.ytimg.com/vi/" + d + "/0.jpg"
                        }
                        return b
                    });
                e ? c.e(3, function(b) {
                    var d = c(106);
                    d.register(window.jwplayer), a()
                }) : a()
            }

            function w(a, b, c, d) {
                d.setup(), a()
            }

            function x(a) {
                a({
                    type: "complete"
                })
            }

            function y(a, b, c) {
                a({
                    type: "error",
                    msg: b,
                    reason: c
                })
            }
            var z, A;
            return {
                getQueue: i,
                error: y
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(99), c(102), c(103), c(100)], e = function(a, b, c, d) {
            var e = {},
                f = {},
                g = function(c, d) {
                    return f[c] = new a(new b(e), d), f[c]
                },
                h = function(a, b, f, g) {
                    var h = d.getPluginName(a);
                    e[h] || (e[h] = new c(a)), e[h].registerPlugin(a, b, f, g)
                };
            return {
                loadPlugins: g,
                registerPlugin: h
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(100), c(48), c(46), c(47), c(45), c(101)], e = function(a, b, c, d, e, f) {
            function g(a, b, c) {
                return function() {
                    var d = a.getContainer().getElementsByClassName("jw-overlays")[0];
                    d && (d.appendChild(c), c.left = d.style.left, c.top = d.style.top, b.displayArea = d)
                }
            }

            function h(a) {
                function b() {
                    var b = a.displayArea;
                    b && a.resize(b.clientWidth, b.clientHeight)
                }
                return function() {
                    b(), setTimeout(b, 400)
                }
            }
            var i = function(i, j) {
                function k() {
                    q || (q = !0, p = f.loaderstatus.COMPLETE, o.trigger(c.COMPLETE))
                }

                function l() {
                    if (!s && (j && 0 !== e.keys(j).length || k(), !q)) {
                        var d = i.getPlugins();
                        n = e.after(r, k), e.each(j, function(e, g) {
                            var h = a.getPluginName(g),
                                i = d[h],
                                j = i.getJS(),
                                k = i.getTarget(),
                                l = i.getStatus();
                            l !== f.loaderstatus.LOADING && l !== f.loaderstatus.NEW && (j && !b.versionCheck(k) && o.trigger(c.ERROR, {
                                message: "Incompatible player version"
                            }), n())
                        })
                    }
                }

                function m(a) {
                    if (!s) {
                        var d = "File not found";
                        a.url && b.log(d, a.url), this.off(), this.trigger(c.ERROR, {
                            message: d
                        }), l()
                    }
                }
                var n, o = e.extend(this, d),
                    p = f.loaderstatus.NEW,
                    q = !1,
                    r = e.size(j),
                    s = !1;
                this.setupPlugins = function(c, d) {
                    var f = [],
                        j = i.getPlugins(),
                        k = d.get("plugins");
                    e.each(k, function(d, i) {
                        var l = a.getPluginName(i),
                            m = j[l],
                            n = m.getFlashPath(),
                            o = m.getJS(),
                            p = m.getURL();
                        if (n) {
                            var q = e.extend({
                                name: l,
                                swf: n,
                                pluginmode: m.getPluginmode()
                            }, d);
                            f.push(q)
                        }
                        var r = b.tryCatch(function() {
                            if (o && k[p]) {
                                var a = document.createElement("div");
                                a.id = c.id + "_" + l, a.className = "jw-plugin jw-reset";
                                var b = e.extend({}, k[p]),
                                    d = m.getNewInstance(c, b, a);
                                d.addToPlayer = g(c, d, a), d.resizeHandler = h(d), c.addPlugin(l, d, a)
                            }
                        });
                        r instanceof b.Error && b.log("ERROR: Failed to load " + l + ".")
                    }), d.set("flashPlugins", f)
                }, this.load = function() {
                    if (b.exists(j) && "object" !== b.typeOf(j)) return void l();
                    p = f.loaderstatus.LOADING, e.each(j, function(a, d) {
                        if (b.exists(d)) {
                            var e = i.addPlugin(d);
                            e.on(c.COMPLETE, l), e.on(c.ERROR, m)
                        }
                    });
                    var a = i.getPlugins();
                    e.each(a, function(a) {
                        a.load()
                    }), l()
                }, this.destroy = function() {
                    s = !0, this.off()
                }, this.getStatus = function() {
                    return p
                }
            };
            return i
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(51)], e = function(a) {
            var b = {},
                c = b.pluginPathType = {
                    ABSOLUTE: 0,
                    RELATIVE: 1,
                    CDN: 2
                };
            return b.getPluginPathType = function(b) {
                if ("string" == typeof b) {
                    b = b.split("?")[0];
                    var d = b.indexOf("://");
                    if (d > 0) return c.ABSOLUTE;
                    var e = b.indexOf("/"),
                        f = a.extension(b);
                    return !(0 > d && 0 > e) || f && isNaN(f) ? c.RELATIVE : c.CDN
                }
            }, b.getPluginName = function(a) {
                return a.replace(/^(.*\/)?([^-]*)-?.*\.(swf|js)$/, "$2")
            }, b.getPluginVersion = function(a) {
                return a.replace(/[^-]*-?([^\.]*).*$/, "$1")
            }, b
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(46), c(47), c(45)], e = function(a, b, c) {
            var d = {},
                e = {
                    NEW: 0,
                    LOADING: 1,
                    ERROR: 2,
                    COMPLETE: 3
                },
                f = function(f, g) {
                    function h(b) {
                        k = e.ERROR, j.trigger(a.ERROR, b)
                    }

                    function i(b) {
                        k = e.COMPLETE, j.trigger(a.COMPLETE, b)
                    }
                    var j = c.extend(this, b),
                        k = e.NEW;
                    this.addEventListener = this.on, this.removeEventListener = this.off, this.makeStyleLink = function(a) {
                        var b = document.createElement("link");
                        return b.type = "text/css", b.rel = "stylesheet", b.href = a, b
                    }, this.makeScriptTag = function(a) {
                        var b = document.createElement("script");
                        return b.src = a, b
                    }, this.makeTag = g ? this.makeStyleLink : this.makeScriptTag, this.load = function() {
                        if (k === e.NEW) {
                            var b = d[f];
                            if (b && (k = b.getStatus(), 2 > k)) return b.on(a.ERROR, h), void b.on(a.COMPLETE, i);
                            var c = document.getElementsByTagName("head")[0] || document.documentElement,
                                j = this.makeTag(f),
                                l = !1;
                            j.onload = j.onreadystatechange = function(a) {
                                l || this.readyState && "loaded" !== this.readyState && "complete" !== this.readyState || (l = !0, i(a), j.onload = j.onreadystatechange = null, c && j.parentNode && !g && c.removeChild(j))
                            }, j.onerror = h, c.insertBefore(j, c.firstChild), k = e.LOADING, d[f] = this
                        }
                    }, this.getStatus = function() {
                        return k
                    }
                };
            return f.loaderstatus = e, f
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(100), c(103)], e = function(a, b) {
            var c = function(c) {
                this.addPlugin = function(d) {
                    var e = a.getPluginName(d);
                    return c[e] || (c[e] = new b(d)), c[e]
                }, this.getPlugins = function() {
                    return c
                }
            };
            return c
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(100), c(46), c(47), c(101), c(45)], e = function(a, b, c, d, e, f) {
            var g = {
                    FLASH: 0,
                    JAVASCRIPT: 1,
                    HYBRID: 2
                },
                h = function(h) {
                    function i() {
                        switch (b.getPluginPathType(h)) {
                            case b.pluginPathType.ABSOLUTE:
                                return h;
                            case b.pluginPathType.RELATIVE:
                                return a.getAbsolutePath(h, window.location.href)
                        }
                    }

                    function j() {
                        f.defer(function() {
                            q = e.loaderstatus.COMPLETE, p.trigger(c.COMPLETE)
                        })
                    }

                    function k() {
                        q = e.loaderstatus.ERROR, p.trigger(c.ERROR, {
                            url: h
                        })
                    }
                    var l, m, n, o, p = f.extend(this, d),
                        q = e.loaderstatus.NEW;
                    this.load = function() {
                        if (q === e.loaderstatus.NEW) {
                            if (h.lastIndexOf(".swf") > 0) return l = h, q = e.loaderstatus.COMPLETE, void p.trigger(c.COMPLETE);
                            if (b.getPluginPathType(h) === b.pluginPathType.CDN) return q = e.loaderstatus.COMPLETE, void p.trigger(c.COMPLETE);
                            q = e.loaderstatus.LOADING;
                            var a = new e(i());
                            a.on(c.COMPLETE, j), a.on(c.ERROR, k), a.load()
                        }
                    }, this.registerPlugin = function(a, b, d, f) {
                        o && (clearTimeout(o), o = void 0), n = b, d && f ? (l = f, m = d) : "string" == typeof d ? l = d : "function" == typeof d ? m = d : d || f || (l = a), q = e.loaderstatus.COMPLETE, p.trigger(c.COMPLETE)
                    }, this.getStatus = function() {
                        return q
                    }, this.getPluginName = function() {
                        return b.getPluginName(h)
                    }, this.getFlashPath = function() {
                        if (l) switch (b.getPluginPathType(l)) {
                            case b.pluginPathType.ABSOLUTE:
                                return l;
                            case b.pluginPathType.RELATIVE:
                                return h.lastIndexOf(".swf") > 0 ? a.getAbsolutePath(l, window.location.href) : a.getAbsolutePath(l, i())
                        }
                        return null
                    }, this.getJS = function() {
                        return m
                    }, this.getTarget = function() {
                        return n
                    }, this.getPluginmode = function() {
                        return void 0 !== typeof l && void 0 !== typeof m ? g.HYBRID : void 0 !== typeof l ? g.FLASH : void 0 !== typeof m ? g.JAVASCRIPT : void 0
                    }, this.getNewInstance = function(a, b, c) {
                        return new m(a, b, c)
                    }, this.getURL = function() {
                        return h
                    }
                };
            return h
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, , , , , , , , function(a, b, c) {
        var d, e;
        d = [c(66), c(112), c(113), c(48)], e = function(a, b, c, d) {
            var e = function(e, f) {
                function g(a) {
                    if (a.tracks.length) {
                        f.mediaController.off("meta", h), s = [], t = {}, u = {}, v = 0;
                        for (var b = a.tracks || [], c = 0; c < b.length; c++) {
                            var d = b[c];
                            d.id = d.name, d.label = d.name || d.language, l(d)
                        }
                        var e = o();
                        this.setCaptionsList(e), p()
                    }
                }

                function h(a) {
                    var b = a.metadata;
                    if (b && "textdata" === b.type) {
                        if (!b.text) return;
                        var c = t[b.trackid];
                        if (!c) {
                            c = {
                                kind: "captions",
                                id: b.trackid,
                                data: []
                            }, l(c);
                            var d = o();
                            this.setCaptionsList(d)
                        }
                        var e, g;
                        b.useDTS ? (c.source || (c.source = b.source || "mpegts"), e = b.begin, g = b.begin + "_" + b.text) : (e = a.position || f.get("position"), g = "" + Math.round(10 * e) + "_" + b.text);
                        var h = u[g];
                        h || (h = {
                            begin: e,
                            text: b.text
                        }, b.end && (h.end = b.end), u[g] = h, c.data.push(h))
                    }
                }

                function i(a) {
                    d.log("CAPTIONS(" + a + ")")
                }

                function j(a, b) {
                    r = b, s = [], t = {}, u = {}, v = 0, f.mediaController.off("meta", h), f.mediaController.off("subtitlesTracks", g);
                    var c, e, i, j = b.tracks;
                    for (i = 0; i < j.length; i++)
                        if (c = j[i], e = c.kind.toLowerCase(), "captions" === e || "subtitles" === e)
                            if (c.file) {
                                var k = d.isIOS() && !d.isSDK(f.getConfiguration()) && -1 !== c.file.indexOf(".vtt");
                                k || (l(c), m(c))
                            } else c.data && l(c);
                    0 === s.length && (f.mediaController.on("meta", h, this), f.mediaController.on("subtitlesTracks", g, this));
                    var n = o();
                    this.setCaptionsList(n), p()
                }

                function k(a, b) {
                    var c = null;
                    0 !== b && (c = s[b - 1]), a.set("captionsTrack", c)
                }

                function l(a) {
                    "number" != typeof a.id && (a.id = a.name || a.file || "cc" + s.length), a.data = a.data || [], a.label || (a.label = "Unknown CC", v++, v > 1 && (a.label += " (" + v + ")")), s.push(a), t[a.id] = a
                }

                function m(a) {
                    d.ajax(a.file, function(b) {
                        n(b, a)
                    }, i)
                }

                function n(e, f) {
                    var g, h = e.responseXML ? e.responseXML.firstChild : null;
                    if (h)
                        for ("xml" === a.localName(h) && (h = h.nextSibling); h.nodeType === h.COMMENT_NODE;) h = h.nextSibling;
                    g = h && "tt" === a.localName(h) ? d.tryCatch(function() {
                        f.data = c(e.responseXML)
                    }) : d.tryCatch(function() {
                        f.data = b(e.responseText)
                    }), g instanceof d.Error && i(g.message + ": " + f.file)
                }

                function o() {
                    for (var a = [{
                            id: "off",
                            label: "Off"
                        }], b = 0; b < s.length; b++) a.push({
                        id: s[b].id,
                        label: s[b].label || "Unknown CC"
                    });
                    return a
                }

                function p() {
                    var a = 0,
                        b = f.get("captionLabel");
                    if ("Off" === b) return void f.set("captionsIndex", 0);
                    for (var c = 0; c < s.length; c++) {
                        var d = s[c];
                        if (b && b === d.label) {
                            a = c + 1;
                            break
                        }
                        d["default"] || d.defaulttrack ? a = c + 1 : d.autoselect
                    }
                    q(a)
                }

                function q(a) {
                    s.length ? f.setVideoSubtitleTrack(a, s) : f.set("captionsIndex", a)
                }
                f.on("change:playlistItem", j, this), f.on("change:captionsIndex", k, this), f.mediaController.on("subtitlesTracks", g, this), f.mediaController.on("subtitlesTrackData", function(a) {
                    var b = t[a.name];
                    if (b) {
                        b.source = a.source;
                        for (var c = a.captions || [], d = !1, e = 0; e < c.length; e++) {
                            var f = c[e],
                                g = a.name + "_" + f.begin + "_" + f.end;
                            u[g] || (u[g] = f, b.data.push(f), d = !0)
                        }
                        d && b.data.sort(function(a, b) {
                            return a.begin - b.begin
                        })
                    }
                }, this), f.mediaController.on("meta", h, this);
                var r = {},
                    s = [],
                    t = {},
                    u = {},
                    v = 0;
                this.getCurrentIndex = function() {
                    return f.get("captionsIndex")
                }, this.getCaptionsList = function() {
                    return f.get("captionsList")
                }, this.setCaptionsList = function(a) {
                    f.set("captionsList", a)
                }
            };
            return e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(51)], e = function(a, b) {
            function c(a) {
                var b = {},
                    c = a.split("\r\n");
                1 === c.length && (c = a.split("\n"));
                var e = 1;
                if (c[0].indexOf(" --> ") > 0 && (e = 0), c.length > e + 1 && c[e + 1]) {
                    var f = c[e],
                        g = f.indexOf(" --> ");
                    g > 0 && (b.begin = d(f.substr(0, g)), b.end = d(f.substr(g + 5)), b.text = c.slice(e + 1).join("<br/>"))
                }
                return b
            }
            var d = a.seconds;
            return function(a) {
                var d = [];
                a = b.trim(a);
                var e = a.split("\r\n\r\n");
                1 === e.length && (e = a.split("\n\n"));
                for (var f = 0; f < e.length; f++)
                    if ("WEBVTT" !== e[f]) {
                        var g = c(e[f]);
                        g.text && d.push(g)
                    }
                if (!d.length) throw new Error("Invalid SRT file");
                return d
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(51)], e = function(a) {
            function b(a) {
                a || c()
            }

            function c() {
                throw new Error("Invalid DFXP file")
            }
            var d = a.seconds;
            return function(e) {
                b(e);
                var f = [],
                    g = e.getElementsByTagName("p");
                b(g), g.length || (g = e.getElementsByTagName("tt:p"), g.length || (g = e.getElementsByTagName("tts:p")));
                for (var h = 0; h < g.length; h++) {
                    var i = g[h],
                        j = i.innerHTML || i.textContent || i.text || "",
                        k = a.trim(j).replace(/>\s+</g, "><").replace(/tts?:/g, "");
                    if (k) {
                        var l = i.getAttribute("begin"),
                            m = i.getAttribute("dur"),
                            n = i.getAttribute("end"),
                            o = {
                                begin: d(l),
                                text: k
                            };
                        n ? o.end = d(n) : m && (o.end = o.begin + d(m)), f.push(o)
                    }
                }
                return f.length || c(), f
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(70), c(71), c(45), c(78)], e = function(a, b, c, d) {
            function e(a, b) {
                for (var c = 0; c < a.length; c++) {
                    var d = a[c],
                        e = b.choose(d);
                    if (e) return d.type
                }
                return null
            }
            var f = function(b) {
                return b = c.isArray(b) ? b : [b], c.compact(c.map(b, a))
            };
            f.filterPlaylist = function(a, b, d, e, f) {
                var i = [];
                return c.each(a, function(a) {
                    a = c.extend({}, a), a.allSources = g(a.sources, d, a.drm || e, a.preload || f), a.sources = h(a.allSources, b), a.sources.length && (a.file = a.sources[0].file, (a.preload || f) && (a.preload = a.preload || f), i.push(a))
                }), i
            };
            var g = function(a, d, e, f) {
                    return c.compact(c.map(a, function(a) {
                        return c.isObject(a) ? (void 0 !== d && null !== d && (a.androidhls = d), (a.drm || e) && (a.drm = a.drm || e), (a.preload || f) && (a.preload = a.preload || f), b(a)) : void 0
                    }))
                },
                h = function(a, b) {
                    b && b.choose || (b = new d({
                        primary: b ? "flash" : null
                    }));
                    var f = e(a, b);
                    return c.where(a, {
                        type: f
                    })
                };
            return f
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [], e = function() {
            return function(a, b) {
                a.getPlaylistIndex = a.getItem;
                var c = {
                    jwPlay: b.play,
                    jwPause: b.pause,
                    jwSetMute: b.setMute,
                    jwLoad: b.load,
                    jwPlaylistItem: b.item,
                    jwGetAudioTracks: b.getAudioTracks,
                    jwDetachMedia: b.detachMedia,
                    jwAttachMedia: b.attachMedia,
                    jwAddEventListener: b.on,
                    jwRemoveEventListener: b.off,
                    jwStop: b.stop,
                    jwSeek: b.seek,
                    jwSetVolume: b.setVolume,
                    jwPlaylistNext: b.next,
                    jwPlaylistPrev: b.prev,
                    jwSetFullscreen: b.setFullscreen,
                    jwGetQualityLevels: b.getQualityLevels,
                    jwGetCurrentQuality: b.getCurrentQuality,
                    jwSetCurrentQuality: b.setCurrentQuality,
                    jwSetCurrentAudioTrack: b.setCurrentAudioTrack,
                    jwGetCurrentAudioTrack: b.getCurrentAudioTrack,
                    jwGetCaptionsList: b.getCaptionsList,
                    jwGetCurrentCaptions: b.getCurrentCaptions,
                    jwSetCurrentCaptions: b.setCurrentCaptions,
                    jwSetCues: b.setCues
                };
                a.callInternal = function(a) {
                    console.log("You are using the deprecated callInternal method for " + a);
                    var d = Array.prototype.slice.call(arguments, 1);
                    c[a].apply(b, d)
                }
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(117), c(46), c(154)], e = function(a, b, c) {
            var d = function(d, e) {
                var f = new a(d, e),
                    g = f.setup;
                return f.setup = function() {
                    if (g.call(this), "trial" === e.get("edition")) {
                        var a = document.createElement("div");
                        a.className = "jw-icon jw-watermark", this.element().appendChild(a)
                    }
                    e.on("change:skipButton", this.onSkipButton, this), e.on("change:castActive change:playlistItem", this.showDisplayIconImage, this)
                }, f.showDisplayIconImage = function(a) {
                    var b = a.get("castActive"),
                        c = a.get("playlistItem"),
                        d = f.controlsContainer().getElementsByClassName("jw-display-icon-container")[0];
                    b && c && c.image ? (d.style.backgroundImage = 'url("' + c.image + '")', d.style.backgroundSize = "contain") : (d.style.backgroundImage = "", d.style.backgroundSize = "")
                }, f.onSkipButton = function(a, b) {
                    b ? this.addSkipButton() : this._skipButton && (this._skipButton.destroy(), this._skipButton = null)
                }, f.addSkipButton = function() {
                    this._skipButton = new c(this.instreamModel), this._skipButton.on(b.JWPLAYER_AD_SKIPPED, function() {
                        this.api.skipAd()
                    }, this), this.controlsContainer().appendChild(this._skipButton.element())
                }, f
            };
            return d
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(46), c(47), c(58), c(62), c(128), c(129), c(130), c(118), c(132), c(134), c(148), c(149), c(152), c(45), c(153)], e = function(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p) {
            var q = a.style,
                r = a.bounds,
                s = a.isMobile(),
                t = ["fullscreenchange", "webkitfullscreenchange", "mozfullscreenchange", "MSFullscreenChange"],
                u = function(u, v) {
                    function w(b) {
                        var c = 0,
                            e = v.get("duration"),
                            f = v.get("position");
                        "DVR" === a.adaptiveType(e) && (c = e, e = Math.max(f, d.dvrSeekLimit));
                        var g = a.between(f + b, c, e);
                        u.seek(g)
                    }

                    function x(b) {
                        var c = a.between(v.get("volume") + b, 0, 100);
                        u.setVolume(c)
                    }

                    function y(a) {
                        return a.ctrlKey || a.metaKey ? !1 : !!v.get("controls")
                    }

                    function z(a) {
                        if (!y(a)) return !0;
                        switch (Ga || aa(), a.keyCode) {
                            case 27:
                                u.setFullscreen(!1);
                                break;
                            case 13:
                            case 32:
                                u.play({
                                    reason: "interaction"
                                });
                                break;
                            case 37:
                                Ga || w(-5);
                                break;
                            case 39:
                                Ga || w(5);
                                break;
                            case 38:
                                x(10);
                                break;
                            case 40:
                                x(-10);
                                break;
                            case 77:
                                u.setMute();
                                break;
                            case 70:
                                u.setFullscreen();
                                break;
                            default:
                                if (a.keyCode >= 48 && a.keyCode <= 59) {
                                    var b = a.keyCode - 48,
                                        c = b / 10 * v.get("duration");
                                    u.seek(c)
                                }
                        }
                        return /13|32|37|38|39|40/.test(a.keyCode) ? (a.preventDefault(), !1) : void 0
                    }

                    function A() {
                        La = !1, a.removeClass(ja, "jw-no-focus")
                    }

                    function B() {
                        La = !0, a.addClass(ja, "jw-no-focus")
                    }

                    function C() {
                        La || A(), Ga || aa()
                    }

                    function D() {
                        var a = r(ja),
                            c = Math.round(a.width),
                            d = Math.round(a.height);
                        return document.body.contains(ja) ? c && d && (c === ma && d === na || (ma = c, na = d, clearTimeout(Ia), Ia = setTimeout(X, 50), v.set("containerWidth", c), v.set("containerHeight", d), Ma.trigger(b.JWPLAYER_RESIZE, {
                            width: c,
                            height: d
                        }))) : (window.removeEventListener("resize", D), s && window.removeEventListener("orientationchange", D)), a
                    }

                    function E(b, c) {
                        c = c || !1, a.toggleClass(ja, "jw-flag-casting", c)
                    }

                    function F(b, c) {
                        a.toggleClass(ja, "jw-flag-cast-available", c), a.toggleClass(ka, "jw-flag-cast-available", c)
                    }

                    function G(b, c) {
                        a.replaceClass(ja, /jw-stretch-\S+/, "jw-stretch-" + c)
                    }

                    function H(b, c) {
                        a.toggleClass(ja, "jw-flag-compact-player", c)
                    }

                    function I(a) {
                        a && !s && (a.element().addEventListener("mousemove", L, !1), a.element().addEventListener("mouseout", M, !1))
                    }

                    function J() {
                        v.get("state") !== e.IDLE && v.get("state") !== e.COMPLETE && v.get("state") !== e.PAUSED || !v.get("controls") || u.play({
                            reason: "interaction"
                        }), Ha ? _() : aa()
                    }

                    function K(a) {
                        a.link ? (u.pause(!0), u.setFullscreen(!1), window.open(a.link, a.linktarget)) : v.get("controls") && u.play({
                            reason: "interaction"
                        })
                    }

                    function L() {
                        clearTimeout(Da)
                    }

                    function M() {
                        aa()
                    }

                    function N(a) {
                        Ma.trigger(a.type, a)
                    }

                    function O(b, c) {
                        c ? (ya && ya.destroy(), a.addClass(ja, "jw-flag-flash-blocked")) : (ya && ya.setup(v, ja, ja), a.removeClass(ja, "jw-flag-flash-blocked"))
                    }

                    function P() {
                        v.get("controls") && u.setFullscreen()
                    }

                    function Q() {
                        var c = ja.getElementsByClassName("jw-overlays")[0];
                        c.addEventListener("mousemove", aa), ra = new g(v, la, {
                            useHover: !0
                        }), ra.on("click", function() {
                            N({
                                type: b.JWPLAYER_DISPLAY_CLICK
                            }), v.get("controls") && u.play({
                                reason: "interaction"
                            })
                        }), ra.on("tap", function() {
                            N({
                                type: b.JWPLAYER_DISPLAY_CLICK
                            }), J()
                        }), ra.on("doubleClick", P), ra.on("move", aa), ra.on("over", aa);
                        var d = new h(v);
                        d.on("click", function() {
                            N({
                                type: b.JWPLAYER_DISPLAY_CLICK
                            }), u.play({
                                reason: "interaction"
                            })
                        }), d.on("tap", function() {
                            N({
                                type: b.JWPLAYER_DISPLAY_CLICK
                            }), J()
                        }), a.isChrome() && d.el.addEventListener("mousedown", function() {
                            var a = v.getVideo(),
                                b = a && 0 === a.getName().name.indexOf("flash");
                            if (b) {
                                var c = function() {
                                    document.removeEventListener("mouseup", c), d.el.style.pointerEvents = "auto"
                                };
                                this.style.pointerEvents = "none", document.addEventListener("mouseup", c)
                            }
                        }), ka.appendChild(d.element()), ta = new i(v), ua = new j(v), ua.on(b.JWPLAYER_LOGO_CLICK, K);
                        var e = document.createElement("div");
                        e.className = "jw-controls-right jw-reset", ua.setup(e), e.appendChild(ta.element()), ka.appendChild(e), wa = new f(v), wa.setup(v.get("captions")), ka.parentNode.insertBefore(wa.element(), va.element());
                        var l = v.get("height");
                        s && ("string" == typeof l || l >= 1.5 * Fa) ? a.addClass(ja, "jw-flag-touch") : (ya = new m, ya.setup(v, ja, ja)), pa = new k(u, v), pa.on(b.JWPLAYER_USER_ACTION, aa), v.on("change:scrubbing", S), v.on("change:compactUI", H), ka.appendChild(pa.element()), ja.addEventListener("focus", C), ja.addEventListener("blur", A), ja.addEventListener("keydown", z), ja.onmousedown = B
                    }

                    function R(b) {
                        return b.get("state") === e.PAUSED ? void b.once("change:state", R) : void(b.get("scrubbing") === !1 && a.removeClass(ja, "jw-flag-dragging"))
                    }

                    function S(b, c) {
                        b.off("change:state", R), c ? a.addClass(ja, "jw-flag-dragging") : R(b)
                    }

                    function T(b, c, d) {
                        var e, f = ja.className;
                        d = !!d, d && (f = f.replace(/\s*aspectMode/, ""), ja.className !== f && (ja.className = f), q(ja, {
                            display: "block"
                        }, d)), a.exists(b) && a.exists(c) && (v.set("width", b), v.set("height", c)), e = {
                            width: b
                        }, a.hasClass(ja, "jw-flag-aspect-mode") || (e.height = c), q(ja, e, !0), U(c), X(b, c)
                    }

                    function U(b) {
                        if (xa = V(b), pa && !xa) {
                            var c = Ga ? oa : v;
                            ia(c, c.get("state"))
                        }
                        a.toggleClass(ja, "jw-flag-audio-player", xa)
                    }

                    function V(a) {
                        if (v.get("aspectratio")) return !1;
                        if (o.isString(a) && a.indexOf("%") > -1) return !1;
                        var b = o.isNumber(a) ? a : v.get("containerHeight");
                        return W(b)
                    }

                    function W(a) {
                        return a && Fa * (s ? 1.75 : 1) >= a
                    }

                    function X(b, c) {
                        if (!b || isNaN(Number(b))) {
                            if (!la) return;
                            b = la.clientWidth
                        }
                        if (!c || isNaN(Number(c))) {
                            if (!la) return;
                            c = la.clientHeight
                        }
                        a.isMSIE(9) && document.all && !window.atob && (b = c = "100%");
                        var d = v.getVideo();
                        if (d) {
                            var e = d.resize(b, c, v.get("stretching"));
                            e && (clearTimeout(Ia), Ia = setTimeout(X, 250)), wa.resize(), pa.checkCompactMode(b)
                        }
                    }

                    function Y() {
                        if (Ka) {
                            var a = document.fullscreenElement || document.webkitCurrentFullScreenElement || document.mozFullScreenElement || document.msFullscreenElement;
                            return !(!a || a.id !== v.get("id"))
                        }
                        return Ga ? oa.getVideo().getFullScreen() : v.getVideo().getFullScreen()
                    }

                    function Z(a) {
                        var b = v.get("fullscreen"),
                            c = void 0 !== a.jwstate ? a.jwstate : Y();
                        b !== c && v.set("fullscreen", c), clearTimeout(Ia), Ia = setTimeout(X, 200)
                    }

                    function $(b, c) {
                        c ? (a.addClass(b, "jw-flag-fullscreen"), q(document.body, {
                            "overflow-y": "hidden"
                        }), aa()) : (a.removeClass(b, "jw-flag-fullscreen"), q(document.body, {
                            "overflow-y": ""
                        })), X()
                    }

                    function _() {
                        Ha = !1, clearTimeout(Da), pa.hideComponents(), a.addClass(ja, "jw-flag-user-inactive")
                    }

                    function aa() {
                        Ha || (a.removeClass(ja, "jw-flag-user-inactive"), pa.checkCompactMode(la.clientWidth)), Ha = !0, clearTimeout(Da), Da = setTimeout(_, Ea)
                    }

                    function ba() {
                        u.setFullscreen(!1)
                    }

                    function ca() {
                        sa && sa.setState(v.get("state")), da(v, v.mediaModel.get("mediaType")), v.mediaModel.on("change:mediaType", da, this)
                    }

                    function da(b, c) {
                        var d = "audio" === c;
                        a.toggleClass(ja, "jw-flag-media-audio", d)
                    }

                    function ea(b, c) {
                        var d = "LIVE" === a.adaptiveType(c);
                        a.toggleClass(ja, "jw-flag-live", d), Ma.setAltText(d ? "Live Broadcast" : "")
                    }

                    function fa(a, b) {
                        return b ? void(b.name ? va.updateText(b.name, b.message) : va.updateText(b.message, "")) : void va.playlistItem(a, a.get("playlistItem"))
                    }

                    function ga() {
                        var a = v.getVideo();
                        return a ? a.isCaster : !1
                    }

                    function ha() {
                        a.replaceClass(ja, /jw-state-\S+/, "jw-state-" + za)
                    }

                    function ia(b, c) {
                        if (za = c, clearTimeout(Ja), c === e.COMPLETE || c === e.IDLE ? Ja = setTimeout(ha, 100) : ha(), ga()) return void a.addClass(la, "jw-media-show");
                        switch (c) {
                            case e.PLAYING:
                                X();
                                break;
                            case e.PAUSED:
                                aa()
                        }
                    }
                    var ja, ka, la, ma, na, oa, pa, qa, ra, sa, ta, ua, va, wa, xa, ya, za, Aa, Ba, Ca, Da = -1,
                        Ea = s ? 4e3 : 2e3,
                        Fa = 40,
                        Ga = !1,
                        Ha = !1,
                        Ia = -1,
                        Ja = -1,
                        Ka = !1,
                        La = !1,
                        Ma = o.extend(this, c);
                    this.model = v, this.api = u, ja = a.createElement(p({
                        id: v.get("id")
                    })), a.isIE() && a.addClass(ja, "jw-ie");
                    var Na = v.get("width"),
                        Oa = v.get("height");
                    q(ja, {
                        width: Na.toString().indexOf("%") > 0 ? Na : Na + "px",
                        height: Oa.toString().indexOf("%") > 0 ? Oa : Oa + "px"
                    }), Ba = ja.requestFullscreen || ja.webkitRequestFullscreen || ja.webkitRequestFullScreen || ja.mozRequestFullScreen || ja.msRequestFullscreen, Ca = document.exitFullscreen || document.webkitExitFullscreen || document.webkitCancelFullScreen || document.mozCancelFullScreen || document.msExitFullscreen, Ka = Ba && Ca, this.onChangeSkin = function(b, c) {
                        a.replaceClass(ja, /jw-skin-\S+/, c ? "jw-skin-" + c : "")
                    }, this.handleColorOverrides = function() {
                        function b(b, d, e) {
                            if (e) {
                                b = a.prefix(b, "#" + c + " ");
                                var f = {};
                                f[d] = e, a.css(b.join(", "), f)
                            }
                        }
                        var c = v.get("id"),
                            d = v.get("skinColorActive"),
                            e = v.get("skinColorInactive"),
                            f = v.get("skinColorBackground");
                        b([".jw-toggle", ".jw-button-color:hover"], "color", d), b([".jw-active-option", ".jw-progress", ".jw-playlist-container .jw-option.jw-active-option", ".jw-playlist-container .jw-option:hover"], "background", d), b([".jw-text", ".jw-option", ".jw-button-color", ".jw-toggle.jw-off", ".jw-tooltip-title", ".jw-skip .jw-skip-icon", ".jw-playlist-container .jw-icon"], "color", e), b([".jw-cue", ".jw-knob"], "background", e), b([".jw-playlist-container .jw-option"], "border-bottom-color", e), b([".jw-background-color", ".jw-tooltip-title", ".jw-playlist", ".jw-playlist-container .jw-option"], "background", f), b([".jw-playlist-container ::-webkit-scrollbar"], "border-color", f)
                    }, this.setup = function() {
                        this.handleColorOverrides(), v.get("skin-loading") === !0 && (a.addClass(ja, "jw-flag-skin-loading"), v.once("change:skin-loading", function() {
                            a.removeClass(ja, "jw-flag-skin-loading")
                        })), this.onChangeSkin(v, v.get("skin"), ""), v.on("change:skin", this.onChangeSkin, this), la = ja.getElementsByClassName("jw-media")[0], ka = ja.getElementsByClassName("jw-controls")[0];
                        var c = ja.getElementsByClassName("jw-preview")[0];
                        qa = new l(v), qa.setup(c);
                        var d = ja.getElementsByClassName("jw-title")[0];
                        va = new n(v), va.setup(d), Q(), aa(), v.set("mediaContainer", la), v.mediaController.on("fullscreenchange", Z);
                        for (var f = t.length; f--;) document.addEventListener(t[f], Z, !1);
                        window.removeEventListener("resize", D), window.addEventListener("resize", D, !1), s && (window.removeEventListener("orientationchange", D), window.addEventListener("orientationchange", D, !1)), v.on("change:errorEvent", fa), v.on("change:controls", Pa), Pa(v, v.get("controls")), v.on("change:state", ia), v.on("change:duration", ea, this), v.on("change:flashBlocked", O), O(v, v.get("flashBlocked")), u.onPlaylistComplete(ba), u.onPlaylistItem(ca), v.on("change:castAvailable", F), F(v, v.get("castAvailable")), v.on("change:castActive", E), E(v, v.get("castActive")), v.get("stretching") && G(v, v.get("stretching")), v.on("change:stretching", G), ia(v, e.IDLE), v.on("change:fullscreen", Qa), I(pa), I(ua);
                        var g = v.get("aspectratio");
                        if (g) {
                            a.addClass(ja, "jw-flag-aspect-mode");
                            var h = ja.getElementsByClassName("jw-aspect")[0];
                            q(h, {
                                paddingTop: g
                            })
                        }
                        u.on(b.JWPLAYER_READY, function() {
                            D(), T(v.get("width"), v.get("height"))
                        })
                    };
                    var Pa = function(b, c) {
                            if (c) {
                                var d = Ga ? oa.get("state") : v.get("state");
                                ia(b, d)
                            }
                            a.toggleClass(ja, "jw-flag-controls-disabled", !c)
                        },
                        Qa = function(b, c) {
                            var d = v.getVideo();
                            Ka ? (c ? Ba.apply(ja) : Ca.apply(document), $(ja, c)) : a.isIE() ? $(ja, c) : (oa && oa.getVideo() && oa.getVideo().setFullscreen(c), d.setFullscreen(c)), d && 0 === d.getName().name.indexOf("flash") && d.setFullscreen(c)
                        };
                    this.resize = function(a, b) {
                        var c = !0;
                        T(a, b, c), D()
                    }, this.resizeMedia = X, this.reset = function() {
                        document.contains(ja) && ja.parentNode.replaceChild(Aa, ja), a.emptyElement(ja)
                    }, this.setupInstream = function(b) {
                        this.instreamModel = oa = b, oa.on("change:controls", Pa, this), oa.on("change:state", ia, this), Ga = !0, a.addClass(ja, "jw-flag-ads"), aa()
                    }, this.setAltText = function(a) {
                        pa.setAltText(a)
                    }, this.useExternalControls = function() {
                        a.addClass(ja, "jw-flag-ads-hide-controls")
                    }, this.destroyInstream = function() {
                        if (Ga = !1, oa && (oa.off(null, null, this), oa = null), this.setAltText(""), a.removeClass(ja, "jw-flag-ads"), a.removeClass(ja, "jw-flag-ads-hide-controls"), v.getVideo) {
                            var b = v.getVideo();
                            b.setContainer(la)
                        }
                        ea(v, v.get("duration")), ra.revertAlternateClickHandlers()
                    }, this.addCues = function(a) {
                        pa && pa.addCues(a)
                    }, this.clickHandler = function() {
                        return ra
                    }, this.controlsContainer = function() {
                        return ka
                    }, this.getContainer = this.element = function() {
                        return ja
                    }, this.getSafeRegion = function(b) {
                        var c = {
                                x: 0,
                                y: 0,
                                width: v.get("containerWidth") || 0,
                                height: v.get("containerHeight") || 0
                            },
                            d = v.get("dock");
                        return d && d.length && v.get("controls") && (c.y = ta.element().clientHeight, c.height -= c.y), b = b || !a.exists(b), b && v.get("controls") && (c.height -= pa.element().clientHeight), c
                    }, this.destroy = function() {
                        window.removeEventListener("resize", D), window.removeEventListener("orientationchange", D);
                        for (var b = t.length; b--;) document.removeEventListener(t[b], Z, !1);
                        v.mediaController && v.mediaController.off("fullscreenchange", Z), ja.removeEventListener("keydown", z, !1), ya && ya.destroy(), sa && (v.off("change:state", sa.statusDelegate), sa.destroy(), sa = null), Ga && this.destroyInstream(), ua && ua.destroy(), a.clearCss("#" + v.get("id"))
                    }
                };
            return u
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(119), c(48), c(45), c(127)], e = function(a, b, c, d) {
            var e = function(a) {
                this.model = a, this.setup(), this.model.on("change:dock", this.render, this)
            };
            return c.extend(e.prototype, {
                setup: function() {
                    var c = this.model.get("dock"),
                        e = this.click.bind(this),
                        f = a(c);
                    this.el = b.createElement(f), new d(this.el).on("click tap", e)
                },
                getDockButton: function(a) {
                    return b.hasClass(a.target, "jw-dock-button") ? a.target : b.hasClass(a.target, "jw-dock-text") ? a.target.parentElement.parentElement : a.target.parentElement
                },
                click: function(a) {
                    var b = this.getDockButton(a),
                        d = b.getAttribute("button"),
                        e = this.model.get("dock"),
                        f = c.findWhere(e, {
                            id: d
                        });
                    f && f.callback && f.callback(a)
                },
                render: function() {
                    var c = this.model.get("dock"),
                        d = a(c),
                        e = b.createElement(d);
                    this.el.innerHTML = e.innerHTML
                },
                element: function() {
                    return this.el
                }
            }), e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d = c(120);
        a.exports = (d["default"] || d).template({
            1: function(a, b, c, d) {
                var e, f, g = "function",
                    h = b.helperMissing,
                    i = this.escapeExpression,
                    j = '    <div class="jw-dock-button jw-background-color jw-reset';
                return e = b["if"].call(a, null != a ? a.btnClass : a, {
                    name: "if",
                    hash: {},
                    fn: this.program(2, d),
                    inverse: this.noop,
                    data: d
                }), null != e && (j += e), j += '" button="' + i((f = null != (f = b.id || (null != a ? a.id : a)) ? f : h, typeof f === g ? f.call(a, {
                    name: "id",
                    hash: {},
                    data: d
                }) : f)) + '">\n        <div class="jw-icon jw-dock-image jw-reset" ', e = b["if"].call(a, null != a ? a.img : a, {
                    name: "if",
                    hash: {},
                    fn: this.program(4, d),
                    inverse: this.noop,
                    data: d
                }), null != e && (j += e), j += '></div>\n        <div class="jw-arrow jw-reset"></div>\n', e = b["if"].call(a, null != a ? a.tooltip : a, {
                    name: "if",
                    hash: {},
                    fn: this.program(6, d),
                    inverse: this.noop,
                    data: d
                }), null != e && (j += e), j + "    </div>\n"
            },
            2: function(a, b, c, d) {
                var e, f = "function",
                    g = b.helperMissing,
                    h = this.escapeExpression;
                return " " + h((e = null != (e = b.btnClass || (null != a ? a.btnClass : a)) ? e : g, typeof e === f ? e.call(a, {
                    name: "btnClass",
                    hash: {},
                    data: d
                }) : e))
            },
            4: function(a, b, c, d) {
                var e, f = "function",
                    g = b.helperMissing,
                    h = this.escapeExpression;
                return "style='background-image: url(\"" + h((e = null != (e = b.img || (null != a ? a.img : a)) ? e : g, typeof e === f ? e.call(a, {
                    name: "img",
                    hash: {},
                    data: d
                }) : e)) + "\")'"
            },
            6: function(a, b, c, d) {
                var e, f = "function",
                    g = b.helperMissing,
                    h = this.escapeExpression;
                return '        <div class="jw-overlay jw-background-color jw-reset">\n            <span class="jw-text jw-dock-text jw-reset">' + h((e = null != (e = b.tooltip || (null != a ? a.tooltip : a)) ? e : g, typeof e === f ? e.call(a, {
                    name: "tooltip",
                    hash: {},
                    data: d
                }) : e)) + "</span>\n        </div>\n"
            },
            compiler: [6, ">= 2.0.0-beta.1"],
            main: function(a, b, c, d) {
                var e, f = '<div class="jw-dock jw-reset">\n';
                return e = b.each.call(a, a, {
                    name: "each",
                    hash: {},
                    fn: this.program(1, d),
                    inverse: this.noop,
                    data: d
                }), null != e && (f += e), f + "</div>"
            },
            useData: !0
        })
    }, function(a, b, c) {
        a.exports = c(121)
    }, function(a, b, c) {
        "use strict";
        var d = c(122),
            e = c(124)["default"],
            f = c(125)["default"],
            g = c(123),
            h = c(126),
            i = function() {
                var a = new d.HandlebarsEnvironment;
                return g.extend(a, d), a.SafeString = e, a.Exception = f, a.Utils = g, a.escapeExpression = g.escapeExpression, a.VM = h, a.template = function(b) {
                    return h.template(b, a)
                }, a
            },
            j = i();
        j.create = i, j["default"] = j, b["default"] = j
    }, function(a, b, c) {
        "use strict";

        function d(a, b) {
            this.helpers = a || {}, this.partials = b || {}, e(this)
        }

        function e(a) {
            a.registerHelper("helperMissing", function() {
                if (1 !== arguments.length) throw new g("Missing helper: '" + arguments[arguments.length - 1].name + "'")
            }), a.registerHelper("blockHelperMissing", function(b, c) {
                var d = c.inverse,
                    e = c.fn;
                if (b === !0) return e(this);
                if (b === !1 || null == b) return d(this);
                if (k(b)) return b.length > 0 ? (c.ids && (c.ids = [c.name]), a.helpers.each(b, c)) : d(this);
                if (c.data && c.ids) {
                    var g = q(c.data);
                    g.contextPath = f.appendContextPath(c.data.contextPath, c.name), c = {
                        data: g
                    }
                }
                return e(b, c)
            }), a.registerHelper("each", function(a, b) {
                if (!b) throw new g("Must pass iterator to #each");
                var c, d, e = b.fn,
                    h = b.inverse,
                    i = 0,
                    j = "";
                if (b.data && b.ids && (d = f.appendContextPath(b.data.contextPath, b.ids[0]) + "."), l(a) && (a = a.call(this)), b.data && (c = q(b.data)), a && "object" == typeof a)
                    if (k(a))
                        for (var m = a.length; m > i; i++) c && (c.index = i, c.first = 0 === i, c.last = i === a.length - 1, d && (c.contextPath = d + i)), j += e(a[i], {
                            data: c
                        });
                    else
                        for (var n in a) a.hasOwnProperty(n) && (c && (c.key = n, c.index = i, c.first = 0 === i, d && (c.contextPath = d + n)), j += e(a[n], {
                            data: c
                        }), i++);
                return 0 === i && (j = h(this)), j
            }), a.registerHelper("if", function(a, b) {
                return l(a) && (a = a.call(this)), !b.hash.includeZero && !a || f.isEmpty(a) ? b.inverse(this) : b.fn(this)
            }), a.registerHelper("unless", function(b, c) {
                return a.helpers["if"].call(this, b, {
                    fn: c.inverse,
                    inverse: c.fn,
                    hash: c.hash
                })
            }), a.registerHelper("with", function(a, b) {
                l(a) && (a = a.call(this));
                var c = b.fn;
                if (f.isEmpty(a)) return b.inverse(this);
                if (b.data && b.ids) {
                    var d = q(b.data);
                    d.contextPath = f.appendContextPath(b.data.contextPath, b.ids[0]), b = {
                        data: d
                    }
                }
                return c(a, b)
            }), a.registerHelper("log", function(b, c) {
                var d = c.data && null != c.data.level ? parseInt(c.data.level, 10) : 1;
                a.log(d, b)
            }), a.registerHelper("lookup", function(a, b) {
                return a && a[b]
            })
        }
        var f = c(123),
            g = c(125)["default"],
            h = "2.0.0";
        b.VERSION = h;
        var i = 6;
        b.COMPILER_REVISION = i;
        var j = {
            1: "<= 1.0.rc.2",
            2: "== 1.0.0-rc.3",
            3: "== 1.0.0-rc.4",
            4: "== 1.x.x",
            5: "== 2.0.0-alpha.x",
            6: ">= 2.0.0-beta.1"
        };
        b.REVISION_CHANGES = j;
        var k = f.isArray,
            l = f.isFunction,
            m = f.toString,
            n = "[object Object]";
        b.HandlebarsEnvironment = d, d.prototype = {
            constructor: d,
            logger: o,
            log: p,
            registerHelper: function(a, b) {
                if (m.call(a) === n) {
                    if (b) throw new g("Arg not supported with multiple helpers");
                    f.extend(this.helpers, a)
                } else this.helpers[a] = b
            },
            unregisterHelper: function(a) {
                delete this.helpers[a]
            },
            registerPartial: function(a, b) {
                m.call(a) === n ? f.extend(this.partials, a) : this.partials[a] = b
            },
            unregisterPartial: function(a) {
                delete this.partials[a]
            }
        };
        var o = {
            methodMap: {
                0: "debug",
                1: "info",
                2: "warn",
                3: "error"
            },
            DEBUG: 0,
            INFO: 1,
            WARN: 2,
            ERROR: 3,
            level: 3,
            log: function(a, b) {
                if (o.level <= a) {
                    var c = o.methodMap[a];
                    "undefined" != typeof console && console[c] && console[c].call(console, b)
                }
            }
        };
        b.logger = o;
        var p = o.log;
        b.log = p;
        var q = function(a) {
            var b = f.extend({}, a);
            return b._parent = a, b
        };
        b.createFrame = q
    }, function(a, b, c) {
        "use strict";

        function d(a) {
            return j[a]
        }

        function e(a) {
            for (var b = 1; b < arguments.length; b++)
                for (var c in arguments[b]) Object.prototype.hasOwnProperty.call(arguments[b], c) && (a[c] = arguments[b][c]);
            return a
        }

        function f(a) {
            return a instanceof i ? a.toString() : null == a ? "" : a ? (a = "" + a, l.test(a) ? a.replace(k, d) : a) : a + ""
        }

        function g(a) {
            return a || 0 === a ? !(!o(a) || 0 !== a.length) : !0
        }

        function h(a, b) {
            return (a ? a + "." : "") + b
        }
        var i = c(124)["default"],
            j = {
                "&": "&amp;",
                "<": "&lt;",
                ">": "&gt;",
                '"': "&quot;",
                "'": "&#x27;",
                "`": "&#x60;"
            },
            k = /[&<>"'`]/g,
            l = /[&<>"'`]/;
        b.extend = e;
        var m = Object.prototype.toString;
        b.toString = m;
        var n = function(a) {
            return "function" == typeof a
        };
        n(/x/) && (n = function(a) {
            return "function" == typeof a && "[object Function]" === m.call(a)
        });
        var n;
        b.isFunction = n;
        var o = Array.isArray || function(a) {
            return a && "object" == typeof a ? "[object Array]" === m.call(a) : !1
        };
        b.isArray = o, b.escapeExpression = f, b.isEmpty = g, b.appendContextPath = h
    }, function(a, b) {
        "use strict";

        function c(a) {
            this.string = a
        }
        c.prototype.toString = function() {
            return "" + this.string
        }, b["default"] = c
    }, function(a, b) {
        "use strict";

        function c(a, b) {
            var c;
            b && b.firstLine && (c = b.firstLine, a += " - " + c + ":" + b.firstColumn);
            for (var e = Error.prototype.constructor.call(this, a), f = 0; f < d.length; f++) this[d[f]] = e[d[f]];
            c && (this.lineNumber = c, this.column = b.firstColumn)
        }
        var d = ["description", "fileName", "lineNumber", "message", "name", "number", "stack"];
        c.prototype = new Error, b["default"] = c
    }, function(a, b, c) {
        "use strict";

        function d(a) {
            var b = a && a[0] || 1,
                c = l;
            if (b !== c) {
                if (c > b) {
                    var d = m[c],
                        e = m[b];
                    throw new k("Template was precompiled with an older version of Handlebars than the current runtime. Please update your precompiler to a newer version (" + d + ") or downgrade your runtime to an older version (" + e + ").")
                }
                throw new k("Template was precompiled with a newer version of Handlebars than the current runtime. Please update your runtime to a newer version (" + a[1] + ").")
            }
        }

        function e(a, b) {
            if (!b) throw new k("No environment passed to template");
            if (!a || !a.main) throw new k("Unknown template object: " + typeof a);
            b.VM.checkRevision(a.compiler);
            var c = function(c, d, e, f, g, h, i, l, m) {
                    g && (f = j.extend({}, f, g));
                    var n = b.VM.invokePartial.call(this, c, e, f, h, i, l, m);
                    if (null == n && b.compile) {
                        var o = {
                            helpers: h,
                            partials: i,
                            data: l,
                            depths: m
                        };
                        i[e] = b.compile(c, {
                            data: void 0 !== l,
                            compat: a.compat
                        }, b), n = i[e](f, o)
                    }
                    if (null != n) {
                        if (d) {
                            for (var p = n.split("\n"), q = 0, r = p.length; r > q && (p[q] || q + 1 !== r); q++) p[q] = d + p[q];
                            n = p.join("\n")
                        }
                        return n
                    }
                    throw new k("The partial " + e + " could not be compiled when running in runtime-only mode")
                },
                d = {
                    lookup: function(a, b) {
                        for (var c = a.length, d = 0; c > d; d++)
                            if (a[d] && null != a[d][b]) return a[d][b]
                    },
                    lambda: function(a, b) {
                        return "function" == typeof a ? a.call(b) : a
                    },
                    escapeExpression: j.escapeExpression,
                    invokePartial: c,
                    fn: function(b) {
                        return a[b]
                    },
                    programs: [],
                    program: function(a, b, c) {
                        var d = this.programs[a],
                            e = this.fn(a);
                        return b || c ? d = f(this, a, e, b, c) : d || (d = this.programs[a] = f(this, a, e)), d
                    },
                    data: function(a, b) {
                        for (; a && b--;) a = a._parent;
                        return a
                    },
                    merge: function(a, b) {
                        var c = a || b;
                        return a && b && a !== b && (c = j.extend({}, b, a)), c
                    },
                    noop: b.VM.noop,
                    compilerInfo: a.compiler
                },
                e = function(b, c) {
                    c = c || {};
                    var f = c.data;
                    e._setup(c), !c.partial && a.useData && (f = i(b, f));
                    var g;
                    return a.useDepths && (g = c.depths ? [b].concat(c.depths) : [b]), a.main.call(d, b, d.helpers, d.partials, f, g)
                };
            return e.isTop = !0, e._setup = function(c) {
                c.partial ? (d.helpers = c.helpers, d.partials = c.partials) : (d.helpers = d.merge(c.helpers, b.helpers), a.usePartial && (d.partials = d.merge(c.partials, b.partials)))
            }, e._child = function(b, c, e) {
                if (a.useDepths && !e) throw new k("must pass parent depths");
                return f(d, b, a[b], c, e)
            }, e
        }

        function f(a, b, c, d, e) {
            var f = function(b, f) {
                return f = f || {}, c.call(a, b, a.helpers, a.partials, f.data || d, e && [b].concat(e))
            };
            return f.program = b, f.depth = e ? e.length : 0, f
        }

        function g(a, b, c, d, e, f, g) {
            var h = {
                partial: !0,
                helpers: d,
                partials: e,
                data: f,
                depths: g
            };
            if (void 0 === a) throw new k("The partial " + b + " could not be found");
            return a instanceof Function ? a(c, h) : void 0
        }

        function h() {
            return ""
        }

        function i(a, b) {
            return b && "root" in b || (b = b ? n(b) : {}, b.root = a), b
        }
        var j = c(123),
            k = c(125)["default"],
            l = c(122).COMPILER_REVISION,
            m = c(122).REVISION_CHANGES,
            n = c(122).createFrame;
        b.checkRevision = d, b.template = e, b.program = f, b.invokePartial = g, b.noop = h
    }, function(a, b, c) {
        var d, e;
        d = [c(47), c(46), c(45), c(48)], e = function(a, b, c, d) {
            function e(a, b) {
                return /touch/.test(a.type) ? (a.originalEvent || a).changedTouches[0]["page" + b] : a["page" + b]
            }

            function f(a) {
                var b = a || window.event;
                return a instanceof MouseEvent ? "which" in b ? 3 === b.which : "button" in b ? 2 === b.button : !1 : !1
            }

            function g(a, b, c) {
                var d;
                return d = b instanceof MouseEvent || !b.touches && !b.changedTouches ? b : b.touches && b.touches.length ? b.touches[0] : b.changedTouches[0], {
                    type: a,
                    target: b.target,
                    currentTarget: c,
                    pageX: d.pageX,
                    pageY: d.pageY
                }
            }

            function h(a) {
                (a instanceof MouseEvent || a instanceof window.TouchEvent) && (a.preventManipulation && a.preventManipulation(), a.cancelable && a.preventDefault && a.preventDefault())
            }
            var i = !c.isUndefined(window.PointerEvent),
                j = !i && d.isMobile(),
                k = !i && !j,
                l = d.isFF() && d.isOSX(),
                m = function(a, d) {
                    function j(a) {
                        (k || i && "touch" !== a.pointerType) && r(b.touchEvents.OVER, a)
                    }

                    function m(a) {
                        (k || i && "touch" !== a.pointerType) && r(b.touchEvents.MOVE, a)
                    }

                    function n(c) {
                        (k || i && "touch" !== c.pointerType && !a.contains(document.elementFromPoint(c.x, c.y))) && r(b.touchEvents.OUT, c)
                    }

                    function o(b) {
                        s = b.target, w = e(b, "X"), x = e(b, "Y"), f(b) || (i ? b.isPrimary && (d.preventScrolling && (t = b.pointerId, a.setPointerCapture(t)), a.addEventListener("pointermove", p), a.addEventListener("pointercancel", q), a.addEventListener("pointerup", q)) : k && (document.addEventListener("mousemove", p), l && "object" === b.target.nodeName.toLowerCase() ? a.addEventListener("click", q) : document.addEventListener("mouseup", q)), s.addEventListener("touchmove", p), s.addEventListener("touchcancel", q), s.addEventListener("touchend", q))
                    }

                    function p(a) {
                        var c = b.touchEvents,
                            f = 6;
                        if (v) r(c.DRAG, a);
                        else {
                            var g = e(a, "X"),
                                i = e(a, "Y"),
                                j = g - w,
                                k = i - x;
                            j * j + k * k > f * f && (r(c.DRAG_START, a), v = !0, r(c.DRAG, a))
                        }
                        d.preventScrolling && h(a)
                    }

                    function q(c) {
                        var e = b.touchEvents;
                        i ? (d.preventScrolling && a.releasePointerCapture(t), a.removeEventListener("pointermove", p), a.removeEventListener("pointercancel", q), a.removeEventListener("pointerup", q)) : k && (document.removeEventListener("mousemove", p), document.removeEventListener("mouseup", q)), s.removeEventListener("touchmove", p), s.removeEventListener("touchcancel", q), s.removeEventListener("touchend", q), v ? r(e.DRAG_END, c) : d.directSelect && c.target !== a || -1 !== c.type.indexOf("cancel") || (i && c instanceof window.PointerEvent ? "touch" === c.pointerType ? r(e.TAP, c) : r(e.CLICK, c) : k ? r(e.CLICK, c) : (r(e.TAP, c), h(c))), s = null, v = !1
                    }

                    function r(a, e) {
                        var f;
                        if (d.enableDoubleTap && (a === b.touchEvents.CLICK || a === b.touchEvents.TAP))
                            if (c.now() - y < z) {
                                var h = a === b.touchEvents.CLICK ? b.touchEvents.DOUBLE_CLICK : b.touchEvents.DOUBLE_TAP;
                                f = g(h, e, u), A.trigger(h, f), y = 0
                            } else y = c.now();
                        f = g(a, e, u), A.trigger(a, f)
                    }
                    var s, t, u = a,
                        v = !1,
                        w = 0,
                        x = 0,
                        y = 0,
                        z = 300;
                    d = d || {}, i ? (a.addEventListener("pointerdown", o), d.useHover && (a.addEventListener("pointerover", j), a.addEventListener("pointerout", n)), d.useMove && a.addEventListener("pointermove", m)) : k && (a.addEventListener("mousedown", o), d.useHover && (a.addEventListener("mouseover", j), a.addEventListener("mouseout", n)), d.useMove && a.addEventListener("mousemove", m)), a.addEventListener("touchstart", o);
                    var A = this;
                    return this.triggerEvent = r, this.destroy = function() {
                        a.removeEventListener("touchstart", o), a.removeEventListener("mousedown", o), s && (s.removeEventListener("touchmove", p), s.removeEventListener("touchcancel", q), s.removeEventListener("touchend", q)), i && (d.preventScrolling && a.releasePointerCapture(t), a.removeEventListener("pointerover", j), a.removeEventListener("pointerdown", o), a.removeEventListener("pointermove", p), a.removeEventListener("pointermove", m), a.removeEventListener("pointercancel", q), a.removeEventListener("pointerout", n), a.removeEventListener("pointerup", q)), a.removeEventListener("click", q), a.removeEventListener("mouseover", j), a.removeEventListener("mousemove", m), a.removeEventListener("mouseout", n), document.removeEventListener("mousemove", p), document.removeEventListener("mouseup", q)
                    }, this
                };
            return c.extend(m.prototype, a), m
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(55), c(62), c(45)], e = function(a, b, c, d) {
            var e = b.style,
                f = {
                    back: !0,
                    fontSize: 15,
                    fontFamily: "Arial,sans-serif",
                    fontOpacity: 100,
                    color: "#FFF",
                    backgroundColor: "#000",
                    backgroundOpacity: 100,
                    edgeStyle: null,
                    windowColor: "#FFF",
                    windowOpacity: 0,
                    preprocessor: d.identity
                },
                g = function(g) {
                    function h(b) {
                        b = b || "";
                        var c = "jw-captions-window jw-reset";
                        b ? (s.innerHTML = b, r.className = c + " jw-captions-window-active") : (r.className = c, a.empty(s))
                    }

                    function i(a) {
                        p = a, k(n, p)
                    }

                    function j(a, b) {
                        var c = a.source,
                            e = b.metadata;
                        return c ? e && d.isNumber(e[c]) ? e[c] : !1 : b.position
                    }

                    function k(a, b) {
                        if (a && a.data && b) {
                            var c = j(a, b);
                            if (c !== !1) {
                                var d = a.data;
                                if (!(o >= 0 && l(d, o, c))) {
                                    for (var e = -1, f = 0; f < d.length; f++)
                                        if (l(d, f, c)) {
                                            e = f;
                                            break
                                        } - 1 === e ? h("") : e !== o && (o = e, h(t.preprocessor(d[o].text)))
                                }
                            }
                        }
                    }

                    function l(a, b, c) {
                        return a[b].begin <= c && (!a[b].end || a[b].end >= c) && (b === a.length - 1 || a[b + 1].begin >= c)
                    }

                    function m(a, c, d) {
                        var e = b.hexToRgba("#000000", d);
                        "dropshadow" === a ? c.textShadow = "0 2px 1px " + e : "raised" === a ? c.textShadow = "0 0 5px " + e + ", 0 1px 5px " + e + ", 0 2px 5px " + e : "depressed" === a ? c.textShadow = "0 -2px 1px " + e : "uniform" === a && (c.textShadow = "-2px 0 1px " + e + ",2px 0 1px " + e + ",0 -2px 1px " + e + ",0 2px 1px " + e + ",-1px 1px 1px " + e + ",1px 1px 1px " + e + ",1px -1px 1px " + e + ",1px 1px 1px " + e)
                    }
                    var n, o, p, q, r, s, t = {};
                    q = document.createElement("div"), q.className = "jw-captions jw-reset", this.show = function() {
                        q.className = "jw-captions jw-captions-enabled jw-reset"
                    }, this.hide = function() {
                        q.className = "jw-captions jw-reset"
                    }, this.populate = function(a) {
                        return o = -1, n = a, a ? void k(a, p) : void h("")
                    }, this.resize = function() {
                        var a = q.clientWidth,
                            b = Math.pow(a / 400, .6);
                        if (b) {
                            var c = t.fontSize * b;
                            e(q, {
                                fontSize: Math.round(c) + "px"
                            })
                        }
                    }, this.setup = function(a) {
                        if (r = document.createElement("div"), s = document.createElement("span"), r.className = "jw-captions-window jw-reset", s.className = "jw-captions-text jw-reset", t = d.extend({}, f, a), a) {
                            var c = t.fontOpacity,
                                h = t.windowOpacity,
                                i = t.edgeStyle,
                                j = t.backgroundColor,
                                k = {},
                                l = {
                                    color: b.hexToRgba(t.color, c),
                                    fontFamily: t.fontFamily,
                                    fontStyle: t.fontStyle,
                                    fontWeight: t.fontWeight,
                                    textDecoration: t.textDecoration
                                };
                            h && (k.backgroundColor = b.hexToRgba(t.windowColor, h)), m(i, l, c), t.back ? l.backgroundColor = b.hexToRgba(j, t.backgroundOpacity) : null === i && m("uniform", l), e(r, k), e(s, l)
                        }
                        r.appendChild(s), q.appendChild(r), this.populate(g.get("captionsTrack"))
                    }, this.element = function() {
                        return q
                    }, g.on("change:playlistItem", function() {
                        p = null, o = -1, h("")
                    }, this), g.on("change:captionsTrack", function(a, b) {
                        this.populate(b)
                    }, this), g.mediaController.on("seek", function() {
                        o = -1
                    }, this), g.mediaController.on("time seek", i, this), g.mediaController.on("subtitlesTrackData", function() {
                        k(n, p)
                    }, this), g.on("change:state", function(a, b) {
                        switch (b) {
                            case c.IDLE:
                            case c.ERROR:
                            case c.COMPLETE:
                                this.hide();
                                break;
                            default:
                                this.show()
                        }
                    }, this)
                };
            return g
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(127), c(46), c(47), c(45)], e = function(a, b, c, d) {
            var e = function(e, f, g) {
                function h(a) {
                    return e.get("flashBlocked") ? void 0 : k ? void k(a) : void o.trigger(a.type === b.touchEvents.CLICK ? "click" : "tap")
                }

                function i() {
                    return l ? void l() : void o.trigger("doubleClick")
                }
                var j, k, l, m = {
                    enableDoubleTap: !0,
                    useMove: !0
                };
                d.extend(this, c), j = f, this.element = function() {
                    return j
                };
                var n = new a(j, d.extend(m, g));
                n.on("click tap", h), n.on("doubleClick doubleTap", i), n.on("move", function() {
                    o.trigger("move")
                }), n.on("over", function() {
                    o.trigger("over")
                }), n.on("out", function() {
                    o.trigger("out")
                }), this.clickHandler = h;
                var o = this;
                this.setAlternateClickHandlers = function(a, b) {
                    k = a, l = b || null
                }, this.revertAlternateClickHandlers = function() {
                    k = null, l = null
                }
            };
            return e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(47), c(127), c(131), c(45)], e = function(a, b, c, d, e) {
            var f = function(f) {
                e.extend(this, b), this.model = f, this.el = a.createElement(d({}));
                var g = this;
                this.iconUI = new c(this.el).on("click tap", function(a) {
                    g.trigger(a.type)
                })
            };
            return e.extend(f.prototype, {
                element: function() {
                    return this.el
                }
            }), f
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d = c(120);
        a.exports = (d["default"] || d).template({
            compiler: [6, ">= 2.0.0-beta.1"],
            main: function(a, b, c, d) {
                return '<div class="jw-display-icon-container jw-background-color jw-reset">\n    <div class="jw-icon jw-icon-display jw-button-color jw-reset"></div>\n</div>\n'
            },
            useData: !0
        })
    }, function(a, b, c) {
        var d, e;
        d = [c(127), c(48), c(46), c(45), c(47), c(133)], e = function(a, b, c, d, e, f) {
            var g = b.style,
                h = {
                    linktarget: "_blank",
                    margin: 8,
                    hide: !1,
                    position: "top-right"
                },
                i = function(i) {
                    var j, k, l = new Image,
                        m = d.extend({}, i.get("logo"));
                    return d.extend(this, e), this.setup = function(e) {
                        if (k = d.extend({}, h, m), k.hide = "true" === k.hide.toString(), j = b.createElement(f()), k.file) {
                            k.hide && b.addClass(j, "jw-hide"), b.addClass(j, "jw-logo-" + (k.position || h.position)), "top-right" === k.position && (i.on("change:dock", this.topRight, this), i.on("change:controls", this.topRight, this), this.topRight(i)), i.set("logo", k), l.onload = function() {
                                var a = {
                                    backgroundImage: 'url("' + this.src + '")',
                                    width: this.width,
                                    height: this.height
                                };
                                if (k.margin !== h.margin) {
                                    var b = /(\w+)-(\w+)/.exec(k.position);
                                    3 === b.length && (a["margin-" + b[1]] = k.margin, a["margin-" + b[2]] = k.margin)
                                }
                                g(j, a), i.set("logoWidth", a.width)
                            }, l.src = k.file;
                            var n = new a(j);
                            n.on("click tap", function(a) {
                                b.exists(a) && a.stopPropagation && a.stopPropagation(), this.trigger(c.JWPLAYER_LOGO_CLICK, {
                                    link: k.link,
                                    linktarget: k.linktarget
                                })
                            }, this), e.appendChild(j)
                        }
                    }, this.topRight = function(a) {
                        var b = a.get("controls"),
                            c = a.get("dock"),
                            d = b && (c && c.length || a.get("sharing") || a.get("related"));
                        g(j, {
                            top: d ? "3.5em" : 0
                        })
                    }, this.element = function() {
                        return j
                    }, this.position = function() {
                        return k.position
                    }, this.destroy = function() {
                        l.onload = null
                    }, this
                };
            return i
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d = c(120);
        a.exports = (d["default"] || d).template({
            compiler: [6, ">= 2.0.0-beta.1"],
            main: function(a, b, c, d) {
                return '<div class="jw-logo jw-reset"></div>'
            },
            useData: !0
        })
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(45), c(47), c(58), c(127), c(136), c(135), c(142), c(144), c(146), c(147)], e = function(a, b, c, d, e, f, g, h, i, j, k) {
            function l(a, b) {
                var c = document.createElement("div");
                return c.className = "jw-icon jw-icon-inline jw-button-color jw-reset " + a, c.style.display = "none", b && new e(c).on("click tap", function() {
                    b()
                }), {
                    element: function() {
                        return c
                    },
                    toggle: function(a) {
                        a ? this.show() : this.hide()
                    },
                    show: function() {
                        c.style.display = ""
                    },
                    hide: function() {
                        c.style.display = "none"
                    }
                }
            }

            function m(a) {
                var b = document.createElement("span");
                return b.className = "jw-text jw-reset " + a, b
            }

            function n(a) {
                var b = new h(a);
                return b
            }

            function o(a, c) {
                var d = document.createElement("div");
                return d.className = "jw-group jw-controlbar-" + a + "-group jw-reset", b.each(c, function(a) {
                    a.element && (a = a.element()), d.appendChild(a)
                }), d
            }

            function p(b, c) {
                this._api = b, this._model = c, this._isMobile = a.isMobile(), this._compactModeMaxSize = 400, this._maxCompactWidth = -1, this.setup()
            }
            return b.extend(p.prototype, c, {
                setup: function() {
                    this.build(), this.initialize()
                },
                build: function() {
                    var a, c, d, e, h = new g(this._model, this._api),
                        p = new k("jw-icon-more");
                    this._model.get("visualplaylist") !== !1 && (a = new i("jw-icon-playlist")), this._isMobile || (e = l("jw-icon-volume", this._api.setMute), c = new f("jw-slider-volume", "horizontal"), d = new j(this._model, "jw-icon-volume")), this.elements = {
                        alt: m("jw-text-alt"),
                        play: l("jw-icon-playback", this._api.play.bind(this, {
                            reason: "interaction"
                        })),
                        prev: l("jw-icon-prev", this._api.playlistPrev.bind(this, {
                            reason: "interaction"
                        })),
                        next: l("jw-icon-next", this._api.playlistNext.bind(this, {
                            reason: "interaction"
                        })),
                        playlist: a,
                        elapsed: m("jw-text-elapsed"),
                        time: h,
                        duration: m("jw-text-duration"),
                        drawer: p,
                        hd: n("jw-icon-hd"),
                        cc: n("jw-icon-cc"),
                        audiotracks: n("jw-icon-audio-tracks"),
                        mute: e,
                        volume: c,
                        volumetooltip: d,
                        cast: l("jw-icon-cast jw-off", this._api.castToggle),
                        fullscreen: l("jw-icon-fullscreen", this._api.setFullscreen)
                    }, this.layout = {
                        left: [this.elements.play, this.elements.prev, this.elements.playlist, this.elements.next, this.elements.elapsed],
                        center: [this.elements.time, this.elements.alt],
                        right: [this.elements.duration, this.elements.hd, this.elements.cc, this.elements.audiotracks, this.elements.drawer, this.elements.mute, this.elements.cast, this.elements.volume, this.elements.volumetooltip, this.elements.fullscreen],
                        drawer: [this.elements.hd, this.elements.cc, this.elements.audiotracks]
                    }, this.menus = b.compact([this.elements.playlist, this.elements.hd, this.elements.cc, this.elements.audiotracks, this.elements.volumetooltip]), this.layout.left = b.compact(this.layout.left), this.layout.center = b.compact(this.layout.center), this.layout.right = b.compact(this.layout.right), this.layout.drawer = b.compact(this.layout.drawer), this.el = document.createElement("div"), this.el.className = "jw-controlbar jw-background-color jw-reset", this.elements.left = o("left", this.layout.left), this.elements.center = o("center", this.layout.center), this.elements.right = o("right", this.layout.right), this.el.appendChild(this.elements.left), this.el.appendChild(this.elements.center), this.el.appendChild(this.elements.right)
                },
                initialize: function() {
                    this.elements.play.show(), this.elements.fullscreen.show(), this.elements.mute && this.elements.mute.show(), this.onVolume(this._model, this._model.get("volume")), this.onPlaylist(this._model, this._model.get("playlist")), this.onPlaylistItem(this._model, this._model.get("playlistItem")), this.onMediaModel(this._model, this._model.get("mediaModel")), this.onCastAvailable(this._model, this._model.get("castAvailable")), this.onCastActive(this._model, this._model.get("castActive")), this.onCaptionsList(this._model, this._model.get("captionsList")), this._model.on("change:volume", this.onVolume, this), this._model.on("change:mute", this.onMute, this), this._model.on("change:playlist", this.onPlaylist, this), this._model.on("change:playlistItem", this.onPlaylistItem, this), this._model.on("change:mediaModel", this.onMediaModel, this), this._model.on("change:castAvailable", this.onCastAvailable, this), this._model.on("change:castActive", this.onCastActive, this), this._model.on("change:duration", this.onDuration, this), this._model.on("change:position", this.onElapsed, this), this._model.on("change:fullscreen", this.onFullscreen, this), this._model.on("change:captionsList", this.onCaptionsList, this), this._model.on("change:captionsIndex", this.onCaptionsIndex, this), this._model.on("change:compactUI", this.onCompactUI, this), this.elements.volume && this.elements.volume.on("update", function(a) {
                        var b = a.percentage;
                        this._api.setVolume(b)
                    }, this), this.elements.volumetooltip && (this.elements.volumetooltip.on("update", function(a) {
                        var b = a.percentage;
                        this._api.setVolume(b)
                    }, this), this.elements.volumetooltip.on("toggleValue", function() {
                        this._api.setMute()
                    }, this)), this.elements.playlist && this.elements.playlist.on("select", function(a) {
                        this._model.once("itemReady", function() {
                            this._api.play({
                                reason: "interaction"
                            })
                        }, this), this._api.load(a)
                    }, this), this.elements.hd.on("select", function(a) {
                        this._model.getVideo().setCurrentQuality(a)
                    }, this), this.elements.hd.on("toggleValue", function() {
                        this._model.getVideo().setCurrentQuality(0 === this._model.getVideo().getCurrentQuality() ? 1 : 0)
                    }, this), this.elements.cc.on("select", function(a) {
                        this._api.setCurrentCaptions(a)
                    }, this), this.elements.cc.on("toggleValue", function() {
                        var a = this._model.get("captionsIndex");
                        this._api.setCurrentCaptions(a ? 0 : 1)
                    }, this), this.elements.audiotracks.on("select", function(a) {
                        this._model.getVideo().setCurrentAudioTrack(a)
                    }, this), new e(this.elements.duration).on("click tap", function() {
                        if ("DVR" === a.adaptiveType(this._model.get("duration"))) {
                            var b = this._model.get("position");
                            this._api.seek(Math.max(d.dvrSeekLimit, b))
                        }
                    }, this), new e(this.el).on("click tap drag", function() {
                        this.trigger("userAction")
                    }, this), this.elements.drawer.on("open-drawer close-drawer", function(b, c) {
                        a.toggleClass(this.el, "jw-drawer-expanded", c.isOpen), c.isOpen || this.closeMenus()
                    }, this), b.each(this.menus, function(a) {
                        a.on("open-tooltip", this.closeMenus, this)
                    }, this)
                },
                onCaptionsList: function(a, b) {
                    var c = a.get("captionsIndex");
                    this.elements.cc.setup(b, c), this.clearCompactMode()
                },
                onCaptionsIndex: function(a, b) {
                    this.elements.cc.selectItem(b)
                },
                onPlaylist: function(a, b) {
                    var c = b.length > 1;
                    this.elements.next.toggle(c), this.elements.prev.toggle(c), this.elements.playlist && this.elements.playlist.setup(b, a.get("item"))
                },
                onPlaylistItem: function(a) {
                    this.elements.time.updateBuffer(0), this.elements.time.render(0), this.elements.duration.innerHTML = "00:00", this.elements.elapsed.innerHTML = "00:00", this.clearCompactMode();
                    var b = a.get("item");
                    this.elements.playlist && this.elements.playlist.selectItem(b), this.elements.audiotracks.setup()
                },
                onMediaModel: function(c, d) {
                    d.on("change:levels", function(a, b) {
                        this.elements.hd.setup(b, a.get("currentLevel")), this.clearCompactMode()
                    }, this), d.on("change:currentLevel", function(a, b) {
                        this.elements.hd.selectItem(b)
                    }, this), d.on("change:audioTracks", function(a, c) {
                        var d = b.map(c, function(a) {
                            return {
                                label: a.name
                            }
                        });
                        this.elements.audiotracks.setup(d, a.get("currentAudioTrack"), {
                            toggle: !1
                        }), this.clearCompactMode()
                    }, this), d.on("change:currentAudioTrack", function(a, b) {
                        this.elements.audiotracks.selectItem(b)
                    }, this), d.on("change:state", function(b, c) {
                        "complete" === c && (this.elements.drawer.closeTooltip(), a.removeClass(this.el, "jw-drawer-expanded"))
                    }, this)
                },
                onVolume: function(a, b) {
                    this.renderVolume(a.get("mute"), b)
                },
                onMute: function(a, b) {
                    this.renderVolume(b, a.get("volume"))
                },
                renderVolume: function(b, c) {
                    this.elements.mute && a.toggleClass(this.elements.mute.element(), "jw-off", b), this.elements.volume && this.elements.volume.render(b ? 0 : c), this.elements.volumetooltip && (this.elements.volumetooltip.volumeSlider.render(b ? 0 : c), a.toggleClass(this.elements.volumetooltip.element(), "jw-off", b))
                },
                onCastAvailable: function(a, b) {
                    this.elements.cast.toggle(b), this.clearCompactMode()
                },
                onCastActive: function(b, c) {
                    a.toggleClass(this.elements.cast.element(), "jw-off", !c)
                },
                onElapsed: function(b, c) {
                    var d, e = b.get("duration");
                    d = "DVR" === a.adaptiveType(e) ? "-" + a.timeFormat(-e) : a.timeFormat(c), this.elements.elapsed.innerHTML = d
                },
                onDuration: function(b, c) {
                    var d;
                    "DVR" === a.adaptiveType(c) ? (d = "Live", this.clearCompactMode()) : d = a.timeFormat(c), this.elements.duration.innerHTML = d
                },
                onFullscreen: function(b, c) {
                    a.toggleClass(this.elements.fullscreen.element(), "jw-off", c)
                },
                element: function() {
                    return this.el
                },
                getVisibleBounds: function() {
                    var b, c = this.el,
                        d = getComputedStyle ? getComputedStyle(c) : c.currentStyle;
                    return "table" === d.display ? a.bounds(c) : (c.style.visibility = "hidden", c.style.display = "table", b = a.bounds(c), c.style.visibility = c.style.display = "", b)
                },
                setAltText: function(a) {
                    this.elements.alt.innerHTML = a
                },
                addCues: function(a) {
                    this.elements.time && (b.each(a, function(a) {
                        this.elements.time.addCue(a)
                    }, this), this.elements.time.drawCues())
                },
                closeMenus: function(a) {
                    b.each(this.menus, function(b) {
                        a && a.target === b.el || b.closeTooltip(a)
                    })
                },
                hideComponents: function() {
                    this.closeMenus(), this.elements.drawer.closeTooltip(), a.removeClass(this.el, "jw-drawer-expanded")
                },
                clearCompactMode: function() {
                    this._maxCompactWidth = -1, this._model.set("compactUI", !1), this._containerWidth && this.checkCompactMode(this._containerWidth)
                },
                setCompactModeBounds: function() {
                    if (this.element().offsetWidth > 0) {
                        var b = this.elements.left.offsetWidth + this.elements.right.offsetWidth;
                        if ("LIVE" === a.adaptiveType(this._model.get("duration"))) this._maxCompactWidth = b + this.elements.alt.offsetWidth;
                        else {
                            var c = b + (this.elements.center.offsetWidth - this.elements.time.el.offsetWidth),
                                d = .2;
                            this._maxCompactWidth = c / (1 - d)
                        }
                    }
                },
                checkCompactMode: function(a) {
                    -1 === this._maxCompactWidth && this.setCompactModeBounds(), this._containerWidth = a, -1 !== this._maxCompactWidth && (a >= this._compactModeMaxSize && a > this._maxCompactWidth ? this._model.set("compactUI", !1) : (a < this._compactModeMaxSize || a <= this._maxCompactWidth) && this._model.set("compactUI", !0))
                },
                onCompactUI: function(c, d) {
                    a.removeClass(this.el, "jw-drawer-expanded"), this.elements.drawer.setup(this.layout.drawer, d), (!d || this.elements.drawer.activeContents.length < 2) && b.each(this.layout.drawer, function(a) {
                        this.elements.right.insertBefore(a.el, this.elements.drawer.el)
                    }, this)
                }
            }), p
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(48), c(58), c(136), c(139), c(140), c(141)], e = function(a, b, c, d, e, f, g) {
            var h = e.extend({
                    setup: function() {
                        this.text = document.createElement("span"), this.text.className = "jw-text jw-reset", this.img = document.createElement("div"), this.img.className = "jw-reset";
                        var a = document.createElement("div");
                        a.className = "jw-time-tip jw-background-color jw-reset", a.appendChild(this.img), a.appendChild(this.text), b.removeClass(this.el, "jw-hidden"), this.addContent(a)
                    },
                    image: function(a) {
                        b.style(this.img, a)
                    },
                    update: function(a) {
                        this.text.innerHTML = a
                    }
                }),
                i = d.extend({
                    constructor: function(b, c) {
                        this._model = b, this._api = c, this.timeTip = new h("jw-tooltip-time"), this.timeTip.setup(), this.cues = [], this.seekThrottled = a.throttle(this.performSeek, 400), this._model.on("change:playlistItem", this.onPlaylistItem, this).on("change:position", this.onPosition, this).on("change:duration", this.onDuration, this).on("change:buffer", this.onBuffer, this), d.call(this, "jw-slider-time", "horizontal")
                    },
                    setup: function() {
                        d.prototype.setup.apply(this, arguments), this._model.get("playlistItem") && this.onPlaylistItem(this._model, this._model.get("playlistItem")), this.elementRail.appendChild(this.timeTip.element()), this.el.addEventListener("mousemove", this.showTimeTooltip.bind(this), !1), this.el.addEventListener("mouseout", this.hideTimeTooltip.bind(this), !1)
                    },
                    limit: function(d) {
                        if (this.activeCue && a.isNumber(this.activeCue.pct)) return this.activeCue.pct;
                        var e = this._model.get("duration"),
                            f = b.adaptiveType(e);
                        if ("DVR" === f) {
                            var g = (1 - d / 100) * e,
                                h = this._model.get("position"),
                                i = Math.min(g, Math.max(c.dvrSeekLimit, h)),
                                j = 100 * i / e;
                            return 100 - j
                        }
                        return d
                    },
                    update: function(a) {
                        this.seekTo = a, this.seekThrottled(), d.prototype.update.apply(this, arguments)
                    },
                    dragStart: function() {
                        this._model.set("scrubbing", !0), d.prototype.dragStart.apply(this, arguments)
                    },
                    dragEnd: function() {
                        d.prototype.dragEnd.apply(this, arguments), this._model.set("scrubbing", !1)
                    },
                    onSeeked: function() {
                        this._model.get("scrubbing") && this.performSeek()
                    },
                    onBuffer: function(a, b) {
                        this.updateBuffer(b)
                    },
                    onPosition: function(a, b) {
                        this.updateTime(b, a.get("duration"))
                    },
                    onDuration: function(a, b) {
                        this.updateTime(a.get("position"), b)
                    },
                    updateTime: function(a, c) {
                        var d = 0;
                        if (c) {
                            var e = b.adaptiveType(c);
                            "DVR" === e ? d = (c - a) / c * 100 : "VOD" === e && (d = a / c * 100)
                        }
                        this.render(d)
                    },
                    onPlaylistItem: function(b, c) {
                        this.reset(), b.mediaModel.on("seeked", this.onSeeked, this);
                        var d = c.tracks;
                        a.each(d, function(a) {
                            a && a.kind && "thumbnails" === a.kind.toLowerCase() ? this.loadThumbnails(a.file) : a && a.kind && "chapters" === a.kind.toLowerCase() && this.loadChapters(a.file)
                        }, this)
                    },
                    performSeek: function() {
                        var a, c = this.seekTo,
                            d = this._model.get("duration"),
                            e = b.adaptiveType(d);
                        0 === d ? this._api.play() : "DVR" === e ? (a = (100 - c) / 100 * d, this._api.seek(a)) : (a = c / 100 * d, this._api.seek(Math.min(a, d - .25)))
                    },
                    showTimeTooltip: function(a) {
                        var d = this._model.get("duration");
                        if (0 !== d) {
                            var e = b.bounds(this.elementRail),
                                f = a.pageX ? a.pageX - e.left : a.x;
                            f = b.between(f, 0, e.width);
                            var g = f / e.width,
                                h = d * g;
                            0 > d && (h = d - h);
                            var i;
                            if (this.activeCue) i = this.activeCue.text;
                            else {
                                var j = !0;
                                i = b.timeFormat(h, j), 0 > d && h > c.dvrSeekLimit && (i = "Live")
                            }
                            this.timeTip.update(i), this.showThumbnail(h), b.addClass(this.timeTip.el, "jw-open"), this.timeTip.el.style.left = 100 * g + "%"
                        }
                    },
                    hideTimeTooltip: function() {
                        b.removeClass(this.timeTip.el, "jw-open")
                    },
                    reset: function() {
                        this.resetChapters(), this.resetThumbnails()
                    }
                });
            return a.extend(i.prototype, f, g), i
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(137), c(127), c(138), c(48)], e = function(a, b, c, d) {
            var e = a.extend({
                constructor: function(a, b) {
                    this.className = a + " jw-background-color jw-reset", this.orientation = b, this.dragStartListener = this.dragStart.bind(this), this.dragMoveListener = this.dragMove.bind(this), this.dragEndListener = this.dragEnd.bind(this), this.tapListener = this.tap.bind(this), this.setup()
                },
                setup: function() {
                    var a = {
                        "default": this["default"],
                        className: this.className,
                        orientation: "jw-slider-" + this.orientation
                    };
                    this.el = d.createElement(c(a)), this.elementRail = this.el.getElementsByClassName("jw-slider-container")[0], this.elementBuffer = this.el.getElementsByClassName("jw-buffer")[0], this.elementProgress = this.el.getElementsByClassName("jw-progress")[0], this.elementThumb = this.el.getElementsByClassName("jw-knob")[0], this.userInteract = new b(this.element(), {
                        preventScrolling: !0
                    }), this.userInteract.on("dragStart", this.dragStartListener), this.userInteract.on("drag", this.dragMoveListener), this.userInteract.on("dragEnd", this.dragEndListener), this.userInteract.on("tap click", this.tapListener)
                },
                dragStart: function() {
                    this.trigger("dragStart"), this.railBounds = d.bounds(this.elementRail)
                },
                dragEnd: function(a) {
                    this.dragMove(a), this.trigger("dragEnd")
                },
                dragMove: function(a) {
                    var b, c, e = this.railBounds = this.railBounds ? this.railBounds : d.bounds(this.elementRail);
                    "horizontal" === this.orientation ? (b = a.pageX, c = b < e.left ? 0 : b > e.right ? 100 : 100 * d.between((b - e.left) / e.width, 0, 1)) : (b = a.pageY, c = b >= e.bottom ? 0 : b <= e.top ? 100 : 100 * d.between((e.height - (b - e.top)) / e.height, 0, 1));
                    var f = this.limit(c);
                    return this.render(f), this.update(f), !1
                },
                tap: function(a) {
                    this.railBounds = d.bounds(this.elementRail), this.dragMove(a)
                },
                limit: function(a) {
                    return a
                },
                update: function(a) {
                    this.trigger("update", {
                        percentage: a
                    })
                },
                render: function(a) {
                    a = Math.max(0, Math.min(a, 100)), "horizontal" === this.orientation ? (this.elementThumb.style.left = a + "%", this.elementProgress.style.width = a + "%") : (this.elementThumb.style.bottom = a + "%", this.elementProgress.style.height = a + "%")
                },
                updateBuffer: function(a) {
                    this.elementBuffer.style.width = a + "%"
                },
                element: function() {
                    return this.el
                }
            });
            return e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(47), c(45)], e = function(a, b) {
            function c() {}
            var d = function(a, c) {
                var d, e = this;
                d = a && b.has(a, "constructor") ? a.constructor : function() {
                    return e.apply(this, arguments)
                }, b.extend(d, e, c);
                var f = function() {
                    this.constructor = d
                };
                return f.prototype = e.prototype, d.prototype = new f, a && b.extend(d.prototype, a), d.__super__ = e.prototype, d
            };
            return c.extend = d, b.extend(c.prototype, a), c
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d = c(120);
        a.exports = (d["default"] || d).template({
            compiler: [6, ">= 2.0.0-beta.1"],
            main: function(a, b, c, d) {
                var e, f = "function",
                    g = b.helperMissing,
                    h = this.escapeExpression;
                return '<div class="' + h((e = null != (e = b.className || (null != a ? a.className : a)) ? e : g, typeof e === f ? e.call(a, {
                    name: "className",
                    hash: {},
                    data: d
                }) : e)) + " " + h((e = null != (e = b.orientation || (null != a ? a.orientation : a)) ? e : g, typeof e === f ? e.call(a, {
                    name: "orientation",
                    hash: {},
                    data: d
                }) : e)) + ' jw-reset">\n    <div class="jw-slider-container jw-reset">\n        <div class="jw-rail jw-reset"></div>\n        <div class="jw-buffer jw-reset"></div>\n        <div class="jw-progress jw-reset"></div>\n        <div class="jw-knob jw-reset"></div>\n    </div>\n</div>'
            },
            useData: !0
        })
    }, function(a, b, c) {
        var d, e;
        d = [c(137), c(48)], e = function(a, b) {
            var c = a.extend({
                constructor: function(a) {
                    this.el = document.createElement("div"), this.el.className = "jw-icon jw-icon-tooltip " + a + " jw-button-color jw-reset jw-hidden", this.container = document.createElement("div"), this.container.className = "jw-overlay jw-reset", this.openClass = "jw-open", this.componentType = "tooltip", this.el.appendChild(this.container)
                },
                addContent: function(a) {
                    this.content && this.removeContent(), this.content = a, this.container.appendChild(a)
                },
                removeContent: function() {
                    this.content && (this.container.removeChild(this.content), this.content = null)
                },
                hasContent: function() {
                    return !!this.content
                },
                element: function() {
                    return this.el
                },
                openTooltip: function(a) {
                    this.trigger("open-" + this.componentType, a, {
                        isOpen: !0
                    }), this.isOpen = !0, b.toggleClass(this.el, this.openClass, this.isOpen)
                },
                closeTooltip: function(a) {
                    this.trigger("close-" + this.componentType, a, {
                        isOpen: !1
                    }), this.isOpen = !1, b.toggleClass(this.el, this.openClass, this.isOpen)
                },
                toggleOpenState: function(a) {
                    this.isOpen ? this.closeTooltip(a) : this.openTooltip(a)
                }
            });
            return c
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(48), c(112)], e = function(a, b, c) {
            function d(a, b) {
                this.time = a, this.text = b, this.el = document.createElement("div"), this.el.className = "jw-cue jw-reset"
            }
            a.extend(d.prototype, {
                align: function(a) {
                    if ("%" === this.time.toString().slice(-1)) this.pct = this.time;
                    else {
                        var b = this.time / a * 100;
                        this.pct = b + "%"
                    }
                    this.el.style.left = this.pct
                }
            });
            var e = {
                loadChapters: function(a) {
                    b.ajax(a, this.chaptersLoaded.bind(this), this.chaptersFailed, {
                        plainText: !0
                    })
                },
                chaptersLoaded: function(b) {
                    var d = c(b.responseText);
                    a.isArray(d) && (a.each(d, this.addCue, this), this.drawCues())
                },
                chaptersFailed: function() {},
                addCue: function(a) {
                    this.cues.push(new d(a.begin, a.text))
                },
                drawCues: function() {
                    var b = this._model.mediaModel.get("duration");
                    if (!b || 0 >= b) return void this._model.mediaModel.once("change:duration", this.drawCues, this);
                    var c = this;
                    a.each(this.cues, function(a) {
                        a.align(b), a.el.addEventListener("mouseover", function() {
                            c.activeCue = a
                        }), a.el.addEventListener("mouseout", function() {
                            c.activeCue = null
                        }), c.elementRail.appendChild(a.el)
                    })
                },
                resetChapters: function() {
                    a.each(this.cues, function(a) {
                        a.el.parentNode && a.el.parentNode.removeChild(a.el)
                    }, this), this.cues = []
                }
            };
            return e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(48), c(112)], e = function(a, b, c) {
            function d(a) {
                this.begin = a.begin, this.end = a.end, this.img = a.text
            }
            var e = {
                loadThumbnails: function(a) {
                    a && (this.vttPath = a.split("?")[0].split("/").slice(0, -1).join("/"), this.individualImage = null, b.ajax(a, this.thumbnailsLoaded.bind(this), this.thumbnailsFailed.bind(this), {
                        plainText: !0
                    }))
                },
                thumbnailsLoaded: function(b) {
                    var e = c(b.responseText);
                    a.isArray(e) && (a.each(e, function(a) {
                        this.thumbnails.push(new d(a))
                    }, this), this.drawCues())
                },
                thumbnailsFailed: function() {},
                chooseThumbnail: function(b) {
                    var c = a.sortedIndex(this.thumbnails, {
                        end: b
                    }, a.property("end"));
                    c >= this.thumbnails.length && (c = this.thumbnails.length - 1);
                    var d = this.thumbnails[c].img;
                    return d.indexOf("://") < 0 && (d = this.vttPath ? this.vttPath + "/" + d : d), d
                },
                loadThumbnail: function(b) {
                    var c = this.chooseThumbnail(b),
                        d = {
                            display: "block",
                            margin: "0 auto",
                            backgroundPosition: "0 0"
                        },
                        e = c.indexOf("#xywh");
                    if (e > 0) try {
                        var f = /(.+)\#xywh=(\d+),(\d+),(\d+),(\d+)/.exec(c);
                        c = f[1], d.backgroundPosition = -1 * f[2] + "px " + -1 * f[3] + "px", d.width = f[4], d.height = f[5]
                    } catch (g) {
                        return
                    } else this.individualImage || (this.individualImage = new Image, this.individualImage.onload = a.bind(function() {
                        this.individualImage.onload = null, this.timeTip.image({
                            width: this.individualImage.width,
                            height: this.individualImage.height
                        })
                    }, this), this.individualImage.src = c);
                    return d.backgroundImage = 'url("' + c + '")', d
                },
                showThumbnail: function(a) {
                    this.thumbnails.length < 1 || this.timeTip.image(this.loadThumbnail(a))
                },
                resetThumbnails: function() {
                    this.timeTip.image({
                        backgroundImage: "",
                        width: 0,
                        height: 0
                    }), this.thumbnails = []
                }
            };
            return e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(139), c(48), c(45), c(127), c(143)], e = function(a, b, c, d, e) {
            var f = a.extend({
                setup: function(a, f, g) {
                    this.iconUI || (this.iconUI = new d(this.el, {
                            useHover: !0,
                            directSelect: !0
                        }), this.toggleValueListener = this.toggleValue.bind(this), this.toggleOpenStateListener = this.toggleOpenState.bind(this), this.openTooltipListener = this.openTooltip.bind(this), this.closeTooltipListener = this.closeTooltip.bind(this),
                        this.selectListener = this.select.bind(this)), this.reset(), a = c.isArray(a) ? a : [], b.toggleClass(this.el, "jw-hidden", a.length < 2);
                    var h = a.length > 2 || 2 === a.length && g && g.toggle === !1,
                        i = !h && 2 === a.length;
                    if (b.toggleClass(this.el, "jw-toggle", i), b.toggleClass(this.el, "jw-button-color", !i), this.isActive = h || i, h) {
                        b.removeClass(this.el, "jw-off"), this.iconUI.on("tap", this.toggleOpenStateListener).on("over", this.openTooltipListener).on("out", this.closeTooltipListener);
                        var j = e(a),
                            k = b.createElement(j);
                        this.addContent(k), this.contentUI = new d(this.content).on("click tap", this.selectListener)
                    } else i && this.iconUI.on("click tap", this.toggleValueListener);
                    this.selectItem(f)
                },
                toggleValue: function() {
                    this.trigger("toggleValue")
                },
                select: function(a) {
                    if (a.target.parentElement === this.content) {
                        var d = b.classList(a.target),
                            e = c.find(d, function(a) {
                                return 0 === a.indexOf("jw-item")
                            });
                        e && (this.trigger("select", parseInt(e.split("-")[2])), this.closeTooltipListener())
                    }
                },
                selectItem: function(a) {
                    if (this.content)
                        for (var c = 0; c < this.content.children.length; c++) b.toggleClass(this.content.children[c], "jw-active-option", a === c);
                    else b.toggleClass(this.el, "jw-off", 0 === a)
                },
                reset: function() {
                    b.addClass(this.el, "jw-off"), this.iconUI.off(), this.contentUI && this.contentUI.off().destroy(), this.removeContent()
                }
            });
            return f
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d = c(120);
        a.exports = (d["default"] || d).template({
            1: function(a, b, c, d) {
                var e = this.lambda,
                    f = this.escapeExpression;
                return "        <li class='jw-text jw-option jw-item-" + f(e(d && d.index, a)) + " jw-reset'>" + f(e(null != a ? a.label : a, a)) + "</li>\n"
            },
            compiler: [6, ">= 2.0.0-beta.1"],
            main: function(a, b, c, d) {
                var e, f = '<ul class="jw-menu jw-background-color jw-reset">\n';
                return e = b.each.call(a, a, {
                    name: "each",
                    hash: {},
                    fn: this.program(1, d),
                    inverse: this.noop,
                    data: d
                }), null != e && (f += e), f + "</ul>"
            },
            useData: !0
        })
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(45), c(139), c(127), c(145)], e = function(a, b, c, d, e) {
            var f = c.extend({
                setup: function(c, e) {
                    if (this.iconUI || (this.iconUI = new d(this.el, {
                            useHover: !0
                        }), this.toggleOpenStateListener = this.toggleOpenState.bind(this), this.openTooltipListener = this.openTooltip.bind(this), this.closeTooltipListener = this.closeTooltip.bind(this), this.selectListener = this.onSelect.bind(this)), this.reset(), c = b.isArray(c) ? c : [], a.toggleClass(this.el, "jw-hidden", c.length < 2), c.length >= 2) {
                        this.iconUI = new d(this.el, {
                            useHover: !0
                        }).on("tap", this.toggleOpenStateListener).on("over", this.openTooltipListener).on("out", this.closeTooltipListener);
                        var f = this.menuTemplate(c, e),
                            g = a.createElement(f);
                        this.addContent(g), this.contentUI = new d(this.content), this.contentUI.on("click tap", this.selectListener)
                    }
                    this.originalList = c
                },
                menuTemplate: function(a, c) {
                    var d = b.map(a, function(a, b) {
                        return {
                            active: b === c,
                            label: b + 1 + ".",
                            title: a.title
                        }
                    });
                    return e(d)
                },
                onSelect: function(c) {
                    var d = c.target;
                    if ("UL" !== d.tagName) {
                        "LI" !== d.tagName && (d = d.parentElement);
                        var e = a.classList(d),
                            f = b.find(e, function(a) {
                                return 0 === a.indexOf("jw-item")
                            });
                        f && (this.trigger("select", parseInt(f.split("-")[2])), this.closeTooltip())
                    }
                },
                selectItem: function(a) {
                    this.setup(this.originalList, a)
                },
                reset: function() {
                    this.iconUI.off(), this.contentUI && this.contentUI.off().destroy(), this.removeContent()
                }
            });
            return f
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d = c(120);
        a.exports = (d["default"] || d).template({
            1: function(a, b, c, d) {
                var e, f = "";
                return e = b["if"].call(a, null != a ? a.active : a, {
                    name: "if",
                    hash: {},
                    fn: this.program(2, d),
                    inverse: this.program(4, d),
                    data: d
                }), null != e && (f += e), f
            },
            2: function(a, b, c, d) {
                var e = this.lambda,
                    f = this.escapeExpression;
                return "                <li class='jw-option jw-text jw-active-option jw-item-" + f(e(d && d.index, a)) + ' jw-reset\'>\n                    <span class="jw-label jw-reset"><span class="jw-icon jw-icon-play jw-reset"></span></span>\n                    <span class="jw-name jw-reset">' + f(e(null != a ? a.title : a, a)) + "</span>\n                </li>\n"
            },
            4: function(a, b, c, d) {
                var e = this.lambda,
                    f = this.escapeExpression;
                return "                <li class='jw-option jw-text jw-item-" + f(e(d && d.index, a)) + ' jw-reset\'>\n                    <span class="jw-label jw-reset">' + f(e(null != a ? a.label : a, a)) + '</span>\n                    <span class="jw-name jw-reset">' + f(e(null != a ? a.title : a, a)) + "</span>\n                </li>\n"
            },
            compiler: [6, ">= 2.0.0-beta.1"],
            main: function(a, b, c, d) {
                var e, f = '<div class="jw-menu jw-playlist-container jw-background-color jw-reset">\n\n    <div class="jw-tooltip-title jw-reset">\n        <span class="jw-icon jw-icon-inline jw-icon-playlist jw-reset"></span>\n        <span class="jw-text jw-reset">PLAYLIST</span>\n    </div>\n\n    <ul class="jw-playlist jw-reset">\n';
                return e = b.each.call(a, a, {
                    name: "each",
                    hash: {},
                    fn: this.program(1, d),
                    inverse: this.noop,
                    data: d
                }), null != e && (f += e), f + "    </ul>\n</div>"
            },
            useData: !0
        })
    }, function(a, b, c) {
        var d, e;
        d = [c(139), c(136), c(127), c(48)], e = function(a, b, c, d) {
            var e = a.extend({
                constructor: function(e, f) {
                    this._model = e, a.call(this, f), this.volumeSlider = new b("jw-slider-volume jw-volume-tip", "vertical"), this.addContent(this.volumeSlider.element()), this.volumeSlider.on("update", function(a) {
                        this.trigger("update", a)
                    }, this), d.removeClass(this.el, "jw-hidden"), new c(this.el, {
                        useHover: !0,
                        directSelect: !0
                    }).on("click", this.toggleValue, this).on("tap", this.toggleOpenState, this).on("over", this.openTooltip, this).on("out", this.closeTooltip, this), this._model.on("change:volume", this.onVolume, this)
                },
                toggleValue: function() {
                    this.trigger("toggleValue")
                }
            });
            return e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(139), c(48), c(45), c(127)], e = function(a, b, c, d) {
            var e = a.extend({
                constructor: function(b) {
                    a.call(this, b), this.container.className = "jw-overlay-horizontal jw-reset", this.openClass = "jw-open-drawer", this.componentType = "drawer"
                },
                setup: function(a, e) {
                    this.iconUI || (this.iconUI = new d(this.el, {
                        useHover: !0,
                        directSelect: !0
                    }), this.toggleOpenStateListener = this.toggleOpenState.bind(this), this.openTooltipListener = this.openTooltip.bind(this), this.closeTooltipListener = this.closeTooltip.bind(this)), this.reset(), a = c.isArray(a) ? a : [], this.activeContents = c.filter(a, function(a) {
                        return a.isActive
                    }), b.toggleClass(this.el, "jw-hidden", !e || this.activeContents.length < 2), e && this.activeContents.length > 1 && (b.removeClass(this.el, "jw-off"), this.iconUI.on("tap", this.toggleOpenStateListener).on("over", this.openTooltipListener).on("out", this.closeTooltipListener), c.each(a, function(a) {
                        this.container.appendChild(a.el)
                    }, this))
                },
                reset: function() {
                    b.addClass(this.el, "jw-off"), this.iconUI.off(), this.contentUI && this.contentUI.off().destroy(), this.removeContent()
                }
            });
            return e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(48)], e = function(a, b) {
            function c(a, b) {
                b.off("change:mediaType", null, this), b.on("change:mediaType", function(b, c) {
                    "audio" === c && this.setImage(a.get("playlistItem").image)
                }, this)
            }

            function d(a, c) {
                var d = a.get("autostart") && !b.isMobile() || a.get("item") > 0;
                return d ? (this.setImage(null), a.off("change:state", null, this), void a.on("change:state", function(a, b) {
                    "complete" !== b && "idle" !== b && "error" !== b || this.setImage(c.image)
                }, this)) : void this.setImage(c.image)
            }
            var e = function(a) {
                this.model = a, a.on("change:playlistItem", d, this), a.on("change:mediaModel", c, this)
            };
            return a.extend(e.prototype, {
                setup: function(a) {
                    this.el = a;
                    var b = this.model.get("playlistItem");
                    b && this.setImage(b.image)
                },
                setImage: function(c) {
                    this.model.off("change:state", null, this);
                    var d = "";
                    a.isString(c) && (d = 'url("' + c + '")'), b.style(this.el, {
                        backgroundImage: d
                    })
                },
                element: function() {
                    return this.el
                }
            }), e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(150), c(45), c(59)], e = function(a, b, c) {
            var d = {
                    free: "f",
                    premium: "r",
                    enterprise: "e",
                    ads: "a",
                    unlimited: "u",
                    trial: "t"
                },
                e = function() {};
            return b.extend(e.prototype, a.prototype, {
                buildArray: function() {
                    var b = a.prototype.buildArray.apply(this, arguments),
                        e = this.model.get("edition"),
                        f = "https://jwplayer.com/learn-more/?m=h&e=" + (d[e] || e) + "&v=" + c;
                    if (b.items[0].link = f, this.model.get("abouttext")) {
                        b.items[0].showLogo = !1, b.items.push(b.items.shift());
                        var g = {
                            title: this.model.get("abouttext"),
                            link: this.model.get("aboutlink") || f
                        };
                        b.items.unshift(g)
                    }
                    return b
                }
            }), e
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(151), c(45), c(127), c(59)], e = function(a, b, c, d, e) {
            var f = function() {};
            return c.extend(f.prototype, {
                buildArray: function() {
                    var b = e.split("+"),
                        c = b[0],
                        d = {
                            items: [{
                                title: "Powered by JW Player " + c,
                                featured: !0,
                                showLogo: !0,
                                link: "https://jwplayer.com/learn-more/?m=h&e=o&v=" + e
                            }]
                        },
                        f = c.indexOf("-") > 0,
                        g = b[1];
                    if (f && g) {
                        var h = g.split(".");
                        d.items.push({
                            title: "build: (" + h[0] + "." + h[1] + ")",
                            link: "#"
                        })
                    }
                    var i = this.model.get("provider").name;
                    if (i.indexOf("flash") >= 0) {
                        var j = "Flash Version " + a.flashVersion();
                        d.items.push({
                            title: j,
                            link: "http://www.adobe.com/software/flash/about/"
                        })
                    }
                    return d
                },
                buildMenu: function() {
                    var c = this.buildArray();
                    return a.createElement(b(c))
                },
                updateHtml: function() {
                    this.el.innerHTML = this.buildMenu().innerHTML
                },
                rightClick: function(a) {
                    return this.lazySetup(), this.mouseOverContext ? !1 : (this.hideMenu(), this.showMenu(a), !1)
                },
                getOffset: function(a) {
                    for (var b = a.target, c = a.offsetX || a.layerX, d = a.offsetY || a.layerY; b !== this.playerElement;) c += b.offsetLeft, d += b.offsetTop, b = b.parentNode;
                    return {
                        x: c,
                        y: d
                    }
                },
                showMenu: function(b) {
                    var c = this.getOffset(b);
                    return this.el.style.left = c.x + "px", this.el.style.top = c.y + "px", a.addClass(this.playerElement, "jw-flag-rightclick-open"), a.addClass(this.el, "jw-open"), !1
                },
                hideMenu: function() {
                    this.mouseOverContext || (a.removeClass(this.playerElement, "jw-flag-rightclick-open"), a.removeClass(this.el, "jw-open"))
                },
                lazySetup: function() {
                    this.el || (this.el = this.buildMenu(), this.layer.appendChild(this.el), this.hideMenuHandler = this.hideMenu.bind(this), this.addOffListener(this.playerElement), this.addOffListener(document), this.model.on("change:provider", this.updateHtml, this), this.elementUI = new d(this.el, {
                        useHover: !0
                    }).on("over", function() {
                        this.mouseOverContext = !0
                    }, this).on("out", function() {
                        this.mouseOverContext = !1
                    }, this))
                },
                setup: function(a, b, c) {
                    this.playerElement = b, this.model = a, this.mouseOverContext = !1, this.layer = c, b.oncontextmenu = this.rightClick.bind(this)
                },
                addOffListener: function(a) {
                    a.addEventListener("mousedown", this.hideMenuHandler), a.addEventListener("touchstart", this.hideMenuHandler), a.addEventListener("pointerdown", this.hideMenuHandler)
                },
                removeOffListener: function(a) {
                    a.removeEventListener("mousedown", this.hideMenuHandler), a.removeEventListener("touchstart", this.hideMenuHandler), a.removeEventListener("pointerdown", this.hideMenuHandler)
                },
                destroy: function() {
                    this.el && (this.hideMenu(), this.elementUI.off(), this.removeOffListener(this.playerElement), this.removeOffListener(document), this.hideMenuHandler = null, this.el = null), this.playerElement && (this.playerElement.oncontextmenu = null, this.playerElement = null), this.model && (this.model.off("change:provider", this.updateHtml), this.model = null)
                }
            }), f
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d = c(120);
        a.exports = (d["default"] || d).template({
            1: function(a, b, c, d) {
                var e, f, g = "function",
                    h = b.helperMissing,
                    i = this.escapeExpression,
                    j = '        <li class="jw-reset';
                return e = b["if"].call(a, null != a ? a.featured : a, {
                    name: "if",
                    hash: {},
                    fn: this.program(2, d),
                    inverse: this.noop,
                    data: d
                }), null != e && (j += e), j += '">\n            <a href="' + i((f = null != (f = b.link || (null != a ? a.link : a)) ? f : h, typeof f === g ? f.call(a, {
                    name: "link",
                    hash: {},
                    data: d
                }) : f)) + '" class="jw-reset" target="_top">\n', e = b["if"].call(a, null != a ? a.showLogo : a, {
                    name: "if",
                    hash: {},
                    fn: this.program(4, d),
                    inverse: this.noop,
                    data: d
                }), null != e && (j += e), j + "                " + i((f = null != (f = b.title || (null != a ? a.title : a)) ? f : h, typeof f === g ? f.call(a, {
                    name: "title",
                    hash: {},
                    data: d
                }) : f)) + "\n            </a>\n        </li>\n"
            },
            2: function(a, b, c, d) {
                return " jw-featured"
            },
            4: function(a, b, c, d) {
                return '                <span class="jw-icon jw-rightclick-logo jw-reset"></span>\n'
            },
            compiler: [6, ">= 2.0.0-beta.1"],
            main: function(a, b, c, d) {
                var e, f = '<div class="jw-rightclick jw-reset">\n    <ul class="jw-reset">\n';
                return e = b.each.call(a, null != a ? a.items : a, {
                    name: "each",
                    hash: {},
                    fn: this.program(1, d),
                    inverse: this.noop,
                    data: d
                }), null != e && (f += e), f + "    </ul>\n</div>"
            },
            useData: !0
        })
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(48)], e = function(a, b) {
            var c = function(a) {
                this.model = a, this.model.on("change:playlistItem", this.playlistItem, this)
            };
            return a.extend(c.prototype, {
                hide: function() {
                    this.el.style.display = "none"
                },
                show: function() {
                    this.el.style.display = ""
                },
                setup: function(a) {
                    this.el = a;
                    var b = this.el.getElementsByTagName("div");
                    this.title = b[0], this.description = b[1], this.model.get("playlistItem") && this.playlistItem(this.model, this.model.get("playlistItem")), this.model.on("change:logoWidth", this.update, this), this.model.on("change:dock", this.update, this)
                },
                update: function(a) {
                    var c = {
                            paddingLeft: 0,
                            paddingRight: 0
                        },
                        d = a.get("controls"),
                        e = a.get("dock"),
                        f = a.get("logo");
                    if (f) {
                        var g = 1 * ("" + f.margin).replace("px", ""),
                            h = a.get("logoWidth") + (isNaN(g) ? 0 : g);
                        "top-left" === f.position ? c.paddingLeft = h : "top-right" === f.position && (c.paddingRight = h)
                    }
                    if (d && e && e.length) {
                        var i = 56 * e.length;
                        c.paddingRight = Math.max(c.paddingRight, i)
                    }
                    b.style(this.el, c)
                },
                playlistItem: function(a, b) {
                    if (a.get("displaytitle") || a.get("displaydescription")) {
                        var c = "",
                            d = "";
                        b.title && a.get("displaytitle") && (c = b.title), b.description && a.get("displaydescription") && (d = b.description), this.updateText(c, d)
                    } else this.hide()
                },
                updateText: function(a, b) {
                    this.title.innerHTML = a, this.description.innerHTML = b, this.title.firstChild || this.description.firstChild ? this.show() : this.hide()
                },
                element: function() {
                    return this.el
                }
            }), c
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d = c(120);
        a.exports = (d["default"] || d).template({
            compiler: [6, ">= 2.0.0-beta.1"],
            main: function(a, b, c, d) {
                var e, f = "function",
                    g = b.helperMissing,
                    h = this.escapeExpression;
                return '<div id="' + h((e = null != (e = b.id || (null != a ? a.id : a)) ? e : g, typeof e === f ? e.call(a, {
                    name: "id",
                    hash: {},
                    data: d
                }) : e)) + '" class="jwplayer jw-reset" tabindex="0">\n    <div class="jw-aspect jw-reset"></div>\n    <div class="jw-media jw-reset"></div>\n    <div class="jw-preview jw-reset"></div>\n    <div class="jw-title jw-reset">\n        <div class="jw-title-primary jw-reset"></div>\n        <div class="jw-title-secondary jw-reset"></div>\n    </div>\n    <div class="jw-overlays jw-reset"></div>\n    <div class="jw-controls jw-reset"></div>\n</div>'
            },
            useData: !0
        })
    }, function(a, b, c) {
        var d, e;
        d = [c(48), c(46), c(127), c(47), c(45), c(155)], e = function(a, b, c, d, e, f) {
            var g = function(a) {
                this.model = a, this.setup()
            };
            return e.extend(g.prototype, d, {
                setup: function() {
                    this.destroy(), this.skipMessage = this.model.get("skipText"), this.skipMessageCountdown = this.model.get("skipMessage"), this.setWaitTime(this.model.get("skipOffset"));
                    var b = f();
                    this.el = a.createElement(b), this.skiptext = this.el.getElementsByClassName("jw-skiptext")[0], this.skipAdOnce = e.once(this.skipAd.bind(this)), new c(this.el).on("click tap", e.bind(function() {
                        this.skippable && this.skipAdOnce()
                    }, this)), this.model.on("change:duration", this.onChangeDuration, this), this.model.on("change:position", this.onChangePosition, this), this.onChangeDuration(this.model, this.model.get("duration")), this.onChangePosition(this.model, this.model.get("position"))
                },
                updateMessage: function(a) {
                    this.skiptext.innerHTML = a
                },
                updateCountdown: function(a) {
                    this.updateMessage(this.skipMessageCountdown.replace(/xx/gi, Math.ceil(this.waitTime - a)))
                },
                onChangeDuration: function(b, c) {
                    if (c) {
                        if (this.waitPercentage) {
                            if (!c) return;
                            this.itemDuration = c, this.setWaitTime(this.waitPercentage), delete this.waitPercentage
                        }
                        a.removeClass(this.el, "jw-hidden")
                    }
                },
                onChangePosition: function(b, c) {
                    this.waitTime - c > 0 ? this.updateCountdown(c) : (this.updateMessage(this.skipMessage), this.skippable = !0, a.addClass(this.el, "jw-skippable"))
                },
                element: function() {
                    return this.el
                },
                setWaitTime: function(b) {
                    if (e.isString(b) && "%" === b.slice(-1)) {
                        var c = parseFloat(b);
                        return void(this.itemDuration && !isNaN(c) ? this.waitTime = this.itemDuration * c / 100 : this.waitPercentage = b)
                    }
                    e.isNumber(b) ? this.waitTime = b : "string" === a.typeOf(b) ? this.waitTime = a.seconds(b) : isNaN(Number(b)) ? this.waitTime = 0 : this.waitTime = Number(b)
                },
                skipAd: function() {
                    this.trigger(b.JWPLAYER_AD_SKIPPED)
                },
                destroy: function() {
                    this.el && (this.el.removeEventListener("click", this.skipAdOnce), this.el.parentElement && this.el.parentElement.removeChild(this.el)), delete this.skippable, delete this.itemDuration, delete this.waitPercentage
                }
            }), g
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d = c(120);
        a.exports = (d["default"] || d).template({
            compiler: [6, ">= 2.0.0-beta.1"],
            main: function(a, b, c, d) {
                return '<div class="jw-skip jw-background-color jw-hidden jw-reset">\n    <span class="jw-text jw-skiptext jw-reset"></span>\n    <span class="jw-icon-inline jw-skip-icon jw-reset"></span>\n</div>'
            },
            useData: !0
        })
    }, function(a, b, c) {
        var d, e;
        d = [c(157)], e = function(a) {
            function b(b, c, d, e) {
                return a({
                    id: b,
                    skin: c,
                    title: d,
                    body: e
                })
            }
            return b
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d = c(120);
        a.exports = (d["default"] || d).template({
            compiler: [6, ">= 2.0.0-beta.1"],
            main: function(a, b, c, d) {
                var e, f = "function",
                    g = b.helperMissing,
                    h = this.escapeExpression;
                return '<div id="' + h((e = null != (e = b.id || (null != a ? a.id : a)) ? e : g, typeof e === f ? e.call(a, {
                    name: "id",
                    hash: {},
                    data: d
                }) : e)) + '"class="jw-skin-' + h((e = null != (e = b.skin || (null != a ? a.skin : a)) ? e : g, typeof e === f ? e.call(a, {
                    name: "skin",
                    hash: {},
                    data: d
                }) : e)) + ' jw-error jw-reset">\n    <div class="jw-title jw-reset">\n        <div class="jw-title-primary jw-reset">' + h((e = null != (e = b.title || (null != a ? a.title : a)) ? e : g, typeof e === f ? e.call(a, {
                    name: "title",
                    hash: {},
                    data: d
                }) : e)) + '</div>\n        <div class="jw-title-secondary jw-reset">' + h((e = null != (e = b.body || (null != a ? a.body : a)) ? e : g, typeof e === f ? e.call(a, {
                    name: "body",
                    hash: {},
                    data: d
                }) : e)) + '</div>\n    </div>\n\n    <div class="jw-icon-container jw-reset">\n        <div class="jw-display-icon-container jw-background-color jw-reset">\n            <div class="jw-icon jw-icon-display jw-reset"></div>\n        </div>\n    </div>\n</div>\n'
            },
            useData: !0
        })
    }, function(a, b, c) {
        var d, e;
        d = [], e = function() {
            function a() {
                var a = document.createElement("div");
                return a.className = c, a.innerHTML = "&nbsp;", a.style.width = "1px", a.style.height = "1px", a.style.position = "absolute", a.style.background = "transparent", a
            }

            function b() {
                function b() {
                    var a = this,
                        b = a._view.element();
                    b.appendChild(f), d() && a.trigger("adBlock")
                }

                function d() {
                    return e ? !0 : ("" !== f.innerHTML && f.className === c && null !== f.offsetParent && 0 !== f.clientHeight || (e = !0), e)
                }
                var e = !1,
                    f = a();
                return {
                    onReady: b,
                    checkAdBlock: d
                }
            }
            var c = "afs_ads";
            return {
                setup: b
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, , , , function(a, b, c) {
        var d, e;
        d = [], e = function() {
            var a = window.chrome,
                b = {};
            return b.NS = "urn:x-cast:com.longtailvideo.jwplayer", b.debug = !1, b.availability = null, b.available = function(c) {
                c = c || b.availability;
                var d = "available";
                return a && a.cast && a.cast.ReceiverAvailability && (d = a.cast.ReceiverAvailability.AVAILABLE), c === d
            }, b.log = function() {
                if (b.debug) {
                    var a = Array.prototype.slice.call(arguments, 0);
                    console.log.apply(console, a)
                }
            }, b.error = function() {
                var a = Array.prototype.slice.call(arguments, 0);
                console.error.apply(console, a)
            }, b
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, , , function(a, b, c) {
        var d, e;
        d = [c(98), c(45)], e = function(a, b) {
            return function(c, d) {
                var e = ["seek", "skipAd", "stop", "playlistNext", "playlistPrev", "playlistItem", "resize", "addButton", "removeButton", "registerPlugin", "attachMedia"];
                b.each(e, function(a) {
                    c[a] = function() {
                        return d[a].apply(d, arguments), c
                    }
                }), c.registerPlugin = a.registerPlugin
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45)], e = function(a) {
            return function(b, c) {
                var d = ["buffer", "controls", "position", "duration", "fullscreen", "volume", "mute", "item", "stretching", "playlist"];
                a.each(d, function(a) {
                    var d = a.slice(0, 1).toUpperCase() + a.slice(1);
                    b["get" + d] = function() {
                        return c._model.get(a)
                    }
                });
                var e = ["getAudioTracks", "getCaptionsList", "getWidth", "getHeight", "getCurrentAudioTrack", "setCurrentAudioTrack", "getCurrentCaptions", "setCurrentCaptions", "getCurrentQuality", "setCurrentQuality", "getQualityLevels", "getVisualQuality", "getConfig", "getState", "getSafeRegion", "isBeforeComplete", "isBeforePlay", "getProvider", "detachMedia"],
                    f = ["setControls", "setFullscreen", "setVolume", "setMute", "setCues"];
                a.each(e, function(a) {
                    b[a] = function() {
                        return c[a] ? c[a].apply(c, arguments) : null
                    }
                }), a.each(f, function(a) {
                    b[a] = function() {
                        return c[a].apply(c, arguments), b
                    }
                })
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d, e;
        d = [c(45), c(46)], e = function(a, b) {
            return function(c) {
                var d = {
                        onBufferChange: b.JWPLAYER_MEDIA_BUFFER,
                        onBufferFull: b.JWPLAYER_MEDIA_BUFFER_FULL,
                        onError: b.JWPLAYER_ERROR,
                        onSetupError: b.JWPLAYER_SETUP_ERROR,
                        onFullscreen: b.JWPLAYER_FULLSCREEN,
                        onMeta: b.JWPLAYER_MEDIA_META,
                        onMute: b.JWPLAYER_MEDIA_MUTE,
                        onPlaylist: b.JWPLAYER_PLAYLIST_LOADED,
                        onPlaylistItem: b.JWPLAYER_PLAYLIST_ITEM,
                        onPlaylistComplete: b.JWPLAYER_PLAYLIST_COMPLETE,
                        onReady: b.JWPLAYER_READY,
                        onResize: b.JWPLAYER_RESIZE,
                        onComplete: b.JWPLAYER_MEDIA_COMPLETE,
                        onSeek: b.JWPLAYER_MEDIA_SEEK,
                        onTime: b.JWPLAYER_MEDIA_TIME,
                        onVolume: b.JWPLAYER_MEDIA_VOLUME,
                        onBeforePlay: b.JWPLAYER_MEDIA_BEFOREPLAY,
                        onBeforeComplete: b.JWPLAYER_MEDIA_BEFORECOMPLETE,
                        onDisplayClick: b.JWPLAYER_DISPLAY_CLICK,
                        onControls: b.JWPLAYER_CONTROLS,
                        onQualityLevels: b.JWPLAYER_MEDIA_LEVELS,
                        onQualityChange: b.JWPLAYER_MEDIA_LEVEL_CHANGED,
                        onCaptionsList: b.JWPLAYER_CAPTIONS_LIST,
                        onCaptionsChange: b.JWPLAYER_CAPTIONS_CHANGED,
                        onAdError: b.JWPLAYER_AD_ERROR,
                        onAdClick: b.JWPLAYER_AD_CLICK,
                        onAdImpression: b.JWPLAYER_AD_IMPRESSION,
                        onAdTime: b.JWPLAYER_AD_TIME,
                        onAdComplete: b.JWPLAYER_AD_COMPLETE,
                        onAdCompanions: b.JWPLAYER_AD_COMPANIONS,
                        onAdSkipped: b.JWPLAYER_AD_SKIPPED,
                        onAdPlay: b.JWPLAYER_AD_PLAY,
                        onAdPause: b.JWPLAYER_AD_PAUSE,
                        onAdMeta: b.JWPLAYER_AD_META,
                        onCast: b.JWPLAYER_CAST_SESSION,
                        onAudioTrackChange: b.JWPLAYER_AUDIO_TRACK_CHANGED,
                        onAudioTracks: b.JWPLAYER_AUDIO_TRACKS
                    },
                    e = {
                        onBuffer: "buffer",
                        onPause: "pause",
                        onPlay: "play",
                        onIdle: "idle"
                    };
                a.each(e, function(b, d) {
                    c[d] = a.partial(c.on, b, a)
                }), a.each(d, function(b, d) {
                    c[d] = a.partial(c.on, b, a)
                })
            }
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }, function(a, b, c) {
        var d = c(169);
        "string" == typeof d && (d = [
            [a.id, d, ""]
        ]);
        c(173)(d, {});
        d.locals && (a.exports = d.locals)
    }, function(a, b, c) {
        b = a.exports = c(170)(), b.push([a.id, ".jw-reset{color:inherit;background-color:transparent;padding:0;margin:0;float:none;font-family:Arial,Helvetica,sans-serif;font-size:1em;line-height:1em;list-style:none;text-align:left;text-transform:none;vertical-align:baseline;border:0;direction:ltr;font-variant:inherit;font-stretch:inherit;-webkit-tap-highlight-color:rgba(255,255,255,0)}@font-face{font-family:'jw-icons';src:url(" + c(171) + ") format('woff'),url(" + c(172) + ') format(\'truetype\');font-weight:normal;font-style:normal}.jw-icon-inline,.jw-icon-tooltip,.jw-icon-display,.jw-controlbar .jw-menu .jw-option:before{font-family:\'jw-icons\';-webkit-font-smoothing:antialiased;font-style:normal;font-weight:normal;text-transform:none;background-color:transparent;font-variant:normal;-webkit-font-feature-settings:"liga";-ms-font-feature-settings:"liga" 1;-o-font-feature-settings:"liga";font-feature-settings:"liga";-moz-osx-font-smoothing:grayscale}.jw-icon-audio-tracks:before{content:"\\E600"}.jw-icon-buffer:before{content:"\\E601"}.jw-icon-cast:before{content:"\\E603"}.jw-icon-cast.jw-off:before{content:"\\E602"}.jw-icon-cc:before{content:"\\E605"}.jw-icon-cue:before{content:"\\E606"}.jw-icon-menu-bullet:before{content:"\\E606"}.jw-icon-error:before{content:"\\E607"}.jw-icon-fullscreen:before{content:"\\E608"}.jw-icon-fullscreen.jw-off:before{content:"\\E613"}.jw-icon-hd:before{content:"\\E60A"}.jw-watermark:before,.jw-rightclick-logo:before{content:"\\E60B"}.jw-icon-next:before{content:"\\E60C"}.jw-icon-pause:before{content:"\\E60D"}.jw-icon-play:before{content:"\\E60E"}.jw-icon-prev:before{content:"\\E60F"}.jw-icon-replay:before{content:"\\E610"}.jw-icon-volume:before{content:"\\E612"}.jw-icon-volume.jw-off:before{content:"\\E611"}.jw-icon-more:before{content:"\\E614"}.jw-icon-close:before{content:"\\E615"}.jw-icon-playlist:before{content:"\\E616"}.jwplayer{width:100%;font-size:16px;position:relative;display:block;min-height:0;overflow:hidden;box-sizing:border-box;font-family:Arial,Helvetica,sans-serif;background-color:#000;-webkit-touch-callout:none;-webkit-user-select:none;-khtml-user-select:none;-moz-user-select:none;-ms-user-select:none;user-select:none}.jwplayer *{box-sizing:inherit}.jwplayer.jw-flag-aspect-mode{height:auto !important}.jwplayer.jw-flag-aspect-mode .jw-aspect{display:block}.jwplayer .jw-aspect{display:none}.jwplayer.jw-no-focus:focus,.jwplayer .jw-swf{outline:none}.jwplayer.jw-ie:focus{outline:#585858 dotted 1px}.jwplayer:hover .jw-display-icon-container{background-color:#333;background:#333;background-size:#333}.jw-media,.jw-preview,.jw-overlays,.jw-controls{position:absolute;width:100%;height:100%;top:0;left:0;bottom:0;right:0}.jw-media{overflow:hidden;cursor:pointer}.jw-overlays{cursor:auto}.jw-media.jw-media-show{visibility:visible;opacity:1}.jw-controls.jw-controls-disabled{display:none}.jw-controls .jw-controls-right{position:absolute;top:0;right:0;left:0;bottom:2em}.jw-text{height:1em;font-family:Arial,Helvetica,sans-serif;font-size:.75em;font-style:normal;font-weight:normal;color:white;text-align:center;font-variant:normal;font-stretch:normal}.jw-plugin{position:absolute;bottom:2.5em}.jw-plugin .jw-banner{max-width:100%;opacity:0;cursor:pointer;position:absolute;margin:auto auto 0 auto;left:0;right:0;bottom:0;display:block}.jw-cast-screen{width:100%;height:100%}.jw-instream{position:absolute;top:0;right:0;bottom:0;left:0;display:none}.jw-icon-playback:before{content:"\\E60E"}.jw-preview,.jw-captions,.jw-title,.jw-overlays,.jw-controls{pointer-events:none}.jw-overlays>div,.jw-media,.jw-controlbar,.jw-dock,.jw-logo,.jw-skip,.jw-display-icon-container{pointer-events:all}.jwplayer video{position:absolute;top:0;right:0;bottom:0;left:0;width:100%;height:100%;margin:auto;background:transparent}.jwplayer video::-webkit-media-controls-start-playback-button{display:none}.jwplayer video::-webkit-media-text-track-display{-webkit-transform:translateY(-1.5em);transform:translateY(-1.5em)}.jwplayer.jw-flag-user-inactive.jw-state-playing video::-webkit-media-text-track-display{-webkit-transform:translateY(0);transform:translateY(0)}.jwplayer.jw-stretch-uniform video{-o-object-fit:contain;object-fit:contain}.jwplayer.jw-stretch-none video{-o-object-fit:none;object-fit:none}.jwplayer.jw-stretch-fill video{-o-object-fit:cover;object-fit:cover}.jwplayer.jw-stretch-exactfit video{-o-object-fit:fill;object-fit:fill}.jw-click{position:absolute;width:100%;height:100%}.jw-preview{position:absolute;display:none;opacity:1;visibility:visible;width:100%;height:100%;background:#000 no-repeat 50% 50%}.jwplayer .jw-preview,.jw-error .jw-preview,.jw-stretch-uniform .jw-preview{background-size:contain}.jw-stretch-none .jw-preview{background-size:auto auto}.jw-stretch-fill .jw-preview{background-size:cover}.jw-stretch-exactfit .jw-preview{background-size:100% 100%}.jw-display-icon-container{position:relative;top:50%;display:table;height:3.5em;width:3.5em;margin:-1.75em auto 0;cursor:pointer}.jw-display-icon-container .jw-icon-display{position:relative;display:table-cell;text-align:center;vertical-align:middle !important;background-position:50% 50%;background-repeat:no-repeat;font-size:2em}.jw-flag-audio-player .jw-display-icon-container,.jw-flag-dragging .jw-display-icon-container{display:none}.jw-icon{font-family:\'jw-icons\';-webkit-font-smoothing:antialiased;font-style:normal;font-weight:normal;text-transform:none;background-color:transparent;font-variant:normal;-webkit-font-feature-settings:"liga";-ms-font-feature-settings:"liga" 1;-o-font-feature-settings:"liga";font-feature-settings:"liga";-moz-osx-font-smoothing:grayscale}.jw-controlbar{display:table;position:absolute;right:0;left:0;bottom:0;height:2em;padding:0 .25em}.jw-controlbar .jw-hidden{display:none}.jw-controlbar.jw-drawer-expanded .jw-controlbar-left-group,.jw-controlbar.jw-drawer-expanded .jw-controlbar-center-group{opacity:0}.jw-background-color{background-color:#414040}.jw-group{display:table-cell}.jw-controlbar-center-group{width:100%;padding:0 .25em}.jw-controlbar-center-group .jw-slider-time,.jw-controlbar-center-group .jw-text-alt{padding:0}.jw-controlbar-center-group .jw-text-alt{display:none}.jw-controlbar-left-group,.jw-controlbar-right-group{white-space:nowrap}.jw-knob:hover,.jw-icon-inline:hover,.jw-icon-tooltip:hover,.jw-icon-display:hover,.jw-option:before:hover{color:#eee}.jw-icon-inline,.jw-icon-tooltip,.jw-slider-horizontal,.jw-text-elapsed,.jw-text-duration{display:inline-block;height:2em;position:relative;line-height:2em;vertical-align:middle;cursor:pointer}.jw-icon-inline,.jw-icon-tooltip{min-width:1.25em;text-align:center}.jw-icon-playback{min-width:2.25em}.jw-icon-volume{min-width:1.75em;text-align:left}.jw-time-tip{line-height:1em;pointer-events:none}.jw-icon-inline:after,.jw-icon-tooltip:after{width:100%;height:100%;font-size:1em}.jw-icon-cast{display:none}.jw-slider-volume.jw-slider-horizontal,.jw-icon-inline.jw-icon-volume{display:none}.jw-dock{margin:.75em;display:block;opacity:1;clear:right}.jw-dock:after{content:\'\';clear:both;display:block}.jw-dock-button{cursor:pointer;float:right;position:relative;width:2.5em;height:2.5em;margin:.5em}.jw-dock-button .jw-arrow{display:none;position:absolute;bottom:-0.2em;width:.5em;height:.2em;left:50%;margin-left:-0.25em}.jw-dock-button .jw-overlay{display:none;position:absolute;top:2.5em;right:0;margin-top:.25em;padding:.5em;white-space:nowrap}.jw-dock-button:hover .jw-overlay,.jw-dock-button:hover .jw-arrow{display:block}.jw-dock-image{width:100%;height:100%;background-position:50% 50%;background-repeat:no-repeat;opacity:.75}.jw-title{display:none;position:absolute;top:0;width:100%;font-size:.875em;height:8em;background:-webkit-linear-gradient(top, #000 0, #000 18%, rgba(0,0,0,0) 100%);background:linear-gradient(to bottom, #000 0, #000 18%, rgba(0,0,0,0) 100%)}.jw-title-primary,.jw-title-secondary{padding:.75em 1.5em;min-height:2.5em;width:100%;color:white;white-space:nowrap;text-overflow:ellipsis;overflow-x:hidden}.jw-title-primary{font-weight:bold}.jw-title-secondary{margin-top:-0.5em}.jw-slider-container{display:inline-block;height:1em;position:relative;touch-action:none}.jw-rail,.jw-buffer,.jw-progress{position:absolute;cursor:pointer}.jw-progress{background-color:#fff}.jw-rail{background-color:#aaa}.jw-buffer{background-color:#202020}.jw-cue,.jw-knob{position:absolute;cursor:pointer}.jw-cue{background-color:#fff;width:.1em;height:.4em}.jw-knob{background-color:#aaa;width:.4em;height:.4em}.jw-slider-horizontal{width:4em;height:1em}.jw-slider-horizontal.jw-slider-volume{margin-right:5px}.jw-slider-horizontal .jw-rail,.jw-slider-horizontal .jw-buffer,.jw-slider-horizontal .jw-progress{width:100%;height:.4em}.jw-slider-horizontal .jw-progress,.jw-slider-horizontal .jw-buffer{width:0}.jw-slider-horizontal .jw-rail,.jw-slider-horizontal .jw-progress,.jw-slider-horizontal .jw-slider-container{width:100%}.jw-slider-horizontal .jw-knob{left:0;margin-left:-0.325em}.jw-slider-vertical{width:.75em;height:4em;bottom:0;position:absolute;padding:1em}.jw-slider-vertical .jw-rail,.jw-slider-vertical .jw-buffer,.jw-slider-vertical .jw-progress{bottom:0;height:100%}.jw-slider-vertical .jw-progress,.jw-slider-vertical .jw-buffer{height:0}.jw-slider-vertical .jw-slider-container,.jw-slider-vertical .jw-rail,.jw-slider-vertical .jw-progress{bottom:0;width:.75em;height:100%;left:0;right:0;margin:0 auto}.jw-slider-vertical .jw-slider-container{height:4em;position:relative}.jw-slider-vertical .jw-knob{bottom:0;left:0;right:0;margin:0 auto}.jw-slider-time{right:0;left:0;width:100%}.jw-tooltip-time{position:absolute}.jw-slider-volume .jw-buffer{display:none}.jw-captions{position:absolute;display:none;margin:0 auto;width:100%;left:0;bottom:3em;right:0;max-width:90%;text-align:center}.jw-captions.jw-captions-enabled{display:block}.jw-captions-window{display:none;padding:.25em;border-radius:.25em}.jw-captions-window.jw-captions-window-active{display:inline-block}.jw-captions-text{display:inline-block;color:white;background-color:black;word-wrap:break-word;white-space:pre-line;font-style:normal;font-weight:normal;text-align:center;text-decoration:none;line-height:1.3em;padding:.1em .8em}.jw-rightclick{display:none;position:absolute;white-space:nowrap}.jw-rightclick.jw-open{display:block}.jw-rightclick ul{list-style:none;font-weight:bold;border-radius:.15em;margin:0;border:1px solid #444;padding:0}.jw-rightclick .jw-rightclick-logo{font-size:2em;color:#ff0147;vertical-align:middle;padding-right:.3em;margin-right:.3em;border-right:1px solid #444}.jw-rightclick li{background-color:#000;border-bottom:1px solid #444;margin:0}.jw-rightclick a{color:#fff;text-decoration:none;padding:1em;display:block;font-size:.6875em}.jw-rightclick li:last-child{border-bottom:none}.jw-rightclick li:hover{background-color:#1a1a1a;cursor:pointer}.jw-rightclick .jw-featured{background-color:#252525;vertical-align:middle}.jw-rightclick .jw-featured a{color:#777}.jw-logo{position:absolute;margin:.75em;cursor:pointer;pointer-events:all;background-repeat:no-repeat;background-size:contain;top:auto;right:auto;left:auto;bottom:auto}.jw-logo .jw-flag-audio-player{display:none}.jw-logo-top-right{top:0;right:0}.jw-logo-top-left{top:0;left:0}.jw-logo-bottom-left{bottom:0;left:0}.jw-logo-bottom-right{bottom:0;right:0}.jw-watermark{position:absolute;top:50%;left:0;right:0;bottom:0;text-align:center;font-size:14em;color:#eee;opacity:.33;pointer-events:none}.jw-icon-tooltip.jw-open .jw-overlay{opacity:1;visibility:visible}.jw-icon-tooltip.jw-hidden{display:none}.jw-overlay-horizontal{display:none}.jw-icon-tooltip.jw-open-drawer:before{display:none}.jw-icon-tooltip.jw-open-drawer .jw-overlay-horizontal{opacity:1;display:inline-block;vertical-align:top}.jw-overlay:before{position:absolute;top:0;bottom:0;left:-50%;width:100%;background-color:rgba(0,0,0,0);content:" "}.jw-slider-time .jw-overlay:before{height:1em;top:auto}.jw-time-tip,.jw-volume-tip,.jw-menu{position:relative;left:-50%;border:solid 1px #000;margin:0}.jw-volume-tip{width:100%;height:100%;display:block}.jw-time-tip{text-align:center;font-family:inherit;color:#aaa;bottom:1em;border:solid 4px #000}.jw-time-tip .jw-text{line-height:1em}.jw-controlbar .jw-overlay{margin:0;position:absolute;bottom:2em;left:50%;opacity:0;visibility:hidden}.jw-controlbar .jw-overlay .jw-contents{position:relative}.jw-controlbar .jw-option{position:relative;white-space:nowrap;cursor:pointer;list-style:none;height:1.5em;font-family:inherit;line-height:1.5em;color:#aaa;padding:0 .5em;font-size:.8em}.jw-controlbar .jw-option:hover,.jw-controlbar .jw-option:before:hover{color:#eee}.jw-controlbar .jw-option:before{padding-right:.125em}.jw-playlist-container ::-webkit-scrollbar-track{background-color:#333;border-radius:10px}.jw-playlist-container ::-webkit-scrollbar{width:5px;border:10px solid black;border-bottom:0;border-top:0}.jw-playlist-container ::-webkit-scrollbar-thumb{background-color:white;border-radius:5px}.jw-tooltip-title{border-bottom:1px solid #444;text-align:left;padding-left:.7em}.jw-playlist{max-height:11em;min-height:4.5em;overflow-x:hidden;overflow-y:scroll;width:calc(100% - 4px)}.jw-playlist .jw-option{height:3em;margin-right:5px;color:white;padding-left:1em;font-size:.8em}.jw-playlist .jw-label,.jw-playlist .jw-name{display:inline-block;line-height:3em;text-align:left;overflow:hidden;white-space:nowrap}.jw-playlist .jw-label{width:1em}.jw-playlist .jw-name{width:11em}.jw-skip{cursor:default;position:absolute;float:right;display:inline-block;right:.75em;bottom:3em}.jw-skip.jw-skippable{cursor:pointer}.jw-skip.jw-hidden{visibility:hidden}.jw-skip .jw-skip-icon{display:none;margin-left:-0.75em}.jw-skip .jw-skip-icon:before{content:"\\E60C"}.jw-skip .jw-text,.jw-skip .jw-skip-icon{color:#aaa;vertical-align:middle;line-height:1.5em;font-size:.7em}.jw-skip.jw-skippable:hover{cursor:pointer}.jw-skip.jw-skippable:hover .jw-text,.jw-skip.jw-skippable:hover .jw-skip-icon{color:#eee}.jw-skip.jw-skippable .jw-skip-icon{display:inline;margin:0}.jwplayer.jw-state-playing.jw-flag-casting .jw-display-icon-container,.jwplayer.jw-state-paused.jw-flag-casting .jw-display-icon-container{display:table}.jwplayer.jw-flag-casting .jw-display-icon-container{border-radius:0;border:1px solid white;position:absolute;top:auto;left:.5em;right:.5em;bottom:50%;margin-bottom:-12.5%;height:50%;width:50%;padding:0;background-repeat:no-repeat;background-position:center}.jwplayer.jw-flag-casting .jw-display-icon-container .jw-icon{font-size:3em}.jwplayer.jw-flag-casting.jw-state-complete .jw-preview{display:none}.jw-cast{position:absolute;width:100%;height:100%;background-repeat:no-repeat;background-size:auto;background-position:50% 50%}.jw-cast-label{position:absolute;left:.5em;right:.5em;bottom:75%;margin-bottom:1.5em;text-align:center}.jw-cast-name{color:#ccc}.jw-state-idle .jw-preview{display:block}.jw-state-idle .jw-icon-display:before{content:"\\E60E"}.jw-state-idle .jw-controlbar{display:none}.jw-state-idle .jw-captions{display:none}.jw-state-idle .jw-title{display:block}.jwplayer.jw-state-playing .jw-display-icon-container{display:none}.jwplayer.jw-state-playing .jw-display-icon-container .jw-icon-display:before{content:"\\E60D"}.jwplayer.jw-state-playing .jw-icon-playback:before{content:"\\E60D"}.jwplayer.jw-state-paused .jw-display-icon-container{display:none}.jwplayer.jw-state-paused .jw-display-icon-container .jw-icon-display:before{content:"\\E60E"}.jwplayer.jw-state-paused .jw-icon-playback:before{content:"\\E60E"}.jwplayer.jw-state-buffering .jw-display-icon-container .jw-icon-display{-webkit-animation:spin 2s linear infinite;animation:spin 2s linear infinite}.jwplayer.jw-state-buffering .jw-display-icon-container .jw-icon-display:before{content:"\\E601"}@-webkit-keyframes spin{100%{-webkit-transform:rotate(360deg)}}@keyframes spin{100%{-webkit-transform:rotate(360deg);transform:rotate(360deg)}}.jwplayer.jw-state-buffering .jw-display-icon-container .jw-text{display:none}.jwplayer.jw-state-buffering .jw-icon-playback:before{content:"\\E60D"}.jwplayer.jw-state-complete .jw-preview{display:block}.jwplayer.jw-state-complete .jw-display-icon-container .jw-icon-display:before{content:"\\E610"}.jwplayer.jw-state-complete .jw-display-icon-container .jw-text{display:none}.jwplayer.jw-state-complete .jw-icon-playback:before{content:"\\E60E"}.jwplayer.jw-state-complete .jw-captions{display:none}body .jw-error .jw-title,.jwplayer.jw-state-error .jw-title{display:block}body .jw-error .jw-title .jw-title-primary,.jwplayer.jw-state-error .jw-title .jw-title-primary{white-space:normal}body .jw-error .jw-preview,.jwplayer.jw-state-error .jw-preview{display:block}body .jw-error .jw-controlbar,.jwplayer.jw-state-error .jw-controlbar{display:none}body .jw-error .jw-captions,.jwplayer.jw-state-error .jw-captions{display:none}body .jw-error:hover .jw-display-icon-container,.jwplayer.jw-state-error:hover .jw-display-icon-container{cursor:default;color:#fff;background:#000}body .jw-error .jw-icon-display,.jwplayer.jw-state-error .jw-icon-display{cursor:default;font-family:\'jw-icons\';-webkit-font-smoothing:antialiased;font-style:normal;font-weight:normal;text-transform:none;background-color:transparent;font-variant:normal;-webkit-font-feature-settings:"liga";-ms-font-feature-settings:"liga" 1;-o-font-feature-settings:"liga";font-feature-settings:"liga";-moz-osx-font-smoothing:grayscale}body .jw-error .jw-icon-display:before,.jwplayer.jw-state-error .jw-icon-display:before{content:"\\E607"}body .jw-error .jw-icon-display:hover,.jwplayer.jw-state-error .jw-icon-display:hover{color:#fff}body .jw-error{font-size:16px;background-color:#000;color:#eee;width:100%;height:100%;display:table;opacity:1;position:relative}body .jw-error .jw-icon-container{position:absolute;width:100%;height:100%;top:0;left:0;bottom:0;right:0}.jwplayer.jw-flag-cast-available .jw-controlbar{display:table}.jwplayer.jw-flag-cast-available .jw-icon-cast{display:inline-block}.jwplayer.jw-flag-skin-loading .jw-captions,.jwplayer.jw-flag-skin-loading .jw-controls,.jwplayer.jw-flag-skin-loading .jw-title{display:none}.jwplayer.jw-flag-fullscreen{width:100% !important;height:100% !important;top:0;right:0;bottom:0;left:0;z-index:1000;margin:0;position:fixed}.jwplayer.jw-flag-fullscreen.jw-flag-user-inactive{cursor:none;-webkit-cursor-visibility:auto-hide}.jwplayer.jw-flag-live .jw-controlbar .jw-text-elapsed,.jwplayer.jw-flag-live .jw-controlbar .jw-text-duration,.jwplayer.jw-flag-live .jw-controlbar .jw-slider-time{display:none}.jwplayer.jw-flag-live .jw-controlbar .jw-text-alt{display:inline}.jwplayer.jw-flag-user-inactive.jw-state-playing .jw-controlbar,.jwplayer.jw-flag-user-inactive.jw-state-playing .jw-dock{display:none}.jwplayer.jw-flag-user-inactive.jw-state-playing .jw-logo.jw-hide{display:none}.jwplayer.jw-flag-user-inactive.jw-state-playing .jw-plugin,.jwplayer.jw-flag-user-inactive.jw-state-playing .jw-captions{bottom:.5em}.jwplayer.jw-flag-user-inactive.jw-state-buffering .jw-controlbar{display:none}.jwplayer.jw-flag-media-audio .jw-controlbar{display:table}.jwplayer.jw-flag-media-audio.jw-flag-user-inactive .jw-controlbar{display:table}.jwplayer.jw-flag-media-audio.jw-flag-user-inactive.jw-state-playing .jw-plugin,.jwplayer.jw-flag-media-audio.jw-flag-user-inactive.jw-state-playing .jw-captions{bottom:3em}.jw-flag-media-audio .jw-preview{display:block}.jwplayer.jw-flag-ads .jw-preview,.jwplayer.jw-flag-ads .jw-dock{display:none}.jwplayer.jw-flag-ads .jw-controlbar .jw-icon-inline,.jwplayer.jw-flag-ads .jw-controlbar .jw-icon-tooltip,.jwplayer.jw-flag-ads .jw-controlbar .jw-text,.jwplayer.jw-flag-ads .jw-controlbar .jw-slider-horizontal{display:none}.jwplayer.jw-flag-ads .jw-controlbar .jw-icon-playback,.jwplayer.jw-flag-ads .jw-controlbar .jw-icon-volume,.jwplayer.jw-flag-ads .jw-controlbar .jw-slider-volume,.jwplayer.jw-flag-ads .jw-controlbar .jw-icon-fullscreen{display:inline-block}.jwplayer.jw-flag-ads .jw-controlbar .jw-text-alt{display:inline}.jwplayer.jw-flag-ads .jw-controlbar .jw-slider-volume.jw-slider-horizontal,.jwplayer.jw-flag-ads .jw-controlbar .jw-icon-inline.jw-icon-volume{display:inline-block}.jwplayer.jw-flag-ads .jw-controlbar .jw-icon-tooltip.jw-icon-volume{display:none}.jwplayer.jw-flag-ads .jw-logo,.jwplayer.jw-flag-ads .jw-captions{display:none}.jwplayer.jw-flag-ads-googleima .jw-controlbar{display:table;bottom:0}.jwplayer.jw-flag-ads-googleima.jw-flag-touch .jw-controlbar{font-size:1em}.jwplayer.jw-flag-ads-googleima.jw-flag-touch.jw-state-paused .jw-display-icon-container{display:none}.jwplayer.jw-flag-ads-googleima.jw-skin-seven .jw-controlbar{font-size:.9em}.jwplayer.jw-flag-ads-vpaid .jw-controlbar{display:none}.jwplayer.jw-flag-ads-hide-controls .jw-controls{display:none !important}.jwplayer.jw-flag-ads.jw-flag-touch .jw-controlbar{display:table}.jwplayer.jw-flag-overlay-open .jw-title{display:none}.jwplayer.jw-flag-overlay-open .jw-controls-right .jw-logo{display:none}.jwplayer.jw-flag-overlay-open-sharing .jw-controls,.jwplayer.jw-flag-overlay-open-related .jw-controls,.jwplayer.jw-flag-overlay-open-sharing .jw-title,.jwplayer.jw-flag-overlay-open-related .jw-title{display:none}.jwplayer.jw-flag-rightclick-open{overflow:visible}.jwplayer.jw-flag-rightclick-open .jw-rightclick{z-index:16777215}.jw-flag-controls-disabled .jw-controls{visibility:hidden}.jw-flag-controls-disabled .jw-logo{visibility:visible}.jw-flag-controls-disabled .jw-media{cursor:auto}body .jwplayer.jw-flag-flash-blocked .jw-title{display:block}body .jwplayer.jw-flag-flash-blocked .jw-controls,body .jwplayer.jw-flag-flash-blocked .jw-overlays,body .jwplayer.jw-flag-flash-blocked .jw-preview{display:none}.jw-flag-touch .jw-controlbar,.jw-flag-touch .jw-skip,.jw-flag-touch .jw-plugin{font-size:1.5em}.jw-flag-touch .jw-captions{bottom:4.25em}.jw-flag-touch .jw-icon-tooltip.jw-open-drawer:before{display:inline}.jw-flag-touch .jw-icon-tooltip.jw-open-drawer:before{content:"\\E615"}.jw-flag-touch .jw-display-icon-container{pointer-events:none}.jw-flag-touch.jw-state-paused .jw-display-icon-container{display:table}.jw-flag-touch.jw-state-paused.jw-flag-dragging .jw-display-icon-container{display:none}.jw-flag-compact-player .jw-icon-playlist,.jw-flag-compact-player .jw-text-elapsed,.jw-flag-compact-player .jw-text-duration{display:none}.jwplayer.jw-flag-audio-player{background-color:transparent}.jwplayer.jw-flag-audio-player .jw-media{visibility:hidden}.jwplayer.jw-flag-audio-player .jw-media object{width:1px;height:1px}.jwplayer.jw-flag-audio-player .jw-preview,.jwplayer.jw-flag-audio-player .jw-display-icon-container{display:none}.jwplayer.jw-flag-audio-player .jw-controlbar{display:table;height:auto;left:0;bottom:0;margin:0;width:100%;min-width:100%;opacity:1}.jwplayer.jw-flag-audio-player .jw-controlbar .jw-icon-fullscreen,.jwplayer.jw-flag-audio-player .jw-controlbar .jw-icon-tooltip{display:none}.jwplayer.jw-flag-audio-player .jw-controlbar .jw-slider-volume.jw-slider-horizontal,.jwplayer.jw-flag-audio-player .jw-controlbar .jw-icon-inline.jw-icon-volume{display:inline-block}.jwplayer.jw-flag-audio-player .jw-controlbar .jw-icon-tooltip.jw-icon-volume{display:none}.jwplayer.jw-flag-audio-player.jw-flag-user-inactive .jw-controlbar{display:table}.jw-skin-seven .jw-background-color{background:#000}.jw-skin-seven .jw-controlbar{border-top:#333 1px solid;height:2.5em}.jw-skin-seven .jw-group{vertical-align:middle}.jw-skin-seven .jw-playlist{background-color:rgba(0,0,0,0.5)}.jw-skin-seven .jw-playlist-container{left:-43%;background-color:rgba(0,0,0,0.5)}.jw-skin-seven .jw-playlist-container .jw-option{border-bottom:1px solid #444}.jw-skin-seven .jw-playlist-container .jw-option:hover,.jw-skin-seven .jw-playlist-container .jw-option.jw-active-option{background-color:black}.jw-skin-seven .jw-playlist-container .jw-option:hover .jw-label{color:#FF0046}.jw-skin-seven .jw-playlist-container .jw-icon-playlist{margin-left:0}.jw-skin-seven .jw-playlist-container .jw-label .jw-icon-play{color:#FF0046}.jw-skin-seven .jw-playlist-container .jw-label .jw-icon-play:before{padding-left:0}.jw-skin-seven .jw-tooltip-title{background-color:#000;color:#fff}.jw-skin-seven .jw-text{color:#fff}.jw-skin-seven .jw-button-color{color:#fff}.jw-skin-seven .jw-button-color:hover{color:#FF0046}.jw-skin-seven .jw-toggle{color:#FF0046}.jw-skin-seven .jw-toggle.jw-off{color:#fff}.jw-skin-seven .jw-controlbar .jw-icon:before,.jw-skin-seven .jw-text-elapsed,.jw-skin-seven .jw-text-duration{padding:0 .7em}.jw-skin-seven .jw-controlbar .jw-icon-prev:before{padding-right:.25em}.jw-skin-seven .jw-controlbar .jw-icon-playlist:before{padding:0 .45em}.jw-skin-seven .jw-controlbar .jw-icon-next:before{padding-left:.25em}.jw-skin-seven .jw-icon-prev,.jw-skin-seven .jw-icon-next{font-size:.7em}.jw-skin-seven .jw-icon-prev:before{border-left:1px solid #666}.jw-skin-seven .jw-icon-next:before{border-right:1px solid #666}.jw-skin-seven .jw-icon-display{color:#fff}.jw-skin-seven .jw-icon-display:before{padding-left:0}.jw-skin-seven .jw-display-icon-container{border-radius:50%;border:1px solid #333}.jw-skin-seven .jw-rail{background-color:#384154;box-shadow:none}.jw-skin-seven .jw-buffer{background-color:#666F82}.jw-skin-seven .jw-progress{background:#FF0046}.jw-skin-seven .jw-knob{width:.6em;height:.6em;background-color:#fff;box-shadow:0 0 0 1px #000;border-radius:1em}.jw-skin-seven .jw-slider-horizontal .jw-slider-container{height:.95em}.jw-skin-seven .jw-slider-horizontal .jw-rail,.jw-skin-seven .jw-slider-horizontal .jw-buffer,.jw-skin-seven .jw-slider-horizontal .jw-progress{height:.2em;border-radius:0}.jw-skin-seven .jw-slider-horizontal .jw-knob{top:-0.2em}.jw-skin-seven .jw-slider-horizontal .jw-cue{top:-0.05em;width:.3em;height:.3em;background-color:#fff;border-radius:50%}.jw-skin-seven .jw-slider-vertical .jw-rail,.jw-skin-seven .jw-slider-vertical .jw-buffer,.jw-skin-seven .jw-slider-vertical .jw-progress{width:.2em}.jw-skin-seven .jw-slider-vertical .jw-knob{margin-bottom:-0.3em}.jw-skin-seven .jw-volume-tip{width:100%;left:-45%;padding-bottom:.7em}.jw-skin-seven .jw-text-duration{color:#666F82}.jw-skin-seven .jw-controlbar-right-group .jw-icon-tooltip:before,.jw-skin-seven .jw-controlbar-right-group .jw-icon-inline:before{border-left:1px solid #666}.jw-skin-seven .jw-controlbar-right-group .jw-icon-inline:first-child:before{border:none}.jw-skin-seven .jw-dock .jw-dock-button{border-radius:50%;border:1px solid #333}.jw-skin-seven .jw-dock .jw-overlay{border-radius:2.5em}.jw-skin-seven .jw-icon-tooltip .jw-active-option{background-color:#FF0046;color:#fff}.jw-skin-seven .jw-icon-volume{min-width:2.6em}.jw-skin-seven .jw-time-tip,.jw-skin-seven .jw-menu,.jw-skin-seven .jw-volume-tip,.jw-skin-seven .jw-skip{border:1px solid #333}.jw-skin-seven .jw-time-tip{padding:.2em;bottom:1.3em}.jw-skin-seven .jw-menu,.jw-skin-seven .jw-volume-tip{bottom:.24em}.jw-skin-seven .jw-skip{padding:.4em;border-radius:1.75em}.jw-skin-seven .jw-skip .jw-text,.jw-skin-seven .jw-skip .jw-icon-inline{color:#fff;line-height:1.75em}.jw-skin-seven .jw-skip.jw-skippable:hover .jw-text,.jw-skin-seven .jw-skip.jw-skippable:hover .jw-icon-inline{color:#FF0046}.jw-skin-seven.jw-flag-touch .jw-controlbar .jw-icon:before,.jw-skin-seven.jw-flag-touch .jw-text-elapsed,.jw-skin-seven.jw-flag-touch .jw-text-duration{padding:0 .35em}.jw-skin-seven.jw-flag-touch .jw-controlbar .jw-icon-prev:before{padding:0 .125em 0 .7em}.jw-skin-seven.jw-flag-touch .jw-controlbar .jw-icon-next:before{padding:0 .7em 0 .125em}.jw-skin-seven.jw-flag-touch .jw-controlbar .jw-icon-playlist:before{padding:0 .225em}', ""]);
    }, function(a, b) {
        a.exports = function() {
            var a = [];
            return a.toString = function() {
                for (var a = [], b = 0; b < this.length; b++) {
                    var c = this[b];
                    c[2] ? a.push("@media " + c[2] + "{" + c[1] + "}") : a.push(c[1])
                }
                return a.join("")
            }, a.i = function(b, c) {
                "string" == typeof b && (b = [
                    [null, b, ""]
                ]);
                for (var d = {}, e = 0; e < this.length; e++) {
                    var f = this[e][0];
                    "number" == typeof f && (d[f] = !0)
                }
                for (e = 0; e < b.length; e++) {
                    var g = b[e];
                    "number" == typeof g[0] && d[g[0]] || (c && !g[2] ? g[2] = c : c && (g[2] = "(" + g[2] + ") and (" + c + ")"), a.push(g))
                }
            }, a
        }
    }, function(a, b) {
        a.exports = "data:application/font-woff;base64,d09GRgABAAAAABQ4AAsAAAAAE+wAAQABAAAAAAAAAAAAAAAAAAAAAAAAAABPUy8yAAABCAAAAGAAAABgDxID2WNtYXAAAAFoAAAAVAAAAFQaVsydZ2FzcAAAAbwAAAAIAAAACAAAABBnbHlmAAABxAAAD3AAAA9wKJaoQ2hlYWQAABE0AAAANgAAADYIhqKNaGhlYQAAEWwAAAAkAAAAJAmCBdxobXR4AAARkAAAAGwAAABscmAHPWxvY2EAABH8AAAAOAAAADg2EDnwbWF4cAAAEjQAAAAgAAAAIAAiANFuYW1lAAASVAAAAcIAAAHCwZOZtHBvc3QAABQYAAAAIAAAACAAAwAAAAMEmQGQAAUAAAKZAswAAACPApkCzAAAAesAMwEJAAAAAAAAAAAAAAAAAAAAARAAAAAAAAAAAAAAAAAAAAAAQAAA5hYDwP/AAEADwABAAAAAAQAAAAAAAAAAAAAAIAAAAAAAAwAAAAMAAAAcAAEAAwAAABwAAwABAAAAHAAEADgAAAAKAAgAAgACAAEAIOYW//3//wAAAAAAIOYA//3//wAB/+MaBAADAAEAAAAAAAAAAAAAAAEAAf//AA8AAQAAAAAAAAAAAAIAADc5AQAAAAABAAAAAAAAAAAAAgAANzkBAAAAAAEAAAAAAAAAAAACAAA3OQEAAAAABABgAAAFoAOAADoAPwBEAEkAACUVIi4CNTQ2Ny4BNTQ+AjMyHgIVFAYHHgEVFA4CIxEyFhc+ATU0LgIjIg4CFRQWFz4BMxExARUhNSEXFSE1IRcVITUhAUAuUj0jCgoKCkZ6o11do3pGCgoKCiM9Ui4qSh4BAjpmiE1NiGY6AQIeSioCVQIL/fWWAXX+i0oBK/7VHh4jPVIuGS4VH0MiXaN6RkZ6o10iQx8VLhkuUj0jAcAdGQ0bDk2IZjo6ZohNDhsNGR3+XgNilZXglZXglZUAAAABAEAAAAPAA4AAIQAAExQeAjMyPgI1MxQOAiMiLgI1ND4CMxUiDgIVMYs6ZohNTYhmOktGeqNdXaN6RkZ6o11NiGY6AcBNiGY6OmaITV2jekZGeqNdXaN6Rks6ZohNAAAEAEAAAATAA4AADgAcACoAMQAAJS4BJyERIREuAScRIREhByMuAyc1HgMXMSsBLgMnNR4DFzErATUeARcxAn8DBQQCDPxGCysLBDz9v1NaCERrjE9irINTCLVbByc6Sio9a1I1CLaBL0YMQgsoCgLB/ukDCgIBSPzCQk6HaEIIWAhQgKdgKUg5JgdYBzRRZzx9C0QuAAAAAAUAQAAABMADgAAOABkAJwA1ADwAACUuASchESERLgEnESERIQE1IREhLgMnMQEjLgMnNR4DFzErAS4DJzUeAxcxKwE1HgEXMQKAAgYFAg38QAwqCgRA/cD+gANA/iAYRVlsPgEtWghFa4xPYq2DUgmzWgcnO0oqPGpSNgm6gDBEDEAMKAwCwP7tAggDAUb8wAHQ8P3APWdUQRf98E2IaEIHWghQgKhgKUg4JgdaCDVRZzt9DEMuAAAEAEAAAAXAA4AABAAJAGcAxQAANxEhESEBIREhEQU+ATc+ATMyFhceARceARceARcjLgEnLgEnLgEnLgEjIgYHDgEHDgEHDgEVFBYXHgEXHgEXHgEzMjY3PgE3Mw4BBw4BBw4BBw4BIyImJy4BJy4BJy4BNTQ2Nz4BNzEhPgE3PgEzMhYXHgEXHgEXHgEXIy4BJy4BJy4BJy4BIyIGBw4BBw4BBw4BFRQWFx4BFx4BFx4BMzI2Nz4BNzMOAQcOAQcOAQcOASMiJicuAScuAScuATU0Njc+ATcxQAWA+oAFNvsUBOz8Iw4hExQsGBIhEA8cDQwUCAgLAlsBBQUECgYHDggIEAkQGgsLEgcHCgMDAwMDAwoHBxILCxoQFiEMDA8DWgIJBwgTDQwcERAkFBgsFBMhDg0VBwcHBwcHFQ0Bug0hFBMsGREhEBAcDAwVCAgKAloCBQQECwYGDggIEQgQGwsLEgcHCgMDAwMDAwoHBxILCxsQFSIMDA4DWwIJCAcUDAwdEBEkExksExQhDQ4UBwcICAcHFA4AA4D8gAM1/RYC6tcQGAgJCQUFBQ8KChgPDiETCQ4HBwwFBQgDAwIGBgYRCgoYDQ0cDg0aDQ0XCgoRBgYGDQ0OIhYUJBEQHAsLEgYGBgkICRcPDyQUFCwXGC0VFCQPEBgICQkFBQUPCgoYDw4hEwkOBwcMBQUIAwMCBgYGEQoKGA0NHA4NGg0NFwoKEQYGBg0NDiIWFCQREBwLCxIGBgYJCAkXDw8kFBQsFxgtFRQkDwAAAAADAEAAAAXAA4AAEABvAM4AACUhIiY1ETQ2MyEyFhURFAYjAT4BNz4BNz4BMzIWFx4BFx4BFx4BFzMuAScuAScuAScuASMiBgcOAQcOAQcOARUUFhceARceARceATMyNjc+ATc+ATc+ATcjDgEHDgEjIiYnLgEnLgEnLgE1NDY3OQEhPgE3PgE3PgEzMhYXHgEXHgEXHgEXMy4BJy4BJy4BJy4BIyIGBw4BBw4BBw4BFRQWFx4BFx4BFx4BMzI2Nz4BNz4BNz4BNyMOAQcOASMiJicuAScuAScuATU0Njc5AQUs+6g9V1c9BFg9V1c9/JoDCgcGEgsLGxAJEAgIDgYHCgQEBgFaAgoICBQNDBwQDyESGCwUEyEODRUHBwcHBwcVDQ4hExQrGRQkEBAdDAwUCAcJAloDDwwMIhUQGwsLEgYHCgMEAwMEAbkDCgcHEgsLGxAIEQgHDwYGCwQEBQFbAgoICBUMDBwQECERGSwTFCENDhQHBwgIBwcUDg0hFBMsGRMkERAdDAwUBwgJAlsDDgwNIRUQGwsLEgcHCgMDAwMDAFc+AlY+V1c+/ao+VwH0DRgKCxAGBgYCAwMIBQUMBwcOCRMhDg8YCgoOBgUFCQkIGBAPJBQVLRgXLBQUJA8PFwkICQYGBhILCxwQESQUFiIODQ0GBgYRCgoXDQ0aDg4bDQ0YCgsQBgYGAgMDCAUFDAcHDgkTIQ4PGAoKDgYFBQkJCBgQDyQUFS0YFywUFCQPDxcJCAkGBgYSCwscEBEkFBYiDg0NBgYGEQoKFw0NGg4OGw0AAAABAOAAoAMgAuAAFAAAARQOAiMiLgI1ND4CMzIeAhUDIC1OaTw8aU4tLU5pPDxpTi0BwDxpTi0tTmk8PGlOLS1OaTwAAAMAQAAQBEADkAADABAAHwAANwkBISUyNjU0JiMiBhUUFjMTNCYjIgYVERQWMzI2NRFAAgACAPwAAgAOFRUODhUVDiMVDg4VFQ4OFRADgPyAcBYQDxgWERAWAeYPGBYR/tcPGBYRASkAAgBAAAADwAOAAAcADwAANxEXNxcHFyEBIREnByc3J0CAsI2wgP5zAfMBjYCwjbCAAAGNgLCNsIADgP5zgLCNsIAAAAAFAEAAAAXAA4AABAAJABYAMwBPAAA3ESERIQEhESERATM1MxEjNSMVIxEzFSUeARceARceARUUBgcOAQcOAQcOASsBETMeARcxBxEzMjY3PgE3PgE3PgE1NCYnLgEnLgEnLgErAUAFgPqABTb7FATs/FS2YGC2ZGQCXBQeDg8UBwcJBgcHEwwMIRMTLBuwsBYqE6BHCRcJChIIBw0FBQUEAwINBwcTDAwgETcAA4D8gAM2/RcC6f7Arf5AwMABwK2dBxQODyIWFTAbGC4TFiIPDhgKCQcBwAIHB0P+5gQDAg0HBxcMDCETER0PDhgKCQ8EBQUABAA9AAAFwAOAABAAHQA7AFkAACUhIiY1ETQ2MyEyFhURFAYjASMVIzUjETM1MxUzEQUuAScuAScuASsBETMyNjc+ATc+ATc+ATUuASc5AQcOAQcOASsBETMyFhceARceARceARUUBgcOAQc5AQUq+6k+WFg+BFc+WFg+/bNgs2Rks2ABsAcXDA4fExMnFrCwGywTEx4PDBMHBwYCCAl3CBIKCRQMRzcTHgwMEwcHCwQDBAUFBQ0HAFg+AlQ+WFg+/aw+WAKdra3+QMDAAcB9FiIODxQHBwb+QAkHCRgPDiUTFiwYHTAW4ggNAgMEAR8EBQUPCgoYDw4fERMfDwwXBwAAAAABAEMABgOgA3oAjwAAExQiNScwJic0JicuAQcOARUcARUeARceATc+ATc+ATE2MhUwFAcUFhceARceATMyNjc+ATc+ATc+AzE2MhUwDgIVFBYXHgEXFjY3PgE3PgE3PgE3PgM3PAE1NCYnJgYHDgMxBiI1MDwCNTQmJyYGBw4BBw4DMQYiNTAmJy4BJyYGBw4BMRWQBgQIBAgCBQ4KBwkDFgcHIQ8QDwcHNgUEAwMHBQsJChcMBQ0FBwsHDBMICR8cFQUFAwQDCAUHFRERJBEMEwgJEgUOGQwGMjgvBAkHDBYEAz1IPAQFLyQRIhEQFgoGIiUcBQUEAgMYKCcmCgcsAboFBQwYDwUKBwUEAgMNBwcLBxRrDhENBwggDxOTCgqdMBM1EQwTCAcFBAIFCgcPIw4UQ0IxCgpTc3glEyMREBgIBwEKBxUKESUQJ00mE6/JrA8FBgIHDQMECAkGla2PCQk1VGYxNTsHAgUKChwQC2BqVQoKehYfTwUDRx8TkAMAAAAAAgBGAAAENgOAAAQACAAAJREzESMJAhEDxnBw/IADgPyAAAOA/IADgP5A/kADgAAAAgCAAAADgAOAAAQACQAAJREhESEBIREhEQKAAQD/AP4AAQD/AAADgPyAA4D8gAOAAAAAAAEAgAAABAADgAADAAAJAREBBAD8gAOAAcD+QAOA/kAAAgBKAAAEOgOAAAQACAAANxEjETMJAhG6cHADgPyAA4AAA4D8gAOA/kD+QAOAAAAAAQBDACADQwOgACkAAAEeARUUDgIjIi4CNTQ+AjM1DQE1Ig4CFRQeAjMyPgI1NCYnNwMNGhw8aYxPT4xoPT1ojE8BQP7APGlOLS1OaTw8aU4tFhNTAmMrYzVPjGg9PWiMT0+MaD2ArbOALU5pPDxpTi0tTmk8KUsfMAAAAAEAQABmAiADEwAGAAATETMlESUjQM0BE/7tzQEzARPN/VPNAAQAQAAABJADgAAXACsAOgBBAAAlJz4DNTQuAic3HgMVFA4CBzEvAT4BNTQmJzceAxUOAwcxJz4BNTQmJzceARUUBgcnBREzJRElIwPaKiY+KxcXKz4mKipDMBkZMEMqpCk5REQ5KSE0JBQBFCQzIcMiKCgiKiYwMCYq/c3NARP+7c0AIyheaXI8PHFpXikjK2ZyfEFBfHJmK4MjNZFUVJE1Ix5IUFgvL1lRRx2zFkgpK0YVIxxcNDVZHykDARPN/VPNAAACAEAAAAPDA4AABwAPAAABFyERFzcXBwEHJzcnIREnAypw/qlwl3mZ/iaWepZwAVdtAnNwAVdwlnqT/iOWepZw/qpsAAMAQAETBcACYAAMABkAJgAAARQGIyImNTQ2MzIWFSEUBiMiJjU0NjMyFhUhFAYjIiY1NDYzMhYVAY1iRUVhYUVFYgIWYUVFYmJFRWECHWFFRWJiRUVhAbpFYmJFRWFhRUViYkVFYWFFRWJiRUVhYUUAAAAAAQBmACYDmgNaACAAAAEXFhQHBiIvAQcGIicmND8BJyY0NzYyHwE3NjIXFhQPAQKj9yQkJGMd9vYkYx0kJPf3JCQkYx329iRjHSQk9wHA9iRjHSQk9/ckJCRjHfb2JGMdJCT39yQkJGMd9gAABgBEAAQDvAN8AAQACQAOABMAGAAdAAABIRUhNREhFSE1ESEVITUBMxUjNREzFSM1ETMVIzUBpwIV/esCFf3rAhX96/6dsrKysrKyA3xZWf6dWVn+nVlZAsaysv6dsrL+nbKyAAEAAAABGZqh06s/Xw889QALBAAAAAAA0dQiKwAAAADR1CIrAAAAAAXAA6AAAAAIAAIAAAAAAAAAAQAAA8D/wAAABgAAAAAABcAAAQAAAAAAAAAAAAAAAAAAABsEAAAAAAAAAAAAAAACAAAABgAAYAQAAEAFAABABQAAQAYAAEAGAABABAAA4ASAAEAEAABABgAAQAYAAD0D4ABDBIAARgQAAIAEAACABIAASgOAAEMEwABABMAAQAQAAEAGAABABAAAZgQAAEQAAAAAAAoAFAAeAIgAuAEEAWAChgOyA9QECAQqBKQFJgXoBgAGGgYqBkIGgAaSBvQHFgdQB4YHuAABAAAAGwDPAAYAAAAAAAIAAAAAAAAAAAAAAAAAAAAAAAAADgCuAAEAAAAAAAEADAAAAAEAAAAAAAIABwCNAAEAAAAAAAMADABFAAEAAAAAAAQADACiAAEAAAAAAAUACwAkAAEAAAAAAAYADABpAAEAAAAAAAoAGgDGAAMAAQQJAAEAGAAMAAMAAQQJAAIADgCUAAMAAQQJAAMAGABRAAMAAQQJAAQAGACuAAMAAQQJAAUAFgAvAAMAAQQJAAYAGAB1AAMAAQQJAAoANADganctc2l4LWljb25zAGoAdwAtAHMAaQB4AC0AaQBjAG8AbgBzVmVyc2lvbiAxLjEAVgBlAHIAcwBpAG8AbgAgADEALgAxanctc2l4LWljb25zAGoAdwAtAHMAaQB4AC0AaQBjAG8AbgBzanctc2l4LWljb25zAGoAdwAtAHMAaQB4AC0AaQBjAG8AbgBzUmVndWxhcgBSAGUAZwB1AGwAYQByanctc2l4LWljb25zAGoAdwAtAHMAaQB4AC0AaQBjAG8AbgBzRm9udCBnZW5lcmF0ZWQgYnkgSWNvTW9vbi4ARgBvAG4AdAAgAGcAZQBuAGUAcgBhAHQAZQBkACAAYgB5ACAASQBjAG8ATQBvAG8AbgAuAAAAAwAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA=="
    }, function(a, b) {
        a.exports = "data:application/octet-stream;base64,AAEAAAALAIAAAwAwT1MvMg8SA9kAAAC8AAAAYGNtYXAaVsydAAABHAAAAFRnYXNwAAAAEAAAAXAAAAAIZ2x5ZiiWqEMAAAF4AAAPcGhlYWQIhqKNAAAQ6AAAADZoaGVhCYIF3AAAESAAAAAkaG10eHJgBz0AABFEAAAAbGxvY2E2EDnwAAARsAAAADhtYXhwACIA0QAAEegAAAAgbmFtZcGTmbQAABIIAAABwnBvc3QAAwAAAAATzAAAACAAAwSZAZAABQAAApkCzAAAAI8CmQLMAAAB6wAzAQkAAAAAAAAAAAAAAAAAAAABEAAAAAAAAAAAAAAAAAAAAABAAADmFgPA/8AAQAPAAEAAAAABAAAAAAAAAAAAAAAgAAAAAAADAAAAAwAAABwAAQADAAAAHAADAAEAAAAcAAQAOAAAAAoACAACAAIAAQAg5hb//f//AAAAAAAg5gD//f//AAH/4xoEAAMAAQAAAAAAAAAAAAAAAQAB//8ADwABAAAAAAAAAAAAAgAANzkBAAAAAAEAAAAAAAAAAAACAAA3OQEAAAAAAQAAAAAAAAAAAAIAADc5AQAAAAAEAGAAAAWgA4AAOgA/AEQASQAAJRUiLgI1NDY3LgE1ND4CMzIeAhUUBgceARUUDgIjETIWFz4BNTQuAiMiDgIVFBYXPgEzETEBFSE1IRcVITUhFxUhNSEBQC5SPSMKCgoKRnqjXV2jekYKCgoKIz1SLipKHgECOmaITU2IZjoBAh5KKgJVAgv99ZYBdf6LSgEr/tUeHiM9Ui4ZLhUfQyJdo3pGRnqjXSJDHxUuGS5SPSMBwB0ZDRsOTYhmOjpmiE0OGw0ZHf5eA2KVleCVleCVlQAAAAEAQAAAA8ADgAAhAAATFB4CMzI+AjUzFA4CIyIuAjU0PgIzFSIOAhUxizpmiE1NiGY6S0Z6o11do3pGRnqjXU2IZjoBwE2IZjo6ZohNXaN6RkZ6o11do3pGSzpmiE0AAAQAQAAABMADgAAOABwAKgAxAAAlLgEnIREhES4BJxEhESEHIy4DJzUeAxcxKwEuAyc1HgMXMSsBNR4BFzECfwMFBAIM/EYLKwsEPP2/U1oIRGuMT2Ksg1MItVsHJzpKKj1rUjUItoEvRgxCCygKAsH+6QMKAgFI/MJCTodoQghYCFCAp2ApSDkmB1gHNFFnPH0LRC4AAAAABQBAAAAEwAOAAA4AGQAnADUAPAAAJS4BJyERIREuAScRIREhATUhESEuAycxASMuAyc1HgMXMSsBLgMnNR4DFzErATUeARcxAoACBgUCDfxADCoKBED9wP6AA0D+IBhFWWw+AS1aCEVrjE9irYNSCbNaByc7Sio8alI2CbqAMEQMQAwoDALA/u0CCAMBRvzAAdDw/cA9Z1RBF/3wTYhoQgdaCFCAqGApSDgmB1oINVFnO30MQy4AAAQAQAAABcADgAAEAAkAZwDFAAA3ESERIQEhESERBT4BNz4BMzIWFx4BFx4BFx4BFyMuAScuAScuAScuASMiBgcOAQcOAQcOARUUFhceARceARceATMyNjc+ATczDgEHDgEHDgEHDgEjIiYnLgEnLgEnLgE1NDY3PgE3MSE+ATc+ATMyFhceARceARceARcjLgEnLgEnLgEnLgEjIgYHDgEHDgEHDgEVFBYXHgEXHgEXHgEzMjY3PgE3Mw4BBw4BBw4BBw4BIyImJy4BJy4BJy4BNTQ2Nz4BNzFABYD6gAU2+xQE7PwjDiETFCwYEiEQDxwNDBQICAsCWwEFBQQKBgcOCAgQCRAaCwsSBwcKAwMDAwMDCgcHEgsLGhAWIQwMDwNaAgkHCBMNDBwRECQUGCwUEyEODRUHBwcHBwcVDQG6DSEUEywZESEQEBwMDBUICAoCWgIFBAQLBgYOCAgRCBAbCwsSBwcKAwMDAwMDCgcHEgsLGxAVIgwMDgNbAgkIBxQMDB0QESQTGSwTFCENDhQHBwgIBwcUDgADgPyAAzX9FgLq1xAYCAkJBQUFDwoKGA8OIRMJDgcHDAUFCAMDAgYGBhEKChgNDRwODRoNDRcKChEGBgYNDQ4iFhQkERAcCwsSBgYGCQgJFw8PJBQULBcYLRUUJA8QGAgJCQUFBQ8KChgPDiETCQ4HBwwFBQgDAwIGBgYRCgoYDQ0cDg0aDQ0XCgoRBgYGDQ0OIhYUJBEQHAsLEgYGBgkICRcPDyQUFCwXGC0VFCQPAAAAAAMAQAAABcADgAAQAG8AzgAAJSEiJjURNDYzITIWFREUBiMBPgE3PgE3PgEzMhYXHgEXHgEXHgEXMy4BJy4BJy4BJy4BIyIGBw4BBw4BBw4BFRQWFx4BFx4BFx4BMzI2Nz4BNz4BNz4BNyMOAQcOASMiJicuAScuAScuATU0Njc5ASE+ATc+ATc+ATMyFhceARceARceARczLgEnLgEnLgEnLgEjIgYHDgEHDgEHDgEVFBYXHgEXHgEXHgEzMjY3PgE3PgE3PgE3Iw4BBw4BIyImJy4BJy4BJy4BNTQ2NzkBBSz7qD1XVz0EWD1XVz38mgMKBwYSCwsbEAkQCAgOBgcKBAQGAVoCCggIFA0MHBAPIRIYLBQTIQ4NFQcHBwcHBxUNDiETFCsZFCQQEB0MDBQIBwkCWgMPDAwiFRAbCwsSBgcKAwQDAwQBuQMKBwcSCwsbEAgRCAcPBgYLBAQFAVsCCggIFQwMHBAQIREZLBMUIQ0OFAcHCAgHBxQODSEUEywZEyQREB0MDBQHCAkCWwMODA0hFRAbCwsSBwcKAwMDAwMAVz4CVj5XVz79qj5XAfQNGAoLEAYGBgIDAwgFBQwHBw4JEyEODxgKCg4GBQUJCQgYEA8kFBUtGBcsFBQkDw8XCQgJBgYGEgsLHBARJBQWIg4NDQYGBhEKChcNDRoODhsNDRgKCxAGBgYCAwMIBQUMBwcOCRMhDg8YCgoOBgUFCQkIGBAPJBQVLRgXLBQUJA8PFwkICQYGBhILCxwQESQUFiIODQ0GBgYRCgoXDQ0aDg4bDQAAAAEA4ACgAyAC4AAUAAABFA4CIyIuAjU0PgIzMh4CFQMgLU5pPDxpTi0tTmk8PGlOLQHAPGlOLS1OaTw8aU4tLU5pPAAAAwBAABAEQAOQAAMAEAAfAAA3CQEhJTI2NTQmIyIGFRQWMxM0JiMiBhURFBYzMjY1EUACAAIA/AACAA4VFQ4OFRUOIxUODhUVDg4VEAOA/IBwFhAPGBYREBYB5g8YFhH+1w8YFhEBKQACAEAAAAPAA4AABwAPAAA3ERc3FwcXIQEhEScHJzcnQICwjbCA/nMB8wGNgLCNsIAAAY2AsI2wgAOA/nOAsI2wgAAAAAUAQAAABcADgAAEAAkAFgAzAE8AADcRIREhASERIREBMzUzESM1IxUjETMVJR4BFx4BFx4BFRQGBw4BBw4BBw4BKwERMx4BFzEHETMyNjc+ATc+ATc+ATU0JicuAScuAScuASsBQAWA+oAFNvsUBOz8VLZgYLZkZAJcFB4ODxQHBwkGBwcTDAwhExMsG7CwFioToEcJFwkKEggHDQUFBQQDAg0HBxMMDCARNwADgPyAAzb9FwLp/sCt/kDAwAHArZ0HFA4PIhYVMBsYLhMWIg8OGAoJBwHAAgcHQ/7mBAMCDQcHFwwMIRMRHQ8OGAoJDwQFBQAEAD0AAAXAA4AAEAAdADsAWQAAJSEiJjURNDYzITIWFREUBiMBIxUjNSMRMzUzFTMRBS4BJy4BJy4BKwERMzI2Nz4BNz4BNz4BNS4BJzkBBw4BBw4BKwERMzIWFx4BFx4BFx4BFRQGBw4BBzkBBSr7qT5YWD4EVz5YWD79s2CzZGSzYAGwBxcMDh8TEycWsLAbLBMTHg8MEwcHBgIICXcIEgoJFAxHNxMeDAwTBwcLBAMEBQUFDQcAWD4CVD5YWD79rD5YAp2trf5AwMABwH0WIg4PFAcHBv5ACQcJGA8OJRMWLBgdMBbiCA0CAwQBHwQFBQ8KChgPDh8REx8PDBcHAAAAAAEAQwAGA6ADegCPAAATFCI1JzAmJzQmJy4BBw4BFRwBFR4BFx4BNz4BNz4BMTYyFTAUBxQWFx4BFx4BMzI2Nz4BNz4BNz4DMTYyFTAOAhUUFhceARcWNjc+ATc+ATc+ATc+Azc8ATU0JicmBgcOAzEGIjUwPAI1NCYnJgYHDgEHDgMxBiI1MCYnLgEnJgYHDgExFZAGBAgECAIFDgoHCQMWBwchDxAPBwc2BQQDAwcFCwkKFwwFDQUHCwcMEwgJHxwVBQUDBAMIBQcVEREkEQwTCAkSBQ4ZDAYyOC8ECQcMFgQDPUg8BAUvJBEiERAWCgYiJRwFBQQCAxgoJyYKBywBugUFDBgPBQoHBQQCAw0HBwsHFGsOEQ0HCCAPE5MKCp0wEzURDBMIBwUEAgUKBw8jDhRDQjEKClNzeCUTIxEQGAgHAQoHFQoRJRAnTSYTr8msDwUGAgcNAwQICQaVrY8JCTVUZjE1OwcCBQoKHBALYGpVCgp6Fh9PBQNHHxOQAwAAAAACAEYAAAQ2A4AABAAIAAAlETMRIwkCEQPGcHD8gAOA/IAAA4D8gAOA/kD+QAOAAAACAIAAAAOAA4AABAAJAAAlESERIQEhESERAoABAP8A/gABAP8AAAOA/IADgPyAA4AAAAAAAQCAAAAEAAOAAAMAAAkBEQEEAPyAA4ABwP5AA4D+QAACAEoAAAQ6A4AABAAIAAA3ESMRMwkCEbpwcAOA/IADgAADgPyAA4D+QP5AA4AAAAABAEMAIANDA6AAKQAAAR4BFRQOAiMiLgI1ND4CMzUNATUiDgIVFB4CMzI+AjU0Jic3Aw0aHDxpjE9PjGg9PWiMTwFA/sA8aU4tLU5pPDxpTi0WE1MCYytjNU+MaD09aIxPT4xoPYCts4AtTmk8PGlOLS1OaTwpSx8wAAAAAQBAAGYCIAMTAAYAABMRMyURJSNAzQET/u3NATMBE839U80ABABAAAAEkAOAABcAKwA6AEEAACUnPgM1NC4CJzceAxUUDgIHMS8BPgE1NCYnNx4DFQ4DBzEnPgE1NCYnNx4BFRQGBycFETMlESUjA9oqJj4rFxcrPiYqKkMwGRkwQyqkKTlERDkpITQkFAEUJDMhwyIoKCIqJjAwJir9zc0BE/7tzQAjKF5pcjw8cWleKSMrZnJ8QUF8cmYrgyM1kVRUkTUjHkhQWC8vWVFHHbMWSCkrRhUjHFw0NVkfKQMBE839U80AAAIAQAAAA8MDgAAHAA8AAAEXIREXNxcHAQcnNychEScDKnD+qXCXeZn+JpZ6lnABV20Cc3ABV3CWepP+I5Z6lnD+qmwAAwBAARMFwAJgAAwAGQAmAAABFAYjIiY1NDYzMhYVIRQGIyImNTQ2MzIWFSEUBiMiJjU0NjMyFhUBjWJFRWFhRUViAhZhRUViYkVFYQIdYUVFYmJFRWEBukViYkVFYWFFRWJiRUVhYUVFYmJFRWFhRQAAAAABAGYAJgOaA1oAIAAAARcWFAcGIi8BBwYiJyY0PwEnJjQ3NjIfATc2MhcWFA8BAqP3JCQkYx329iRjHSQk9/ckJCRjHfb2JGMdJCT3AcD2JGMdJCT39yQkJGMd9vYkYx0kJPf3JCQkYx32AAAGAEQABAO8A3wABAAJAA4AEwAYAB0AAAEhFSE1ESEVITURIRUhNQEzFSM1ETMVIzURMxUjNQGnAhX96wIV/esCFf3r/p2ysrKysrIDfFlZ/p1ZWf6dWVkCxrKy/p2ysv6dsrIAAQAAAAEZmqHTqz9fDzz1AAsEAAAAAADR1CIrAAAAANHUIisAAAAABcADoAAAAAgAAgAAAAAAAAABAAADwP/AAAAGAAAAAAAFwAABAAAAAAAAAAAAAAAAAAAAGwQAAAAAAAAAAAAAAAIAAAAGAABgBAAAQAUAAEAFAABABgAAQAYAAEAEAADgBIAAQAQAAEAGAABABgAAPQPgAEMEgABGBAAAgAQAAIAEgABKA4AAQwTAAEAEwABABAAAQAYAAEAEAABmBAAARAAAAAAACgAUAB4AiAC4AQQBYAKGA7ID1AQIBCoEpAUmBegGAAYaBioGQgaABpIG9AcWB1AHhge4AAEAAAAbAM8ABgAAAAAAAgAAAAAAAAAAAAAAAAAAAAAAAAAOAK4AAQAAAAAAAQAMAAAAAQAAAAAAAgAHAI0AAQAAAAAAAwAMAEUAAQAAAAAABAAMAKIAAQAAAAAABQALACQAAQAAAAAABgAMAGkAAQAAAAAACgAaAMYAAwABBAkAAQAYAAwAAwABBAkAAgAOAJQAAwABBAkAAwAYAFEAAwABBAkABAAYAK4AAwABBAkABQAWAC8AAwABBAkABgAYAHUAAwABBAkACgA0AOBqdy1zaXgtaWNvbnMAagB3AC0AcwBpAHgALQBpAGMAbwBuAHNWZXJzaW9uIDEuMQBWAGUAcgBzAGkAbwBuACAAMQAuADFqdy1zaXgtaWNvbnMAagB3AC0AcwBpAHgALQBpAGMAbwBuAHNqdy1zaXgtaWNvbnMAagB3AC0AcwBpAHgALQBpAGMAbwBuAHNSZWd1bGFyAFIAZQBnAHUAbABhAHJqdy1zaXgtaWNvbnMAagB3AC0AcwBpAHgALQBpAGMAbwBuAHNGb250IGdlbmVyYXRlZCBieSBJY29Nb29uLgBGAG8AbgB0ACAAZwBlAG4AZQByAGEAdABlAGQAIABiAHkAIABJAGMAbwBNAG8AbwBuAC4AAAADAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"
    }, function(a, b, c) {
        function d(a, b) {
            for (var c = 0; c < a.length; c++) {
                var d = a[c],
                    e = l[d.id];
                if (e) {
                    e.refs++;
                    for (var f = 0; f < e.parts.length; f++) e.parts[f](d.parts[f]);
                    for (; f < d.parts.length; f++) e.parts.push(h(d.parts[f], b))
                } else {
                    for (var g = [], f = 0; f < d.parts.length; f++) g.push(h(d.parts[f], b));
                    l[d.id] = {
                        id: d.id,
                        refs: 1,
                        parts: g
                    }
                }
            }
        }

        function e(a) {
            for (var b = [], c = {}, d = 0; d < a.length; d++) {
                var e = a[d],
                    f = e[0],
                    g = e[1],
                    h = e[2],
                    i = e[3],
                    j = {
                        css: g,
                        media: h,
                        sourceMap: i
                    };
                c[f] ? c[f].parts.push(j) : b.push(c[f] = {
                    id: f,
                    parts: [j]
                })
            }
            return b
        }

        function f() {
            var a = document.createElement("style"),
                b = o();
            return a.type = "text/css", b.appendChild(a), a
        }

        function g() {
            var a = document.createElement("link"),
                b = o();
            return a.rel = "stylesheet", b.appendChild(a), a
        }

        function h(a, b) {
            var c, d, e;
            if (b.singleton) {
                var h = q++;
                c = p || (p = f()), d = i.bind(null, c, h, !1), e = i.bind(null, c, h, !0)
            } else a.sourceMap && "function" == typeof URL && "function" == typeof URL.createObjectURL && "function" == typeof URL.revokeObjectURL && "function" == typeof Blob && "function" == typeof btoa ? (c = g(), d = k.bind(null, c), e = function() {
                c.parentNode.removeChild(c), c.href && URL.revokeObjectURL(c.href)
            }) : (c = f(), d = j.bind(null, c), e = function() {
                c.parentNode.removeChild(c)
            });
            return d(a),
                function(b) {
                    if (b) {
                        if (b.css === a.css && b.media === a.media && b.sourceMap === a.sourceMap) return;
                        d(a = b)
                    } else e()
                }
        }

        function i(a, b, c, d) {
            var e = c ? "" : d.css;
            if (a.styleSheet) a.styleSheet.cssText = r(b, e);
            else {
                var f = document.createTextNode(e),
                    g = a.childNodes;
                g[b] && a.removeChild(g[b]), g.length ? a.insertBefore(f, g[b]) : a.appendChild(f)
            }
        }

        function j(a, b) {
            var c = b.css,
                d = b.media;
            b.sourceMap;
            if (d && a.setAttribute("media", d), a.styleSheet) a.styleSheet.cssText = c;
            else {
                for (; a.firstChild;) a.removeChild(a.firstChild);
                a.appendChild(document.createTextNode(c))
            }
        }

        function k(a, b) {
            var c = b.css,
                d = (b.media, b.sourceMap);
            d && (c += "\n/*# sourceMappingURL=data:application/json;base64," + btoa(unescape(encodeURIComponent(JSON.stringify(d)))) + " */");
            var e = new Blob([c], {
                    type: "text/css"
                }),
                f = a.href;
            a.href = URL.createObjectURL(e), f && URL.revokeObjectURL(f)
        }
        var l = {},
            m = function(a) {
                var b;
                return function() {
                    return "undefined" == typeof b && (b = a.apply(this, arguments)), b
                }
            },
            n = m(function() {
                return /msie [6-9]\b/.test(window.navigator.userAgent.toLowerCase())
            }),
            o = m(function() {
                return document.head || document.getElementsByTagName("head")[0]
            }),
            p = null,
            q = 0;
        a.exports = function(a, b) {
            b = b || {}, "undefined" == typeof b.singleton && (b.singleton = n());
            var c = e(a);
            return d(c, b),
                function(a) {
                    for (var f = [], g = 0; g < c.length; g++) {
                        var h = c[g],
                            i = l[h.id];
                        i.refs--, f.push(i)
                    }
                    if (a) {
                        var j = e(a);
                        d(j, b)
                    }
                    for (var g = 0; g < f.length; g++) {
                        var i = f[g];
                        if (0 === i.refs) {
                            for (var k = 0; k < i.parts.length; k++) i.parts[k]();
                            delete l[i.id]
                        }
                    }
                }
        };
        var r = function() {
            var a = [];
            return function(b, c) {
                return a[b] = c, a.filter(Boolean).join("\n")
            }
        }()
    }, function(a, b, c) {
        var d, e;
        d = [c(42), c(45), c(59), c(48), c(89), c(51), c(127), c(95), c(101), c(96), c(83), c(46), c(62), c(114), c(70), c(162), c(67), c(98)], e = function(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r) {
            var s = {};
            return s.api = a, s._ = b, s.version = c, s.utils = b.extend(d, f, {
                canCast: p.available,
                key: h,
                extend: b.extend,
                scriptloader: i,
                rssparser: q,
                tea: j,
                UI: g
            }), s.utils.css.style = s.utils.style, s.vid = k, s.events = b.extend({}, l, {
                state: m
            }), s.playlist = b.extend({}, n, {
                item: o
            }), s.plugins = r, s.cast = p, s
        }.apply(b, d), !(void 0 !== e && (a.exports = e))
    }])
});

function play_video(pos, file, image, width, height, auto) {
    jwplayer(pos).setup({
        file: file,
        image: image,
        abouttext: 'ONENET - jwplayer build 01032015',
        width: width,
        height: height,
        stretching: 'exactfit',
        autostart: auto,
        logo: {
            file: '',
            link: '',
        }
    });
}