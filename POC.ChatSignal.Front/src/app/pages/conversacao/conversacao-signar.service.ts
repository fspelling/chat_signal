import * as signalR from '@microsoft/signalr';

import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Observable } from 'rxjs';

const HUB = environment.API_URL + '/ChatHub';

@Injectable({
    providedIn: 'root',
})
export class ConversacaoSignalRService {
    private connection: signalR.HubConnection;

    constructor() {
        this.connection = new signalR.HubConnectionBuilder().withUrl(HUB).withAutomaticReconnect().build();
    }

    startConnection(): Observable<string> {
        return new Observable<string>((observer) => {
            this.connection.start()
                .then(() => {
                    console.info("hub connect");

                    observer.next(this.connection.connectionId!);
                    observer.complete();
                })
                .catch((error => {
                    console.error(error);
                    observer.error();
                }));
        });
    }

    closeConnection(): Observable<void> {
        return new Observable<void>((observer) => {
            this.connection.stop()
                .then(() => {
                    console.info("hub disconnect");

                    observer.next();
                    observer.complete();
                })
                .catch((error => {
                    console.error(error);
                    observer.error();
                }));
        });
    }
}