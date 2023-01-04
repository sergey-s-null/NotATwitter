const path = require("path");
// todo проверить, работает ли без этого
const webpack = require("webpack");
const HtmlWebpackPlugin = require('html-webpack-plugin');

const host = process.env.HOST || 'localhost';

module.exports = {
    entry: "./src/index.js",
    mode: "development",
    output: {
        filename: "bundle.js",
        path: path.resolve("dist"),
        publicPath: "/",
        clean: true,
    },
    devServer: {
        compress: true,
        hot: true,
        host,
        port: 3000,
    },
    module: {
        rules:[
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: "babel-loader"
            },
        ],
    },
    plugins: [
        new HtmlWebpackPlugin({
            inject: true,
            template: "public/index.html",
        }),
    ],
}