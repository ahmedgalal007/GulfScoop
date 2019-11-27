
import { registeredGridFactories } from "_shared/templates/Abstract";

//app/js/_shared/templates/grids
import { GridCardCategoryTitle } from "_shared/templates/grids/GridCardCategoryTitle";
import { GridCardCategoryTitleParagraphReadMore } from "_shared/templates/grids/GridCardCategoryTitleParagraphReadMore";
import { GridRecentPost } from "_shared/templates/grids/GridRecentPost";
import { GridCardDeck } from "_shared/templates/grids/GridCardDeck";
registeredGridFactories["GridCardCategoryTitle"] =  GridCardCategoryTitle;
registeredGridFactories["GridCardCategoryTitleParagraphReadMore"] = GridCardCategoryTitleParagraphReadMore;
registeredGridFactories["GridRecentPost"] = GridRecentPost;
registeredGridFactories["GridCardDeck"] = GridCardDeck;
export let GridFactories = registeredGridFactories ;