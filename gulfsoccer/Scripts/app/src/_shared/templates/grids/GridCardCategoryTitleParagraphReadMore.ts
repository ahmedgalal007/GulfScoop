import { BaseGrid, IGridData } from "_shared/templates/Abstract"
// <div class="container mt-5">
//
//
//    <!--Section: Content-- >
//    <section class="" >
//
//         <!--Section heading-- >
//         <h3 class="text-center font-weight-bold mb-5" > Latest news < /h3>
//
//         < !--Grid row-- >
//         <div class="row" >
//              < !-- Render Grid column-- >
//         </div>
//         <!--Grid row-- >
//
//    </section>
//    < !--Section: Content-- >
//
// </div>

export class GridCardCategoryTitleParagraphReadMore extends BaseGrid {
    constructor() {
        super();
    }

    public render(container: JQuery, d: IGridData): void{
        container.addClass('container mt-5');
        // this.card = d.card;
        this.grid = $('<section/>');
        this.gridHeader = $('<h3/>').addClass('text-center font-weight-bold mb-5').text(d.headerText);
        if (d.gridHeaderLink != null && d.gridHeaderLink != "" && d.gridHeaderLink.length > 0) {
            this.gridHeader = $('<a/>').attr("href", d.gridHeaderLink).append(this.gridHeader);
        }

        // this.gridParagraph = $('<p>').addClass('text-center mx-auto w-responsive mb-5').text(paragraphTxt);
        this.gridRow = $('<div/>').addClass('row');

        for (var i = 0; i < d.cards.length; i++) {
            //d.imgSrc, d.imgAlt, d.categoryLink,d.categoryTxt,d.title,d.writerName,d.writerLink,d.lastUpdated,d.excerpt,d.articleLink,d.readMoreText
            this.gridRow.append(this.card.createCard(d.cards[i]));
        }

        this.grid.append(this.gridHeader);
        // this.grid.append(this.gridParagraph);
        this.grid.append(this.gridRow);
        container.append(this.grid);
    }
}