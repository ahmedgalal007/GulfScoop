///// <reference path="../../../typings/require/require.d.ts" />
///// <reference path="../../../typings/jquery/jquery.d.ts" />

/// <reference path="../../../typings/kendo/kendo.all.d.ts" />
/// <amd-dependency path="Scripts/app/lib/kendo.ui.core.min" >

// import $ = require("jquery");
// import kendo = require("Scripts/app/lib/kendo.ui.core.min");

class MultiSelect {
    public constructor(selector: string, returnElementSelector: string, serviceUrl: string, data: number[], noDataTemplateSelector: string, onCheck: Function) {
        var options =
        {
            valuePrimitive: true,
            autoBind: false,
            change: (e: any) => {
                if (e) { }
                // console.log("Change");
                $(returnElementSelector).val($(selector).data("kendoMultiSelect").value().join(","));
            },
            filtering: function (e: any) {
                if (e) { }
                // console.log(e);
                if ($(selector).data("kendoMultiSelect").input.val() != "") {
                    $.ajax({
                        type: "POST",
                        url: serviceUrl + "/ServerFiltering_GetProducts",
                        data: { text: "\"" + $("#tags").data("kendoMultiSelect").input.val() + "\"", val: JSON.stringify($("#tags").data("kendoMultiSelect").value()) },
                        success: function (d) {
                            var MS = $("#tags").data("kendoMultiSelect");

                            MS.dataSource.data().forEach(function (item: any) {
                                if (item && item["ID"]) {
                                    var selectedTags = (<string>$("#selectedTags").val()).split(',').map(function (item) {
                                        return parseInt(item, 10);
                                    });
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
                                }
                            });
                            d.forEach(function (item: any) {
                                var valStr = $(selector).data("kendoMultiSelect").value().join(",");
                                // console.log(valStr);
                                if (!valStr.match(new RegExp("(?:^|,)" + item["ID"] + "(?:,|$)"))) {
                                    // it's there
                                    if (!$(selector).data("kendoMultiSelect").dataSource.get(item["ID"]))
                                        $(selector).data("kendoMultiSelect").dataSource.add({ ID: item["ID"], Name: item["Name"] });
                                }
                            });
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
                cache: false,
                schema: {
                    model: {
                        id: "ID",
                        fields: {
                            ID: { from: "ID", type: "number" },
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
                        url: serviceUrl + "/create"
                    },
                    parameterMap: function (options: any, operation: any) {
                        if (operation !== "read") {
                            console.log(operation);
                            console.log(kendo.stringify(options));
                            return kendo.stringify(options);
                        }
                        return kendo.stringify(options);
                    },
                    read: {
                        contentType: "application/x-www-form-urlencoded",
                        dataType: "json",
                        type: "post",
                        url: serviceUrl + "/ServerFiltering_GetProducts",
                    },
                },
            },
            dataTextField: "Name",
            dataValueField: "ID",
            filter: "startswith",
            noDataTemplate: $(noDataTemplateSelector).html(),
            placeholder: "",  /*"Select ..."*/
            oncheck: onCheck,
        };
        if (data) { }

        $(document).ready(() => {
            $(selector).kendoMultiSelect(options);
            $.get(serviceUrl + "/SelectValues", { data: $(returnElementSelector).val() }, function (d) {
                var multiselect = $(selector).data("kendoMultiSelect");
                multiselect.dataSource.data(JSON.parse(d));
                multiselect.value(JSON.parse(d));
                multiselect.trigger("change");
            });
        });
    }
}

// export default MultiSelect;