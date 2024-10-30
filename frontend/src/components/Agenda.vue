<template>
    <div class="agenda">
      <h2>Minha Agenda</h2>
      
      <div class="form">
        <InputText v-model="nome" placeholder="Nome" />
        <Button label="Adicionar" icon="pi pi-plus" @click="adicionar" />
      </div>
      
      <DataTable :value="agenda" dataKey="id" :paginator="true" rows="5" :responsiveLayout="true">
        <Column field="id" header="ID" />
        <Column field="nome" header="Nome" />
        <Column
          header="Ações"
          body="actionsTemplate"
        />
      </DataTable>
  
      <Dialog header="Editar Item" visible="dialogVisible" @hide="fecharDialog">
        <div class="form">
          <InputText v-model="itemSelecionado.nome" />
          <Button label="Salvar" icon="pi pi-check" @click="atualizar" />
        </div>
      </Dialog>
    </div>
  </template>
  <script>
  import { ref } from 'vue';


  

  export default {
    components: {
      InputText,
      Button,
      DataTable,
      Column,
      Dialog
    },
    setup() {
      const agenda = ref([]);
      const nome = ref('');
      const dialogVisible = ref(false);
      const itemSelecionado = ref(null);
  
      const adicionar = () => {
        if (nome.value.trim() === '') return;
        agenda.value.push({ id: agenda.value.length + 1, nome: nome.value });
        nome.value = '';
      };
  
      const remover = (id) => {
        agenda.value = agenda.value.filter(item => item.id !== id);
      };
  
      const editar = (item) => {
        itemSelecionado.value = { ...item };
        dialogVisible.value = true;
      };
  
      const atualizar = () => {
        const index = agenda.value.findIndex(item => item.id === itemSelecionado.value.id);
        if (index !== -1) {
          agenda.value[index].nome = itemSelecionado.value.nome;
          fecharDialog();
        }
      };
  
      const fecharDialog = () => {
        dialogVisible.value = false;
        itemSelecionado.value = null;
      };
  
      return {
        agenda,
        nome,
        dialogVisible,
        itemSelecionado,
        adicionar,
        remover,
        editar,
        atualizar,
        fecharDialog
      };
    }
  };
  </script>
  
  <style scoped>
  .agenda {
    max-width: 600px;
    margin: auto;
  }
  
  .form {
    display: flex;
    gap: 10px;
    margin-bottom: 20px;
  }
  </style>
  
  <!-- <script>
  import api from '../api.js';
  
  export default {
    data() {
      return {
        agenda: [], // Armazena a lista de itens da agenda
      };
    },
    mounted() {
      this.buscarAgenda();
    },
    methods: {
      async buscarAgenda() {
        try {
          const response = await api.get('/eventos/agenda'); // Substitua pelo endpoint adequado
          this.agenda = response.data;
        } catch (error) {
          console.error('Erro ao buscar agenda:', error);
        }
      },
    },
  };
  </script>
   -->