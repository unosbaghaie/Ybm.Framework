var newItemId = 0;
var message;
var selectedRows;



function windowClosed(e) {
    e.preventDefault();
    e.sender.content("")
}


function Delete(e) {
    e.preventDefault();
    var Id = GetKendoGridId(e);
    ShowDeleteNotification(Id);

};

//function Delete(url,gridName) {
//    //debugger;
//   // e.preventDefault();
//    var t = confirm("آیا برای حذف اطلاعات اطمینان دارید؟");
//    if (t == true) {
//        $.ajax({
//            type: 'POST',
//            url: url,
//            data: prepared,
//            success: function () {
//                //$("div[data-role=grid]:first").data("kendoGrid").dataSource.read();
//                return false;
//            },
//            error: function (e, xhr, s) {
//                //debugger;
//            }
//        });
//        var kendoGrid = $("div[data-role=grid]:first").data("kendoGrid");
//        kendoGrid.dataSource.filter({});
//        return false;
//    }
//};
function GetKendoGridId(e) {
    debugger; 
    return (parseInt($(e.target).parent().siblings("td[role='gridcell']")[0].innerHTML));
}