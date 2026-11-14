export interface Album {
  id: string;
  title: string;
  trackCount: number;
}

export interface Track {
  id: string;
  title: string;
  albumId: string;
  albumTitle: string;
  number: number;
  format: string;
  sizeBytes: number;
  streamUrl: string;
}

export interface AlbumDetail {
  id: string;
  title: string;
  tracks: Track[];
}
