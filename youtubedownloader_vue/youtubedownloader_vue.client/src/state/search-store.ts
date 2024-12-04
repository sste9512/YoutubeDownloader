
import { defineStore } from "pinia"
import { container } from "tsyringe";
import {
    Channel,
    ChannelHandle,
    ChannelId,
    ChannelSlug,
    type IChannel, type IChannelClient, type IVideoClient,
    type IPlaylistClient,
    Playlist,
    PlaylistId,
    PlaylistVideo,
    UserName,
    type ISearchClient,
    VideoSearchResult,
    ChannelSearchResult,
    ISearchResult,
    PlaylistSearchResult,
    SearchFilter,
    Video
} from "@/data/web-api-client";
import type { Result } from "@/data/result";
import { browserLogger } from "@/main";




/**
 * Store for managing search state and operations
 */
export const useSearchStore = defineStore('search', {


    state: () => ({
        searchResults: [] as ISearchResult[],
        videoSearchResults: [] as VideoSearchResult[],
        loading: false,
        error: null as Error | null
    }),

    actions: {
        /**
         * Searches for videos, playlists and channels
         * @param query - The search query
         */
        async search(query: string) {
            this.loading = true;
            this.error = null;

            try {
                const searchClient = container.resolve<ISearchClient>("ISearchClient");
                const results = await searchClient.getVideos(query);
                this.searchResults = results;
            } catch (err) {
                this.error = err as Error;
                console.error('Search failed:', err);
            } finally {
                this.loading = false;
            }
        },
        /**
       * Searches for videos
       * @param searchQuery - The query to search for
       * @returns A Result containing either an array of search results or an error
       * @remarks This method uses dependency injection to resolve the ISearchClient
       * and handles any errors that occur during the search operation
       */
        async searchVideo(searchQuery: string): Promise<Result<ISearchResult[]>> {
            try {
                const results = await container.resolve<ISearchClient>("ISearchClient").getVideos(searchQuery);
                browserLogger.info('Search results:', results);
                this.videoSearchResults = results as VideoSearchResult[];
                return { ok: true, value: results };
            } catch (error) {
                console.error('Failed to search videos:', error);
                return { ok: false, error: error as Error };
            }
        },


        /**
         * Searches for playlists
         * @param searchQuery - The query to search for
         * @returns A Result containing either an array of playlist search results or an error
         * @remarks This method uses dependency injection to resolve the ISearchClient
         * and handles any errors that occur during the search operation
         */
        async searchPlaylists(searchQuery: string): Promise<Result<PlaylistSearchResult[]>> {
            try {
                const playlists = await container.resolve<ISearchClient>("ISearchClient").getPlaylists(searchQuery);
                return { ok: true, value: playlists };
            } catch (error) {
                console.error('Failed to search playlists:', error);
                return { ok: false, error: error as Error };
            }
        },

        /**
         * Searches for channels
         * @param searchQuery - The query to search for
         * @returns A Result containing either an array of channel search results or an error
         * @remarks This method uses dependency injection to resolve the ISearchClient
         * and handles any errors that occur during the search operation
         */
        async searchChannels(searchQuery: string): Promise<Result<ChannelSearchResult[]>> {
            try {
                const channels = await container.resolve<ISearchClient>("ISearchClient").getChannels(searchQuery);
                return { ok: true, value: channels };
            } catch (error) {
                console.error('Failed to search channels:', error);
                return { ok: false, error: error as Error };
            }
        },

        /**
         * Clears the search results and resets the store state
         */
        clearResults() {
            this.searchResults = [];
            this.error = null;
            this.loading = false;
        }
    },

    getters: {
        /**
         * Returns whether there are any search results
         */
        hasResults: (state) => state.searchResults.length > 0,

        /**
         * Returns only video results
         */
        videoResults: (state) => state.searchResults.filter(r => r.url?.includes('Video')),

        /**
         * Returns only playlist results
         */
        playlistResults: (state) => state.searchResults.filter(r => r.url?.includes('Playlist')),

        /**
         * Returns only channel results
         */
        channelResults: (state) => state.searchResults.filter(r => r.url?.includes('Channel'))
    }
})

