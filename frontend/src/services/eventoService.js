import axios from 'axios';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL, // URL definida no .env
});

export const getEventos = async () => {
  const response = await api.get('/eventos');
  return response.data;
};

export const criarEvento = async (evento) => {
  const response = await api.post('/eventos', evento);
  return response.data;
};
