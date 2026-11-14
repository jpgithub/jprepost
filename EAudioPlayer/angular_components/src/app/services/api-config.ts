import { InjectionToken } from '@angular/core';

/** Base URL of the AudioPlayer API (dotnet_backend). Its CORS policy allows http://localhost:4200. */
export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL', {
  providedIn: 'root',
  factory: () => 'http://localhost:5132',
});
