///<reference path="../../../node_modules/@types/promise/index.d.ts"/>
///<reference path="../../../node_modules/@types/systemjs/index.d.ts"/>

module Pages.Home {
    export class Index {
        constructor() {
            debugger; 
            System.import("Pages/Home/Blah").then(() => {
                var b = new Pages.Home.Blah();
                console.log("hello");
            });
        }
    }
}
