<script setup>

</script>


<template>
  <div>
    <h1>Lista de Eventos</h1>
    <ul>
      <li v-for="evento in eventos" :key="evento.id">
        {{ evento.nome }} - {{ evento.data }}
      </li>
    </ul>
    <button @click="adicionarEvento">Adicionar Evento</button>
  </div>
</template>

