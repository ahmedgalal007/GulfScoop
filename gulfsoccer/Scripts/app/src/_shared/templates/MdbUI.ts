import { CardFactoeies } from "_shared/templates/cards/index";
import { GridFactories } from "_shared/templates/grids/index";
import { IGridData } from "_shared/templates/Abstract";
// Abstract = module("./Abstract")
function CardFactory(type: string) {
    return new CardFactoeies[type]();
};

function GridFactory(type: string) {
    return new GridFactories[type]();
};
class grid { Type: string; Card: string; Category: 0; Cols: 0; Rows: 0; ThumbSize: string; Elem: string };
export abstract class MdbGridUI {
    public static buildGrids(elements: JQuery) {
        // let result:JQuery = $('<div/>');
        let Grids: Array<grid> = new Array<grid>();
        let gridPointers: any = {};
        let count: number = 0;
        let prefix = "mdb-doc-grid-"
        elements.each(function () {
            let _grid: grid = new grid();
            // console.log($(this).data());
            _grid.Type = $(this).data('mdbGridType');
            _grid.Card = $(this).data('mdbGridCard');
            _grid.Category = $(this).data('mdbGridCategory');
            _grid.Cols = $(this).data('mdbGridCols');
            _grid.Rows = $(this).data('mdbGridRows');
            _grid.ThumbSize = $(this).data('mdbGridThumbSize');
            _grid.Elem = prefix + count;
            Grids.push(_grid);
            gridPointers[prefix + count] = $(this);
            count++;
        });

        // console.log(Grids);
        $.post("/MDBGrid/GetPageGrids", { "grids": Grids }, function (data/*, textStatus, jqXHR: JQueryXHR*/) {
            let g: IGridData[] = JSON.parse(data);
            for (let i = 0; i < g.length; i++) {
                MdbGridUI.renderGrid(gridPointers[g[i].element], g[i]);
            }
            //console.log(result);
        });
        //return result;
    }

    public static renderGrid = function (container: JQuery, grid: IGridData) {
        const _grid = GridFactory(grid.type);
        _grid.card = CardFactory(grid.card);
        return _grid.render(container, grid);
    }

    //public static fillCard(d:ICardData, castedCard:BaseCard){
    //    return castedCard;
    //}
}