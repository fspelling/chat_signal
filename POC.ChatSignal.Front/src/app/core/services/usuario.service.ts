import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";

import { environment } from "src/environments/environment";

const API = environment.API_URL + '/api';

@Injectable({ providedIn: 'root' })
export class UsuarioService {
    constructor(private http: HttpClient) { }

    signup(nome: string, email: string, senha: string): Observable<any> {
        const payload = { usuarioNome: nome, usuarioEmail: email, usuarioSenha: senha };
        return this.http.post(`${API}/usuario/signup`, payload);
    }

    login(email: string, senha: string): Observable<any> {
        const payload = { usuarioEmail: email, usuarioSenha: senha };
        return this.http.post(`${API}/usuario/signin`, payload);
    }

    atualizarConnection(usuarioId: number, connectionId: string): Observable<any> {
        const payload = { usuarioId, connectionId };
        return this.http.patch(`${API}/usuario/connection`, payload);
    }

    removerConnection(usuarioId: number, connectionId: string): Observable<any> {
        return this.http.delete(`${API}/usuario/${usuarioId}/connection/${connectionId}`);
    }
}