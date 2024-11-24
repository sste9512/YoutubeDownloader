<script setup lang="ts">
import { ref } from 'vue';

const props = defineProps<{
    showDialog: boolean
}>()
const showDialog = ref(props.showDialog)
</script>

<template>
    <div class="text-center pa-4">
        <v-dialog v-model="showDialog" max-width="400" persistent>
            <template v-slot:activator="{ props: activatorProps }">
                <v-btn v-bind="activatorProps">
                    Open Dialog
                </v-btn>
            </template>

            <v-card prepend-icon="mdi-map-marker"
                text="Let Google help apps determine location. This means sending anonymous location data to Google, even when no apps are running."
                title="Use Google's location service?">
                <template v-slot:actions>
                    <div class="settings-container">
                        <!-- Left sidebar with categories -->
                        <div class="settings-sidebar">
                            <div class="settings-categories">
                                <div class="category-item active">
                                    <span class="category-icon">⚙️</span>
                                    General
                                </div>
                                <div class="category-item">
                                    <span class="category-icon">💾</span>
                                    Downloads
                                </div>
                                <div class="category-item">
                                    <span class="category-icon">🎥</span>
                                    Video Quality
                                </div>
                                <div class="category-item">
                                    <span class="category-icon">🔊</span>
                                    Audio
                                </div>
                                <div class="category-item">
                                    <span class="category-icon">📁</span>
                                    File Management
                                </div>
                            </div>
                        </div>

                        <!-- Main content area -->
                        <div class="settings-content">
                            <div class="settings-header">
                                <h1>Settings</h1>
                                <div class="settings-search">
                                    <input type="text" placeholder="Search settings...">
                                </div>
                            </div>

                            <div class="settings-body">
                                <!-- Settings groups will be rendered here -->
                                <div class="settings-group">
                                    <h2>General Settings</h2>
                                    <div class="setting-item">
                                        <div class="setting-label">
                                            <label>Default Download Location</label>
                                            <span class="setting-description">Choose where your downloads will be saved
                                                by
                                                default</span>
                                        </div>
                                        <div class="setting-control">
                                            <input type="text" value="/downloads">
                                            <button class="browse-button">Browse...</button>
                                        </div>
                                    </div>

                                    <div class="setting-item">
                                        <div class="setting-label">
                                            <label>Theme</label>
                                            <span class="setting-description">Choose application theme</span>
                                        </div>
                                        <div class="setting-control">
                                            <select>
                                                <option>Light</option>
                                                <option>Dark</option>
                                                <option>System</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <v-btn @click="showDialog = false">
                        Disagree
                    </v-btn>

                    <v-btn @click="showDialog = false">
                        Agree
                    </v-btn>
                </template>
            </v-card>
        </v-dialog>
    </div>


</template>

<style scoped></style>
