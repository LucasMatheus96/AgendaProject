
<template>
  <div id="app">
    <Agenda />
  </div>
</template>

   


<script>
import Agenda from './components/Agenda.vue'; // Importando o componente

const app = createApp(App);
app.component('Button', Button);
</script>