import './assets/main.css'
// main.ts
import "reflect-metadata";
// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { container } from "tsyringe";
import axios from 'axios';
import type { AxiosInstance } from 'axios';
import { createMemoryHistory, createRouter } from 'vue-router'

// Components
import App from './App.vue'
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import type { IChannelClient, IPlaylistClient, ISearchClient, IVideoClient, IYoutubeClient } from "@/data/web-api-client";
import { ChannelClient, VideoClient, PlaylistClient, SearchClient, YoutubeClient } from "@/data/web-api-client";
import router from './router';

const vuetify = createVuetify({
  components,
  directives,
})

//const baseUrl = import.meta.env.VITE_API_BASE_URL;
const baseUrl = "http://localhost:5276";

const axiosClient = axios.create({
  baseURL: baseUrl
});

// Configure axios retry logic
axiosClient.interceptors.response.use(undefined, async (error) => {
  const { config, response } = error;

  // Only retry on network errors or 5xx server errors
  if (!response || (response.status >= 500 && response.status <= 599)) {
    // Set max retries
    const maxRetries = 3;
    const retryCount = config.retryCount || 0;

    // Check if we should retry
    if (retryCount < maxRetries) {
      // Exponential backoff delay
      const delayMs = Math.pow(2, retryCount) * 1000;

      // Update retry count
      config.retryCount = retryCount + 1;

      // Log retry attempt
      browserLogger.info(`Retrying request (${config.retryCount}/${maxRetries}) after ${delayMs}ms`);

      // Wait before retrying
      await new Promise(resolve => setTimeout(resolve, delayMs));

      // Retry request
      return axiosClient(config);
    }
  }

  return Promise.reject(error);
});



// Configure axios interceptors for session management
axiosClient.interceptors.request.use(
  config => {
    const sessionId = localStorage.getItem('sessionId');
    if (sessionId) {
      config.headers['X-Session-ID'] = sessionId;
    }
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

axiosClient.interceptors.response.use(
  response => {
    const sessionId = response.headers['x-session-id'];
    if (sessionId) {
      localStorage.setItem('sessionId', sessionId);
    }
    return response;
  },
  error => {
    if (error.response?.status === 401) {
      localStorage.removeItem('sessionId');
    }
    return Promise.reject(error);
  }
);


// Create a simple browser logger
export const browserLogger = {
  log: (...args: any[]) => {
    console.log('[YoutubeDownloader]', ...args);
  },
  error: (...args: any[]) => {
    console.error('[YoutubeDownloader]', ...args);
  },
  warn: (...args: any[]) => {
    console.warn('[YoutubeDownloader]', ...args);
  },
  info: (...args: any[]) => {
    console.info('[YoutubeDownloader]', ...args);
  }
};

browserLogger.info("Registering services");
container.register("Logger", { useValue: browserLogger });
container.register<AxiosInstance>("AxiosInstance", { useValue: axiosClient })
container.register<string>("ApiBaseUrl", { useValue: baseUrl })
container.register<IChannelClient>("IChannelClient", { useValue: new ChannelClient(baseUrl, axiosClient) })
container.register<IPlaylistClient>("IPlaylistClient", { useValue: new PlaylistClient(baseUrl, axiosClient) })
container.register<IVideoClient>("IVideoClient", { useValue: new VideoClient(baseUrl, axiosClient) })
container.register<ISearchClient>("ISearchClient", { useValue: new SearchClient(baseUrl, axiosClient) })
container.register<IYoutubeClient>("IYoutubeClient", { useValue: new YoutubeClient(baseUrl, axiosClient) })

browserLogger.info("Creating pinia");
const pinia = createPinia();

browserLogger.info("Creating app");
const app = createApp(App);


app.use(pinia)
app.use(router)
app.use(vuetify)
app.mount('#app')


