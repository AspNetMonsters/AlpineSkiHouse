// Karma configuration
module.exports = function(config) {
  config.set({
    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: 'wwwroot',
    frameworks: ['jasmine'],

    // list of files / patterns to load in the browser
    files: [
      'jspm_packages/system.js',
      'jspm_packages/system-polyfills.js',
      'js/jspmconfig.js',
      { pattern: 'js/**/*.js', included: false, watched: true, served: true },
      { pattern: 'jspm_packages/**/*.js', included: false, watched: false, served: true },
      '../spec/**/*Tests.js'
    ],
    reporters: ['progress'],
    // web server port
    port: 9876,
    colors: true,
    logLevel: config.LOG_INFO,
    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,
    browsers: ['Chrome', 'PhantomJS', 'IE'],
    singleRun: false,
    concurrency: Infinity,
    proxies: {
        '/js': '/base/js'
    }
  })
}