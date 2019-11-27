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
    // <div class="container mt-5">
    //
    //
    //    <!--Section: Content-- >
    //    <section class="" >
    //
    //         <!--Section heading-- >
    //         <h3 class="text-center font-weight-bold mb-5" > Latest news < /h3>
    //
    //         < !--Grid row-- >
    //         <div class="row" >
    //              < !-- Render Grid column-- >
    //         </div>
    //         <!--Grid row-- >
    //
    //    </section>
    //    < !--Section: Content-- >
    //
    // </div>
    var GridCardCategoryTitleParagraphReadMore = /** @class */ (function (_super) {
        __extends(GridCardCategoryTitleParagraphReadMore, _super);
        function GridCardCategoryTitleParagraphReadMore() {
            return _super.call(this) || this;
        }
        GridCardCategoryTitleParagraphReadMore.prototype.render = function (container, d) {
            container.addClass('container mt-5');
            // this.card = d.card;
            this.grid = $('<section/>');
            this.gridHeader = $('<h3/>').addClass('text-center font-weight-bold mb-5').text(d.headerText);
            if (d.gridHeaderLink != null && d.gridHeaderLink != "" && d.gridHeaderLink.length > 0) {
                this.gridHeader = $('<a/>').attr("href", d.gridHeaderLink).append(this.gridHeader);
            }
            // this.gridParagraph = $('<p>').addClass('text-center mx-auto w-responsive mb-5').text(paragraphTxt);
            this.gridRow = $('<div/>').addClass('row');
            for (var i = 0; i < d.cards.length; i++) {
                //d.imgSrc, d.imgAlt, d.categoryLink,d.categoryTxt,d.title,d.writerName,d.writerLink,d.lastUpdated,d.excerpt,d.articleLink,d.readMoreText
                this.gridRow.append(this.card.createCard(d.cards[i]));
            }
            this.grid.append(this.gridHeader);
            // this.grid.append(this.gridParagraph);
            this.grid.append(this.gridRow);
            container.append(this.grid);
        };
        return GridCardCategoryTitleParagraphReadMore;
    }(Abstract_1.BaseGrid));
    exports.GridCardCategoryTitleParagraphReadMore = GridCardCategoryTitleParagraphReadMore;
});
//# sourceMappingURL=GridCardCategoryTitleParagraphReadMore.js.map