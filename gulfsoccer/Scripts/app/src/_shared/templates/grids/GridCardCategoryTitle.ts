import { BaseGrid, IGridData } from "_shared/templates/Abstract"
// <div class="container mt-5" >
// 
// 
//     <!--Section: Content-- >
//     <section class="text-center" >
// 
//         <!--Section heading-- >
//         <h3 class="font-weight-bold mb-5" > Recent Blog Posts < /h3>
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

export class GridCardCategoryTitle extends BaseGrid {
    // public card: BaseCard; public grid: JQuery; public gridHeader: JQuery; public gridParagraph: JQuery; public gridRow: JQuery; public ParagraphTxt: JQuery;

    // public render(d: IGridData): JQuery{
    //     // this.card = d.card;
    //     this.grid = $('<section>').addClass('text-center');
    //     this.gridHeader = $('<h3>').addClass('font-weight-bold mb-5').text(d.headerText);
    //     if (d.gridHeaderLink != null && d.gridHeaderLink != "" && d.gridHeaderLink.length > 0) {
    //         this.gridHeader.wrap($('<a>').attr("href", d.gridHeaderLink));
    //     }
    // 
    //     // this.gridParagraph = $('<p>').addClass('text-center mx-auto w-responsive mb-5').text(paragraphTxt);
    //     this.gridRow = $('<div>').addClass('row');
    //     d.cards.forEach((d) => {
    //         //d.imgSrc, d.imgAlt, d.categoryLink,d.categoryTxt,d.title,d.writerName,d.writerLink,d.lastUpdated,d.excerpt,d.articleLink,d.readMoreText
    //         this.gridRow.append(this.card.createCard(d));
    //     });
    //     this.grid.append(this.gridHeader);
    //     // this.grid.append(this.gridParagraph);
    //     this.grid.append(this.gridRow);
    //     return this.grid;
    // }

    public render(container:JQuery, d: IGridData): void {
        container.addClass('container mt-5');
        this.grid = $('<section/>');
        this.grid.addClass('text-center');
        this.gridHeader = $('<h3/>');
        this.gridHeader.addClass('font-weight-bold mb-5').text(d.headerText);
        if (d.gridHeaderLink != null && d.gridHeaderLink != "" && d.gridHeaderLink.length > 0) {
            this.gridHeader.wrap($('<a/>').attr("href", d.gridHeaderLink));
        }
        this.grid.append(this.gridHeader);
        this.gridRow = $('<div/>');
        this.gridRow.addClass('row');
        d.cards.forEach((d) => {
            this.gridRow.append(this.card.createCard(d));
        });
        this.grid.append(this.gridRow);
        container.append(this.grid);
    }
}



