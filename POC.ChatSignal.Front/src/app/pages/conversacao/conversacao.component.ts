import { Component, OnDestroy, OnInit } from "@angular/core";
import { Usuario } from "src/app/core/models/usuario.model";
import { LocalStorageService } from "src/app/core/services/local-storage.service";
import { UsuarioService } from "src/app/core/services/usuario.service";
import { ConversacaoSignalRService } from "./conversacao-signar.service";
import { switchMap } from "rxjs";

@Component({
  selector: 'app-conversacao',
  templateUrl: './conversacao.component.html',
  styleUrls: ['./conversacao.component.scss']
})
export class ConversacaoComponent implements OnInit, OnDestroy {
  usuarioLogado: Usuario;

  constructor(
    private storageService: LocalStorageService<Usuario>,
    private usuarioService: UsuarioService,
    private signalService: ConversacaoSignalRService
  ) { }

  ngOnInit(): void {
    this.usuarioLogado = this.storageService.getItem('usuarioLogado') as Usuario;
    this.loadConnectionSignalR();
  }

  ngOnDestroy(): void {
    this.signalService.closeConnection().subscribe();
  }

  loadConnectionSignalR() {
    this.signalService.startConnection()
      .pipe(
        switchMap((connectionId: string) => {
          this.usuarioLogado.connectionId = connectionId
          return this.usuarioService.atualizarConnection(this.usuarioLogado.id, connectionId)
        })
      ).subscribe();
  }

  logout() {
    this.usuarioService.removerConnection(this.usuarioLogado.id, this.usuarioLogado.connectionId)
      .subscribe(() => {
        this.storageService.deleteItem("usuarioLogado");
        location.href = '';
      });
  }
}