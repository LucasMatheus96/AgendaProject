<template>
    <div v-if="visible" class="dialog-overlay" @click.self="closeDialog">
      <div :class="['dialog-box', dialogType]" role="alert">
        <button class="dialog-close" @click="closeDialog">✖</button>
        <h3>{{ title }}</h3>
        <p>{{ message }}</p>
      </div>
    </div>
  </template>

<script>
export default {
    props: {
      visible: {
        type: Boolean,
        default: false
      },
      type: {
        type: String,
        default: 'info', // Tipos: 'info', 'warning', 'error', 'success'
      },
      title: {
        type: String,
        default: 'Mensagem'
      },
      message: {
        type: String,
        required: true
      }
    },
    computed: {
      dialogType() {
        return `dialog-${this.type}`; // Ex: 'dialog-info', 'dialog-error'
      }
    },
    methods: {
      closeDialog() {
        this.$emit('close');
      }
    }
  };
</script>

<style scoped>
.dialog-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100vw;
  height: 100vh;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.dialog-box {
  position: relative;
  width: 90%;
  padding: 20px;
  border-radius: 8px;
  max-width: 600px;
  text-align: center;
  background: #fff;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.dialog-close {
  position: absolute;
  top: 10px;
  right: 10px;
  border: none;
  background: none;
  font-size: 18px;
  cursor: pointer;
}

.dialog-info { border-left: 6px solid #007bff; }
.dialog-warning { border-left: 6px solid #ffcc00; }
.dialog-error { border-left: 6px solid #ff4d4d; }
.dialog-success { border-left: 6px solid #28a745; }
</style>