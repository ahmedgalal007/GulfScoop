///// <reference path="../../../../typings/require/require.d.ts" />
/// <reference path="../../../../typings/jquery/jquery.d.ts" />
/// <reference path="../../../../typings/kendo/kendo.all.d.ts" />
/// <reference path="../../../lib/cropperjs/cropper.d.ts" />
//,
// import * as $ from 'jquery';
// import * as kendo from 'kendo';

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
//requirejs(['jquery', 'kendo'], function ($, kendo) {
//    kendo.parseInt("1");
    const callback = (checkedNodes: any/*, ...args: any[]*/) => {
        // let message: string;
        // if (checkedNodes.length > 0) {
        //     message = "IDs of checked nodes: " + checkedNodes.join(",");
        // } else {
        //     message = "No nodes checked.";
        // }
        $("#selectedCategories").val(checkedNodes.join(","));
    };
    const ServiceUrl: string = "/admin/CategoryTreeView/Remote_Data_Binding_Get_Categories";

    const baseImgUrl = "/Admin/ImageBrowser/";
    const baseFileUrl = "/Admin/FileBrowser/";

    const ImageBrowserOptions = {
        apply: (e: any) => {
            if (e) { }
            // console.log(e.sender.dataSource.data()); // Get the Image Array
            // console.log(e.sender.options.transport.imageUrl);
            // console.log(e);
        },
        change: (e: any) => {
            // console.log(e.sender.breadcrumbs._value);
            const path = e.sender.options.transport.imageUrl;
            const breadcrumbs = e.sender.breadcrumbs._value;
            const imgUrl = path.replace("{0}", "") + ((breadcrumbs === "/") ? "" : breadcrumbs) + e.selected.id;
            $("#featuredImage").val(imgUrl);
            $("#thumbnail").css({ "background-image": "url('" + imgUrl + "')", "background-size": "100% 100%" });
            $("#ImgCropper").attr("src", imgUrl);
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

    new CheckTreeView("#treeview", ServiceUrl, (<string>$("#selectedCategories").val()).split(',').map(function (item) {
        return parseInt(item, 10);
    }), callback);
    new MultiSelect("#tags", "#selectedTags", "/Admin/MultiSelect", [], "#noDataTemplate", () => { });
    $(document).ready(() => {
        if (false) { addNew("", ""); }
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

        let createdDate: Date = new Date();
        if ($("#Id").val() as number > 0) {
            createdDate = $("#createddatetimepicker").val() as unknown as Date;
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
                    parameterMap: (_data: any, type: any) => {
                        // if type is "read", then data is { foo: 1 }, we also want to add { "bar": 2 }
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
            // "serverFiltering": true,
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
                    parameterMap: (_data: any, type: any) => {
                        // if type is "read", then data is { foo: 1 }, we also want to add { "bar": 2 }
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
            // "serverFiltering": true,
            filter: "contains",
            minLength: 3,
        });


        
    });

    function initOpen(e: any) {
        if (e) { }
        $("#imgBrowser").kendoImageBrowser(ImageBrowserOptions);
    }
    function dialogOpen(e: any) {
        if (e) { }
        // console.log(e);
    }

    function actionOK(e: any) {
        if (e) { }
        // console.log(e);
    }

    $("#changeImage").kendoButton({
        click: openDialog,
    });
    $("#thumbnail").click(openDialog);

    function openDialog(e: any) {
        e.preventDefault();
        e.stopPropagation = true;
        $("#dialog").data("kendoDialog").open();
    }

    function onAdditionalData() {
        return {
            text: $("#products").val(),
        };
    }

    /* tslint:disable:no-unused-variable */
    function addNew(widgetId: any, value: any) {
        var widget = $("#" + widgetId).getKendoMultiSelect();
        var dataSource = widget.dataSource;

        if (confirm("Are you sure?")) {
            dataSource.add({
                ID: 0,
                Name: value
            });

            dataSource.one("requestEnd", function (args: any) {
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
};

//// ImageCropper /////////////////////////////////////////////////////

    var ImageCropper = $('#ImageCropper'),
        undo = $(".btn-thumbnail");
    //var image = document.getElementById('thumbnail');
    var cropBoxData;
    var canvasData;
    var cropper;
    var cropOptions = {
        autoCropArea: 0.9,
        aspectRatio: 16 / 9,
        ready: function () {
            //Should set crop box data first here
            cropper.setCropBoxData(cropBoxData).setCanvasData(canvasData);
        },
        cropstart: function (e) {
            console.log(e.type, e.detail.action);
        },
        cropmove: function (e) {
            console.log(e.type, e.detail.action);

        },
        cropend: function (e) {
            console.log(e.type, e.detail.action);
        },
        crop: function (e) {
            let currentThumbBox = cropOptions.thumbnailsBoxes[cropOptions.currentthumbindex];
            let crpBoxData = cropper.getCropBoxData();
            currentThumbBox.x = Math.round(e.detail.x);
            currentThumbBox.y = Math.round(e.detail.y);
            currentThumbBox.width = Math.round(e.detail.width);
            currentThumbBox.height = Math.round(e.detail.height);
            currentThumbBox.rotate = Math.round(e.detail.rotate);
            currentThumbBox.scaleX = Math.round(e.detail.scaleX);
            currentThumbBox.scaleY = Math.round(e.detail.scaleY);
            currentThumbBox.left = Math.round(crpBoxData.left);
            currentThumbBox.top = Math.round(crpBoxData.top);
            currentThumbBox.boxWidth = Math.round(crpBoxData.width);
            currentThumbBox.boxHeight = Math.round(crpBoxData.height);
            currentThumbBox.ThumbSizeId = cropOptions.currentThumbSizeId;
            $('#featuredImagethumbs').val(JSON.stringify(cropOptions.thumbnailsBoxes));
            console.log($('#featuredImagethumbs').val());
        },
        currentthumbindex: 0,
        currentThumbSizeId:0,
        thumbnailsBoxes: JSON.parse($('#featuredImagethumbs').val() as string),
    };

    undo.click(function (e:any) {
        e.preventDefault();
        e.stopPropagation = true;
        ImageCropper.kendoDialog({
            width: "450px",
            title: "Crop Featured Image",
            closable: true,
            modal: true,
            content: $('#cropperContentTemplate').html(),
            actions: [
                { text: 'Cancel' },
                { text: 'OK', primary: true, action: actionOK }
            ],
            close: onCropperClose,
            open: onCropperOpen,
            // initOpen: onCropperInitOpen,
            // show: onShow,
            // hide: onHide
        });
        ImageCropper.data("kendoDialog").open();
        //undo.fadeOut();
    });

    function onCropperClose() {
        cropBoxData = cropper.getCropBoxData();
        canvasData = cropper.getCanvasData();
        cropper.destroy();
        // undo.fadeIn();
    }

    function onCropperOpen() {
        var image = $('#cropper-img');
        image.attr('src', $('#ImgCropper').attr("src") as string);
        console.log("Image-src:" + image.attr('src'));
        //cropper = new Cropper(document.getElementById("cropper-img"), cropOptions);
        $("#select-AspectRatio").kendoButtonGroup({
            select: function (e) {
                // let BoxSizesElem: Array<CropBox> = $('#featuredImagethumbs').val() as unknown as Array<CropBox>;
                let BoxSizes: Array<CropBox> = cropOptions.thumbnailsBoxes;
                if (BoxSizes.length == 0) {
                    for (var i = 0; i < e.sender.element.children().length; i++) {
                        BoxSizes[i] = new CropBox();
                    }
                }
                
                cropOptions.currentthumbindex = e.indices;
                cropOptions.currentThumbSizeId = e.sender.element.children().eq(e.indices).data("thumbSizeId");
                cropOptions.aspectRatio = e.sender.element.children().eq(e.indices).data("aspectRatio") // (<HTMLElement>(e.sender).element.data("aspectRatio")) as unknown as number;
                if (cropper) {
                    cropper.destroy(); 
                };
                if (BoxSizes[e.indices].width > 0 && BoxSizes[e.indices].height >0) {
                    cropBoxData = { left: BoxSizes[e.indices].left, top: BoxSizes[e.indices].top, width: BoxSizes[e.indices].boxWidth, height: BoxSizes[e.indices].boxHeight };
                }
                cropper = new Cropper(document.getElementById("cropper-img"), cropOptions);
            },
            index: 0
        });
    }
class CropBox {
    public CropBox(ThumbSizeId: number, x: number, y: number, width: number, height: number
        , rotate: number, scaleX: number, scaleY: number
        , left: number, top: number, boxWidth: number, boxHeight: number) {
        this.ThumbSizeId = ThumbSizeId;
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
        this.rotate = rotate;
        this.scaleX = scaleX;
        this.scaleY = scaleY;
        this.left = left;
        this.top = top;
        this.boxWidth = boxWidth;
        this.boxHeight = boxHeight;
    };   
    ThumbSizeId: number = 0; 
    x: number = 0;
    y: number = 0;
    width: number = 0;
    height: number = 0;
    rotate: number = 0;
    scaleX: number = 0;
    scaleY: number = 0;
    left: number = 0;
    top: number = 0;
    boxWidth: number = 0;
    boxHeight: number = 0;
}
// });
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