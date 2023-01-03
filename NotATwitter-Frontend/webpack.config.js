const path = require("path");
const webpack = require("webpack");// todo проверить, необходимо ли, или можно удалить
const HTMLWebpackPlugin = require("html-webpack-plugin");

module.exports = {
    entry: "./index.js",
    output: {
        filename: "bundle.js",
        path: path.resolve("dist"),
        publicPath: "/",
        clean: true,
    },
    module: {
        rules:[
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: "babel-loader"
            },
            {
                test: /\.html$/,
                use: "html-loader"
            },
            {
                test: /\.css$/,
                use: ["style-loader", "css-loader"],
            },
        ],
    },
    plugins: [
        new HTMLWebpackPlugin({
            template: "index.html"
        }),
    ]
}