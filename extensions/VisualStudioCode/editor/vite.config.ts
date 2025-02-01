import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [
        vue({
            script: {
                propsDestructure: true
            }
        })
    ],
    resolve: {
        alias: [
            {
                find: '@/',
                replacement: `${path.resolve(__dirname, './src')}/`,
            },
        ]
    },
    build: {
        target: 'esnext',
        commonjsOptions: { 
            transformMixedEsModules: true 
        },
        lib: {
            entry: './src/main.ts',
            name: 'editor',
            fileName: 'editor',
        },
        outDir: '../client/out/views',
        rollupOptions: {},
        minify: 'esbuild',
    },
    define: {
        "process.env.NODE_ENV": JSON.stringify(process.env.NODE_ENV),
    }
})



// export default defineConf({
//     resolve: {
//         alias: [
//             {
//                 find: '@/',
//                 replacement: `${path.resolve(__dirname, './src')}/`,
//             },
//         ],
//     },
//     plugins: [
//         createVuePlugin(),
//     ],
//     build: {
//         target: 'es2021',
//         commonjsOptions: {transformMixedEsModules: true},
//         lib: {
//             entry: 'src/main.ts',
//             name: 'test',
//             fileName: 'client',
//         },
//         outDir: 'dist/client',
//         rollupOptions: {},
//         minify: 'esbuild',
//     },
//     define: {
//         "process.env.NODE_ENV": JSON.stringify(process.env.NODE_ENV),
//     }
// })