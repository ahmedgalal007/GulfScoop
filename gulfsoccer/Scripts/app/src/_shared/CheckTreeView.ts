///// <reference path="../../../typings/require/require.d.ts" />
///// <reference path="../../../typings/jquery/jquery.d.ts" />

/// <reference path="../../../typings/kendo/kendo.all.d.ts" />
/// <amd-dependency path="Scripts/app/lib/kendo.ui.core.min" >

// import $ = require("jquery");
// import kendo = require("Scripts/app/lib/kendo.ui.core.min");
// import { kendoTreeView } from "../../../kendo/2019.2.619/kendo.treeview.min";
class CheckTreeView {
    // public that = this;
    public _selector: string;
    private _checkedNodes: any[] = [];
    private _serviceUrl: string;
    private _homogeneous: any;
    private _onCheck: Function;

    public constructor(selector: string, serviceUrl: string, data: number[], onCheck: Function) {
        this._selector = selector;
        this._serviceUrl = serviceUrl;
        this._homogeneous = new kendo.data.HierarchicalDataSource({
            schema: {
                model: {
                    hasChildren: "hasChildren",
                    id: "id",
                },
            },
            transport: {
                read: {
                    dataType: "json",
                    url: this._serviceUrl,
                },
            },
        });

        $(selector).kendoTreeView({
            check: this.onCheck,
            loadOnDemand: false,
            //ExpandAll:true,
            checkboxes: {
                checkChildren: false,
            },
            dataSource: this._homogeneous,
            dataTextField: "Name",
            dataBound: function (e) {
                // Populate Treeview with Intial Values
                var treeview = e.sender;
                if (treeview.dataSource.data().length > 0) {
                    data.forEach(d => {
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

    public onCheck = () => {
        // console.log(this._selector);
        this._checkedNodes = Array<any>();
        CheckTreeView.checkedNodeIds($(this._selector).data("kendoTreeView").dataSource.view(), this._checkedNodes);
        this._onCheck(this._checkedNodes);
    }

    public static checkedNodeIds(nodes: any, checkedNodes: any[]) {
        nodes.forEach((node: any) => {
            if (node.checked) {
                checkedNodes.push(node.id);
            }

            if (node.hasChildren) {
                CheckTreeView.checkedNodeIds(node.children.view(), checkedNodes);
            }
        });
    }
}

// export default CheckTreeView;