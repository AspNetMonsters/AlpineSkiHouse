var Pages;
(function (Pages) {
    var Home;
    (function (Home) {
        var Blah = (function () {
            function Blah() {
                console.log("blah");
            }
            return Blah;
        }());
        Home.Blah = Blah;
    })(Home = Pages.Home || (Pages.Home = {}));
})(Pages || (Pages = {}));
