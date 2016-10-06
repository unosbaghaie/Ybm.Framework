

function DisplayModal(_url, windowName) {
    debugger;

        $.post(_url, null)
     .done(function (response) {
         //$("#listOfCustomers").html(response);

         var _windowForm = $(windowName);
         _windowForm.kendoWindow({
             scrollable: false
         });
         _windowForm.data("kendoWindow").center().open();
         _windowForm.html(response);

     });
}

function AjaxCall2(_url, seccess) {
    debugger;

    $.ajax({
                type: 'get',
                url: _url,
                //data: { Id: this.dataItem($(e.currentTarget).closest("tr")).Id },
                success: seccess
            });
}


//    $.ajax({
//        type: 'get',
//        url: _url,
//        //data: { Id: this.dataItem($(e.currentTarget).closest("tr")).Id },
//        success: function (data) {
//            debugger;
//            var _windowForm = $(windowName);
//            _windowForm.kendoWindow({
//                scrollable: false
//            });
//            _windowForm.data("kendoWindow").center().open();
//            _windowForm.html(data);
//        }
//    });
//}

var NotificationType = {
    success: 'success',
    info: 'info',
    error: 'error',
    dark: 'dark'

}
var ShowComfirmNotification = function (title, text, type) {
    var html = '<div id="form_notice" ><form class="pf-form pform_custom" action="#" method="post"> <div class="pf-element pf-heading"><h4>Login to Continue</h4><p>Just an example.</p></div><div class="pf-element"><label><span class="pf-label">Username</span><div class="form-group form-group-sm is-empty"><input class="pf-field form-control" type="text" name="username"><span class="material-input"></span></div></label></div><div class="pf-element"><label> <span class="pf-label">Password</span><div class="form-group form-group-sm is-empty"><input class="pf-field form-control" type="password" name="password"><span class="material-input"></span></div> </label></div> <div class="mb checkbox-inline"><label><input type="checkbox"> Remember Me</label></div><div><input class="pf-button btn btn-primary" type="submit" name="submit" value="Submit"><input class="pf-button btn btn-primary" type="button" name="cancel" value="Cancel"></div></form></div>';
    var notice = new PNotify({

        text: html,// $('#form_notice').html(),
        icon: false,
        width: 'auto',
        hide: false,
        styling: 'fontawesome',
        buttons: {
            closer: false,
            sticker: false
        },
        insert_brs: false
    });
    notice.get().find('form.pf-form').on('click', '[name=cancel]', function () {
        notice.remove();
    }).submit(function () {
        var username = $(this).find('input[name=username]').val();
        if (!username) {
            //alert('Please provide a username.');
            ShowNotification("خطا", 'Please provide a username', NotificationType.error);
            return false;
        }
        notice.update({
            title: 'Welcome',
            text: 'Successfully logged in as ' + username,
            icon: true,
            width: PNotify.prototype.options.width,
            hide: true,
            buttons: {
                closer: true,
                sticker: true
            },
            type: 'success'
        });
        return false;
    });

}
var ShowNotification = function (title, text, type, icon, width, minheight) {
    debugger;
    if (width === undefined)
        width = '300px'
    if (type === undefined)
        type = "info"
    if (text === undefined)
        text = "Notification Text"
    if (title === undefined)
        title = "Notification title"
    if (minheight === undefined)
        minheight = 'auto'
    new PNotify({
        title: title,
        text: text,
        icon: icon,
        styling: 'fontawesome',
        type: type,
        shadow: true,
        cornerclass: 'rtl',
        animate_speed: 'slow',
        width: width,
        min_height: minheight

    });
}
function ShowDeleteNotification(Id) {
    window.Id = Id;
    var html = '<div id=form_notice><form class="pf-form pform_custom" action=# method=post><div class="pf-element pf-heading"><p>آیا از حذف اطلاعات اطمینان دارید؟</p></div><div><input class="pf-button btn btn-danger" type=submit name=delete value=بله> <input class="pf-button btn btn-primary" type=button name=cancel value=خیر></div></form></div>';
    var notice = new PNotify({
        type: "warning",
        text: html,// $('#form_notice').html(),
        icon: false,
        width: 'auto',
        hide: false,
        styling: 'fontawesome',
        buttons: {
            closer: false,
            sticker: false
        },
        insert_brs: false
    });
    notice.get().find('form.pf-form').on('click', '[name=cancel]', function () {
        notice.remove();
    }).submit(function (Id) {
        debugger;
      //  var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        var Id = window.Id;
        var grid = $("div[data-role=grid]:first").data("kendoGrid");
        var data = grid.dataSource._params();
        var prepared = grid.dataSource.transport.parameterMap(data);
        prepared["Id"] = Id;
        $.ajax({
            type: 'POST',
            url: getCurentControllerUrl('Delete'),
            data: prepared,
            success: function () {
                debugger;
                // $("div[data-role=grid]:first").data("kendoGrid").dataSource.read();
                notice.remove();
                ShowNotification('', 'با موفقیت حذف گردید.', NotificationType.success);
              
            },
            error: function (e, xhr, s) {
               
                debugger;

                var errMassage = '';


                if (e.responseJSON && e.responseJSON.errors)
                {
                    $.map(e.responseJSON.errors, function (value) {
                        errMassage = errMassage + '\n' + value;
                    });

                    ShowNotification("خطا", errMassage, NotificationType.error);
                    return;
                }
                else {

                } ShowNotification('خطا', 'بروز خطا در عملیات حذف.', NotificationType.error);
            }
        });
 
        var kendoGrid = $("div[data-role=grid]:first").data("kendoGrid");
        kendoGrid.dataSource.filter({});
        return false;
    });
}

function ShowErrors(result) {

    var errMassage = '';
    if (result) {
        if (result.errors.length > 0) {
            $.map(result.errors, function (value) {
                errMassage = errMassage + '\n' + value;
            });
        }
        if (errMassage && errMassage != '')
            ShowNotification("خطا", errMassage, NotificationType.error);
            //alert(errMassage);
    }
}

function SubmitForm(e) {
    var form = $("#" + e.target.id).closest('form')
    var url = form.attr('action'),
    data = form.serialize();
    $.ajax({
        url: url,
        type: 'post',
        data: data,
        success: function () {
            if (form.attr('datawindowname') != undefined) {
                var window = form.attr('datawindowname');
                var _windowForm = $("#" + window);
                _windowForm.data("kendoWindow").close();
            }

            if (form.attr('datakendogridname') != undefined) {
                var kendogrid = form.attr('datakendogridname');
                $("#" + kendogrid).data("kendoGrid").dataSource.read();
            }
        }
    });
}

function JQueyAjaxCall(url, data, success, error) {
    $.skylo('start');

    setTimeout(function () {
        $.skylo('set', 50);
    }, 1000);

    $.ajax({
        url: url,
        type: 'post',
        data: data,
        success: function (respone) {
            if(success && $.isFunction(success))
                success(respone);
                setTimeout(function () {
                $.skylo('end');
            }, 1500);
        },
        error: function (respone) {

            debugger;
            var errMassage = '';
            var title = 'خطا';
            debugger;
            if (response.data) {

                if (response.data.length > 0) {
                    $.map(response.data, function (value) {
                        errMassage = errMassage + '\n' + showErrorMessage(value.errors);
                    });
                }
                else if (response.data.errors && response.data.errors.length > 0) {
                    errMassage = showErrorMessage(response.data.errors);
                }

                if (errMassage && errMassage != '')
                    ShowNotification("خطا", errMassage, NotificationType.error);
                //alert(errMassage);
            }


            if(error && $.isFunction(error))
                error(respone);
        }
    });
}