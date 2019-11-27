function ArticleViewModel(app, dataModel) {
    var self = this;

    self.myArticleTitle = ko.observable("");

    Sammy(function () {
        this.get('#article', function () {
            // Make a call to the protected Web API by passing in a Bearer Authorization Header
            $.ajax({
                method: 'get',
                url: app.dataModel.articleUrl,
                contentType: "application/json; charset=utf-8",
                headers: {
                    'Authorization': 'Bearer ' + app.dataModel.getAccessToken()
                },
                success: function (data) {
                    self.myArticleTitle('Your Article Title is : ' + data.title);
                }
            });
        });
        //this.get('/article', function () { this.app.runRoute('get', '#article'); });
    });

    return self;
}

app.addViewModel({
    name: "Article",
    bindingMemberName: "article",
    factory: ArticleViewModel
});
