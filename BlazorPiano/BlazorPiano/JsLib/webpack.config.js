const path = require("path");

module.exports = {
    module: {

        rules: [{
            test: /\.(js|jsx|ts|tsx)$/,
            exclude: /node_modules/,
            use: {
                loader: "ts-loader"
            }
        }]
    },
    resolve: {
        extensions: ['.ts', '.js'],
    },
    output: {
        path: path.resolve(__dirname, '../wwwroot/js'),
        filename: "piano_lib.js",
        library: "PianoLib"
    }
};