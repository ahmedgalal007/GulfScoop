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
    // <div class="container mt-5" >
    // 
    // 
    //     <!--Section: Content-- >
    //     <section class="text-center" >
    // 
    //         <!--Section heading-- >
    //         <h3 class="font-weight-bold mb-5" > Recent Blog Posts < /h3>
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
    var GridCardCategoryTitle = /** @class */ (function (_super) {
        __extends(GridCardCategoryTitle, _super);
        function GridCardCategoryTitle() {
            return _super !== null && _super.apply(this, arguments) || this;
        }
        // public card: BaseCard; public grid: JQuery; public gridHeader: JQuery; public gridParagraph: JQuery; public gridRow: JQuery; public ParagraphTxt: JQuery;
        // public render(d: IGridData): JQuery{
        //     // this.card = d.card;
        //     this.grid = $('<section>').addClass('text-center');
        //     this.gridHeader = $('<h3>').addClass('font-weight-bold mb-5').text(d.headerText);
        //     if (d.gridHeaderLink != null && d.gridHeaderLink != "" && d.gridHeaderLink.length > 0) {
        //         this.gridHeader.wrap($('<a>').attr("href", d.gridHeaderLink));
        //     }
        // 
        //     // this.gridParagraph = $('<p>').addClass('text-center mx-auto w-responsive mb-5').text(paragraphTxt);
        //     this.gridRow = $('<div>').addClass('row');
        //     d.cards.forEach((d) => {
        //         //d.imgSrc, d.imgAlt, d.categoryLink,d.categoryTxt,d.title,d.writerName,d.writerLink,d.lastUpdated,d.excerpt,d.articleLink,d.readMoreText
        //         this.gridRow.append(this.card.createCard(d));
        //     });
        //     this.grid.append(this.gridHeader);
        //     // this.grid.append(this.gridParagraph);
        //     this.grid.append(this.gridRow);
        //     return this.grid;
        // }
        GridCardCategoryTitle.prototype.render = function (container, d) {
            var _this = this;
            container.addClass('container mt-5');
            this.grid = $('<section/>');
            this.grid.addClass('text-center');
            this.gridHeader = $('<h3/>');
            this.gridHeader.addClass('font-weight-bold mb-5').text(d.headerText);
            if (d.gridHeaderLink != null && d.gridHeaderLink != "" && d.gridHeaderLink.length > 0) {
                this.gridHeader.wrap($('<a/>').attr("href", d.gridHeaderLink));
            }
            this.grid.append(this.gridHeader);
            this.gridRow = $('<div/>');
            this.gridRow.addClass('row');
            d.cards.forEach(function (d) {
                _this.gridRow.append(_this.card.createCard(d));
            });
            this.grid.append(this.gridRow);
            container.append(this.grid);
        };
        return GridCardCategoryTitle;
    }(Abstract_1.BaseGrid));
    exports.GridCardCategoryTitle = GridCardCategoryTitle;
});
//# sourceMappingURL=GridCardCategoryTitle.js.map