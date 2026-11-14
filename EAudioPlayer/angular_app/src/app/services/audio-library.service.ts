import { Injectable, inject } from '@angular/core';
import { GrpcWebFetchTransport } from '@protobuf-ts/grpcweb-transport';
import { AudioLibraryClient } from '../grpc/audio_library.client';
import { AlbumDetail } from '../models/library.models';
import { GRPC_WEB_URL } from './grpc-config';

/** All backend access goes through the generated gRPC-Web client; there is no REST/HTTP API usage. */
@Injectable({ providedIn: 'root' })
export class AudioLibraryService {
  private readonly client = new AudioLibraryClient(new GrpcWebFetchTransport({ baseUrl: inject(GRPC_WEB_URL) }));

  async listAlbums(): Promise<{ id: string; title: string; trackCount: number }[]> {
    const { response } = await this.client.listAlbums({});
    return response.albums;
  }

  async getAlbum(albumId: string): Promise<AlbumDetail> {
    const { response } = await this.client.getAlbum({ albumId });
    return response;
  }

  /** Consumes the StreamTrack server stream and returns the audio as a Blob playable by an audio element. */
  async downloadTrack(trackId: string, contentType: string, signal?: AbortSignal): Promise<Blob> {
    const call = this.client.streamTrack({ trackId, offset: 0 }, { abort: signal });
    const parts: BlobPart[] = [];
    for await (const chunk of call.responses) {
      parts.push(chunk.data as BlobPart);
    }
    return new Blob(parts, { type: contentType });
  }
}
