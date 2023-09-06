import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'
// https://vitejs.dev/config/

/** @type {import('vite').UserConfig} */
export default defineConfig({
  server: {
    proxy: {
      '/api': {
        target: 'https://localhost:7043',
        changeOrigin: true,
        secure: false,
        ws: true
      }
    }
  },
  plugins: [react()]
})
