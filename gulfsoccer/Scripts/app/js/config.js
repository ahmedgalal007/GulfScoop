requirejs.config({
    baseUrl: 'Scripts/app/js/',
    paths: {
        MdbUI: './_shared/templates/MdbUI',
        jquery: '../../jquery-3.4.1.min',
        kendo: '../../kendo/2019.2.619/kendo.all.min'
    }
});
requirejs(['jquery', 'MdbUI'], function ($, MdbUI) {
    $(document).ready(function () {
        var cardTemplate = $('#card-template').html();
        var container = $("#cards-container");
        $.get("/Home/getLatestPosts/", { "count": 3, "skip": 0 }, function (data) {
            var items = JSON.parse(data);
            $(items).each(function (item) {
                var card = $("<div>").addClass("col-md-4 py-2").html(cardTemplate);
                card.find('h5').first().text(items[item]["title"]);
                card.find('pattern').first().attr('id', 'img-' + items[item]["id"]);
                card.find('image').first().attr('xlink:href', items[item]["featuredImage"]["Uri"]);
                card.find('rect').first().attr('fill', 'url(#img-' + items[item]["id"] + ')');
                card.find('a').first().attr('href', items[item]["permalink"]);
                container.append(card);
                console.log(items[item]);
            });
        }, "json");
        MdbUI.MdbGridUI.buildGrids($('[role="mdb-grid"]'));
    });
});
//# sourceMappingURL=config.js.map