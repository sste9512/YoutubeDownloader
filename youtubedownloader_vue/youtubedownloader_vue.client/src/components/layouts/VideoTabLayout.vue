<script setup lang="ts">

import VideoPlayer from "@/components/VideoPlayer.vue";

interface VideoTabLayoutProps {
  videoId: string
}

const props = defineProps<VideoTabLayoutProps>()
</script>

<template>
  <VideoPlayer video-id="dQw4w9WgXcQ" />
</template>

<style scoped></style>
