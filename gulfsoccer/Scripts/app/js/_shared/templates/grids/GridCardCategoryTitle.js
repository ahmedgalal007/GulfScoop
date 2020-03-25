define(["require", "exports", "_shared/templates/Abstract"], function (require, exports, Abstract_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class GridCardCategoryTitle extends Abstract_1.BaseGrid {
        render(container, d) {
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
            d.cards.forEach((d) => {
                this.gridRow.append(this.card.createCard(d));
            });
            this.grid.append(this.gridRow);
            container.append(this.grid);
        }
    }
    exports.GridCardCategoryTitle = GridCardCategoryTitle;
});
//# sourceMappingURL=GridCardCategoryTitle.js.map