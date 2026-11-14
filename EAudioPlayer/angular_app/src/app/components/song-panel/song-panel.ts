import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { PlayerService } from '../../services/player.service';

@Component({
  selector: 'app-song-panel',
  imports: [MatCardModule, MatIconModule, MatListModule, MatProgressBarModule],
  templateUrl: './song-panel.html',
  styleUrl: './song-panel.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SongPanel {
  protected readonly player = inject(PlayerService);
}
