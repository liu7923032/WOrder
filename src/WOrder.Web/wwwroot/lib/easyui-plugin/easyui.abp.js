
var abp = abp || {};


//abp.notify
(function () {

    /* NOTIFICATION *********************************************/

    var showNotification = function (type, message, title, options) {
        $.messager.alert(title, message, type);
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

    /* DEFAULTS *************************************************/

    abp.libs = abp.libs || {};

    /* MESSAGE **************************************************/

    var showMessage = function (type, message, title) {
        var iconValue = 6;
        //error, question, info, warning
        //switch (type) {
        //    case "info":
        //        iconValue = 6;
        //        break;
        //    case "success":
        //        iconValue = 1;
        //        break;
        //    case "warn":
        //        iconValue = 0;
        //        break;
        //    case "error":
        //        iconValue = 2;
        //        break;
        //}

        // show message window on top center
        $.messager.show({
            title: title,
            msg: message,
            showType: 'show',
            style: {
                right: '',
                top: document.body.scrollTop + document.documentElement.scrollTop,
                bottom: ''
            }
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
            title: '你确定?',
            btn: ["确定", "取消"],
        };

        if (typeof (titleOrCallback) == "function") {
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        };

        $.messager.confirm(userOpts.title, message, function (r) {
            if (r) {
                // 退出操作;
                callback();
            }
        });
    };

    //prompt
    abp.message.prompt = function (message, titleOrCallback, callback) {

        var title = "提示信息";

        if (typeof (titleOrCallback) == "function") {
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            title = titleOrCallback;
        };

        $.messager.prompt(title, message, function (r) {
            if (r) {
                callback(r)
            }
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


    abp.ui.block = function (elm) {
        $.messager.progress();
    };

    abp.ui.unblock = function (elm) {
        $.messager.progress('close');
    };

})();

//abp.spin
(function () {


    abp.ui.setBusy = function (elm, optionsOrPromise) {
        abp.ui.block();
    };

    abp.ui.clearBusy = function (elm) {
        //TODO@Halil: Maybe better to do not call unblock if it's not blocked by setBusy
        abp.ui.unblock(elm);
    };

})();





