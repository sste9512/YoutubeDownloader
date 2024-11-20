import './assets/main.css'

// Vuetify
import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'

// Components
import App from './App.vue'
import { createApp } from 'vue'
import { createPinia } from 'pinia'

const vuetify = createVuetify({
    components,
    directives,
})


var pinia = createPinia()

var app = createApp(App)

app.use(pinia)
app.use(vuetify)
app.mount('#app')


