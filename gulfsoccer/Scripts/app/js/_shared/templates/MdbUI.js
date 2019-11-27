define(["require", "exports", "_shared/templates/cards/index", "_shared/templates/grids/index"], function (require, exports, index_1, index_2) {
    "use strict";
    exports.__esModule = true;
    // Abstract = module("./Abstract")
    function CardFactory(type) {
        return new index_1.CardFactoeies[type]();
    }
    ;
    function GridFactory(type) {
        return new index_2.GridFactories[type]();
    }
    ;
    var grid = /** @class */ (function () {
        function grid() {
        }
        return grid;
    }());
    ;
    var MdbGridUI = /** @class */ (function () {
        function MdbGridUI() {
        }
        MdbGridUI.buildGrids = function (elements) {
            // let result:JQuery = $('<div/>');
            var Grids = new Array();
            var gridPointers = {};
            var count = 0;
            var prefix = "mdb-doc-grid-";
            elements.each(function () {
                var _grid = new grid();
                // console.log($(this).data());
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
            // console.log(Grids);
            $.post("/MDBGrid/GetPageGrids", { "grids": Grids }, function (data, textStatus, jqXHR) {
                var g = JSON.parse(data);
                for (var i = 0; i < g.length; i++) {
                    MdbGridUI.renderGrid(gridPointers[g[i].element], g[i]);
                }
                //console.log(result);
            });
            //return result;
        };
        MdbGridUI.renderGrid = function (container, grid) {
            var _grid = GridFactory(grid.type);
            _grid.card = CardFactory(grid.card);
            return _grid.render(container, grid);
        };
        return MdbGridUI;
    }());
    exports.MdbGridUI = MdbGridUI;
});
//# sourceMappingURL=MdbUI.js.map