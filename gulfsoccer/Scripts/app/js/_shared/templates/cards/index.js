define(["require", "exports", "_shared/templates/Abstract", "_shared/templates/cards/CardButtonTitleParagraphFooter", "_shared/templates/cards/CardColumnPostWithCategory", "_shared/templates/cards/CardTitleParagraphReadmore"], function (require, exports, Abstract_1, CardButtonTitleParagraphFooter_1, CardColumnPostWithCategory_1, CardTitleParagraphReadmore_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.CardFactoeies = void 0;
    Abstract_1.registeredCardFactories["CardButtonTitleParagraphFooter"] = CardButtonTitleParagraphFooter_1.CardButtonTitleParagraphFooter;
    Abstract_1.registeredCardFactories["CardColumnPostWithCategory"] = CardColumnPostWithCategory_1.CardColumnPostWithCategory;
    Abstract_1.registeredCardFactories["CardTitleParagraphReadmore"] = CardTitleParagraphReadmore_1.CardTitleParagraphReadmore;
    exports.CardFactoeies = Abstract_1.registeredCardFactories;
});
//# sourceMappingURL=index.js.map