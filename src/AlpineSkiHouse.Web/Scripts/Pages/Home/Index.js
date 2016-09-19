System.register(["./Blah"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Blah_1;
    var Index;
    return {
        setters:[
            function (Blah_1_1) {
                Blah_1 = Blah_1_1;
            }],
        execute: function() {
            Index = (function () {
                function Index() {
                    var b = new Blah_1.Blah();
                    console.log("hello");
                }
                return Index;
            }());
            exports_1("Index", Index);
        }
    }
});
