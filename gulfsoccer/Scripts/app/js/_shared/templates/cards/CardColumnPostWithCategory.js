define(["require", "exports", "_shared/templates/Abstract"], function (require, exports, Abstract_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class CardColumnPostWithCategory extends Abstract_1.BaseCard {
        constructor() {
            super();
        }
        createCard(d) {
            this.cardElem = $('<div/>');
            this.cardElem.addClass('col-lg-4 col-md-6 mb-4');
            this.card_featuredImage = $('<div/>');
            this.card_featuredImage.addClass('view overlay rounded z-depth-2 mb-4');
            this.card_featuredImage_img = $('<img/>');
            this.card_featuredImage_img.addClass('img-fluid');
            this.card_featuredImage_img.attr({ "alt": d.imgAlt, "src": d.imgSrc });
            this.card_featuredImage_imgMask = $('<a/>');
            this.card_featuredImage_imgMask.append($('<div class="mask rgba-white-slight"></div>'));
            this.card_category = $('<a/>');
            this.card_category.addClass('deep-orange-text');
            this.card_category.attr("href", d.categoryLink);
            this.card_category_Header = $('<h6/>');
            this.card_category_Header.addClass('font-weight-bold mb-3');
            this.card_category_Header.text(d.categoryTxt);
            this.card_category_Header_Icon = $('<i/>');
            this.card_category_Header_Icon.addClass('fas fa-graduation-cap pr-2');
            this.card_title = $('<h4/>');
            this.card_title.addClass('font-weight-bold mb-3');
            this.card_title.text(d.title);
            this.card_data = $('<p/>');
            this.card_data.html('by <a class= "font-weight-bold" href="' + d.writerLink + '" > ' + d.writerName + ' </a>, ' + new Date());
            this.card_excerpt = $('<p/>');
            this.card_excerpt.addClass('dark-grey-text');
            this.card_excerpt.text(d.excerpt == null ? "" : d.excerpt);
            this.card_readMore = $('<a/>');
            this.card_readMore.addClass('btn btn-deep-orange btn-rounded btn-md');
            this.card_readMore.attr('href', d.articleLink);
            this.card_readMore.text(d.readMoreText);
            this.card_featuredImage.append(this.card_featuredImage_img);
            this.card_featuredImage.append(this.card_featuredImage_imgMask);
            this.card_category_Header.append(this.card_category_Header_Icon);
            this.card_category.append(this.card_category_Header);
            this.cardElem.append(this.card_featuredImage);
            this.cardElem.append(this.card_category);
            this.cardElem.append(this.card_title);
            this.cardElem.append(this.card_data);
            this.cardElem.append(this.card_excerpt);
            this.cardElem.append(this.card_readMore);
            return this.cardElem;
        }
    }
    exports.CardColumnPostWithCategory = CardColumnPostWithCategory;
});
//# sourceMappingURL=CardColumnPostWithCategory.js.map