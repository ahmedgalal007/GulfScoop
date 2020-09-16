define(["require", "exports", "_shared/templates/Abstract"], function (require, exports, Abstract_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    exports.CardButtonTitleParagraphFooter = void 0;
    class CardButtonTitleParagraphFooter extends Abstract_1.BaseCard {
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
            this.card_button = $('<a/>');
            this.card_button.addClass('btn-floating btn-action ml-auto mr-4 mdb-color lighten-3');
            this.card_button.attr("href", d.articleLink);
            this.card_button_icon = $('<i/>');
            this.card_button_icon.addClass('fas fa-chevron-right pl-1');
            this.card_body = $('<div/>');
            this.card_body.addClass('card-body');
            if (d.dark) {
                this.card_body.addClass('elegant-color white-text rounded-bottom');
            }
            this.card_title = $('<h4/>');
            this.card_title.addClass('card-title');
            this.card_title.text(d.title);
            this.card_excerpt = $('<p/>');
            this.card_excerpt.addClass('card-text');
            this.card_excerpt.text(d.excerpt);
            this.card_footer = $('<div/>');
            this.card_footer.addClass('rounded-bottom mdb-color lighten-3 text-center pt-3');
            this.card_footer_list = $('<ul/>');
            this.card_footer_list.addClass('list-unstyled list-inline font-small');
            this.card_footer_list.append($('<li/>').addClass('list-inline-item pr-2 white-text').html('<i class="far fa-clock pr-1" > </i>' + d.lastUpdated));
            this.card_footer_list.append($('<li/>').addClass('list-inline-item pr-2').html('<a href="#" class="white-text" > <i class="far fa-comments pr-1" > </i>12</a >'));
            this.card_footer_list.append($('<li/>').addClass('list-inline-item pr-2').html('<a href="#" class="white-text" > <i class="fab fa-facebook-f pr-1" > </i>21</a >'));
            this.card_footer_list.append($('<li/>').addClass('list-inline-item').html('<a href="#" class="white-text" > <i class="fab fa-twitter pr-1" > </i>5</a >'));
            this.card_featuredImage.append(this.card_featuredImage_img);
            this.card_featuredImage.append(this.card_featuredImage_imgMask);
            this.card_button.append(this.card_button_icon);
            this.card_body.append(this.card_title);
            this.card_body.append(this.card_excerpt);
            this.card_footer.append(this.card_footer_list);
            this.cardElem.append(this.card_featuredImage);
            this.cardElem.append(this.card_button);
            this.cardElem.append(this.card_body);
            this.cardElem.append(this.card_footer);
            return this.cardElem;
        }
    }
    exports.CardButtonTitleParagraphFooter = CardButtonTitleParagraphFooter;
});
//# sourceMappingURL=CardButtonTitleParagraphFooter.js.map