import { InjectionToken } from '@angular/core';

/** gRPC-Web endpoint of the AudioPlayer gRPC service (dotnet_grpc). Its CORS policy allows http://localhost:4200. */
export const GRPC_WEB_URL = new InjectionToken<string>('GRPC_WEB_URL', {
  providedIn: 'root',
  factory: () => 'http://localhost:5133',
});
