import { createApp } from 'vue';
import App from './App.vue';
// import router from './router'; // se estiver usando Vue Router
import PrimeVue from 'primevue/config';
import Aura from '@primevue/themes/aura';
import 'primeicons/primeicons.css' // tema do PrimeVue


const app = createApp(App);

// app.use(router); // se estiver usando Vue Router
app.use(PrimeVue , {
    theme:{
        present:Aura,
    },
});

app.mount('#app');