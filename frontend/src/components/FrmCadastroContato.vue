<template>
    <div class="form">
      <InputText v-model="nome" placeholder="Nome" />
      <InputText v-model="telefone" placeholder="(00) 99999-9999" />
      <InputText v-model="email" placeholder="Email" />
      <Button label="Adicionar" icon="pi pi-plus" @click="adicionar" />

      
    </div>
    <div class="gridCadastroContato">
      <DataTable  :value="agenda" dataKey="id" :paginator="true" :rows="5" :responsiveLayout="'scroll'">
      <Column selectionMode="single" headerStyle="width: 3rem"></Column>
      <Column field="id" header="ID" />
      <Column field="nome" header="Nome" />
      <Column field="telefone" header="Telefone" />
      <Column field="email" header="Email" />
      <Column header="Ações">
        <template #body="slotProps">
          <Button class="btnAlterar" label="Alterar" @click="editRow(slotProps.data)" />
          <Button label="Excluir" @click="deleteRow(slotProps.data.id)" class="p-button-danger" />
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
import axios from 'axios';

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
  

  const carregarAgenda = async () => {
      try {
        const response = await axios.get('http://localhost:5000/api/agenda');
        agenda.value = response.data;
      } catch (error) {
        console.error('Erro ao carregar a agenda:', error);
      }
    };

    const adicionar = async () => { // Adiciona a nova linha
debugger
try {
      if (!nome.value || !telefone.value || !email.value) {
        alert('Por favor, preencha todos os campos.');
        return;
      }      
        const response = await axios.post('http://localhost:5000/api/agenda/contato', {
          nome: nome.value,
          telefone: telefone.value,
          email: email.value,
          dtDataCadastro: new Date().toISOString()
        });

        agenda.value.push(response.data);     
       // Limpa os campos
      nome.value = '';
      telefone.value = '';
      email.value = '';

    }catch(error){
      alert('Erro ao adicionar contato', error)
    };
  }
    const editRow = (row) => {
      dialogVisible.value = true;
      newRow.value = { ...row };
      currentRowIndex.value = row.id;

      // Carrega os dados nos campos
      nome.value = row.nome; // Carrega o nome no campo correspondente
      telefone.value = row.telefone; // Carrega o telefone no campo correspondente
      email.value = row.email; // Carrega o email no campo correspondente
    };

    const saveRow = async () => {
      try {
      if (currentRowIndex.value !== null && currentRowIndex.value >= 0) {

        await axios.put(`http://localhost:5000/api/agenda/contato/${currentRowIndex.value}`, {
          nome: newRow.value.nome,
          telefone: newRow.value.telefone,
          email: newRow.value.email,
        });
        // Atualiza a linha existente
        const index = agenda.value.findIndex((item) => item.id === currentRowIndex.value);
        agenda.value[index] = { ...newRow.value, id: currentRowIndex.value };
      }
      dialogVisible.value = false;
      resetNewRow();
    }catch(error){
      console.error('Erro ao salvar contato:', error);
    };}

     // Função para excluir contato
     const deleteRow = async (id) => {
      try {
        await axios.delete(`http://localhost:5000/api/agenda/contato/${id}`);
        agenda.value = agenda.value.filter((item) => item.id !== id);
      } catch (error) {
        console.error('Erro ao excluir contato:', error);
      }
    };

    const resetNewRow = () => {
      newRow.value = { nome: '', telefone: '', email: '' };
      currentRowIndex.value = null;
    };

    carregarAgenda();
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
