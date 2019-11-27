import { BaseGrid, IGridData } from "_shared/templates/Abstract"
//    <!--Section: Content-- >
//    <section class="dark-grey-text text-center" >
//
//        <!--Section heading-- >
//        <h2 class="font-weight-bold mb-4 pb-2" > Recent posts < /h2>
//        < !--Section description-- >
//        <p class="text-center mx-auto w-responsive mb-5" > Lorem ipsum dolor sit amet, consectetur adipisicing elit.Fugit, error amet numquam iure provident voluptate esse quasi, veritatis totam voluptas nostrum quisquam eum porro a pariatur veniam.< /p>
//
//        < !--Grid row-- >
//        <div class="row" >
//              < !-- Render Grid column-- >
//        </div>
//        < !--Grid row-- >
//
//    </section>
//    < !--Section: Content-- >



export class GridRecentPost extends BaseGrid {
    constructor() {
        super();
    }

    public render(container: JQuery, d: IGridData): void{
        // this.card = d.card;
        this.grid = $('<section/>').addClass('dark-grey-text text-center');
        this.gridHeader = $('<h2/>').addClass('font-weight-bold mb-4 pb-2').text(d.headerText);
        if (d.gridHeaderLink != null && d.gridHeaderLink != "" && d.gridHeaderLink.length > 0) {
            this.gridHeader = $('<a/>').attr("href", d.gridHeaderLink).append(this.gridHeader);
        }
        this.gridParagraph = $('<p/>').addClass('text-center mx-auto w-responsive mb-5').text(d.paragraphTxt);
        this.gridRow = $('<div/>').addClass('row');

        d.cards.forEach((d) => {
            //d.imgSrc, d.imgAlt, d.categoryLink,d.categoryTxt,d.title,d.writerName,d.writerLink,d.lastUpdated,d.excerpt,d.articleLink,d.readMoreText
            this.gridRow.append(this.card.createCard(d));
        });
        this.grid.append(this.gridHeader);
        this.grid.append(this.gridParagraph);
        this.grid.append(this.gridRow);
        container.append(this.grid);
    }
}



