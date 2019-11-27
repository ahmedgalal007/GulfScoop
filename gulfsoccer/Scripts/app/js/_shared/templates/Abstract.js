define(["require", "exports"], function (require, exports) {
    "use strict";
    exports.__esModule = true;
    var BaseGrid = /** @class */ (function () {
        function BaseGrid() {
        }
        BaseGrid.prototype.render = function (container, d) { };
        ;
        return BaseGrid;
    }());
    exports.BaseGrid = BaseGrid;
    var BaseCard = /** @class */ (function () {
        function BaseCard() {
        }
        BaseCard.prototype.createCard = function (data) { return $("<div>"); };
        return BaseCard;
    }());
    exports.BaseCard = BaseCard;
    exports.registeredCardFactories = {};
    exports.registeredGridFactories = {};
});
//# sourceMappingURL=Abstract.js.map