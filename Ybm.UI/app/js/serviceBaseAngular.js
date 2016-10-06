app.factory('serviceBaseAngular', ['$http','$q', function ($http, $q) {


   


    var showErrorMessage = function (errors) {
        var errMassage = '';
        if (errors && errors.length > 0) {
            $.map(errors, function (error) {
                errMassage = errMassage + '\n' + error;
            });
        }
        return errMassage;
    }
    var getUrlId = function () {
        var url = window.location.pathname;
        return (url.substring(url.lastIndexOf('/') + 1));
    }
    var ajaxCall = function (url, model, success, error, optoins, dataType) {
       
            $.skylo('start');

            //setTimeout(function () {
            //    $.skylo('set', 50);
            //}, 1000);

         
     
        var http = $http({
            url: url,
            method: "POST",
            dataType: dataType,
            data: model,
            contentType: 'application/json;',

        });
        //var OPTIONS = {
        //    theme: "sk-cube-grid",
        //    content: '',
        //    message: '',
        //    textColor: "white",
        //}
        //if (LoadingCounter <= 0) {
        //    HoldOn.open(OPTIONS);
        //    LoadingCounter++;
        //}

        var deferred = $q.defer();

        var onSuccess = function (response) {

            //HoldOn.close();
            //LoadingCounter--;

            //result = response.data;

            //if (!!result.notPermission && result.notPermission == true) {
            //    ShowNotification("خطا", "مجوز دسترسی به این بخش را ندارید", NotificationType.error);

            //    return;
            //}

            //if (!result.IsValid && result.MessageType == 1) {

            //    var errMassage = '';
            //    var alerttype = '';
            //    var title;

            //    if (result.Messages && result.Messages.length > 0) {
            //        $.map(result.Messages, function (value) {
            //            errMassage = errMassage + '\n' + value;

            //            if (result.AlertType == 3) {
            //                alerttype = NotificationType.error;
            //                title = 'خطا';
            //            }

            //            else if (result.AlertType == 2) {
            //                alerttype = NotificationType.info;
            //                title = 'اعلان ها';

            //            }
            //            else if (result.AlertType == 4) {
            //                alerttype = NotificationType.dark;
            //            }
            //            else if (result.AlertType == 1) {
            //                alerttype = NotificationType.success;
            //                title = 'انجام شد';
            //            }
            //        });
            //        ShowNotification(title, errMassage, alerttype);


            //    }
            //    else if (result.Message) {

            //        ShowNotification('خطا', result.Message, NotificationType.error);
            //    }
            //}

            deferred.resolve(response.data);

            if (angular.isDefined(success) && angular.isFunction(success)) {

                success(response.data);
            }
            //setTimeout(function () {
               $.skylo('end');
            //}, 1500);
          
        };

        var onFail = function (response) {

            //HoldOn.close();
            //LoadingCounter--;


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



            if (angular.isDefined(error) && angular.isFunction(error)) {
                error(response);
            }


            return deferred.reject(response);
        };

        http.then(onSuccess).catch(onFail);


        return deferred.promise;
    }

    var selectedFilters = function () {
        var selectedFilters = [];

        $('select[class~=FilterDescriptor]').each(function () {
            var propertyName = $(this).attr('Id');
            var propertyValue = $(this).val();
            selectedFilters.push(
                {
                    key: propertyName,
                    value: propertyValue
                });
            debugger;
        });

        $('input[type=text].FilterDescriptor').each(function () {
            var propertyName = $(this).attr('Id');
            var propertyValue = $(this).val();
            selectedFilters.push(
                {
                    key: propertyName,
                    value: propertyValue
                });
        });
        $('input[type=number].FilterDescriptor').each(function () {
            var propertyName = $(this).attr('Id');
            var propertyValue = $(this).val();
            selectedFilters.push(
                {
                    key: propertyName,
                    value: propertyValue
                });
        });

        $('input[type=checkbox].FilterDescriptor').each(function () {
            var propertyName = $(this).attr('Id');
            var propertyValue = $(this).prop('checked');
            selectedFilters.push(
                {
                    key: propertyName,
                    value: propertyValue
                });
        });

        
        debugger;
        return  selectedFilters;
    }

    return {
        AjaxCall: function (url, model, success, error, options) {
            return ajaxCall(url, model, success, error, options, "json");
        },
        GetUrlId: function () {
            return getUrlId();
        },
        SelectedFilters: function () {
            return selectedFilters();
        },


        
    };

}]);
