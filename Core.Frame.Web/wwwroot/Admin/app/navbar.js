﻿layui.define(['element', 'common'], function (exports) {
    "use strict";
    var $ = layui.jquery,
        layer = parent.layer === undefined ? layui.layer : parent.layer,
        element = layui.element,
        common = layui.common,
        cacheName = 'tb_navbar';

    var Navbar = function () {
        /**
		 *  默认配置 
		 */
        this.config = {
            elem: undefined, //容器
            data: undefined, //数据源
            url: undefined, //数据源地址
            type: 'GET', //读取方式
            cached: false, //是否使用缓存
            spreadOne: false //设置是否只展开一个二级菜单
        };
        this.v = '0.0.1';
    };

    Navbar.prototype.render = function () {
        var _that = this;
        var _config = _that.config;
        if (typeof (_config.elem) !== 'string' && typeof (_config.elem) !== 'object') {
            common.throwError('Navbar error: elem参数未定义或设置出错，具体设置格式请参考文档API.');
        }
        var $container;
        if (typeof (_config.elem) === 'string') {
            $container = $('' + _config.elem + '');
        }
        if (typeof (_config.elem) === 'object') {
            $container = _config.elem;
        }
        if ($container.length === 0) {
            common.throwError('Navbar error:找不到elem参数配置的容器，请检查.');
        }
        if (_config.data === undefined && _config.url === undefined) {
            common.throwError('Navbar error:请为Navbar配置数据源.')
        }
        if (_config.data !== undefined && typeof (_config.data) === 'object') {
            var html = getHtml(_config.data);
            $container.html(html);
            element.init();
            _that.config.elem = $container;
        } else {
            if (_config.cached) {
                var cacheNavbar = layui.data(cacheName);
                if (cacheNavbar.navbar === undefined) {
                    $.ajax({
                        type: _config.type,
                        url: _config.url,
                        async: false, //_config.async,
                        dataType: 'json',
                        success: function (result, status, xhr) {
                            //添加缓存
                            layui.data(cacheName, {
                                key: 'navbar',
                                value: result
                            });
                            var html = getHtml(result.value);
                            $container.html(html);
                            element.init();
                        },
                        error: function (xhr, status, error) {
                            common.msgError('Navbar error:' + error);
                        },
                        complete: function (xhr, status) {
                            _that.config.elem = $container;
                        }
                    });
                } else {
                    var html2 = getHtml(cacheNavbar.navbar);
                    $container.html(html2);
                    element.init();
                    _that.config.elem = $container;
                }
            } else {
                //清空缓存
                layui.data(cacheName, null);
                $.ajax({
                    type: _config.type,
                    url: _config.url,
                    async: false, //_config.async,
                    dataType: 'json',
                    success: function (result, status, xhr) {
                        debugger;
                        var html = getHtml(result.value);
                        $container.html(html);
                        element.init();
                    },
                    error: function (xhr, status, error) {
                        common.msgError('Navbar error:' + error);
                    },
                    complete: function (xhr, status) {
                        _that.config.elem = $container;
                    }
                });
            }
        }

        //只展开一个二级菜单
        if (_config.spreadOne) {
            var $ul = $container.children('ul');
            $ul.find('li.layui-nav-item').each(function () {
                $(this).on('click', function () {
                    $(this).siblings().removeClass('layui-nav-itemed');
                });
            });
        }
        return _that;
    };

    Navbar.prototype.set = function (options) {
        var that = this;
        that.config.data = undefined;
        $.extend(true, that.config, options);
        return that;
    };

    Navbar.prototype.on = function (events, callback) {
        var that = this;
        var _con = that.config.elem;
        if (typeof (events) !== 'string') {
            common.throwError('Navbar error:事件名配置出错，请参考API文档.');
        }
        var lIndex = events.indexOf('(');
        var eventName = events.substr(0, lIndex);
        var filter = events.substring(lIndex + 1, events.indexOf(')'));
        if (eventName === 'click') {
            if (_con.attr('lay-filter') !== undefined) {
                _con.children('ul').find('li').each(function () {
                    var $this = $(this);
                    if ($this.find('dl').length > 0) {
                        var $dd = $this.find('dd').each(function () {
                            $(this).on('click', function () {
                                var $a = $(this).children('a');
                                var id = $a.data('id');
                                var href = $a.data('url');
                                var icon = $a.children('i:first').data('icon');
                                var title = $a.children('cite').text();
                                var data = {
                                    elem: $a,
                                    field: {
                                        id: id,
                                        href: href,
                                        icon: icon,
                                        title: title
                                    }
                                }
                                callback(data);
                            });
                        });
                    } else {
                        $this.on('click', function () {
                            var $a = $this.children('a');
                            var id = $a.data('id');
                            var href = $a.data('url');
                            var icon = $a.children('i:first').data('icon');
                            var title = $a.children('cite').text();
                            var data = {
                                elem: $a,
                                field: {
                                    id: id,
                                    href: href,
                                    icon: icon,
                                    title: title
                                }
                            }
                            callback(data);
                        });
                    }
                });
            }
        }
    };

    Navbar.prototype.cleanCached = function () {
        layui.data(cacheName, null);
    };

    var ulHtml;

    function getChildHtml(data) {
        if (data && data.children && data.children.length > 0) {
            ulHtml += '<dl class="layui-nav-child">'
            for (var j = 0; j < data.children.length; j++) {
                ulHtml += '<dd>';
                ulHtml += '<a data-id=' + data.children[j].id + ' href="javascript:;" data-url="' + data.children[j].moduleUrl + '?id=' + data.children[j].id + '" lay-href="' + data.children[j].moduleUrl + '">';
                if (data.children[j].icon !== undefined && data.children[j].icon !== '') {
                    if (data.children[j].icon.indexOf('icon-') !== -1) {
                        ulHtml += '<i class="iconfont icon ' + data.children[j].icon + '" data-icon="' + data.children[j].icon + '" aria-hidden="true"></i>';
                    } else {
                        ulHtml += '<i class="iconfont ' + data.icon + '" data-icon="' + data.children[j].icon + '"></i>';
                    }
                }
                ulHtml += '<cite>' + data.children[j].moduleName + '</cite>';
                ulHtml += '</a>';
                getChildHtml(data.children[j], '');
                ulHtml += '</dd>';
            }
            ulHtml += '</dl>';
        }
    }

    function getHtml(data) {
        debugger;
        ulHtml = '<ul class="layui-nav layui-nav-tree beg-navbar">';
        for (var i = 0; i < data.length; i++) {
            if (data[i].spread) {
                ulHtml += '<li class="layui-nav-item layui-nav-itemed">';
            } else {
                ulHtml += '<li class="layui-nav-item">';
            }
            if (data[i].children && data[i].children.length > 0) {
                ulHtml += '<a href="javascript:;">';
                if (data[i].icon !== undefined && data[i].icon !== '') {
                    if (data[i].icon.indexOf('icon-') === -1) {
                        ulHtml += '<i class="iconfont icon-' + data[i].icon + '" aria-hidden="true" data-icon="' + data[i].icon + '"></i>';
                    } else {
                        ulHtml += '<i class="iconfont ' + data[i].icon + '" data-icon="' + data[i].icon + '"></i>';
                    }
                }
                ulHtml += '<cite>' + data[i].moduleName + '</cite>'
                ulHtml += '</a>';
                //ulHtml += '<dl class="layui-nav-child">'
                getChildHtml(data[i]);

                //ulHtml += '</dl>';
            } else {
                var dataUrl = (data[i].moduleUrl !== undefined && data[i].moduleUrl !== '') ? 'data-url="' + data[i].moduleUrl + '" lay-href="' + data[i].moduleUrl + '"' : '';
                ulHtml += '<a href="javascript:;" ' + dataUrl + '>';
                if (data[i].icon !== undefined && data[i].icon !== '') {
                    if (data[i].icon.indexOf('icon-') === -1) {
                        ulHtml += '<i class="iconfont icon-' + data[i].icon + '" aria-hidden="true" data-icon="' + data[i].icon + '"></i>';
                    } else {
                        ulHtml += '<i class="iconfont ' + data[i].icon + '" data-icon="' + data[i].icon + '"></i>';
                    }
                }
                ulHtml += '<cite>' + data[i].moduleName + '</cite>'
                ulHtml += '</a>';
            }
            ulHtml += '</li>';
        }
        ulHtml += '</ul>';

        return ulHtml;
    }

    var navbar = new Navbar();

    exports('navbar', function (options) {
        return navbar.set(options);
    });
});