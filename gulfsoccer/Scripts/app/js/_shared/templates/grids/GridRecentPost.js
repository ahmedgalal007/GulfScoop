define(["require", "exports", "_shared/templates/Abstract"], function (require, exports, Abstract_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class GridRecentPost extends Abstract_1.BaseGrid {
        constructor() {
            super();
        }
        render(container, d) {
            this.grid = $('<section/>').addClass('dark-grey-text text-center');
            this.gridHeader = $('<h2/>').addClass('font-weight-bold mb-4 pb-2').text(d.headerText);
            if (d.gridHeaderLink != null && d.gridHeaderLink != "" && d.gridHeaderLink.length > 0) {
                this.gridHeader = $('<a/>').attr("href", d.gridHeaderLink).append(this.gridHeader);
            }
            this.gridParagraph = $('<p/>').addClass('text-center mx-auto w-responsive mb-5').text(d.paragraphTxt);
            this.gridRow = $('<div/>').addClass('row');
            d.cards.forEach((d) => {
                this.gridRow.append(this.card.createCard(d));
            });
            this.grid.append(this.gridHeader);
            this.grid.append(this.gridParagraph);
            this.grid.append(this.gridRow);
            container.append(this.grid);
        }
    }
    exports.GridRecentPost = GridRecentPost;
});
//# sourceMappingURL=GridRecentPost.js.map