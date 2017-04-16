app.controller('UserGroupIndexController', ["$scope", "serviceBaseAngular", "$compile", function ($scope, serviceBaseAngular, $compile) {

$scope.FilterDescriptorClick = function () {
        var selectedFilters = serviceBaseAngular.SelectedFilters();
        serviceBaseAngular.AjaxCall('/Admin/UserGroup/FilterThePage', { selectedFilters: selectedFilters }, function (result) {
            $('.k-grid').data('kendoGrid').dataSource.read();
        });
}

}]);


