const callback = (checkedNodes) => {
    $("#selectedCategories").val(checkedNodes.join(","));
};
const ServiceUrl = "/admin/CategoryTreeView/Remote_Data_Binding_Get_Categories";
const baseImgUrl = "/Admin/ImageBrowser/";
const baseFileUrl = "/Admin/FileBrowser/";
const ImageBrowserOptions = {
    apply: (e) => {
        if (e) { }
    },
    change: (e) => {
        const path = e.sender.options.transport.imageUrl;
        const breadcrumbs = e.sender.breadcrumbs._value;
        const imgUrl = path.replace("{0}", "") + ((breadcrumbs === "/") ? "" : breadcrumbs) + e.selected.id;
        $("#featuredImage").val(imgUrl);
        $("#thumbnail").css({ "background-image": "url('" + imgUrl + "')", "background-size": "100% 100%" });
    },
    properties: [{ addToPath: "" }],
    transport: {
        create: {
            type: "POST",
            url: baseImgUrl + "Create",
        },
        destroy: {
            type: "POST",
            url: baseImgUrl + "Destroy",
        },
        imageUrl: "/Content/UserFiles/Images/{0}",
        read: baseImgUrl + "Read",
        thumbnailUrl: baseImgUrl + "Thumbnail",
        type: "imagebrowser-aspnetmvc",
        uploadUrl: baseImgUrl + "Upload",
    },
};
const EditorOptions = {
    fileBrowser: {
        transport: {
            create: { url: baseFileUrl + "Create" },
            destroy: { url: baseFileUrl + "Destroy" },
            fileUrl: "/Content/UserFiles/Images/{0}",
            read: { url: baseFileUrl + "Read" },
            type: "filebrowser-aspnetmvc",
            uploadUrl: "Upload",
        },
    },
    imageBrowser: ImageBrowserOptions,
    tools: [
        { name: "bold" },
        { name: "italic" },
        { name: "underline" },
        { name: "strikethrough" },
        { name: "justifyLeft" },
        { name: "justifyCenter" },
        { name: "justifyRight" },
        { name: "justifyFull" },
        { name: "insertUnorderedList" },
        { name: "insertOrderedList" },
        { name: "indent" },
        { name: "outdent" },
        { name: "createLink" },
        { name: "unlink" },
        { name: "insertImage" },
        { name: "insertFile" },
        { name: "foreColor" },
        { name: "backColor" },
        { name: "subscript" },
        { name: "superscript" },
        { name: "tableWizard" },
        { name: "createTable" },
        { name: "addRowAbove" },
        { name: "addRowBelow" },
        { name: "addColumnLeft" },
        { name: "addColumnRight" },
        { name: "deleteRow" },
        { name: "deleteColumn" },
        { name: "mergeCellsHorizontally" },
        { name: "mergeCellsVertically" },
        { name: "splitCellHorizontally" },
        { name: "splitCellVertically" },
        { name: "viewHtml" },
        { name: "formatting" },
        { name: "cleanFormatting" },
        { name: "fontName" },
        { name: "fontSize" },
        { name: "print" }
    ],
};
new CheckTreeView("#treeview", ServiceUrl, $("#selectedCategories").val().split(',').map(function (item) {
    return parseInt(item, 10);
}), callback);
new MultiSelect("#tags", "#selectedTags", "/Admin/MultiSelect", [], "#noDataTemplate", () => { });
$(document).ready(() => {
    if (false) {
        addNew("", "");
    }
    $("#editor").kendoEditor(EditorOptions);
    $("#dialog").kendoDialog({
        actions: [
            { text: "Cancel" },
            { text: "OK", primary: true, action: actionOK },
        ],
        closable: true,
        content: "<div id='imgBrowser'></div> <br /> <div id='imgUrl'></div>",
        initOpen: initOpen,
        modal: true,
        open: dialogOpen,
        title: "Employees",
        visible: false,
        width: "90%",
    });
    $("#updateddatetimepicker").kendoDateTimePicker({
        dateInput: true,
        value: new Date(),
    });
    let createdDate = new Date();
    if ($("#Id").val() > 0) {
        createdDate = $("#createddatetimepicker").val();
    }
    $("#createddatetimepicker").kendoDateTimePicker({
        dateInput: true,
        value: createdDate,
    });
    $("#owner").kendoAutoComplete({
        dataSource: {
            schema: {
                model: {
                    fields: {
                        Name: { type: "string" },
                    },
                    id: "ID",
                },
            },
            transport: {
                parameterMap: (_data, type) => {
                    if (type) { }
                    _data = {};
                    return { filter: $("#owner").val() };
                },
                prefix: "",
                read: {
                    data: onAdditionalData,
                    url: "/Autocompelete/FilterUsersList",
                },
            },
        },
        dataTextField: "Name",
        filter: "contains",
        minLength: 3,
    });
    $("#mainCategory").kendoAutoComplete({
        dataSource: {
            schema: {
                model: {
                    fields: {
                        Name: { type: "string" },
                    },
                    id: "ID",
                },
            },
            transport: {
                parameterMap: (_data, type) => {
                    if (type) { }
                    _data = {};
                    return { filter: $("#mainCategory").val() };
                },
                prefix: "",
                read: {
                    data: onAdditionalData,
                    url: "/Admin/Autocompelete/FilterCategoryList",
                },
            },
        },
        dataTextField: "Name",
        filter: "contains",
        minLength: 3,
    });
});
function initOpen(e) {
    if (e) { }
    $("#imgBrowser").kendoImageBrowser(ImageBrowserOptions);
}
function dialogOpen(e) {
    if (e) { }
}
function actionOK(e) {
    if (e) { }
}
$("#changeImage").kendoButton({
    click: openDialog,
});
$("#thumbnail").click(openDialog);
function openDialog(e) {
    e.preventDefault();
    e.stopPropagation = true;
    $("#dialog").data("kendoDialog").open();
}
function onAdditionalData() {
    return {
        text: $("#products").val(),
    };
}
function addNew(widgetId, value) {
    var widget = $("#" + widgetId).getKendoMultiSelect();
    var dataSource = widget.dataSource;
    if (confirm("Are you sure?")) {
        dataSource.add({
            ID: 0,
            Name: value
        });
        dataSource.one("requestEnd", function (args) {
            console.log("args =");
            console.log(args.type);
            if (args.type !== "create") {
                return;
            }
            var newValue = args.response[0].ID;
            console.log("newValue = " + newValue);
            dataSource.one("sync", function () {
                widget.value(widget.value().concat([newValue]));
            });
        });
        dataSource.sync();
    }
}
;
//# sourceMappingURL=admin.post.create.js.map