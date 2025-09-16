import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

export class SignalRService {
  private static connection: HubConnection | null = null;
  private static readonly HUB_URL = "http://localhost:5278/mediaHub";

  static async connect(): Promise<void> {
    if (this.connection?.state === 'Connected') return;

    this.connection = new HubConnectionBuilder()
      .withUrl(this.HUB_URL)
      .withAutomaticReconnect()
      .build();

    await this.connection.start();
    console.log('SignalR Connected');
  }

  static async disconnect(): Promise<void> {
    await this.connection?.stop();
    this.connection = null;
  }

  static onPlaylistStarted(callback: (data: { PlaylistId: number, action: string }) => void): void {
    this.connection?.on('PlaylistStarted', callback);
  }

  static off(eventName: string): void {
    this.connection?.off(eventName);
  }
}