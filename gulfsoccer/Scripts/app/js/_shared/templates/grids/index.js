define(["require", "exports", "_shared/templates/Abstract", "_shared/templates/grids/GridCardCategoryTitle", "_shared/templates/grids/GridCardCategoryTitleParagraphReadMore", "_shared/templates/grids/GridRecentPost", "_shared/templates/grids/GridCardDeck"], function (require, exports, Abstract_1, GridCardCategoryTitle_1, GridCardCategoryTitleParagraphReadMore_1, GridRecentPost_1, GridCardDeck_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.GridFactories = void 0;
    Abstract_1.registeredGridFactories["GridCardCategoryTitle"] = GridCardCategoryTitle_1.GridCardCategoryTitle;
    Abstract_1.registeredGridFactories["GridCardCategoryTitleParagraphReadMore"] = GridCardCategoryTitleParagraphReadMore_1.GridCardCategoryTitleParagraphReadMore;
    Abstract_1.registeredGridFactories["GridRecentPost"] = GridRecentPost_1.GridRecentPost;
    Abstract_1.registeredGridFactories["GridCardDeck"] = GridCardDeck_1.GridCardDeck;
    exports.GridFactories = Abstract_1.registeredGridFactories;
});
//# sourceMappingURL=index.js.map