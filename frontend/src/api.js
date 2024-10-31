import axios from 'axios';

// Configura a URL base do backend (.NET Core)
const api = axios.create({
  baseURL: 'http://localhost:5000/', // Substitua pela URL do seu backend
});

export default api;