describe("When viewing the MetersSkied chart", function () {
    var MetersSkied;
    beforeAll(function (done) {
        System.import("Controls/MetersSkied").then(function (m) {            
            MetersSkied = m;
            done();
        });
    });

    it("should display data for the ski season", function () {        
        var wrapper = document.createElement('div');
        var canvas = document.createElement('canvas');
        wrapper.appendChild(canvas);
        window.document.body.appendChild(wrapper);

        var chart = new MetersSkied.MetersSkied(canvas);
        
        expect(chart.labels.length).toBe(6);
        expect(chart.labels).toContain("November");
        expect(chart.labels).toContain("December");
        expect(chart.labels).toContain("January");
        expect(chart.labels).toContain("February");
        expect(chart.labels).toContain("March");
        expect(chart.labels).toContain("April");
    })
});