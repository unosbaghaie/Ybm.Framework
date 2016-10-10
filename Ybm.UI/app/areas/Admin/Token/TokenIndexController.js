app.controller('TokenIndexController', ["$scope", "serviceBaseAngular", "$compile", function ($scope, serviceBaseAngular, $compile) {

    save = function () {
       
        var selectedIds = [];
        $('input:checkbox[name=tokens]').each(function () {
            if ($(this).is(':checked')) {
                selectedIds.push($(this).attr('Id'));
            }
        });
        var userGroupId = $('#_UserGroupId').val();
        if (!userGroupId) {
            ShowNotification('', 'گروه کاربری را انتخاب کنید', NotificationType.error);
            return;
        }
        serviceBaseAngular.AjaxCall('/Token/Create/', { tokenIds: selectedIds, userGroupId: userGroupId }, function (result) {
            alert('create successfuly...');
        });

    }

    checkTheGroup= function (e) {
        debugger;
        var ss = $(e.parentElement.parentElement);
        ss.find('input:checkbox[name=tokens]')
        .each(function (i, value) {
            $(value).prop('checked', true);
        });
    }


    checkedAll = function () {
        $('input:checkbox[name=tokens]').each(function (i,value) {
           
            $(value).prop('checked', true);
        });

        $('#checkedAll').hide();
        $('#unCheckedAll').show();

    }

    unCheckedAll = function () {
        $('input:checkbox[name=tokens]').each(function (i, value) {
            $(value).prop('checked', false);
        });

        $('#checkedAll').show();
        $('#unCheckedAll').hide();
    }
}]);

function UserGroupOnSelect(e) {
    var dataItem = this.dataItem(e.item);

    $('#_UserGroupId').val(dataItem.Id);

    //dataItem.Text + " : " + dataItem.Value + ")");
    JQueyAjaxCall("/Token/GetUserGroupTokens",
        { userGroupId: dataItem.Id },
        function (success) {
            $('input:checkbox[name=tokens]').each(function () {
                $(this).prop('checked', false);
            });
            $.each(success, function (i, b) {
                $('input:checkbox[id=' + b.Name + ']').prop('checked', true);
            });
        },
        function (error) { }
        );
}

