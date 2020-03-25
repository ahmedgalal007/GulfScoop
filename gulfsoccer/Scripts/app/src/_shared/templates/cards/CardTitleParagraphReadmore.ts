///// <reference path="../../../../../typings/jquery/jquery.d.ts" />
///// <reference path="../abstract.ts" />
// import { BaseCard, ICardData } from "_shared/templates/Abstract";
import { BaseCard, ICardData } from "_shared/templates/Abstract";
//
// <!--Card Light-- >
// <div class="card" >
//     <!--Card image-- >
//     <div class="view overlay" >
//         <img class="card-img-top" src = "https://mdbootstrap.com/img/Photos/Horizontal/Nature/4-col/img%20%28131%29.jpg" alt = "Card image cap" >
//         <a>
//             <div class="mask rgba-white-slight" > </div>
//         </a>
//     </div>
//
//     <!--Card content-- >
//     <div class="card-body" >
//
//         <!--Social shares button-- >
//         <a class="activator waves-effect waves-light mr-4" > <i class="fas fa-share-alt" > </i></a >
//         <!--Title -->
//         <h4 class="card-title" > Card title < /h4>
//         < hr >
//         <!--Text -->
//         <p class="card-text" > Some quick example text to build on the card title and make up the bulk of the card's content.</p>
//         < !--Link -->
//         <a href="#!" class="black-text d-flex justify-content-end" > <h5>Read more < i class="fas fa-angle-double-right" > </i></h5 > </a>
//
//     </div>
//
// </div>
// <!--Card Light-- >
//

export class CardTitleParagraphReadmore extends BaseCard {
    constructor() {
        super();
    }

    // public createCard(d: ICardData): JQuery{
    //     this.cardElem = $('<div>').addClass('card');
    //     this.card_featuredImage = $('<div>').addClass('view overlay');
    //     this.card_featuredImage_img = $('<img>').addClass('card-img-top').attr({ "alt": d.imgAlt, "src": d.imgSrc });
    //     this.card_featuredImage_imgMask = $('<a>').append($('<div>').addClass('mask rgba-white-slight'));
    //     this.card_body = $('div').addClass('card-body');
    //     this.card_body_socialbutton = $('<a>').addClass('activator waves-effect waves-light mr-4').attr("href", d.categoryLink);
    //     //this.card_category_Header = $('<h6>').addClass('font-weight-bold mb-3').text(d.categoryTxt);
    //     this.card_body_socialbutton_icon = $('<i>').addClass('fas fa-share-alt');
    //     this.card_title = $('<h4>').addClass('card-title').text(d.title);
    //     // this.card_data = $('<p>').html('by <a class= "font-weight-bold" href="' + d.writerLink + '" > ' + d.writerName + ' </a>, ' + new Date(d.lastUpdated));
    //     this.card_excerpt = $('<p>').addClass('card-text').text(d.excerpt);
    //     this.card_readMore = $('<a>').addClass('black-text d-flex justify-content-end').attr('href', d.articleLink).html('<h5>'+ d.readMoreText +' < i class="fas fa-angle-double-right" > </i></h5 > ');
    //
    //
    //     this.card_featuredImage.append(this.card_featuredImage_img);
    //     this.card_featuredImage.append(this.card_featuredImage_imgMask);
    //     this.cardElem.append(this.card_featuredImage);
    //     this.card_body_socialbutton.append(this.card_body_socialbutton_icon);
    //     this.card_body.append(this.card_body_socialbutton);
    //     this.card_body.append(this.card_title);
    //     this.card_body.append(this.card_excerpt);
    //     this.card_body.append(this.card_readMore);
    //     if (d.dark) {
    //         this.card_body.addClass('elegant-color white-text rounded-bottom');
    //     }
    //     this.cardElem.append(this.card_body);
    //     return this.cardElem;
    // };
    public createCard(d: ICardData): JQuery {
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

        ///Render ////////////////////////////////////////////////
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