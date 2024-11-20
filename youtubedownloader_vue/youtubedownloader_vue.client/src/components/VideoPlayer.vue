<script setup lang="ts">
import { ref } from 'vue'

interface VideoPlayerProps {
    videoId: string
}

/**
 * Define the component props using Vue's defineProps
 * Accepts a VideoPlayerProps interface that requires:
 * @property {string} videoId - The YouTube video ID or URL path to display
 */
const props = defineProps<VideoPlayerProps>()
</script>

<template>
    <div class="video-player">
        <v-responsive class="video-container" aspect-ratio="16/9">
            <iframe :src="`https://www.youtube.com/embed/${props.videoId}`"
                allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                allowfullscreen class="video-iframe"></iframe>
        </v-responsive>
    </div>
</template>

<style scoped>
.video-player {
    width: 100%;
    max-width: 1280px;
    margin: 0 auto;
}

.video-container {
    position: relative;
    background: #000;
    border-radius: 8px;
    overflow: hidden;
}

.video-iframe {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
}
</style>
