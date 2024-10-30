import { createApp } from 'vue'; // Certifique-se de importar corretamente
import App from './App.vue';
import PrimeVue from 'primevue/config';

// Estilos globais do PrimeVue (ajuste conforme necessário)
import 'primevue/resources/themes/saga-blue/theme.css';
import 'primevue/resources/primevue.min.css';
import 'primeicons/primeicons.css';

// Criação da aplicação Vue e configuração do PrimeVue
const app = createApp(App);

// Registrar o PrimeVue como plugin global
app.use(PrimeVue);

// Montar a aplicação no elemento com ID "app"
app.mount('#app');
