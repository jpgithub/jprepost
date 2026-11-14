import { ChangeDetectionStrategy, Component, inject, signal } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatSliderModule } from '@angular/material/slider';
import { FileSizePipe } from '../../services/file-size.pipe';
import { MmssPipe } from '../../services/mmss.pipe';
import { PlayerService } from '../../services/player.service';

@Component({
  selector: 'app-now-playing',
  imports: [MatButtonModule, MatCardModule, MatIconModule, MatSliderModule, MmssPipe, FileSizePipe],
  templateUrl: './now-playing.html',
  styleUrl: './now-playing.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NowPlaying {
  protected readonly player = inject(PlayerService);
  protected readonly dragging = signal(false);
  protected readonly dragValue = signal(0);

  protected readonly max = 1000;

  protected sliderValue(): number {
    return this.dragging() ? this.dragValue() : Math.round(this.player.progress() * this.max);
  }

  protected shownTime(): number {
    return this.dragging() ? (this.dragValue() / this.max) * this.player.duration() : this.player.currentTime();
  }

  protected onDrag(value: number): void {
    this.dragging.set(true);
    this.dragValue.set(value);
  }

  protected onCommit(value: number): void {
    this.player.seek(value / this.max);
    this.dragging.set(false);
  }
}
