
var abp = abp || {};

var checkLayui = function () {
    if (!window.layui) {
        abp.log.error("window.layui 未被引用");
        return;
    }
};

var useLayer = function (callback) {
    if (layui.layer) {
       
        callback(layui.layer);
    }
    else {
        layui.use('layer', function () {
            var layer = layui.layer;
            callback(layer);
        });
    }
};

//abp.notify
(function () {

    checkLayui()

    /* NOTIFICATION *********************************************/

    var showNotification = function (type, message, title, options) {
        //toastr[type](message, title, options);

        useLayer(function (layer) {
            var iconValue = 6;
            switch (type) {
                case "info":
                    iconValue = 6;
                    break;
                case "success":
                    iconValue = 1;
                    break;
                case "warn":
                    iconValue = 0;
                    break;
                case "error":
                    iconValue = 2;
                    break;
            }
            layer.alert(message, {
                title: title,
                //content: message,
                //type: 1,
                icon: iconValue,
                skin: 'layui-layer-lan',
                anim: 4,
                //offset: 'rb'
            });
        });
    };


    abp.notify.success = function (message, title, options) {
        showNotification('success', message, title, options);
    };

    abp.notify.info = function (message, title, options) {
        showNotification('info', message, title, options);
    };

    abp.notify.warn = function (message, title, options) {
        showNotification('warning', message, title, options);
    };

    abp.notify.error = function (message, title, options) {
        showNotification('error', message, title, options);
    };
})();

//abp.message
(function () {
    checkLayui()

    /* DEFAULTS *************************************************/

    abp.libs = abp.libs || {};
    abp.libs.layuiAlert = {
        config: {
            'default': {

            },
            info: {
                type: 'info'
            },
            success: {
                type: 'success'
            },
            warn: {
                type: 'warning'
            },
            error: {
                type: 'error'
            },
            confirm: {
                type: 'warning',
                title: 'Are you sure?',
                btn: ["confirm", "cancel"],
                success: function () { },
                fail: function () { }
            }
        }
    };

    /* MESSAGE **************************************************/

    var showMessage = function (type, message, title) {
        var iconValue = 6;
        switch (type) {
            case "info":
                iconValue = 6;
                break;
            case "success":
                iconValue = 1;
                break;
            case "warn":
                iconValue = 0;
                break;
            case "error":
                iconValue = 2;
                break;
        }

        useLayer(function (layer) {
            layer.msg(message, {
                time: 4000
            });
        });
    };

    abp.message.info = function (message, title) {
        return showMessage('info', message, title);
    };

    abp.message.success = function (message, title) {
        return showMessage('success', message, title);
    };

    abp.message.warn = function (message, title) {
        return showMessage('warn', message, title);
    };

    abp.message.error = function (message, title) {
        return showMessage('error', message, title);
    };

    abp.message.confirm = function (message, titleOrCallback, callback) {
        var userOpts = {
            content: message,
            title: 'Are you sure?',
            btn: ["confirm", "cancel"],
        };

        useLayer(function (layer) {

            if (typeof (titleOrCallback)=="function") {
                callback = titleOrCallback;
            } else if (titleOrCallback) {
                userOpts.title = titleOrCallback;
            };
            var index = layer.confirm(message, userOpts, callback, function () {
                layer.close(index);
            });
        })
    };

    //prompt
    abp.message.prompt = function (message, titleOrCallback, callback) {

        useLayer(function (layer) {
            var title = "";

            if (typeof (titleOrCallback) == "function") {
                callback = titleOrCallback;
            } else if (titleOrCallback) {
                title = titleOrCallback;
            };

            var opts = {
                formType: 1,
                title: title
            };

            layer.prompt(opts, function (text, index) {
                layer.close(index);
                callback(text, index);
            });
        });
    };

    abp.event.on('abp.dynamicScriptsInitialized', function () {
        abp.libs.layuiAlert.config.confirm.title = abp.localization.abpWeb('AreYouSure');
        abp.libs.layuiAlert.config.confirm.cancelButtonText = abp.localization.abpWeb('Cancel');
        abp.libs.layuiAlert.config.confirm.confirmButtonText = abp.localization.abpWeb('Yes');
    });

})();


//abp.block
(function () {

    checkLayui()

    abp.ui.block = function (elm) {
        useLayer(function (layer) {
            layer.load(1, {
                shade: [0.3, '#eee'] //0.1透明度的白色背景
            });
        });
    };

    abp.ui.unblock = function (elm) {
        useLayer(function (layer) {
            layer.closeAll('loading')
        });
    };

})();

//abp.spin
(function () {

    checkLayui()
   
    abp.ui.setBusy = function (elm, optionsOrPromise) {
        abp.ui.block();
    };

    abp.ui.clearBusy = function (elm) {
        //TODO@Halil: Maybe better to do not call unblock if it's not blocked by setBusy
        abp.ui.unblock(elm);
    };

})();





