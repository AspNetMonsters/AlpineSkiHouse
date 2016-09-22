var webpack = require('webpack');
module.exports = {  
  entry: './Scripts/Init.ts',
  output: {
    filename: 'wwwroot/js/site.js'
  },
  resolve: {
    extensions: ['', '.webpack.js', '.web.js', '.ts', '.js']
  },
  plugins: [
    new webpack.optimize.UglifyJsPlugin()
  ],
  module: {
    loaders: [
      { test: /\.ts$/, loader: 'ts-loader' }
    ]
  }
}