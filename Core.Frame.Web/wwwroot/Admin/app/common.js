﻿/** common.js By zouqj */
layui.define(['layer'], function (exports) {
    "use strict";

    var $ = layui.jquery,
		layer = layui.layer,
        form = parent.layui.form;

    var common = {
        //加载进度框
        startPageLoading: function(options) {
            if (options && options.animate) {
                $('.page-spinner-bar').remove();
                $('body').append('<div class="page-spinner-bar"><div class="bounce1"></div><div class="bounce2"></div><div class="bounce3"></div></div>');
            } else {
                $('.page-loading').remove();
                $('body').append('<div class="page-loading"><img src="' + this.getGlobalImgPath() + 'loading-spinner-grey.gif"/>&nbsp;&nbsp;<span>' + (options && options.message ? options.message : 'Loading...') + '</span></div>');
            }
        },
        stopPageLoading: function () {
            $('.page-loading, .page-spinner-bar').remove();
        },
        close:function(index){
            layer.close(index);
        },
        openTop: function (options) {
            var that = this; 
            //多窗口模式，层叠置顶
            layer.open({
                type: 2 //此处以iframe举例
              , title: options.title
              , area: options.w===undefined ? 'auto' : [options.w, options.h]
              //,shade: 0
              ,maxmin: true
              ,offset: 'auto'
              ,content: options.content
              , btn: options.detail ? ['确定'] : options.btn  //只是为了演示
              , yes: function (index, layero) {
                  if (options.clickOK) {
                      options.clickOK(index,layero);
                   }
              }
              ,btn2: function(){
                  layer.closeAll();
              }     
              ,zIndex: layer.zIndex //重点1
              ,success: function (layero) {
                  layer.setTop(layero); //重点2
              }
            });
        },
      
        msgError: function (msg) {
            layer.msg(msg, {
                icon: 5
            });
            return;
        },
        msgSuccess: function (msg) {
            layer.msg(msg, {
                icon: 6  //1:勾 6：笑脸
            });
            return;
        },
        zxjs: function (id) {
            $("#d_"+id).addClass("editdivred");
            return;
        }
    };

    exports('common', common);
});
