const path = require('path');
const RenameWebpackPlugin = require('rename-webpack-plugin');
const SuppressChunksPlugin = require('suppress-chunks-webpack-plugin').default;
const HtmlWebpackExcludeAssetsPlugin = require('html-webpack-exclude-assets-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const STYLES = {
};

module.exports = {
    productionSourceMap: false,

    chainWebpack: config => {
        
        config.resolve.alias.set('@', path.join(__dirname, './src'));   
        config.resolve.alias.set('~', path.join(__dirname, './src/sdiKit'));

        config.resolve.alias.set('node_modules', path.join(__dirname, './node_modules'));


        config.plugins.delete('prefetch');

        config.plugin('html')
            .tap(args => {
                args[0].excludeAssets = [/[/\\]vendor[/\\]/];
                return args;
            });

        config.module.rule('vue')
            .use('vue-loader')
            .loader('vue-loader')
            .tap(options => {
                options.compilerOptions.preserveWhitespace = true;
                return options;
            });

        config.plugin('html-exclude-assets').use(HtmlWebpackExcludeAssetsPlugin);

        if (process.env.NODE_ENV !== 'production') {
            config.module.rule('scss')
                .test(/^(?!.*?vendor[/\\]styles[/\\]).*?\.scss$|vendor[/\\]styles[/\\]pages[/\\].*?\.scss$/)

            config.module.rule('vendor-css')
                .test(/^.*?vendor[/\\]styles[/\\](?!.*?pages[/\\]).*?\.scss$/)
                .use('extract-css-loader').loader(MiniCssExtractPlugin.loader).options({
                    publicPath: '../'
                }).end()
                .use('css-loader').loader('css-loader').options({
                    sourceMap: false,
                    importLoaders: 2
                }).end()
                .use('postcss-loader').loader('postcss-loader').options({
                    sourceMap: false
                }).end()
                .use('sass-loader').loader('sass-loader').options({
                    sourceMap: false
                })

            config.plugin('extract-vendor-css')
                .use(MiniCssExtractPlugin, [{
                    filename: 'css/[name].css',
                    chunkFilename: 'css/[name].css'
                }]);

        } else {
            config.plugin('suppress-chunks')
                .use(SuppressChunksPlugin, [
                    Object.keys(STYLES),
                    {
                        filter: /\.js(?:\.map)?$/
                    }
                ]);

            config.plugin('rename-chunks')
                .use(RenameWebpackPlugin, [{
                    targetName: '$1.css'
                }]);
        }
    },

    runtimeCompiler: false
};
