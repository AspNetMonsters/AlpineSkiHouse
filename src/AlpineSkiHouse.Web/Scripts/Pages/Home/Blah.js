System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Blah;
    return {
        setters:[],
        execute: function() {
            Blah = (function () {
                function Blah() {
                    console.log("blah");
                }
                return Blah;
            }());
            exports_1("Blah", Blah);
        }
    }
});
