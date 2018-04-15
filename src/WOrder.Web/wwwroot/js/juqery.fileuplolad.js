
(function ($) {
    $.fn.fileUpload = function (options) {
        // default configuration properties
        var defaults = {
            btnText: "选择文件",
            allowedExtensions: "",
            invalidExtError: "失效文件",
            maxSize: 0,
            sizeError: "文件大小超出限制",
            showPreview: true,
            showFilename: true,
            showPercent: true,
            showErrorAlerts: true,
            errorOnResponse: "上传文件出错",
            onSubmit: false,
            url: "upload.php",
            data: null,
            limit: 0,
            limitError: "你超出了上传文件的大小限制",
            delfiletext: "移除文件",
            error: function (file, error) { },
            success: function (file, data) { }
        };
        var options = $.extend(defaults, options);
        var fileUpload = {
            obj: $(this),
            files: [],
            uparea: null,
            container: null,
            uploadedfiles: 0,
            hasErrors: false,
            init: function () {
                this.replacehtml();
                this.uparea.on("click", function () {
                    fileUpload.selectfiles();
                });

                ///Handle events when drag
                if (options.dragMode) {
                    this.handledragevents();
                }
                this.handlebuttonevents();
                //Dismiss all warnings
                $(document).on("click", ".pkwrncl", function () {
                    $(this).parent("div").remove();
                });
                //Bind event if is on Submit
                if (options.onSubmit) {
                    this.handleFormSubmission();
                }
            },
            replacehtml: function () {
                var fileInput = '<div style="width:120px;height:120px;position:relative">'+
　　　　                            '<img src="/image/uploadfile.png" style="width:100%;height:100%" id="uploadImg">'+
　　　　                            '<input type="file" style="display:none;position:absolute;width:100%;height:100%;top:0;left:0;z-index:1000;opacity:0" id="chooseFile">'+
　　                              '</div>';
                $(fileInput).insertAfter(this.obj);
                var html = '<a href="javascript:void(0)" class="fileUpload-btn-file pkuparea">' + options.btnText + "</a>";;
                this.uparea = $(html).insertAfter(this.obj);
                this.container = $('<div class="pekecontainer"><ul></ul></div>').insertAfter(this.uparea);
                this.obj.hide();
            },
            selectfiles: function () {
                this.obj.click();
            },
            handlebuttonevents: function () {

                $(document).on("change", this.obj.selector, function () {
                    console.log("change")
                    console.log(fileUpload.obj[0].files)
                    fileUpload.checkFile(fileUpload.obj[0].files[0]);
                });

                $(document).on('click', '.pkdel', function () {
                    var parent = $(this).parent('div').parent('div');
                    fileUpload.delAndRearrange(parent);
                });
            },
            checkFile: function (file) {
                error = this.validateFile(file);
                if (error) {
                    if (options.showErrorAlerts) {
                        this.addWarning(error);
                    }
                    this.hasErrors = true;
                    options.error(file, error);
                } else {
                    this.files.push(file);
                    if (this.files.length > options.limit && options.limit > 0) {
                        this.files.splice(this.files.length - 1, 1);
                        if (options.showErrorAlerts) {
                            this.addWarning(options.limitError, this.obj);
                        }
                        this.hasErrors = true;
                        options.error(file, error);
                    } else {
                        this.addRow(file);
                        if (options.onSubmit == false) {
                            this.upload(file, this.files.length - 1);
                        }
                    }
                }
            },
            addWarning: function (error, c) {
                var html = '<div class="alert-fileUpload"><button type="button" class="close pkwrncl" data-dismiss="alert">&times;</button> ' + error + "</div>";
                if (!c) {
                    this.container.append(html);
                } else {
                    $(html).insertBefore(c);
                }
            },
            validateFile: function (file) {
                if (!this.checkExtension(file)) {
                    return options.invalidExtError;
                }
                if (!this.checkSize(file)) {
                    return options.sizeError;
                }
                return null;
            },
            checkExtension: function (file) {
                if (options.allowedExtensions == "") {
                    return true;
                }
                var ext = file.name.split(".").pop().toLowerCase();
                var allowed = options.allowedExtensions.split("|");
                if ($.inArray(ext, allowed) == -1) {
                    return false;
                } else {
                    return true;
                }
            },
            checkSize: function (file) {
                if (options.maxSize == 0) {
                    return true;
                }
                if (file.size > options.maxSize) {
                    return false;
                } else {
                    return true;
                }
            },
            addRow: function (file) {
                var i = this.files.length - 1;

                var newRow = $('<div class="pekerow pkrw" rel="' + i + '"></div>').appendTo(this.container);
                if (options.showPreview) {
                    var prev = $('<div class="pekeitem_preview"></div>').appendTo(newRow);
                    this.previewFile(prev, file);
                }
                var finfo = $('<div class="file"></div>').appendTo(newRow);
                if (options.showFilename) {
                    finfo.append('<div class="filename">' + file.name + "</div>");
                }
                if (options.notAjax == false) {
                    var progress = $('<div class="progress-fileUpload"><div class="pkuppbr bar-fileUpload pekeup-progress-bar" style="min-width: 2em;width:0%"><span></span></div></div>').appendTo(finfo);
                    if (options.showPercent) {
                        progress.find("div.bar-fileUpload").text("0%");
                    }
                }
                var dismiss = $('<div class="pkdelfile"></div>').appendTo(newRow);
                $('<a href="javascript:void(0);" class="delbutton pkdel">' + options.delfiletext + '</a>').appendTo(dismiss);
            },
            previewFile: function (container, file) {
                var type = file.type.split("/")[0];
                switch (type) {
                    case "image":
                        var fileUrl = window.URL.createObjectURL(file);
                        var prev = $('<img class="thumbnail" src="' + fileUrl + '" height="64" />').appendTo(container);
                        break;

                    case "video":
                        var fileUrl = window.URL.createObjectURL(file);
                        var prev = $('<video src="' + fileUrl + '" width="100%" controls></video>').appendTo(container);
                        break;

                    case "audio":
                        var fileUrl = window.URL.createObjectURL(file);
                        var prev = $('<audio src="' + fileUrl + '" width="100%" controls></audio>').appendTo(container);
                        break;

                    default:
                        var prev = $('<div class="fileUpload-item-file"></div>').appendTo(container);
                        break;
                }
            },
            upload: function (file, pos) {

                var formData = new FormData();
                formData.append(this.obj.attr("name"), file);
                for (var key in options.data) {
                    formData.append(key, options.data[key]);
                }
                $.ajax({
                    url: options.url,
                    type: "POST",
                    data: formData,
                    dataType: "json",
                    success: function (data) {
                        if (data == 1 || data.success == 1) {
                            fileUpload.files[pos] = null;
                            $('div.row[rel="' + pos + '"]').find(".pkuppbr").css("width", "100%");
                            options.success(file, data);
                        } else {
                            fileUpload.files.splice(pos, 1);
                            var err = null;
                            if (error in data) {
                                err = null;
                            } else {
                                err = options.errorOnResponse;
                            }
                            if (options.showErrorAlerts) {
                                fileUpload.addWarning(err, $('div.row[rel="' + pos + '"]'));
                            }
                            $('div.row[rel="' + pos + '"]').remove();
                            fileUpload.hasErrors = true;
                            options.error(file, err);
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        fileUpload.files.splice(pos, 1);
                        if (options.showErrorAlerts) {
                            fileUpload.addWarning(thrownError, $('div.pkrw[rel="' + pos + '"]'));
                        }
                        fileUpload.hasErrors = true;
                        options.error(file, thrownError);
                        $('div.pkrw[rel="' + pos + '"]').remove();
                    },
                    xhr: function () {
                        myXhr = $.ajaxSettings.xhr();
                        if (myXhr.upload) {
                            myXhr.upload.addEventListener("progress", function (e) {
                                fileUpload.handleProgress(e, pos);
                            }, false);
                        }
                        return myXhr;
                    },
                    complete: function () {
                        if (options.onSubmit) {
                            fileUpload.uploadedfiles++;
                            if (fileUpload.uploadedfiles == fileUpload.files.length && fileUpload.hasErrors == false) {
                                fileUpload.obj.remove();
                                fileUpload.obj.parent("form").submit();
                            }
                        }
                    },
                    cache: false,
                    contentType: false,
                    processData: false
                });
            },
            handleProgress: function (e, pos) {
                if (e.lengthComputable) {
                    var total = e.total;
                    var loaded = e.loaded;
                    var percent = Number((e.loaded * 100 / e.total).toFixed(2));
                    var progressbar = $('div.pkrw[rel="' + pos + '"]').find(".pkuppbr");
                    progressbar.css("width", percent + "%");
                    if (options.showPercent) {
                        progressbar.text(percent + "%");
                    }
                }
            },
            handleFormSubmission: function () {
                var form = this.obj.parent("form");
                form.submit(function () {
                    fileUpload.hasErrors = false;
                    fileUpload.uploadedfiles = 0;
                    for (var i = 0; i < fileUpload.files.length; i++) {
                        if (fileUpload.files[i]) {
                            fileUpload.upload(fileUpload.files[i], i);
                        } else {
                            fileUpload.uploadedfiles++;
                            if (fileUpload.uploadedfiles == fileUpload.files.length && fileUpload.hasErrors == false) {
                                fileUpload.obj.remove();
                                return true;
                            }
                        }
                    }
                    return false;
                });
            },
            delAndRearrange: function (parent) {
                var id = parent.attr('rel');
                fileUpload.files.splice(parseInt(id), 1);
                parent.remove();
                fileUpload.container.find('div.pkrw').each(function (index) {
                    $(this).attr('rel', index);
                });
            }
        };
        fileUpload.init();
    };
})(jQuery);