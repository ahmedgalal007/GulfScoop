/// <reference path="../../../typings/require/require.d.ts" />
/// <reference path="../../../typings/jquery/jquery.d.ts" />
/// <reference path="../../../typings/kendo/kendo.all.d.ts" />
/// <amd-dependency path="Scripts/app/lib/kendo.ui.core.min" >
// import $ = require("jquery");
// import kendo = require("Scripts/app/lib/kendo.ui.core.min");
// import { kendoTreeView } from "../../../kendo/2019.2.619/kendo.treeview.min";
var CheckTreeView = /** @class */ (function () {
    function CheckTreeView(selector, serviceUrl, data, onCheck) {
        var _this = this;
        this._checkedNodes = [];
        this.onCheck = function () {
            // console.log(this._selector);
            _this._checkedNodes = Array();
            CheckTreeView.checkedNodeIds($(_this._selector).data("kendoTreeView").dataSource.view(), _this._checkedNodes);
            _this._onCheck(_this._checkedNodes);
        };
        this._selector = selector;
        this._serviceUrl = serviceUrl;
        this._homogeneous = new kendo.data.HierarchicalDataSource({
            schema: {
                model: {
                    hasChildren: "hasChildren",
                    id: "id"
                }
            },
            transport: {
                read: {
                    dataType: "json",
                    url: this._serviceUrl
                }
            }
        });
        $(selector).kendoTreeView({
            check: this.onCheck,
            loadOnDemand: false,
            //ExpandAll:true,
            checkboxes: {
                checkChildren: false
            },
            dataSource: this._homogeneous,
            dataTextField: "Name",
            dataBound: function (e) {
                // Populate Treeview with Intial Values
                var treeview = e.sender;
                if (treeview.dataSource.data().length > 0) {
                    data.forEach(function (d) {
                        var getitem = treeview.dataSource.get(d);
                        if (getitem && treeview.findByUid(getitem.uid)) {
                            var selectitem = treeview.findByUid(getitem.uid);
                            treeview.select(selectitem);
                            treeview.dataItem(selectitem).set("checked", true);
                            console.log(selectitem);
                        }
                    });
                }
            }
        });
        this._onCheck = onCheck;
    }
    CheckTreeView.checkedNodeIds = function (nodes, checkedNodes) {
        nodes.forEach(function (node) {
            if (node.checked) {
                checkedNodes.push(node.id);
            }
            if (node.hasChildren) {
                CheckTreeView.checkedNodeIds(node.children.view(), checkedNodes);
            }
        });
    };
    return CheckTreeView;
}());
// export default CheckTreeView;
//# sourceMappingURL=CheckTreeView.js.map