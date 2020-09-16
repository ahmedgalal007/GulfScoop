define(["require", "exports", "_shared/templates/Abstract"], function (require, exports, Abstract_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.GridCardCategoryTitleParagraphReadMore = void 0;
    class GridCardCategoryTitleParagraphReadMore extends Abstract_1.BaseGrid {
        constructor() {
            super();
        }
        render(container, d) {
            container.addClass('container mt-5');
            this.grid = $('<section/>');
            this.gridHeader = $('<h3/>').addClass('text-center font-weight-bold mb-5').text(d.headerText);
            if (d.gridHeaderLink != null && d.gridHeaderLink != "" && d.gridHeaderLink.length > 0) {
                this.gridHeader = $('<a/>').attr("href", d.gridHeaderLink).append(this.gridHeader);
            }
            this.gridRow = $('<div/>').addClass('row');
            for (var i = 0; i < d.cards.length; i++) {
                this.gridRow.append(this.card.createCard(d.cards[i]));
            }
            this.grid.append(this.gridHeader);
            this.grid.append(this.gridRow);
            container.append(this.grid);
        }
    }
    exports.GridCardCategoryTitleParagraphReadMore = GridCardCategoryTitleParagraphReadMore;
});
//# sourceMappingURL=GridCardCategoryTitleParagraphReadMore.js.map