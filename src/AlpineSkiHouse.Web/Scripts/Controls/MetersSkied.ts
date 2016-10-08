///<reference path="../../node_modules/@types/chartjs/index.d.ts" />

import {Chart} from "chart";

export class MetersSkied {
    public labels: string[];

    constructor(container: HTMLElement) {

        this.labels = ["November", "December", "January", "February", "March", "April"];
        var chart = new Chart(container, {
                 type: 'bar',
                 data: {
                     labels: this.labels,
                    datasets: [{
                        label: 'Meters Skied',
                        data: [1200, 1900, 300, 500, 200, 300],
                        backgroundColor: [
                            'rgba(255, 99, 132, 0.2)',
                            'rgba(54, 162, 235, 0.2)',
                            'rgba(255, 206, 86, 0.2)',
                            'rgba(75, 192, 192, 0.2)',
                            'rgba(153, 102, 255, 0.2)',
                            'rgba(255, 159, 64, 0.2)'
                        ],
                        borderColor: [
                            'rgba(255,99,132,1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(75, 192, 192, 1)',
                            'rgba(153, 102, 255, 1)',
                            'rgba(255, 159, 64, 1)'
                        ],
                        borderWidth: 1
                    }]
                },
                options: { 
                    scales: {
                        yAxes: [
                            {
                                ticks:{
                                    beginAtZero: true
                                }
                            }
                        ]

                    }
                }
        });
    }
}