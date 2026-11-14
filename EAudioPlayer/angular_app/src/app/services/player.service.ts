import { Injectable, computed, inject, signal } from '@angular/core';
import { AlbumDetail, Track } from '../models/library.models';
import { AudioLibraryService } from './audio-library.service';

/** Single source of truth shared by the album panel, song panel and playback control. */
@Injectable({ providedIn: 'root' })
export class PlayerService {
  private readonly library = inject(AudioLibraryService);
  private readonly audio = new Audio();

  /** Track whose audio bytes are currently loaded into the audio element. */
  private loadedTrackId: string | null = null;
  private objectUrl: string | null = null;
  private download: AbortController | null = null;

  readonly albums = signal<AlbumDetail[]>([]);
  readonly loading = signal(true);
  readonly error = signal<string | null>(null);

  /** Album whose songs are shown (browsing). */
  readonly selectedAlbumId = signal<string | null>(null);
  /** Track shown in the playback control. */
  readonly currentTrack = signal<Track | null>(null);
  readonly isPlaying = signal(false);
  /** True while the track's audio is being streamed from the server before playback starts. */
  readonly buffering = signal(false);
  readonly currentTime = signal(0);
  readonly duration = signal(0);
  readonly playbackError = signal<string | null>(null);

  readonly isActive = computed(() => this.isPlaying() || this.buffering());
  readonly selectedAlbum = computed(() => this.albums().find((a) => a.id === this.selectedAlbumId()) ?? null);
  /** Album that owns the song being played; drives the active-album indicator. */
  readonly playingAlbumId = computed(() => (this.isActive() ? (this.currentTrack()?.albumId ?? null) : null));
  readonly progress = computed(() => (this.duration() > 0 ? this.currentTime() / this.duration() : 0));

  constructor() {
    this.audio.addEventListener('timeupdate', () => this.currentTime.set(this.audio.currentTime));
    this.audio.addEventListener('loadedmetadata', () => this.duration.set(this.audio.duration));
    this.audio.addEventListener('durationchange', () => this.duration.set(this.audio.duration));
    this.audio.addEventListener('play', () => this.isPlaying.set(true));
    this.audio.addEventListener('pause', () => {
      this.isPlaying.set(false);
      this.currentTime.set(this.audio.currentTime);
    });
    this.audio.addEventListener('error', () => this.playbackError.set('Unable to play this audio file.'));
    this.audio.addEventListener('ended', () => this.onEnded());

    void this.loadLibrary();
  }

  private async loadLibrary(): Promise<void> {
    try {
      const summaries = await this.library.listAlbums();
      const albums = await Promise.all(summaries.map((a) => this.library.getAlbum(a.id)));
      this.albums.set(albums);
      if (albums.length) {
        this.selectAlbum(albums[0].id);
      }
    } catch {
      this.error.set('Could not reach the AudioPlayer gRPC service. Make sure the backend is running.');
    } finally {
      this.loading.set(false);
    }
  }

  /** Browse an album. While idle, its first track is selected so the description box follows the selection. */
  selectAlbum(albumId: string): void {
    this.selectedAlbumId.set(albumId);
    this.syncIdleToSelectedAlbum();
  }

  selectTrack(track: Track): void {
    if (this.currentTrack()?.id !== track.id) {
      this.load(track);
    }
    void this.play();
  }

  /** Stop when active (resets to the start); otherwise play from the selected album's first track. */
  toggle(): void {
    if (this.isActive()) {
      this.download?.abort();
      this.download = null;
      this.buffering.set(false);
      this.audio.pause();
      if (this.loadedTrackId) {
        this.audio.currentTime = 0;
      }
      this.isPlaying.set(false); // the 'pause' event arrives asynchronously
      this.currentTime.set(0);
      this.syncIdleToSelectedAlbum();
    } else {
      this.syncIdleToSelectedAlbum();
      void this.play();
    }
  }

  seek(ratio: number): void {
    if (this.loadedTrackId && this.loadedTrackId === this.currentTrack()?.id && isFinite(this.audio.duration)) {
      this.audio.currentTime = ratio * this.audio.duration;
      this.currentTime.set(this.audio.currentTime);
    }
  }

  private syncIdleToSelectedAlbum(): void {
    const album = this.selectedAlbum();
    if (!album || this.isActive()) {
      return;
    }
    if (this.currentTrack()?.albumId !== album.id && album.tracks.length) {
      this.load(album.tracks[0]);
    }
  }

  /** Makes a track current without fetching its audio; the bytes are streamed on demand by play(). */
  private load(track: Track): void {
    this.download?.abort();
    this.download = null;
    this.buffering.set(false);
    this.audio.pause();
    this.isPlaying.set(false);
    this.loadedTrackId = null;
    this.currentTrack.set(track);
    this.currentTime.set(0);
    this.duration.set(0);
    this.playbackError.set(null);
  }

  private async play(): Promise<void> {
    const track = this.currentTrack();
    if (!track) {
      return;
    }

    if (this.loadedTrackId !== track.id) {
      const download = new AbortController();
      this.download?.abort();
      this.download = download;
      this.buffering.set(true);
      try {
        const blob = await this.library.downloadTrack(track.id, track.contentType, download.signal);
        if (download.signal.aborted) {
          return;
        }
        this.setAudioSource(track, blob);
      } catch (err) {
        if (!download.signal.aborted) {
          this.playbackError.set(`Could not stream this track: ${(err as Error).message}`);
        }
        return;
      } finally {
        if (this.download === download) {
          this.download = null;
          this.buffering.set(false);
        }
      }
    }

    try {
      await this.audio.play();
    } catch (err) {
      this.playbackError.set(`Playback error: ${(err as Error).message}`);
    }
  }

  private setAudioSource(track: Track, blob: Blob): void {
    if (this.objectUrl) {
      URL.revokeObjectURL(this.objectUrl);
    }
    this.objectUrl = URL.createObjectURL(blob);
    this.audio.src = this.objectUrl;
    this.loadedTrackId = track.id;
  }

  private onEnded(): void {
    const current = this.currentTrack();
    const album = this.albums().find((a) => a.id === current?.albumId);
    const next = album?.tracks[album.tracks.findIndex((t) => t.id === current?.id) + 1];
    if (album && next) {
      // Keep the songs panel on the album that is now playing.
      this.selectedAlbumId.set(album.id);
      this.load(next);
      void this.play();
    } else {
      this.isPlaying.set(false);
    }
  }
}
