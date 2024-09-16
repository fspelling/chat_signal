import * as signalR from '@microsoft/signalr';

import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Observable } from 'rxjs';
import { Usuario } from '../models/usuario.model';
import { Mensagem } from '../models/mensagem.model';

const HUB = environment.API_URL + '/ChatHub';

@Injectable({
    providedIn: 'root',
})
export class ChatSignalRService {
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

    getConnection(): string | null {
        return this.connection.connectionId;
    }

    invokeAtualizarConnection(usuarioId: number, connectionId: string) {
        const request = { usuarioId, connectionId };
        this.connection.invoke('AtualizarConnection', request);
    }

    invokeRemoverConnection(usuarioId: number, connectionId: string) {
        const request = { usuarioId, connectionId };
        this.connection.invoke('RemoverConnection', request);
    }

    invokeCriarGrupoUsuario(emailLogado: string, emailConversa: string) {
        const request = { emailLogado, emailConversa };
        this.connection.invoke('CriarGrupoUsuario', request);
    }

    invokeEnviarMensagem(usuarioLogado: Usuario, mensagem: string, nomeGrupo: string) {
        const request = { usuarioLogado, mensagem, nomeGrupo };
        this.connection.invoke('EnviarMensagem', request);
    }

    receiveAtualizarUsuarioConversacao(): Observable<Usuario> {
        return new Observable<Usuario>(observer => {
            this.connection.on('atualizarUsuarioConversacao', (usuario: Usuario) => observer.next(usuario));
        });
    }

    receiveAbrirGrupo(): Observable<{nomeGrupo: string, mensagens: Mensagem[]}> {
        return new Observable<{nomeGrupo: string, mensagens: Mensagem[]}>(observer => {
            this.connection.on('abrirGrupo', (nomeGrupo: string, mensagens: Mensagem[]) => observer.next({nomeGrupo, mensagens}));
        });
    }

    receiveReceberMensagem(): Observable<Mensagem> {
        return new Observable<Mensagem>(observer => {
            this.connection.on('receberMensagem', (msg: Mensagem) => observer.next(msg));
        });
    }
}