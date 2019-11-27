var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
define(["require", "exports", "_shared/templates/Abstract"], function (require, exports, Abstract_1) {
    "use strict";
    exports.__esModule = true;
    //
    //            <div class="col-lg-4 col-md-6 mb-4">
    //
    //                <!-- Featured image -->
    //                <div class="view overlay rounded z-depth-2 mb-4">
    //                    <img class="img-fluid" src="https://mdbootstrap.com/img/Photos/Others/images/43.jpg" alt="Sample image">
    //                    <a>
    //                        <div class="mask rgba-white-slight"></div>
    //                    </a>
    //                </div>
    //
    //                <!-- Category -->
    //                <a href="#!" class="deep-orange-text">
    //                    <h6 class="font-weight-bold mb-3"><i class="fas fa-graduation-cap pr-2"></i>Education</h6>
    //                </a>
    //                <!-- Post title -->
    //                <h4 class="font-weight-bold mb-3"><strong>Title of the news</strong></h4>
    //                <!-- Post data -->
    //                <p>by <a class="font-weight-bold">Billy Forester</a>, 13/07/2018</p>
    //                <!-- Excerpt -->
    //                <p class="dark-grey-text">
    //                    At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis
    //                    voluptatum deleniti atque corrupti quos dolores.
    //                </p>
    //                <!-- Read more button -->
    //                <a class="btn btn-deep-orange btn-rounded btn-md">Read more</a>
    //
    //            </div>
    //
    var CardColumnPostWithCategory = /** @class */ (function (_super) {
        __extends(CardColumnPostWithCategory, _super);
        function CardColumnPostWithCategory() {
            return _super.call(this) || this;
            // return this.createCard(d);
        }
        // public createCard(d: ICardData): JQuery{
        //     this.cardElem = $('<div>').addClass('col-lg-4 col-md-6 mb-4');
        //     this.card_featuredImage = $('<div>').addClass('view overlay rounded z-depth-2 mb-4');
        //     this.card_featuredImage_img = $('<img>').addClass('img-fluid').attr({ "alt": d.imgAlt, "src": d.imgSrc });
        //     this.card_featuredImage_imgMask = $('<a>').append($('<div>').addClass('mask rgba-white-slight'));
        //     this.card_category = $('<a>').addClass('deep-orange-text').attr("href", d.categoryLink);
        //     this.card_category_Header = $('<h6>').addClass('font-weight-bold mb-3').text(d.categoryTxt);
        //     this.card_category_Header_Icon = $('<i>').addClass('fas fa-graduation-cap pr-2');
        //     this.card_title = $('<h4>').addClass('font-weight-bold mb-3').text(d.title);
        //     this.card_data = $('<p>').html('by <a class= "font-weight-bold" href="' + d.writerLink + '" > ' + d.writerName + ' </a>, ' + new Date(d.lastUpdated));
        //     this.card_excerpt = $('<p>').addClass('dark-grey-text').text(d.excerpt==null?"":d.excerpt);
        //     this.card_readMore = $('<a>').addClass('btn btn-deep-orange btn-rounded btn-md').attr('href', d.articleLink).text(d.readMoreText);
        // 
        //     
        //     this.card_featuredImage.append(this.card_featuredImage_img);
        //     this.card_featuredImage.append(this.card_featuredImage_imgMask);
        //     this.cardElem.append(this.card_featuredImage);
        //     this.card_category_Header.append(this.card_category_Header_Icon);
        //     this.card_category.append(this.card_category_Header);
        //     this.cardElem.append(this.card_category);
        //     this.cardElem.append(this.card_title);
        //     this.cardElem.append(this.card_data);
        //     this.cardElem.append(this.card_excerpt);
        //     this.cardElem.append(this.card_readMore);
        //     return this.cardElem;
        // };
        CardColumnPostWithCategory.prototype.createCard = function (d) {
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
            this.card_data.html('by <a class= "font-weight-bold" href="' + d.writerLink + '" > ' + d.writerName + ' </a>, ' + new Date( /*d.lastUpdated*/));
            this.card_excerpt = $('<p/>');
            this.card_excerpt.addClass('dark-grey-text');
            this.card_excerpt.text(d.excerpt == null ? "" : d.excerpt);
            this.card_readMore = $('<a/>');
            this.card_readMore.addClass('btn btn-deep-orange btn-rounded btn-md');
            this.card_readMore.attr('href', d.articleLink);
            this.card_readMore.text(d.readMoreText);
            ///Render ////////////////////////////////////////////////
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
        };
        return CardColumnPostWithCategory;
    }(Abstract_1.BaseCard));
    exports.CardColumnPostWithCategory = CardColumnPostWithCategory;
});
//# sourceMappingURL=CardColumnPostWithCategory.js.map