const path = require("path");
// todo проверить, работает ли без этого
const webpack = require("webpack");

module.exports = {
    entry: "./src/index.js",
    mode: "development",
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
        ],
    }
}