app.controller('LayoutController', ["$scope", "serviceBaseAngular", "$compile", "$timeout", "$rootScope", function ($scope, serviceBaseAngular, $compile, timer, $rootScope) {
  
    $scope.MenuItems = [{
        Id: 0,
        IsVisible: false,
        PageHref: "",
        PageName: "",
        Parent_Id: 0,
        HasSubMenu: false,
        SelectorCssClass: ""

    }]
    $scope.UnreadMessages = [];
    $scope.Alerts = [];
    $scope.subMenu = [{
        Name: "",
        ParentId: 0,
        PageHref: "",
    }]
    $scope.AlertCount = 0;
    $scope.menuData = [];
    Success = function (data) {
        $scope.menuData = data;
        $scope.fillMenu($scope.menuData)
    }


    $scope.fillMenu = function (data) {
        $scope.MenuItems = [];
        $scope.subMenu = [];
        timer(function () {

            for(item of data) {
                if (item.Parent_Id === null) {

                    $scope.MenuItems.push(item);

                }
                else {

                    $scope.subMenu.push({ Name: item.PageName, ParentId: item.Parent_Id, PageHref: item.PageHref });
                }

            }


            $scope.subMenu;


        }, 0);


        $scope.MenuItems;
    }
    $scope.$on('ngRepeatFinished', function (event) {

        timer(function () {
            $('#horizontal-navbar ul.nav.navbar-nav li.dropdown').hover(function () {
                $(this).addClass('open');
            }, function () {
                $(this).removeClass('open');
            })



        }, 0);
        timer(function () {


            angular.forEach($(".subMenuParent"), function (key, value) {

                if ($(key).find('li').length < 1) {
                    $(key).addClass("hidden");
                    $(key).parent().find('.fa-angle-down').hide();
                }
            })

        }, 100)
    })
    //function test() {
    //    alert("test");
    //}
    //$scope.test = function () {
    //    alert("test scope");
    //}
    //AjaxCall2('/Layout/GetMenu', Success);

    //$scope.MessageCount = 0;
    //$scope.getUnreadMessages = function () {
    //    SuccessMessage = function (result) {
    //        $scope.UnreadMessages = result.messages;
    //        $scope.MessageCount = result.UnreadMessageCount;
    //        debugger;
    //    }
    //    ErrorMessage = function () {

    //    }

       // var url = "/Layout/GetUnreadMessages"
       // serviceBaseAngular.AjaxCall(url, null, SuccessMessage, ErrorMessage);

    //}


    //$scope.getUnreadMessages();

    $scope.ReadedMessage = function () {


        SuccessMessage = function (result) {

            $('.modal').modal('hide');
            $scope.getUnreadMessages();
            if ($('.k-grid').length>0)
            $('.k-grid').data('kendoGrid').dataSource.read();
           // $compile($('.k-grid'))($scope);
            timer(function () {
                $compile($('.k-button'))($scope);
            }, 1000)
        }
        ErrorMessage = function () {
            debugger;
        }
        var url = "/Layout/SetMessageReaded"
        serviceBaseAngular.AjaxCall(url, { Id: $rootScope.messageId }, SuccessMessage, ErrorMessage);
    }
    $scope.ReadedAlert = function () {
        debugger;

        SuccessMessage = function (result) {

            $('.modal').modal('hide');
            $scope.getAlert();
            if ($('.k-grid').length > 0)
                $('.k-grid').data('kendoGrid').dataSource.read();
            // $compile($('.k-grid'))($scope);
            timer(function () {
                $compile($('.k-button'))($scope);
            }, 1000)
        }
        ErrorMessage = function () {
            debugger;
        }
        var url = "/Layout/SetAlertReaded"
        serviceBaseAngular.AjaxCall(url, { Id: $rootScope.alertId }, SuccessMessage, ErrorMessage);
    }
    $scope.showAlert = function (element) {
        $rootScope.alertId = parseInt($(element.target).parents('li:first').attr('data-id'));
        debugger;
        SuccessMessage = function (result) {

            $scope.ShowModal(result.Subject, result.Body, "<button type=button class='btn btn-default' data-dismiss=modal>بستن</button> <button type=button class='btn btn-raised btn-primary' ng-click=ReadedAlert()>خواندم</button>")

        }
        ErrorMessage = function () {

        }
        var url = "/Layout/GetFullAlert"
        serviceBaseAngular.AjaxCall(url, { Id: $rootScope.alertId }, SuccessMessage, ErrorMessage);


    }
    $scope.showMessage = function (element) {
        debugger;
        $rootScope.messageId = parseInt($(element.target).parents('li:first').attr('data-id'));
        SuccessMessage = function (result) {

            debugger;
            $scope.ShowModal(result.Subject, result.Body, "<button type=button class='btn btn-default' data-dismiss=modal>بستن</button> <button type=button class='btn btn-raised btn-primary' ng-click=ReadedMessage()>خواندم</button>")
        }
        ErrorMessage = function () {

        }
        var url = "/Layout/GetFullMessage"
        serviceBaseAngular.AjaxCall(url, { Id: $rootScope.messageId }, SuccessMessage, ErrorMessage);
    }


    $scope.ShowModal = function (Title, Body, Footer) {
        $('#myModal').remove();
        var html = '<div class="modal fade" id=myModal tabindex=-1 role=dialog aria-labelledby=myModalLabel aria-hidden=true><div class=modal-dialog><div class=modal-content><div class=modal-header><button type=button class=close data-dismiss=modal aria-hidden=true>&times;</button><h2 class=modal-title>' + Title + '</h2></div><div class=modal-body>' + Body + '</div><div class=modal-footer>' + Footer + '</div></div></div></div>';
        var el = angular.element(html);
        el.appendTo('body');
        $('#myModal').modal('show');
        $compile(el)($scope);
    }
    
    $scope.getAlert = function () {
        SuccessMessage = function (result) {
      
            debugger;
            $scope.Alerts = result.alerts;
            $scope.AlertCount = result.count;

        }
        ErrorMessage = function () {
            debugger;
        }

        var url = "/Layout/GetAlert"
        serviceBaseAngular.AjaxCall(url, null, SuccessMessage, ErrorMessage);

    }
    //$scope.getAlert();
    $('.k-button').attr('ng-click', true).on('click', function () {

    //   alert('clicked !')
    })
    //$(document).on('click','.k-button[ng-click]' , function () {
       
    //    debugger;
    //    $compile($(this).parent())($scope);
      
    //})
  
}]);
