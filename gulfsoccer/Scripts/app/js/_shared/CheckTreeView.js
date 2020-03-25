class CheckTreeView {
    constructor(selector, serviceUrl, data, onCheck) {
        this._checkedNodes = [];
        this.onCheck = () => {
            this._checkedNodes = Array();
            CheckTreeView.checkedNodeIds($(this._selector).data("kendoTreeView").dataSource.view(), this._checkedNodes);
            this._onCheck(this._checkedNodes);
        };
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
            checkboxes: {
                checkChildren: false,
            },
            dataSource: this._homogeneous,
            dataTextField: "Name",
            dataBound: function (e) {
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
    static checkedNodeIds(nodes, checkedNodes) {
        nodes.forEach((node) => {
            if (node.checked) {
                checkedNodes.push(node.id);
            }
            if (node.hasChildren) {
                CheckTreeView.checkedNodeIds(node.children.view(), checkedNodes);
            }
        });
    }
}
//# sourceMappingURL=CheckTreeView.js.map