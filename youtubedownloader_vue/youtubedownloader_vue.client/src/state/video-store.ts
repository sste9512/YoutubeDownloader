import { defineStore } from "pinia"
import { container } from "tsyringe";
import {
    Channel,
    ChannelHandle,
    ChannelId,
    ChannelSlug,
    type IChannelClient,
    type IPlaylistClient,
    type IVideoClient,
    Playlist,
    PlaylistId,
    PlaylistVideo,
    UserName,
    Video
} from "@/data/web-api-client";
import type { Result } from "@/data/result";


export const useYoutubeStore = defineStore('youtube', {
    state: () => ({
        youtubeUrl: '',
        videos: [] as Video[]
    }),

    actions: {
        /**
         * Fetches a YouTube playlist by its ID
         * @param playlistId - The ID of the playlist to fetch
         * @returns A Result containing either the playlist data or an error
         * @remarks This method uses dependency injection to resolve the IPlaylistClient
         * and handles any errors that occur during the fetch operation
         */
        async getPlaylist(playlistId: PlaylistId): Promise<Result<Playlist>> {
            try {
                const playlist = await container.resolve<IPlaylistClient>("IPlaylistClient").get(playlistId);
                return { ok: true, value: playlist };
            } catch (error) {
                console.error("Error fetching playlist:", error);
                return { ok: false, error: error as Error };
            }
        },

        /**
         * Fetches all videos from a YouTube playlist
         * @param playlistId - The ID of the playlist to fetch videos from
         * @returns A Result containing either an array of playlist videos or an error
         * @remarks This method uses dependency injection to resolve the IPlaylistClient
         * and handles any errors that occur during the fetch operation
         */
        async getPlaylistVideos(playlistId: PlaylistId): Promise<Result<PlaylistVideo[]>> {
            try {
                const videos = await container.resolve<IPlaylistClient>("IPlaylistClient").getVideos(playlistId);
                return { ok: true, value: videos };
            } catch (error) {
                console.error("Error fetching playlist videos:", error);
                return { ok: false, error: error as Error };
            }
        },

        /**
         * Fetches a YouTube channel by its ID
         * @param channelId - The ID of the channel to fetch
         * @returns A Result containing either the channel data or an error
         * @remarks This method uses dependency injection to resolve the IChannelClient
         * and handles any errors that occur during the fetch operation
         */
        async get(channelId: ChannelId): Promise<Result<Channel>> {
            try {
                const channel = await container.resolve<IChannelClient>("IChannelClient").get(channelId);
                return { ok: true, value: channel };
            } catch (error) {
                console.error("Error fetching channel:", error);
                return { ok: false, error: error as Error };
            }
        },
        async getByUser(userName: UserName): Promise<Result<Channel>> {
            try {
                const channel = await container.resolve<IChannelClient>("IChannelClient").getByUser(userName);
                return { ok: true, value: channel };
            } catch (error) {
                console.error("Failed to get channel by user:", error);
                return { ok: false, error: error as Error };
            }
        },
        async getBySlug(channelSlug: ChannelSlug): Promise<Result<Channel>> {
            try {
                const channel = await container.resolve<IChannelClient>("IChannelClient").getBySlug(channelSlug);
                return { ok: true, value: channel };
            } catch (error) {
                console.error('Failed to get channel by slug:', error);
                return { ok: false, error: error as Error };
            }
        },
        async getByHandle(channelHandle: ChannelHandle): Promise<Result<Channel>> {
            try {
                const channel = await container.resolve<IChannelClient>("IChannelClient").getByHandle(channelHandle);
                return { ok: true, value: channel };
            } catch (error) {
                console.error('Error occurred while fetching channel by handle:', error);
                return { ok: false, error: error as Error };
            }
        },
        async getUploads(channelId: ChannelId): Promise<Result<PlaylistVideo[]>> {
            try {
                const uploads = await container.resolve<IChannelClient>("IChannelClient").getUploads(channelId);
                return { ok: true, value: uploads };
            } catch (error) {
                console.error('Failed to get channel uploads:', error);
                return { ok: false, error: error as Error };
            }
        },
        async setUrl(url: string): Promise<Result<void>> {
            try {
                this.youtubeUrl = url;
                return { ok: true, value: undefined };
            } catch (error) {
                console.error('Failed to set URL:', error);
                return { ok: false, error: error as Error };
            }
        },

        async downloadVideo(videoId: string): Promise<Result<void>> {
            try {
                // TODO: Implement download logic
                const video = await container.resolve<IVideoClient>("IVideoClient").get(videoId);
                console.log('Downloading video:', video.title);
                return { ok: true, value: undefined };
            } catch (error) {
                console.error('Failed to download video:', error);
                return { ok: false, error: error as Error };
            }
        },
    }
});
