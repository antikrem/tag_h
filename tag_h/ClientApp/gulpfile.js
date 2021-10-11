/// <binding AfterBuild='default' Clean='clean' />
/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/
var gulp = require("gulp");
var concat = require('gulp-concat');
var sourcemaps = require('gulp-sourcemaps');
var ts = require("gulp-typescript");

var exec = require('child_process').exec;
var del = require("del");

var tsProject = ts.createProject("tsconfig.json");

var paths = {
    public: "public",
    packges: ["package.json", "package-lock.json"],
    scripts: ["src/**/*.js", "src/**/*.jsx", "src/**/*.css", "src/**/*.map"]
};

gulp.task("clean", function () {
    return del(["../wwwroot/*"], { force: true });
});

gulp.task("install", function (done) {
    gulp.src(paths.packges).pipe(gulp.dest("../wwwroot/"));
    var process = exec(
        'npm install',
        { cwd: "../wwwroot/" },
        function (err, _, stderr) {
            stderr && console.error(stderr);
            done();
        }
    );

    process.stdout.on('data', function (data) {
        console.log(data.toString());
    });
})

gulp.task("default", function (done) {
    gulp.src(["public/**/*"]).pipe(gulp.dest("../wwwroot/public"));

    tsProject
        .src()
        .pipe(sourcemaps.init())
        .pipe(tsProject())
        .js
        .pipe(gulp.src(paths.scripts))
        .pipe(sourcemaps.write())
        .pipe(gulp.dest("../wwwroot/src"));
    done();
});