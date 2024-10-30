import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
// Se precisar de suporte a JSX, descomente a linha abaixo:
// import vueJsx from '@vitejs/plugin-vue-jsx';

export default defineConfig({
  plugins: [
    vue(),
    // vueJsx(), // Descomente se for necessário
  ],
});