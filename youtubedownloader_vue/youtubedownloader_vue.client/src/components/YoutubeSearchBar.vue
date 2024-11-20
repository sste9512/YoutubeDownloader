<script setup lang="ts">
import type { Video } from '@/data/data'
</script>

<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <v-text-field label="YouTube URL" variant="outlined" v-model="youtubeUrl"
          placeholder="Enter YouTube URL"></v-text-field>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <v-list lines="two">
          <v-list-item v-for="(video, index) in videos" :key="index" :title="video.title" :subtitle="video.duration">
            <template v-slot:prepend>
              <v-avatar>
                <v-img :src="video.thumbnail"></v-img>
              </v-avatar>
            </template>

            <template v-slot:append>
              <v-btn color="primary" variant="tonal" @click="downloadVideo(video)">
                Download
              </v-btn>
            </template>
          </v-list-item>
        </v-list>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">


export default {
  data() {
    return {
      youtubeUrl: '',
      videos: [] as Video[]
    }
  },
  methods: {
    downloadVideo(video: Video) {
      // TODO: Implement download logic
      console.log('Downloading video:', video.title)
    }
  }
}


</script>
