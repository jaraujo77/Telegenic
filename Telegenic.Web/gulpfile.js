/// <binding AfterBuild='default' ProjectOpened='watch' />
const gulp = require('gulp');
const del = require('del');//Deletes files and directories using globs. https://www.npmjs.com/package/del
const concat = require('gulp-concat');//Concatenates .sass and .js files. https://www.npmjs.com/package/gulp-concat
const colors = require('colors');//Adds color to console. https://www.npmjs.com/package/colors
const sass = require('gulp-sass');//Compiles .sass into .css https://www.npmjs.com/package/gulp-sass
sass.compiler = require("node-sass");//https://www.npmjs.com/package/node-sass
const minimist = require('minimist')//Reads command line args in build. https://www.npmjs.com/package/minimist
const terser = require('gulp-terser')//Minifies es6 js https://www.npmjs.com/package/gulp-terser
const rename = require('gulp-rename')//rename files or directories in streams
const webpack = require('webpack-stream')//packages js and resolves dependencies
const named = require('vinyl-named')//used by webpack-stream to rename files
const rev = require('gulp-rev')//add revision hashes to release files
const gulpif = require('gulp-if')//conditionally control the flow of vinyl objects


const config = require('./config');


function clean(cb) {
    cleanCss();
    cleanJS();
    cb();
}


function watch() {
    gulp.watch(config.watch.css, { delay: 500}, compileSass);
    gulp.watch(config.src.js.concat(config.watch.viewsjs), { delay: 500 }, compileJS);
    gulp.watch(config.watch.views).on('change', function (path) { copyFile(path) });
}


function cleanCss() {

    return deleteFile(config.paths.css, config.minFiles.cssFileName);
}

function cleanJS() {

    return deleteFile(config.paths.js, config.minFiles.jsFileName);
}

function compileSass() {
    log("Begin compiling SASS files");

    return gulp.src(getScssCompilePaths())
        .pipe(sass({ outputStyle: isRelease() ? 'compressed' : 'expanded' }).on('error', sass.logError))
        .pipe(concat(config.minFiles.cssFileName))
        .pipe(rename({ dirname: config.paths.css }))
        .pipe(gulp.dest('./'))
        .pipe(toLocalInstance());
}

function compileJS() {
    log("Begin compiling JavaScript files");

    log(getJsCompilePaths());

    return gulp.src(getJsMainPath())
        .pipe(named(function (file) { return config.minFiles.jsFileName }))
        .pipe(webpack({
            devtool: isRelease() ? 'source-map' : 'inline-source-map'
        }))
        //.pipe(gulpif(isRelease(), rev())) TODO: use gulp-inject to update _Layout.cshtml with revisioned file name
        .pipe(rename({ dirname: config.paths.js }))
        .pipe(gulp.dest('./'))
        .pipe(toLocalInstance()).on('end', function () { log('End compiling JavaScript files'.green) });
}

function migrateAssemblies() {
    return gulp
        .src(['bin/Telegenic.*.dll', 'bin/Telegenic.*.pdb'])
        .pipe(rename({ dirname: 'bin/' }))
        .pipe(toLocalInstance());
}

/*
 *
 *Helper methods
 * 
 */

function getJsCompilePaths() {
    return config.src.js.concat(config.watch.viewsjs);
}

function getJsMainPath() {
    return config.paths.js + '/main.js';
}

function getScssCompilePaths() {
    return config.src.css;
}

// copy file to local website instance
function copyFile(file) {

    var fileName = getFileName(file);
    var time = log("Copying file: ".grey + fileName);

    return gulp.src(file)
        .pipe(toLocalInstance()).on('end', function () { log("Files copied: ".grey + fileName, time) });
}

// delete file from local website instance
function deleteFile(directory, fileName) {
    log("Deleting file: ".yellow + fileName);

    return del([directory + "/" + fileName])
}

// gets the file name from an file path
function getFileName(file) {
    return file.substr(file.lastIndexOf('\\') + 1);
}

//gets project build configuration
function isRelease() {    
    return config.buildConfig.config !== 'Debug' && config.buildConfig.config !== 'Development';
}

//copies to local website instance
function toLocalInstance() {
    return gulp.dest(config.WebsitePhysicalPath);
}

// get a timestamp in this format: [hh:mm:ss]
function getTimestamp(now) {
    now = now || new Date();

    return '[' +
        padNumber(now.getHours(), 2) +
        ':' +
        padNumber(now.getMinutes(), 2) +
        ':' +
        padNumber(now.getSeconds(), 2) +
        ']';
}

// pads a number with zeroes
function padNumber(number, length) {
    return (new Array(length).join("0") + number).slice(length * -1);
}

// console.logs with a timestamp
function log(message, prevTime) {
    var now = new Date();
    var timestamp = getTimestamp(now);
    var consoleMessage = timestamp.grey + ' ' + message;
    if (prevTime) {
        var timeDiff = now - prevTime;
        consoleMessage += (' ' + timeDiff + ' ms').magenta;
    }
    console.log(consoleMessage);
    return now;
}

var build = gulp.series(clean, gulp.parallel(compileSass, compileJS), migrateAssemblies);

function test(cb) {
    compileJS();

    cb();
}


exports.test = test;
exports.build = build;
exports.watch = watch;
exports.default = build;