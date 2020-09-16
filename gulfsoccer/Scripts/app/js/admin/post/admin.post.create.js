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
var ImageCropper = $('#ImageCropper'), undo = $(".btn-thumbnail");
var cropBoxData;
var canvasData;
var cropper;
var cropOptions = {
    autoCropArea: 0.9,
    aspectRatio: 16 / 9,
    ready: function () {
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
    currentThumbSizeId: 0,
    thumbnailsBoxes: JSON.parse($('#featuredImagethumbs').val()),
};
undo.click(function (e) {
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
    });
    ImageCropper.data("kendoDialog").open();
});
function onCropperClose() {
    cropBoxData = cropper.getCropBoxData();
    canvasData = cropper.getCanvasData();
    cropper.destroy();
}
function onCropperOpen() {
    var image = $('#cropper-img');
    image.attr('src', $('#ImgCropper').attr("src"));
    console.log("Image-src:" + image.attr('src'));
    $("#select-AspectRatio").kendoButtonGroup({
        select: function (e) {
            let BoxSizes = cropOptions.thumbnailsBoxes;
            if (BoxSizes.length == 0) {
                for (var i = 0; i < e.sender.element.children().length; i++) {
                    BoxSizes[i] = new CropBox();
                }
            }
            cropOptions.currentthumbindex = e.indices;
            cropOptions.currentThumbSizeId = e.sender.element.children().eq(e.indices).data("thumbSizeId");
            cropOptions.aspectRatio = e.sender.element.children().eq(e.indices).data("aspectRatio");
            if (cropper) {
                cropper.destroy();
            }
            ;
            if (BoxSizes[e.indices].width > 0 && BoxSizes[e.indices].height > 0) {
                cropBoxData = { left: BoxSizes[e.indices].left, top: BoxSizes[e.indices].top, width: BoxSizes[e.indices].boxWidth, height: BoxSizes[e.indices].boxHeight };
            }
            cropper = new Cropper(document.getElementById("cropper-img"), cropOptions);
        },
        index: 0
    });
}
class CropBox {
    constructor() {
        this.ThumbSizeId = 0;
        this.x = 0;
        this.y = 0;
        this.width = 0;
        this.height = 0;
        this.rotate = 0;
        this.scaleX = 0;
        this.scaleY = 0;
        this.left = 0;
        this.top = 0;
        this.boxWidth = 0;
        this.boxHeight = 0;
    }
    CropBox(ThumbSizeId, x, y, width, height, rotate, scaleX, scaleY, left, top, boxWidth, boxHeight) {
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
    }
    ;
}
//# sourceMappingURL=admin.post.create.js.map