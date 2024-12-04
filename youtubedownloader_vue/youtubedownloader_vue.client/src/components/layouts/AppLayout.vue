<template>
  <v-responsive class="border rounded">
    <v-app>
      <v-navigation-drawer>
        <v-list>
          <v-list-item title="Navigation drawer"></v-list-item>
        </v-list>
      </v-navigation-drawer>



      <v-main style="height: 100%; width: 100%" min-height="300vh">
        <v-container width="100%" style=" min-width: 140vh">
          <v-toolbar extended prominent class=" d-flex align-center" style="height: 100%">
            <v-app-bar-nav-icon></v-app-bar-nav-icon>

            <v-toolbar-title>Title</v-toolbar-title>
            <v-row>

              <v-text-field label="YouTube URL" variant="outlined" v-model="youtubeUrl"
                placeholder="Enter YouTube URL"></v-text-field>
              <v-btn color="primary" class="ml-2" @click="searchVideo" :loading="loading">
                Search
              </v-btn>
              <v-select label="Search Type" :items="['Videos', 'Playlists', 'Channels', 'Creator']" v-model="searchType"
                variant="outlined" class="ml-2" style="max-width: 150px"></v-select>
              <v-btn color="primary" class="ml-2" @click="showSettings" :loading="loading">
                Settings
              </v-btn>
            </v-row>
            <v-spacer></v-spacer>

            <v-btn icon>
              <v-icon>mdi-magnify</v-icon>
            </v-btn>

            <v-btn icon>
              <v-icon>mdi-heart</v-icon>
            </v-btn>

            <v-btn icon>
              <v-icon>mdi-dots-vertical</v-icon>
            </v-btn>
          </v-toolbar>


          <v-row v-if="videoSearchResults.length > 0">
            <v-col cols="12">
              <v-card>
                <v-container fluid>
                  <v-row dense>
                    <v-col v-for="result in videoSearchResults" :key="result.title" cols="12" sm="6" md="4" lg="3">
                      <v-card>
                        <v-img :src="result.thumbnails?.[0]?.url" :aspect-ratio="16 / 9" cover></v-img>
                        <v-card-title class="text-subtitle-1">{{ result.title }}</v-card-title>
                        <v-card-subtitle>{{ result.title }}</v-card-subtitle>
                        <v-card-actions>
                          <v-btn color="primary" variant="text" @click="downloadVideo(result)">
                            Download
                          </v-btn>

                          <v-btn color="primary" variant="text" @click="downloadVideo(result)">
                            View Info
                          </v-btn>
                        </v-card-actions>
                      </v-card>
                    </v-col>
                  </v-row>
                </v-container>
              </v-card>
            </v-col>
          </v-row>
          <v-row v-else-if="loading">
            <v-col cols="12" class="text-center">
              <v-progress-circular indeterminate></v-progress-circular>
            </v-col>
          </v-row>
          <v-row v-else>
            <v-col cols="12" class="text-center">
              <v-alert type="info">
                Enter a YouTube URL to search for videos
              </v-alert>
            </v-col>
          </v-row>




        </v-container>
      </v-main>



      <SettingsLayout v-if="showDialog" />
    </v-app>
  </v-responsive>
</template>
<script lang="ts">

import { useYoutubeStore } from '@/state/video-store';
import { storeToRefs } from 'pinia';
import { VTabs } from 'vuetify/components';
import { ISearchResult, SearchFilter, Video } from '@/data/web-api-client';
import { useSearchStore } from '@/state/search-store';
import SettingsLayout from './SettingsLayout.vue';


export default {
  setup() {
    const store = useYoutubeStore();
    const searchStore = useSearchStore();
    const { searchResults, videoSearchResults } = storeToRefs(searchStore);
    const { youtubeUrl } = storeToRefs(store);
    return { searchResults, videoSearchResults, youtubeUrl, store, searchStore }
  },
  data() {
    return {
      loading: false,
      videoId: '2epe7FYeuSE',
      tabs: null,
      videos: [] as Video[],
      searchType: 'Videos',
      showDialog: false
    }
  },
  methods: {
    async searchVideo() {
      // TODO: Implement search logic
      this.loading = true;
      const searchResults = await this.searchStore.searchVideo(this.youtubeUrl);
      if (searchResults.ok) {
        this.loading = false;
      }
      else {
        this.loading = false;
      }
    },
    downloadVideo(video: Video) {
      // TODO: Implement download logic
      // TODO: called generated client
      console.log('Downloading video:', video.title)
    },
    showSettings() {
      this.showDialog = true;
    }
  }
}

</script>
