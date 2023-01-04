const path = require("path");
// todo проверить, работает ли без этого
const webpack = require("webpack");
const HtmlWebpackPlugin = require('html-webpack-plugin');

const host = process.env.HOST || 'localhost';

module.exports = {
    entry: "./src/index.tsx",
    mode: "development",
    output: {
        filename: "bundle.js",
        path: path.resolve("dist"),
        publicPath: "/",
        clean: true,
    },
    resolve: {
        extensions: [".ts", ".tsx", ".js"]
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
                test: /\.tsx?$/,
                exclude: /node_modules/,
                use: "ts-loader"
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