<script setup lang="ts">
import type { Video } from '@/data/data';
import TheWelcome from './YoutubeSearchBar.vue';
import { useYoutubeStore } from '@/state/video_state';
import { storeToRefs } from 'pinia';
import { VTabs } from 'vuetify/components';
import VideoPlayer from './VideoPlayer.vue';
</script>

<template>
    <v-responsive class="border rounded" max-height="300">
        <v-app>
            <v-navigation-drawer>
                <v-list>
                    <v-list-item title="Navigation drawer"></v-list-item>
                </v-list>
            </v-navigation-drawer>

            <v-app-bar title="App bar">
                <v-container class="d-flex align-center" style="height: 100%">
                    <v-row>
                        <v-col cols="12">
                            <v-text-field label="YouTube URL" variant="outlined" v-model="youtubeUrl"
                                placeholder="Enter YouTube URL"></v-text-field>
                        </v-col>
                    </v-row>
                </v-container>
            </v-app-bar>

            <v-main>
                <v-container>

                  <v-tabs v-model="tab" grow>
                    <v-tab v-for="(video, index) in videos" :key="index" :href="'#tab-' + index">
                      {{ video.title }}
                    </v-tab>
                  </v-tabs>

                  <v-tabs-items v-model="tab">

                  </v-tabs-items>

                </v-container>
            </v-main>
        </v-app>
    </v-responsive>
</template>
<script lang="ts">





export default {
    setup() {
        const store = useYoutubeStore();
        const { youtubeUrl } = storeToRefs(store);
        return { youtubeUrl }
    },
    data() {
        return {
            videoId: '2epe7FYeuSE',
            tab: null,
            youtubeUrl: "",
            videos: [] as Video[]
        }
    },
    methods: {
        downloadVideo(video: Video) {
            // TODO: Implement download logic
            // TODO: called generated client
            console.log('Downloading video:', video.title)
        }
    }
}

</script>
