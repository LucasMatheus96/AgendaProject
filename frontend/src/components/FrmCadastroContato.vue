<template>
    <div class="form">
      <InputText v-model="nome" placeholder="Nome" />
      <InputText v-model="telefone" placeholder="(00) 99999-9999" />
      <InputText v-model="email" placeholder="Email" />
      <Button label="Adicionar" icon="pi pi-plus" @click="adicionar" />

      
    </div>
    <div class="gridCadastroContato">
      <DataTable v-model:selection="selectedProduct" :value="agenda" dataKey="id" :paginator="true" :rows="5" :responsiveLayout="'scroll'">
      <Column selectionMode="single" headerStyle="width: 3rem"></Column>
      <Column field="id" header="ID" />
      <Column field="nome" header="Nome" />
      <Column field="telefone" header="Telefone" />
      <Column field="email" header="Email" />
      <Column header="Ações">
        <template #body="slotProps">
          <Button class="btnAlterar" label="Alterar" @click="editRow(slotProps.index)" />
          <Button label="Excluir" @click="deleteRow(slotProps.index)" class="p-button-danger" />
        </template>
      </Column> 
      </DataTable>

      <Dialog v-model:visible="dialogVisible" header="Adicionar/Editar Linha">
      <div>
        <InputText v-model="newRow.id" placeholder="id" v-show="false" />
        <InputText v-model="newRow.nome" placeholder="Nome" />
        <InputText v-model="newRow.telefone" placeholder="Telefone" />
        <InputText v-model="newRow.email" placeholder="Email" />
        <Button label="Salvar" @click="saveRow" />
      </div>
      </Dialog>
    </div>


    
</template>
<script>
import { ref, h } from 'vue';

import Button from 'primevue/button';
import InputText from 'primevue/inputtext';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import  Dialog  from 'primevue/dialog';


export default {
    
  components: {  
    Button,
    InputText,
    Column,
    DataTable,
    Dialog

  },
setup(){
  const agenda = ref([])
  const rows = ref([]);
  const newRow = ref({id: '', nome:'',telefone:'',email:''})  
  const dialogVisible = ref(false);
  const currentRowIndex = ref(null);
  const nome = ref('')
  const email = ref('')
  const telefone = ref('')

    const adicionar = () => { // Adiciona a nova linha
debugger
      if (!nome.value || !telefone.value || !email.value) {
        alert('Por favor, preencha todos os campos.');
        return;
      }

      agenda.value.push({
        id: agenda.value.length + 1,
        nome: nome.value,
        telefone: telefone.value,
        email: email.value,
      });

       // Limpa os campos
      nome.value = '';
      telefone.value = '';
      email.value = '';
    };

    const editRow = (row) => {
      dialogVisible.value = true;
      newRow.value = { ...row };
      currentRowIndex.value = row;
    };

    const saveRow = () => {
      if (currentRowIndex.value !== null && currentRowIndex.value >= 0) {
        // Atualiza a linha existente
        agenda.value[currentRowIndex.value] = { ...newRow.value , id: currentRowIndex.value + 1};
      }
      dialogVisible.value = false;
      resetNewRow();
    };

    const deleteRow = (index) => {
  agenda.value.splice(index, 1); // Remove a linha pelo índice
};

    const resetNewRow = () => {
      newRow.value = { nome: '', telefone: '', email: '' };
      currentRowIndex.value = null;
    };
    return {
      agenda,
      rows,
      newRow
      ,nome
      ,email
      ,telefone      
      ,dialogVisible
      ,adicionar 
      ,editRow
      ,saveRow
      ,deleteRow}

}}

</script>
<style scoped>


.form {
  display: flex;
  gap: 10px;
  margin-bottom: 20px;
}


</style>
