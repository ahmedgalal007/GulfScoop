import { registeredCardFactories } from "_shared/templates/Abstract";
// app/js/_shared/templates/cards
import { CardButtonTitleParagraphFooter } from "_shared/templates/cards/CardButtonTitleParagraphFooter";
import { CardColumnPostWithCategory } from "_shared/templates/cards/CardColumnPostWithCategory";
import { CardTitleParagraphReadmore } from "_shared/templates/cards/CardTitleParagraphReadmore";

registeredCardFactories["CardButtonTitleParagraphFooter"] = CardButtonTitleParagraphFooter;
registeredCardFactories["CardColumnPostWithCategory"] = CardColumnPostWithCategory;
registeredCardFactories["CardTitleParagraphReadmore"] = CardTitleParagraphReadmore;

export let CardFactoeies = registeredCardFactories;