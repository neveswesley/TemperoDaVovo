import { Injectable, inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import * as signalR from '@microsoft/signalr';
import { environment } from '../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class SignalrService {
    private connection: signalR.HubConnection | null = null;
    private platformId = inject(PLATFORM_ID);

    async start(): Promise<void> {
        if (!isPlatformBrowser(this.platformId)) return;
        if (this.connection) return;

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl(`${environment.apiUrl}/hubs/orders`, {
                accessTokenFactory: () => localStorage.getItem('token') ?? ''
            })
            .withAutomaticReconnect()
            .build();
        await this.connection.start();
    }

    async stop(): Promise<void> {
        if (!this.connection) return;

        await this.connection.stop();
        this.connection = null;
    }

    async joinRestaurantGroup(restaurantId: string): Promise<void> {
        if (!this.connection) return;
        await this.connection.invoke('JoinRestaurantGroup', restaurantId);
    }

    async leaveRestaurantGroup(restaurantId: string): Promise<void> {
        if (!this.connection) return;
        await this.connection.invoke('LeaveRestaurantGroup', restaurantId);
    }

    async joinCustomerGroup(clientSessionId: string): Promise<void> {
        if (!this.connection) return;
        await this.connection.invoke('JoinCustomerGroup', clientSessionId);
    }

    async leaveCustomerGroup(clientSessionId: string): Promise<void> {
        if (!this.connection) return;
        await this.connection.invoke('LeaveCustomerGroup', clientSessionId);
    }

    onOrderCreated(callback: (payload: any) => void): void {
        this.connection?.on('OrderCreated', callback);
    }

    onOrderUpdated(callback: (payload: any) => void): void {
        this.connection?.on('OrderUpdated', callback);
    }

    offOrderCreated(): void {
        this.connection?.off('OrderCreated');
    }

    offOrderUpdated(): void {
        this.connection?.off('OrderUpdated');
    }
}