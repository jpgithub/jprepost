import { Injectable, computed, inject, signal } from '@angular/core';
import { forkJoin, of, switchMap } from 'rxjs';
import { AlbumDetail, Track } from '../models/library.models';
import { MusicApiService } from './music-api.service';

/** Single source of truth shared by the album panel, song panel and playback control. */
@Injectable({ providedIn: 'root' })
export class PlayerService {
  private readonly api = inject(MusicApiService);
  private readonly audio = new Audio();

  readonly albums = signal<AlbumDetail[]>([]);
  readonly loading = signal(true);
  readonly error = signal<string | null>(null);

  /** Album whose songs are shown (browsing). */
  readonly selectedAlbumId = signal<string | null>(null);
  /** Track loaded in the playback control. */
  readonly currentTrack = signal<Track | null>(null);
  readonly isPlaying = signal(false);
  readonly currentTime = signal(0);
  readonly duration = signal(0);
  readonly playbackError = signal<string | null>(null);

  readonly selectedAlbum = computed(() => this.albums().find((a) => a.id === this.selectedAlbumId()) ?? null);
  /** Album that owns the song being played; drives the active-album indicator. */
  readonly playingAlbumId = computed(() => (this.isPlaying() ? (this.currentTrack()?.albumId ?? null) : null));
  readonly progress = computed(() => (this.duration() > 0 ? this.currentTime() / this.duration() : 0));

  constructor() {
    this.audio.preload = 'metadata';
    this.audio.addEventListener('timeupdate', () => this.currentTime.set(this.audio.currentTime));
    this.audio.addEventListener('loadedmetadata', () => this.duration.set(this.audio.duration));
    this.audio.addEventListener('durationchange', () => this.duration.set(this.audio.duration));
    this.audio.addEventListener('play', () => this.isPlaying.set(true));
    this.audio.addEventListener('pause', () => {
      this.isPlaying.set(false);
      this.currentTime.set(this.audio.currentTime);
    });
    this.audio.addEventListener('error', () => this.playbackError.set('Unable to load this audio file.'));
    this.audio.addEventListener('ended', () => this.onEnded());

    this.loadLibrary();
  }

  private loadLibrary(): void {
    this.api
      .getAlbums()
      .pipe(
        switchMap((albums) => (albums.length ? forkJoin(albums.map((a) => this.api.getAlbum(a.id))) : of([]))),
      )
      .subscribe({
        next: (albums) => {
          this.albums.set(albums);
          this.loading.set(false);
          if (albums.length) {
            this.selectAlbum(albums[0].id);
          }
        },
        error: () => {
          this.error.set('Could not reach the AudioPlayer API. Make sure the backend is running.');
          this.loading.set(false);
        },
      });
  }

  /** Browse an album. While idle, its first track is loaded so the description box follows the selection. */
  selectAlbum(albumId: string): void {
    this.selectedAlbumId.set(albumId);
    this.syncIdleToSelectedAlbum();
  }

  selectTrack(track: Track): void {
    if (this.currentTrack()?.id !== track.id) {
      this.load(track);
    }
    this.play();
  }

  /** Stop when playing (resets to the start); otherwise play from the selected album's first track. */
  toggle(): void {
    if (this.isPlaying()) {
      this.audio.pause();
      this.audio.currentTime = 0;
      this.isPlaying.set(false); // the 'pause' event arrives asynchronously
      this.currentTime.set(0);
      this.syncIdleToSelectedAlbum();
    } else {
      this.syncIdleToSelectedAlbum();
      this.play();
    }
  }

  seek(ratio: number): void {
    if (this.currentTrack() && isFinite(this.audio.duration)) {
      this.audio.currentTime = ratio * this.audio.duration;
      this.currentTime.set(this.audio.currentTime);
    }
  }

  private syncIdleToSelectedAlbum(): void {
    const album = this.selectedAlbum();
    if (!album || this.isPlaying()) {
      return;
    }
    if (this.currentTrack()?.albumId !== album.id && album.tracks.length) {
      this.load(album.tracks[0]);
    }
  }

  private load(track: Track): void {
    this.currentTrack.set(track);
    this.currentTime.set(0);
    this.duration.set(0);
    this.playbackError.set(null);
    this.audio.src = this.api.resolveStreamUrl(track.streamUrl);
  }

  private play(): void {
    if (!this.currentTrack()) {
      return;
    }
    this.audio.play().catch((err: Error) => this.playbackError.set(`Playback error: ${err.message}`));
  }

  private onEnded(): void {
    const current = this.currentTrack();
    const album = this.albums().find((a) => a.id === current?.albumId);
    const next = album?.tracks[album.tracks.findIndex((t) => t.id === current?.id) + 1];
    if (album && next) {
      // Keep the songs panel on the album that is now playing.
      this.selectedAlbumId.set(album.id);
      this.load(next);
      this.play();
    } else {
      this.isPlaying.set(false);
    }
  }
}
