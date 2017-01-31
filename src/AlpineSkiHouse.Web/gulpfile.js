﻿/// <binding Clean='clean' ProjectOpened='watch' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    typescript = require("gulp-typescript"),
    rename = require("gulp-rename2"),
    watch = require("gulp-watch"),
    imageop = require('gulp-image-optimization'),
    sass = require("gulp-sass"),
    merge = require("merge-stream"),
    plumber = require("gulp-plumber");

var webroot = "./wwwroot/";
var sourceroot = "./Scripts/"

var paths = {
    loader: "jspm_packages/**/*.js",
    loaderConfig: "Scripts/jspmconfig.js",
    ts: sourceroot + "**/*.ts*",
    tsDefintionFiles: "npm_modules/@types/**/*.d.ts",
    minJs: webroot + "js/**/*.min.js",
    sass: "style/**/bootstrap-alpine.scss",
    sassDest: webroot + "css",
    images: ["images/**/*.jpg", "images/**/*.jpeg", "images/**/*.png"],
    imagesDest: webroot + "images",
    css: webroot + "css/**/*.css",
    minCss: webroot + "css/**/*.min.css",
    concatJsDest: webroot + "js/site.min.js",
    jsDest: webroot + "js/",
    jsPackages: `${webroot}/jspm_packages`,
    concatCssDest: webroot + "css/bootstrap-alpine.min.css"
};

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("stage-loader", function () {
    var config = gulp.src(paths.loaderConfig)
                    .pipe(gulp.dest(paths.jsDest));
    var packages = gulp.src(paths.loader)
        .pipe(gulp.dest(paths.jsPackages));
    return merge(config, packages);
});

gulp.task("typescript", function(){
    return gulp.src([paths.tsDefintionFiles, paths.ts, "!" + paths.minJs], { base: "." })
        .pipe(typescript({
            module: "system",
            jsx: "react"
        }))
        .pipe(rename((pathObj, file) => {
            return pathObj.join(
                pathObj.dirname(file).replace(/^Scripts\/?\\?/, ''),
                pathObj.basename(file));
        }))
        .pipe(gulp.dest(paths.jsDest));
});

gulp.task("min:js", function () {
    return gulp.src([paths.ts, "!" + paths.minJs], { base: "." })
        .pipe(typescript())
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("sass", function(){
    return gulp.src(paths.sass)
        .pipe(plumber({ errorHandler: handleError }))
        .pipe(sass())
        .pipe(plumber.stop())
        .pipe(gulp.dest(paths.sassDest));
});

gulp.task("images", function()
{
    return gulp.src(paths.images)
        .pipe(imageop({
            optimizationLevel: 5,
            progressive: true,
            interlaced: true
        })).pipe(gulp.dest(paths.imagesDest));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);

gulp.task("default", ["stage-loader", "typescript", "sass", "images"]);

gulp.task("watch", ["default"], function () {
    gulp.watch("style/**/*.s*ss", ["sass"]);
    return gulp.watch(paths.ts, ["typescript"]);
});

function handleError(err) {
    console.log(err.toString());
    this.emit('end');
}
