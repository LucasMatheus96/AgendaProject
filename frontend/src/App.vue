<template>
  <div id="App">
    <TheHeader/>
    <Agenda />
  </div>
</template>

<script>
import Agenda from './components/Agenda.vue'; // Importando o componente
import TheHeader from './components/TheHeader.vue';

export default {
  name: 'App', // Nome do componente
  components: {
    Agenda,
    TheHeader, // Registrando o componente Agenda
  },
};
</script>

<style>
/* Adicione seus estilos aqui */
</style>
