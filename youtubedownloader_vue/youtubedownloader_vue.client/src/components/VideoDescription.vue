<script setup lang="ts">
import {ref} from 'vue';

const headers = ref([
  {text: 'Name', align: 'start', value: 'name'},
  {text: 'Duration', align: 'start', value: 'duration'},
  {text: 'Resolution', align: 'start', value: 'resolution'},
  {text: 'Format', align: 'start', value: 'format'}
]);

const videos = ref([
  {name: 'Video 1', duration: '5:00', resolution: '1080p', format: 'MP4'},
  {name: 'Video 2', duration: '3:20', resolution: '720p', format: 'MKV'},
  {name: 'Video 3', duration: '2:45', resolution: '480p', format: 'AVI'}
]);
</script>

<template>

  <template>
    <v-container>
      <v-row>
        <v-col cols="12">
          <v-data-table
            :headers="headers"
            :items="videos"
            class="elevation-1"
          >
            <template v-slot:[`item.name`]="{ item }">
              <v-chip>{{ item.name }}</v-chip>
            </template>
          </v-data-table>
        </v-col>
      </v-row>
    </v-container>
  </template>


</template>

<style scoped>

</style>
