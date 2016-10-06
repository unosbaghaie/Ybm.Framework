'use strict';
//define('onfinishrender', ['app'], function (app) {
    app.directive('onfinishrender', function () {
        return {
            restrict: 'A',
            link: function (scope, element, attr) {
                if (scope.$last) {
                    scope.$emit('ngRepeatFinished');
                }
            }
        }
    });
//});