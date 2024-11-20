import type { Video } from "@/data/data"
import { defineStore } from "pinia"

export const useYoutubeStore = defineStore('youtube', {
    state: () => ({
        youtubeUrl: '',
        videos: [] as Video[]
    }),

    actions: {
        setUrl(url: string) {
            this.youtubeUrl = url
        },

        async downloadVideo(video: Video) {
            // TODO: Implement download logic
            console.log('Downloading video:', video.title)
        }
    }
})