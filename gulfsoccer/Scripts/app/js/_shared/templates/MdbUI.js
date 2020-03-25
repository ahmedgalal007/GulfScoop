define(["require", "exports", "_shared/templates/cards/index", "_shared/templates/grids/index"], function (require, exports, index_1, index_2) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    function CardFactory(type) {
        return new index_1.CardFactoeies[type]();
    }
    ;
    function GridFactory(type) {
        return new index_2.GridFactories[type]();
    }
    ;
    class grid {
    }
    ;
    class MdbGridUI {
        static buildGrids(elements) {
            let Grids = new Array();
            let gridPointers = {};
            let count = 0;
            let prefix = "mdb-doc-grid-";
            elements.each(function () {
                let _grid = new grid();
                _grid.Type = $(this).data('mdbGridType');
                _grid.Card = $(this).data('mdbGridCard');
                _grid.Category = $(this).data('mdbGridCategory');
                _grid.Cols = $(this).data('mdbGridCols');
                _grid.Rows = $(this).data('mdbGridRows');
                _grid.Elem = prefix + count;
                Grids.push(_grid);
                gridPointers[prefix + count] = $(this);
                count++;
            });
            $.post("/MDBGrid/GetPageGrids", { "grids": Grids }, function (data) {
                let g = JSON.parse(data);
                for (let i = 0; i < g.length; i++) {
                    MdbGridUI.renderGrid(gridPointers[g[i].element], g[i]);
                }
            });
        }
    }
    exports.MdbGridUI = MdbGridUI;
    MdbGridUI.renderGrid = function (container, grid) {
        const _grid = GridFactory(grid.type);
        _grid.card = CardFactory(grid.card);
        return _grid.render(container, grid);
    };
});
//# sourceMappingURL=MdbUI.js.map