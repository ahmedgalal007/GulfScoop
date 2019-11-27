define(["require", "exports"], function (require, exports) {
    "use strict";
    exports.__esModule = true;
    var Site = /** @class */ (function () {
        function Site() {
            this.header = "";
        }
        Site.init = function (_param1) {
            return "";
        };
        Site.prototype.getHeader = function (_selector) {
            return "";
        };
        Site.prototype.getFooter = function (_selector) {
            $.getJSON("url", function () { });
            return "";
        };
        return Site;
    }());
    exports.Site = Site;
    console.log(Site.init(1000));
});
//# sourceMappingURL=site.js.map