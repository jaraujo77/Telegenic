var assetsPath = 'Include';
var viewsPath = 'Views';
var areasPath = 'Areas';

var cssPath = assetsPath + '/css';
var imagesPath = assetsPath + '/images';
var jsPath = assetsPath + '/js';

var bootstrapPath = '/Content/bootstrap';


var minJsFileName = 'telegenic.main.min.js';
var minCssFileName = 'telegenic.site.min.css';

var buildConfigjson = require('./buildConfig.json');

var config = {
    src: {
        css: [
            cssPath + '/**/Site.scss'
        ],
        js: [            
            jsPath + '/**/*.js',            
            '!' + jsPath + '/**/*.min.js'
        ]
    },
    paths: {
        assets: assetsPath,
        css: cssPath,
        images: imagesPath,
        js: jsPath,
        views: viewsPath
    },
    watch: {
        css: [
            cssPath + '/**/*.scss'
        ],
        fonts: [
            assetsPath + '/fonts/**/*.*'
        ],
        images: [
            imagesPath + '/**/*.*'
        ],
        js: [
            jsPath + '/**/*.js',
            '!' + jsPath + '/**/*.min.js'
        ],
        views: [
            viewsPath + '/**/*.cshtml',
            areasPath + '/**/*.cshtml'
        ],
        viewsjs: [
            viewsPath + '/**/*.js',
            areasPath + '/**/*.js'
        ]
    },
    minFiles: {
        cssFileName: minCssFileName,
        jsFileName: minJsFileName
    },
    // The /Website folder in your local Sitecore install
    WebsitePhysicalPath: 'C:\\inetpub\\wwwroot\\Telegenic\\',
    //Current project build configuration
    buildConfig: buildConfigjson
};

module.exports = config;