define(["require", "exports", "_shared/templates/Abstract"], function (require, exports, Abstract_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.CardTitleParagraphReadmore = void 0;
    class CardTitleParagraphReadmore extends Abstract_1.BaseCard {
        constructor() {
            super();
        }
        createCard(d) {
            this.cardElem = $('<div/>');
            this.cardElem.addClass('card');
            this.card_featuredImage = $('<div/>');
            this.card_featuredImage.addClass('view overlay');
            this.card_featuredImage_img = $('<img/>');
            this.card_featuredImage_img.addClass('card-img-top');
            this.card_featuredImage_img.attr({ "alt": d.imgAlt, "src": d.imgSrc });
            this.card_featuredImage_imgMask = $('<a/>');
            this.card_featuredImage_imgMask.append($('<div class="mask rgba-white-slight"></div>'));
            this.card_body = $('<div/>');
            this.card_body.addClass('card-body');
            if (d.dark) {
                this.card_body.addClass('elegant-color white-text rounded-bottom');
            }
            this.card_body_socialbutton = $('<a/>');
            this.card_body_socialbutton.addClass('activator waves-effect waves-light mr-4');
            this.card_body_socialbutton.attr("href", d.categoryLink);
            this.card_body_socialbutton_icon = $('<i/>');
            this.card_body_socialbutton_icon.addClass('fas fa-share-alt');
            this.card_title = $('<h4/>');
            this.card_title.addClass('card-title');
            this.card_title.text(d.title);
            this.card_excerpt = $('<p/>');
            this.card_excerpt.addClass('card-text');
            this.card_excerpt.text(d.excerpt);
            this.card_readMore = $('<a/>');
            this.card_readMore.addClass('black-text d-flex justify-content-end');
            this.card_readMore.attr('href', d.articleLink);
            this.card_readMore.append($('<h5>' + d.readMoreText + ' <i class="fas fa-angle-double-right"> </i></h5>'));
            this.card_featuredImage.append(this.card_featuredImage_img);
            this.card_featuredImage.append(this.card_featuredImage_imgMask);
            this.card_body_socialbutton.append(this.card_body_socialbutton_icon);
            this.card_body.append(this.card_body_socialbutton);
            this.card_body.append(this.card_title);
            this.card_body.append(this.card_excerpt);
            this.card_body.append(this.card_readMore);
            this.cardElem.append(this.card_featuredImage);
            this.cardElem.append(this.card_body);
            return this.cardElem;
        }
    }
    exports.CardTitleParagraphReadmore = CardTitleParagraphReadmore;
});
//# sourceMappingURL=CardTitleParagraphReadmore.js.map