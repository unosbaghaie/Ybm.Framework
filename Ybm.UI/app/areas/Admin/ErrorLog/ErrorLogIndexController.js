app.controller('ErrorLogIndexController', ["$scope", "serviceBaseAngular", "$compile", function ($scope, serviceBaseAngular, $compile) {






$scope.FilterDescriptorClick = function () {
        var selectedFilters = serviceBaseAngular.SelectedFilters();
        serviceBaseAngular.AjaxCall('/Admin/ErrorLog/FilterThePage', { selectedFilters: selectedFilters }, function (result) {
            $('.k-grid').data('kendoGrid').dataSource.read();
        });
    }
    



}]);


