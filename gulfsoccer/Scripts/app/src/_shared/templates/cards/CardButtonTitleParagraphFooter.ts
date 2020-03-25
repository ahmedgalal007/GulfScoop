///// <reference path="../../../../../typings/jquery/jquery.d.ts" />
///// <reference path="../Abstract.ts" />

//import { BaseCard, ICardData } from "_shared/templates/Abstract";
import { BaseCard, ICardData } from "_shared/templates/Abstract";
//
// <!--Card -->
// <div class="card" >
//
//     <!--Card image-- >
//     <div class="view overlay" >
//         <img class="card-img-top" src = "https://mdbootstrap.com/img/Photos/Others/food.jpg" alt = "Card image cap" >
//         <a>
//             <div class="mask rgba-white-slight" > </div>
//         < /a>
//     </div>
//
//     < !--Button -->
//     <a class="btn-floating btn-action ml-auto mr-4 mdb-color lighten-3" > <i class="fas fa-chevron-right pl-1" > </i></a >
//
//     <!--Card content-- >
//     <div class="card-body" >
//         <!--Title -->
//         <h4 class="card-title" > Card title < /h4>
//                                                 < hr >
//         <!--Text -->
//         <p class="card-text" > Some quick example text to build on the card title and make up the bulk of the card's content.</p>
//     </div>
//
//     <!--Card footer-- >
//     <div class="rounded-bottom mdb-color lighten-3 text-center pt-3" >
//         <ul class="list-unstyled list-inline font-small" >
//             <li class="list-inline-item pr-2 white-text" > <i class="far fa-clock pr-1" > </i>05/10 / 2015 < /li>
//             < li class="list-inline-item pr-2" > <a href="#" class="white-text" > <i class="far fa-comments pr-1" > </i>12</a > </li>
//             < li class="list-inline-item pr-2" > <a href="#" class="white-text" > <i class="fab fa-facebook-f pr-1" > </i>21</a > </li>
//             < li class="list-inline-item" > <a href="#" class="white-text" > <i class="fab fa-twitter pr-1" > </i>5</a > </li>
//         </ul>
//     </div>
//
// < /div>
// < !--Card -->
//

export class CardButtonTitleParagraphFooter extends BaseCard {
    constructor() {
        super();
        // return this.createCard(d);
    }

    // public createCard(d: ICardData): JQuery {
    //     this.cardElem = $('<div>').addClass('card');
    //     this.card_featuredImage = $('<div>').addClass('view overlay');
    //     this.card_featuredImage_img = $('<img>').addClass('card-img-top').attr({ "alt": d.imgAlt, "src": d.imgSrc });
    //     this.card_featuredImage_imgMask = $('<a>').append($('<div>').addClass('mask rgba-white-slight'));
    //     this.card_button = $('<a>').addClass('btn-floating btn-action ml-auto mr-4 mdb-color lighten-3').attr("href", d.articleLink);
    //     this.card_button_icon = $('<i>').addClass('fas fa-chevron-right pl-1');
    //     this.card_body = $('div').addClass('card-body');
    //
    //     this.card_title = $('<h4>').addClass('card-title').text(d.title);
    //     this.card_excerpt = $('<p>').addClass('card-text').text(d.excerpt);
    //
    //     this.card_footer = $('<div>').addClass('rounded-bottom mdb-color lighten-3 text-center pt-3');
    //     this.card_footer_list = $('<ul>').addClass('list-unstyled list-inline font-small');
    //
    //     this.card_featuredImage.append(this.card_featuredImage_img);
    //     this.card_featuredImage.append(this.card_featuredImage_imgMask);
    //     this.cardElem.append(this.card_featuredImage);
    //     this.card_button.append(this.card_button_icon);
    //     this.cardElem.append(this.card_button);
    //     this.card_body.append(this.card_title);
    //     this.card_body.append(this.card_excerpt);
    //     if (d.dark) {
    //         this.card_body.addClass('elegant-color white-text rounded-bottom');
    //     }
    //     this.cardElem.append(this.card_body);
    //
    //     this.card_footer_list.append($('<li>').addClass('list-inline-item pr-2 white-text').html('<i class="far fa-clock pr-1" > </i>'+ d.lastUpdated));
    //     this.card_footer_list.append($('<li>').addClass('list-inline-item pr-2').html('<a href="#" class="white-text" > <i class="far fa-comments pr-1" > </i>12</a >'));
    //     this.card_footer_list.append($('<li>').addClass('list-inline-item pr-2').html('<a href="#" class="white-text" > <i class="fab fa-facebook-f pr-1" > </i>21</a >'));
    //     this.card_footer_list.append($('<li>').addClass('list-inline-item').html('<a href="#" class="white-text" > <i class="fab fa-twitter pr-1" > </i>5</a >'));
    //
    //     this.card_footer.append(this.card_footer_list);
    //     this.cardElem.append(this.card_footer);
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

        //this.card_readMore = $('<a/>');
        //this.card_readMore.addClass('black-text d-flex justify-content-end');
        //this.card_readMore.attr('href', d.articleLink);
        //this.card_readMore.append($('<h5>' + d.readMoreText + ' <i class="fas fa-angle-double-right"> </i></h5>'));

        ///Render ////////////////////////////////////////////////
        this.card_featuredImage.append(this.card_featuredImage_img);
        this.card_featuredImage.append(this.card_featuredImage_imgMask);

        this.card_button.append(this.card_button_icon);

        // this.card_body.append(this.card_body_socialbutton);
        this.card_body.append(this.card_title);
        this.card_body.append(this.card_excerpt);
        // this.card_body.append(this.card_readMore);

        this.card_footer.append(this.card_footer_list);

        this.cardElem.append(this.card_featuredImage);
        this.cardElem.append(this.card_button);
        this.cardElem.append(this.card_body);
        this.cardElem.append(this.card_footer);
        return this.cardElem;
    }
}

//export default registeredCardFactories;