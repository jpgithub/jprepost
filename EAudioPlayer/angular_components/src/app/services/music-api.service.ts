import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { Album, AlbumDetail } from '../models/library.models';
import { API_BASE_URL } from './api-config';

@Injectable({ providedIn: 'root' })
export class MusicApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = inject(API_BASE_URL);

  getAlbums(): Observable<Album[]> {
    return this.http.get<Album[]>(`${this.baseUrl}/api/albums`);
  }

  getAlbum(albumId: string): Observable<AlbumDetail> {
    return this.http.get<AlbumDetail>(`${this.baseUrl}/api/albums/${encodeURIComponent(albumId)}`);
  }

  /** Absolute URL for the audio element; the API returns a relative stream URL. */
  resolveStreamUrl(streamUrl: string): string {
    return `${this.baseUrl}${streamUrl}`;
  }
}
