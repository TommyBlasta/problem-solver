import axios from 'axios';

const https = require('https')

const apiClient = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL || 'https://localhost:7165/',
  headers: {
    'Content-Type': 'application/json',
  },

  // This is a workaround to avoid the error "unable to verify the first certificate" when using axios with a self-signed certificate
  httpsAgent: new https.Agent({ rejectUnauthorized: false }),
});

export default apiClient;
