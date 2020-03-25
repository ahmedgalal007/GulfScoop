define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class BaseGrid {
        constructor() { }
        render(container, d) {
            if (container && d) {
            }
        }
        ;
    }
    exports.BaseGrid = BaseGrid;
    class BaseCard {
        constructor() {
        }
        createCard(data) { return $("<div>").data(data); }
    }
    exports.BaseCard = BaseCard;
    exports.registeredCardFactories = {};
    exports.registeredGridFactories = {};
});
//# sourceMappingURL=Abstract.js.map