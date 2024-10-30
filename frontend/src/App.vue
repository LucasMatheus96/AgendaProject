<template>
  <div id="App">
    <Agenda />
  </div>
</template>

<script>
import Agenda from './components/Agenda.vue'; // Importando o componente

export default {
  name: 'App', // Nome do componente
  components: {
    Agenda, // Registrando o componente Agenda
  },
};
</script>

<style>
/* Adicione seus estilos aqui */
</style>
