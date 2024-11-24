<script setup lang="ts">

</script>

<template>
  <v-container>
    <v-row>
      <v-col cols="12">
        <v-text-field label="YouTube URL" variant="outlined" v-model="youtubeUrl" placeholder="Enter YouTube URL"></v-text-field>
      </v-col>
    </v-row>

    <v-row>
      <v-col cols="12">
        <v-list lines="two">
          <v-list-item v-for="(video, index) in videos" :key="index" :title="video.title" :subtitle="video.duration">

            <template v-slot:prepend>
              <v-avatar>
                <v-img :src="video.url"></v-img>
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


import {useYoutubeStore} from "@/state/video-store";
import type {Video} from "@/data/web-api-client";

export default {
  data() {
    return {
      /**
       * The URL of the YouTube video.
       * This property holds the string that represents the YouTube video's URL.
       */
      youtubeUrl: '',
      videos: [] as Video[]
    }
  },
  methods: {
    /**
     * Downloads the given video.
     *
     * @param {Video} video - The video object to be downloaded.
     * @return {Promise<void>} A promise that resolves when the download is complete.
     */
    downloadVideo: async function (video: Video) {
      const store = useYoutubeStore();
      const video2 = await store.downloadVideo("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
    }
  }
}


</script>
