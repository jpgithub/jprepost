import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { AlbumPanel } from './components/album-panel/album-panel';
import { NowPlaying } from './components/now-playing/now-playing';
import { SongPanel } from './components/song-panel/song-panel';
import { PlayerService } from './services/player.service';

@Component({
  selector: 'app-root',
  imports: [MatToolbarModule, MatIconModule, MatProgressSpinnerModule, NowPlaying, AlbumPanel, SongPanel],
  templateUrl: './app.html',
  styleUrl: './app.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class App {
  protected readonly player = inject(PlayerService);

  constructor() {
    inject(MatIconRegistry).setDefaultFontSetClass('material-symbols-outlined');
  }
}
