<template>
    <div class="form">
      <InputText v-model="nome" placeholder="Nome" />
      <InputText v-model="telefone" @input="formatPhone" placeholder="(00) 99999-9999" maxlength="15" />
      <InputText v-model="email" @blur="validateEmail" placeholder="Email"  />     
      <Button label="Adicionar" icon="pi pi-plus" @click="adicionar" />   

      <DialogBox
      :visible="dialogBoxVisible"
      :type="dialogType"
      :title="dialogTitle"
      :message="dialogMessage"
      @close="dialogBoxVisible = false"
    />
      
    </div>
    <div class="gridCadastroContato">
      <DataTable  :value="agenda" dataKey="id" :paginator="true" :rows="5" :responsiveLayout="'scroll'">   
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
        <InputText v-model="newRow.telefone" placeholder="Telefone" @input="formatPhone"  maxlength="15" />
        <InputText v-model="newRow.email" placeholder="Email" />        
        <Button label="Salvar" @click="saveRow" />
      </div>
      </Dialog>

      
      
      
    </div>


    
</template>
<script>
import { ref, h } from 'vue';
import axios from 'axios';
import api from '../services/api.js';


import Button from 'primevue/button';
import InputText from 'primevue/inputtext';
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import  Dialog  from 'primevue/dialog';
import DialogBox from './DialogBox.vue';


export default {
    
  components: {  
    Button,
    InputText,
    Column,
    DataTable,
    Dialog,
    DialogBox

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
  const errorMessage = ref('');
  const dialogBoxVisible = ref(false);
  const dialogType = ref('');
  const dialogTitle = ref('');
  const dialogMessage = ref('');

  

const carregarAgenda = async () => {
      try {
        const response = await api.get('/');
        agenda.value = response.data;
      } catch (error) {
        showDialogBox('error', 'Erro', 'Falha ao carregar lista de usuario');    
      }
    };


const adicionar = async () => { 
  try {
        if (!nome.value || !telefone.value || !email.value) {
          showDialogBox('warning', 'Alerta', 'Por favor, preencha todos os campos.');
         
          return;
        }      
          const response = await api.post('/', {
            nome: nome.value,
            telefone: telefone.value,
            email: email.value,
            dtDataCadastro: new Date().toISOString()
          });

        agenda.value.push(response.data); 

      
        nome.value = '';
        telefone.value = '';
        email.value = '';

        showDialogBox('success', 'Sucesso', 'Contato Cadastrado com sucesso.');   

      }catch(error){
        showDialogBox('error', 'Erro', 'Erro ao Adicionar um contato');    
      };
  }


  const editRow = (row) => {
      dialogVisible.value = true;
      newRow.value = { ...row };
      currentRowIndex.value = row.id;

     
      nome.value = row.nome; 
      telefone.value = row.telefone; 
      email.value = row.email; 
  };

  const saveRow = async () => {
      try {
      if (currentRowIndex.value !== null && currentRowIndex.value >= 0) {

        await api.put(`/${currentRowIndex.value}`, {
          nome: newRow.value.nome,
          telefone: newRow.value.telefone,
          email: newRow.value.email,
          dtDataCadastro: new Date().toISOString()
        });
        const index = agenda.value.findIndex((item) => item.id === currentRowIndex.value);
        agenda.value[index] = { ...newRow.value, id: currentRowIndex.value };
      }
      dialogVisible.value = false;
      resetNewRow();
    }catch(error){
      showDialogBox('error', 'Erro', 'Falha ao concluir a alteração do contato');    
    };}

     
  const deleteRow = async (id) => {
      try {
        await api.delete(`/${id}`);
        agenda.value = agenda.value.filter((item) => item.id !== id);
      } catch (error) {
        showDialogBox('error', 'Erro', 'Erro ao excluir contato');        
      }
  };

  const resetNewRow = () => {
      newRow.value = { nome: '', telefone: '', email: '' };
      currentRowIndex.value = null;
  };

  const formatPhone = () => {
      let fone = telefone.value.replace(/\D/g, ''); 

     
      fone = fone.replace(/^(\d{2})(\d)/, '($1) $2');
      fone = fone.replace(/(\d{5})(\d)/, '$1-$2');

      telefone.value = fone;
    };


    const validateEmail = () => {
      const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailPattern.test(email.value)) {
        showDialogBox('warning', 'Aviso', 'Informe um E-mail inválido');
      } else {
        errorMessage.value = '';      
      }
    };

    const showDialogBox = (type, title, message) => {
    dialogType.value = type;
    dialogTitle.value = title;
    dialogMessage.value = message;
    dialogBoxVisible.value = true;
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
      ,errorMessage
      ,dialogBoxVisible 
      ,dialogType
      ,dialogTitle
      ,dialogMessage
      ,adicionar 
      ,editRow
      ,saveRow
      ,deleteRow
      ,formatPhone
      ,validateEmail
      ,showDialogBox
     }

}}

</script>
<style scoped>


.form {
  display: flex;
  gap: 10px;
  margin-bottom: 20px;
}


</style>
