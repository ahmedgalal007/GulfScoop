var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
define(["require", "exports", "_shared/templates/Abstract"], function (require, exports, Abstract_1) {
    "use strict";
    exports.__esModule = true;
    //    <!--Section: Content-- >
    //    <section class="dark-grey-text text-center" >
    //
    //        <!--Section heading-- >
    //        <h2 class="font-weight-bold mb-4 pb-2" > Recent posts < /h2>
    //        < !--Section description-- >
    //        <p class="text-center mx-auto w-responsive mb-5" > Lorem ipsum dolor sit amet, consectetur adipisicing elit.Fugit, error amet numquam iure provident voluptate esse quasi, veritatis totam voluptas nostrum quisquam eum porro a pariatur veniam.< /p>
    //
    //        < !--Grid row-- >
    //        <div class="row" >
    //              < !-- Render Grid column-- >
    //        </div>
    //        < !--Grid row-- >
    //
    //    </section>
    //    < !--Section: Content-- >
    var GridRecentPost = /** @class */ (function (_super) {
        __extends(GridRecentPost, _super);
        function GridRecentPost() {
            return _super.call(this) || this;
        }
        GridRecentPost.prototype.render = function (container, d) {
            var _this = this;
            // this.card = d.card;
            this.grid = $('<section/>').addClass('dark-grey-text text-center');
            this.gridHeader = $('<h2/>').addClass('font-weight-bold mb-4 pb-2').text(d.headerText);
            if (d.gridHeaderLink != null && d.gridHeaderLink != "" && d.gridHeaderLink.length > 0) {
                this.gridHeader = $('<a/>').attr("href", d.gridHeaderLink).append(this.gridHeader);
            }
            this.gridParagraph = $('<p/>').addClass('text-center mx-auto w-responsive mb-5').text(d.paragraphTxt);
            this.gridRow = $('<div/>').addClass('row');
            d.cards.forEach(function (d) {
                //d.imgSrc, d.imgAlt, d.categoryLink,d.categoryTxt,d.title,d.writerName,d.writerLink,d.lastUpdated,d.excerpt,d.articleLink,d.readMoreText
                _this.gridRow.append(_this.card.createCard(d));
            });
            this.grid.append(this.gridHeader);
            this.grid.append(this.gridParagraph);
            this.grid.append(this.gridRow);
            container.append(this.grid);
        };
        return GridRecentPost;
    }(Abstract_1.BaseGrid));
    exports.GridRecentPost = GridRecentPost;
});
//# sourceMappingURL=GridRecentPost.js.map