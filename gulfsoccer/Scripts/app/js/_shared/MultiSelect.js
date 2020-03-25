class MultiSelect {
    constructor(selector, returnElementSelector, serviceUrl, data, noDataTemplateSelector, onCheck) {
        var options = {
            valuePrimitive: true,
            autoBind: false,
            change: (e) => {
                if (e) { }
                $(returnElementSelector).val($(selector).data("kendoMultiSelect").value().join(","));
            },
            filtering: function (e) {
                if (e) { }
                if ($(selector).data("kendoMultiSelect").input.val() != "") {
                    $.ajax({
                        type: "POST",
                        url: serviceUrl + "/ServerFiltering_GetProducts",
                        data: { text: "\"" + $("#tags").data("kendoMultiSelect").input.val() + "\"", val: JSON.stringify($("#tags").data("kendoMultiSelect").value()) },
                        success: function (d) {
                            var MS = $("#tags").data("kendoMultiSelect");
                            MS.dataSource.data().forEach(function (item) {
                                if (item && item["ID"]) {
                                    var selectedTags = $("#selectedTags").val().split(',').map(function (item) {
                                        return parseInt(item, 10);
                                    });
                                    var ItemFound;
                                    for (var t = 0; t < selectedTags.length; t++) {
                                        var stringPart = selectedTags[t];
                                        console.log(stringPart);
                                        if (stringPart !== item["ID"])
                                            continue;
                                        ItemFound = true;
                                        break;
                                    }
                                    if (!ItemFound || item["ID"] == "0") {
                                        MS.dataSource.remove(item);
                                    }
                                }
                            });
                            d.forEach(function (item) {
                                var valStr = $(selector).data("kendoMultiSelect").value().join(",");
                                if (!valStr.match(new RegExp("(?:^|,)" + item["ID"] + "(?:,|$)"))) {
                                    if (!$(selector).data("kendoMultiSelect").dataSource.get(item["ID"]))
                                        $(selector).data("kendoMultiSelect").dataSource.add({ ID: item["ID"], Name: item["Name"] });
                                }
                            });
                        },
                        dataType: "json",
                        contentType: "application/x-www-form-urlencoded",
                    });
                }
                else {
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
                    parameterMap: function (options, operation) {
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
            placeholder: "",
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
//# sourceMappingURL=MultiSelect.js.map