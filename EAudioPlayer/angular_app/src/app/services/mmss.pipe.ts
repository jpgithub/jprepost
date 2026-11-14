import { Pipe, PipeTransform } from '@angular/core';

/** 83.4 -> "1:23" */
@Pipe({ name: 'mmss' })
export class MmssPipe implements PipeTransform {
  transform(seconds: number | null | undefined): string {
    if (seconds == null || !isFinite(seconds)) {
      return '0:00';
    }
    const s = Math.floor(seconds);
    return `${Math.floor(s / 60)}:${String(s % 60).padStart(2, '0')}`;
  }
}
