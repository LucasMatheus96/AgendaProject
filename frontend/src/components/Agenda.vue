<template>
  <div class="agenda"> 
      <FrmCadastroContato/>    
  </div>
</template>

<script>
import FrmCadastroContato from './FrmCadastroContato.vue';

export default {
  name: 'App', // Nome do componente
  components: {
    FrmCadastroContato // Registrando o componente Agenda
  },
};

</script>


<style>

.agenda {
  max-width: 800px;
  margin: auto;
}
</style>
