import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import { createSvgIconsPlugin } from 'vite-plugin-svg-icons';

export default defineConfig({
  plugins: [vue(), createSvgIconsPlugin({
    // Configurações do plugin SVG, se necessário
  })],
});