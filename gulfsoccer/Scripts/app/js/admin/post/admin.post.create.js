/// <reference path="../../../../typings/require/require.d.ts" />
/// <reference path="../../../../typings/kendo/kendo.all.d.ts" />
//,
//requirejs.config({
//    baseUrl: '',
//    paths: {
//        jquery: 'Scripts/app/lib/jquery.min',
//        kendo: '../../../../../Scripts/app/lib/kendo.ui.core.min',
//        app: 'Scripts/app/js/admin/post/app'
//    },
//    shim: {
//        kendo: {
//            deps: ['jquery'],
//            exports: 'kendo'
//        }
//    }
//});
// requirejs(['jquery','MdbUI'], function (MdbUI) { }); 
// import { JQuery as $ } from "jquery-3.4.1.min"
var callback = function (checkedNodes /*, ...args: any[]*/) {
    // let message: string;
    // if (checkedNodes.length > 0) {
    //     message = "IDs of checked nodes: " + checkedNodes.join(",");
    // } else {
    //     message = "No nodes checked.";
    // }
    $("#selectedCategories").val(checkedNodes.join(","));
};
var ServiceUrl = "/admin/CategoryTreeView/Remote_Data_Binding_Get_Categories";
var baseImgUrl = "/Admin/ImageBrowser/";
var baseFileUrl = "/Admin/FileBrowser/";
var ImageBrowserOptions = {
    apply: function (e) {
        if (e) { }
        // console.log(e.sender.dataSource.data()); // Get the Image Array
        // console.log(e.sender.options.transport.imageUrl);
        // console.log(e);
    },
    change: function (e) {
        // console.log(e.sender.breadcrumbs._value);
        var path = e.sender.options.transport.imageUrl;
        var breadcrumbs = e.sender.breadcrumbs._value;
        var imgUrl = path.replace("{0}", "") + ((breadcrumbs === "/") ? "" : breadcrumbs) + e.selected.id;
        $("#featuredImage").val(imgUrl);
        $("#thumbnail").css({ "background-image": "url('" + imgUrl + "')", "background-size": "100% 100%" });
    },
    properties: [{ addToPath: "" }],
    transport: {
        create: {
            type: "POST",
            url: baseImgUrl + "Create"
        },
        destroy: {
            type: "POST",
            url: baseImgUrl + "Destroy"
        },
        imageUrl: "/Content/UserFiles/Images/{0}",
        read: baseImgUrl + "Read",
        thumbnailUrl: baseImgUrl + "Thumbnail",
        type: "imagebrowser-aspnetmvc",
        uploadUrl: baseImgUrl + "Upload"
    }
};
var EditorOptions = {
    fileBrowser: {
        transport: {
            create: { url: baseFileUrl + "Create" },
            destroy: { url: baseFileUrl + "Destroy" },
            fileUrl: "/Content/UserFiles/Images/{0}",
            read: { url: baseFileUrl + "Read" },
            type: "filebrowser-aspnetmvc",
            uploadUrl: "Upload"
        }
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
    ]
};
new CheckTreeView("#treeview", ServiceUrl, $("#selectedCategories").val().split(','), callback);
new MultiSelect("#tags", "#selectedTags", "/Admin/MultiSelect", [], "#noDataTemplate", function () { });
$(document).ready(function () {
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
        width: "90%"
    });
    $("#updateddatetimepicker").kendoDateTimePicker({
        dateInput: true,
        value: new Date()
    });
    $("#createddatetimepicker").kendoDateTimePicker({
        dateInput: true,
        value: $("#Id").val() > 0 ? $("#createddatetimepicker").val() : new Date()
    });
    $("#owner").kendoAutoComplete({
        dataSource: {
            schema: {
                model: {
                    fields: {
                        Name: { type: "string" }
                    },
                    id: "ID"
                }
            },
            transport: {
                parameterMap: function (_data, type) {
                    // if type is "read", then data is { foo: 1 }, we also want to add { "bar": 2 }
                    if (type) { }
                    _data = {};
                    return { filter: $("#owner").val() };
                },
                prefix: "",
                read: {
                    data: onAdditionalData,
                    url: "/Autocompelete/FilterUsersList"
                }
            }
        },
        dataTextField: "Name",
        // "serverFiltering": true,
        filter: "contains",
        minLength: 3
    });
});
function initOpen(e) {
    if (e) { }
    $("#imgBrowser").kendoImageBrowser(ImageBrowserOptions);
}
function dialogOpen(e) {
    if (e) { }
    // console.log(e);
}
function actionOK(e) {
    if (e) { }
    // console.log(e);
}
$("#changeImage").kendoButton({
    click: openDialog
});
$("#thumbnail").click(openDialog);
function openDialog(e) {
    e.preventDefault();
    e.stopPropagation = true;
    $("#dialog").data("kendoDialog").open();
}
function onAdditionalData() {
    return {
        text: $("#products").val()
    };
}
/* tslint:disable:no-unused-variable */
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
            // var newObj = JSON.parse(args.response);
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
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/*$("#tags").kendoMultiSelect({
    valuePrimitive: true,
    autoBind: false,
    change: (e) => {
        // console.log("Change");
        $("#selectedTags").val($("#tags").data("kendoMultiSelect").value().join(","));
    },
    filtering: function (e) {
        // console.log(e);
        if ($("#tags").data("kendoMultiSelect").input.val() != "") {
            $.ajax({
                type: "POST",
                url: "/Admin/MultiSelect/ServerFiltering_GetProducts",
                data: { text: "\"" + $("#tags").data("kendoMultiSelect").input.val() + "\"", val: JSON.stringify($("#tags").data("kendoMultiSelect").value()) },
                success: function (d) {
                    // console.log(JSON.stringify(d));
                    var MS = $("#tags").data("kendoMultiSelect");
                    var saved_values = MS.value();
                    var filter_value = MS.input.val();
                    // console.log('tagList');
                    var tag_list = MS.tagList.clone();
                    // console.log('list');
                    // console.log(MS.list);
                    ////////////////////
                    // MS.dataSource.data([]);
                   
                   
                   
                    MS.dataSource.data().forEach(function (item: any) {
                        if (item && item["ID"]) {
                            var selectedTags = $("#selectedTags").val().split(',');
                            var ItemFound;
                            for (var t = 0; t < selectedTags.length; t++) {
                                var stringPart = selectedTags[t];
                                console.log(stringPart);
                                if (stringPart !== item["ID"]) continue;

                                ItemFound = true;
                                break;
                            }
                            if (!ItemFound || item["ID"] == "0") {
                                MS.dataSource.remove(item);
                            }
                            // MS.dataSource.remove(item);
                        }
                        // console.log(item);
                      
                    });
                    d.forEach(function (item: any) {
                        // console.log(item["Name"]);
                        var valStr = $("#tags").data("kendoMultiSelect").value().join(",");
                        // console.log(valStr);
                        if ( !valStr.match(new RegExp("(?:^|,)" + item["ID"] + "(?:,|$)"))) {
                            // it's there
                            if (!$("#tags").data("kendoMultiSelect").dataSource.get(item["ID"]))
                                    $("#tags").data("kendoMultiSelect").dataSource.add({ ID: item["ID"], Name: item["Name"] });
                        }
                    });
                    // MS.value(saved_values);
                    // MS.input.val(filter_value);
                    // MS.tagList = tag_list;
                   
                },
                dataType: "json",
                contentType: "application/x-www-form-urlencoded",
            });
            //prevent filtering if the filter does not have value
            // e.preventDefault();
        } else {
            // $("#tags").data("kendoMultiSelect").dataSource.data({});
        }
       
    },
    dataSource: {
         batch: true,
         schema: {
             //data: function (response) {
             //    //alert("Schema-Data");
             //    console.log(response);
             //    if (response.length) {
             //        for (var i = 0; i < response.length; i++) {
             //            var MS = $("#tags").data("kendoMultiSelect");
             //            console.log(MS.dataSource.get(0));
             //            MS.dataSource.get(0).id = response[i].ID;
             //            console.log(MS.dataSource.get(response[i].ID))
             //        }
             //    }
             //    return response.models // twitter's response is { "statuses": [ / results / ] }
             //},
             // data:"items",
             model: {
                 id: "ID",
                 fields: {
                     ID: { from: "ID", type: "number"  },
                     Name: { from: "Name", type: "string" }
                 }
             }
         },
        serverFiltering: true,
        transport: {
            create: {
                contentType: "application/x-www-form-urlencoded",
                dataType: "json",
                type: "post",
                url: "/Admin/MultiSelect/create"
            },
            parameterMap: function (options, operation) {
               
                if (operation !== "read" ) {
                    console.log(operation);
                    console.log(kendo.stringify(options));
                    // console.log({ models: kendo.stringify(options.models || [options]) });
                    // operation.data = [{ ID: 0, Name: $("#tags").data("kendoMultiSelect").input.val() }];
                    return  kendo.stringify(options);
                }
               
                /console.log(operation);
                if ((operation !== "read" && options.models) || operation == "create" ) {
                    console.log("options.models");

                    console.log(kendo.stringify(options.models));
                    //operation.data = options.models;
                    console.log(operation);

                    // console.log(kendo.stringify(options.models));
                    // return { models: kendo.stringify(options.models) };
                    if (options.models) {
                        return { models: kendo.stringify(options.models) };
                    } else {
                        return { models: { ID: "0", Name: $("#tags").data("kendoMultiSelect").input.val() } };
                    }
                   
                }/
            },
            read: {
                    //cache: false,
                contentType: "application/x-www-form-urlencoded",
                dataType: "json",
                type: "post",
                url: "/Admin/MultiSelect/ServerFiltering_GetProducts",
            },
            cache: false,
            serverFiltering: true,
        },
    },
    dataTextField: "Name",
    dataValueField: "ID",
    filter: "startswith",
    noDataTemplate: $("#noDataTemplate").html(),
    placeholder: "",  /*"Select Tag..."/
    //value: [
    //    { Name: "Two", ID: 2 },
    //    { Name: "Three", ID: 3 },
    //],

   
});
$.get("/Admin/MultiSelect/SelectValues", { data: $("#selectedTags").val() }, function (d) {
    var multiselect = $("#tags").data("kendoMultiSelect");
    multiselect.dataSource.data(JSON.parse(d));
    multiselect.value(JSON.parse(d));
    multiselect.trigger("change");
});*/ 
//# sourceMappingURL=admin.post.create.js.map