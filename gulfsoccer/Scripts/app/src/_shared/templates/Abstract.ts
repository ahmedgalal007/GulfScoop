export interface ICardData {
    type:string, imgSrc: string, imgAlt: string, categoryLink: string, categoryTxt: string, title: string, writerName: string, writerLink: string, lastUpdated: string, excerpt: string, articleLink: string, readMoreText: string, dark:boolean
}

export interface IGridData {
    type: string, card: string, cards: Array<ICardData>, headerText: string, gridHeaderLink: string, paragraphTxt: string, element:string
}

export interface IGrid {
    card: any; grid: JQuery; gridHeader: JQuery; gridParagraph: JQuery; gridRow: JQuery; ParagraphTxt: JQuery;
    render(container: JQuery, d: IGridData): void;
}

export class BaseGrid implements IGrid {
    public card: ICard;
    public grid: JQuery;
    public gridHeader: JQuery;
    public gridParagraph: JQuery;
    public gridRow: JQuery;
    public ParagraphTxt: JQuery;

    constructor() {}
    public render(container: JQuery, d: IGridData): void {
        if (container && d) {

        }
    };
}

export interface ICard {
    cardElem: JQuery; card_featuredImage: JQuery; card_featuredImage_img: JQuery; card_featuredImage_imgMask: JQuery; card_category: JQuery;
    card_category_Header: JQuery; card_category_Header_Icon: JQuery; card_title: JQuery; card_data: JQuery; card_excerpt: JQuery;
    card_readMore: JQuery; card_button: JQuery; card_button_icon: JQuery; card_body_socialbutton: JQuery;
    card_body_socialbutton_icon: JQuery; card_body: JQuery; card_footer: JQuery; card_footer_list: JQuery;
    createCard: Function;
}
export class BaseCard implements ICard  {
    constructor() {
    }

    public cardElem: JQuery;
    public card_featuredImage: JQuery;
    public card_featuredImage_img: JQuery;
    public card_featuredImage_imgMask: JQuery;
    public card_category: JQuery;
    public card_category_Header: JQuery;
    public card_category_Header_Icon: JQuery;
    public card_title: JQuery;
    public card_data: JQuery;
    public card_excerpt: JQuery;
    public card_readMore: JQuery;
    public card_button: JQuery;
    public card_button_icon: JQuery;
    public card_body_socialbutton: JQuery;
    public card_body_socialbutton_icon: JQuery;
    public card_body: JQuery;
    public card_footer: JQuery;
    public card_footer_list: JQuery;
    createCard(data: ICardData): JQuery { return $("<div>").data(data);}
}
export interface KeyValueCard {
    [key: string]: any;
}
export interface KeyValueGrid {
    [key: string]: any;
}
export const registeredCardFactories:any = {};
export const registeredGridFactories:any = {};