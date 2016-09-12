///<reference path="../../../node_modules/@types/promise/index.d.ts"/>
///<reference path="../../../node_modules/@types/systemjs/index.d.ts"/>
var Pages;
(function (Pages) {
    var Home;
    (function (Home) {
        var Index = (function () {
            function Index() {
                debugger;
                System.import("Pages/Home/Blah").then(function () {
                    var b = new Pages.Home.Blah();
                    console.log("hello");
                });
            }
            return Index;
        }());
        Home.Index = Index;
    })(Home = Pages.Home || (Pages.Home = {}));
})(Pages || (Pages = {}));
