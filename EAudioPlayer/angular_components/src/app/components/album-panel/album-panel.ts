import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { PlayerService } from '../../services/player.service';

@Component({
  selector: 'app-album-panel',
  imports: [MatBadgeModule, MatCardModule, MatIconModule, MatListModule],
  templateUrl: './album-panel.html',
  styleUrl: './album-panel.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AlbumPanel {
  protected readonly player = inject(PlayerService);
}
