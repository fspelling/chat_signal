import { Component, OnDestroy, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { Usuario } from "src/app/core/models/usuario.model";
import { LocalStorageService } from "src/app/core/services/local-storage.service";
import { UsuarioService } from "src/app/core/services/usuario.service";
import { ChatSignalRService } from "src/app/core/services/chat-signar.service";
import { ChatForm } from "./chat-form.model";
import { Mensagem } from "src/app/core/models/mensagem.model";

@Component({
  selector: 'app-conversacao',
  templateUrl: './conversacao.component.html',
  styleUrls: ['./conversacao.component.scss']
})
export class ConversacaoComponent implements OnInit, OnDestroy {
  usuarioLogado: Usuario;
  usuarios: Usuario[] = [];
  mensagens: { mensagem: Mensagem, usuario: Usuario }[];
  nomeGrupo: string;
  formChat: FormGroup<ChatForm>;

  constructor(
    private fb: FormBuilder,
    private storageService: LocalStorageService<Usuario>,
    private usuarioService: UsuarioService,
    private signalService: ChatSignalRService
  ) { }

  ngOnInit(): void {
    this.usuarioLogado = this.storageService.getItem('usuarioLogado') as Usuario;

    this.verificarConnectionIdUsuario();
    this.loadForm();
    this.obterUsuarios();
    this.atualizarMensagem();
    this.atualizarUsuarioOnline();
    this.obterGrupoCriado();
  }

  ngOnDestroy(): void {
    this.signalService.closeConnection().subscribe();
  }

  verificarConnectionIdUsuario() {
    const connectionId = this.signalService.getConnection();

    console.log(connectionId);
    console.log(this.usuarioLogado.connectionId);
    console.log(connectionId != this.usuarioLogado.connectionId);
    

    if (connectionId != this.usuarioLogado.connectionId) {
      this.signalService.startConnection()
        .subscribe((connectionId: string) => {
          this.signalService.invokeRemoverConnection(this.usuarioLogado.id, this.usuarioLogado.connectionId);

          this.usuarioLogado.connectionId = connectionId;
          this.storageService.setItem('usuarioLogado', this.usuarioLogado)
          this.signalService.invokeAtualizarConnection(this.usuarioLogado.id, connectionId);
        });
    }
  }

  loadForm() {
    this.formChat = this.fb.group({
      mensagem: ['', Validators.required],
    });
  }

  obterUsuarios() {
    this.usuarioService.obterUsuarios()
      .subscribe(response => {
        var usuariosFilter: Usuario[] = response.result.usuarios.filter((u: Usuario) => u.id != this.usuarioLogado.id);
        this.usuarios = usuariosFilter;
      });
  }

  atualizarMensagem() {
    this.signalService.receiveReceberMensagem()
      .subscribe((mensagem: Mensagem) => this.mensagens.push(this.convertObjUsuarioMsg(mensagem)));
  }

  atualizarUsuarioOnline() {
    this.signalService.receiveAtualizarUsuarioConversacao().subscribe((usuario: Usuario) => {
      let usuarioFind = this.usuarios.find(u => u.id == usuario.id);
      usuarioFind!.isOnline = usuario.isOnline;
    });
  }

  selecionarUsuarioConversa(emailUsuario: string) {
    const emailLogado = this.usuarioLogado.email;
    this.signalService.invokeCriarGrupoUsuario(emailLogado, emailUsuario);
  }

  obterGrupoCriado() {
    this.signalService.receiveAbrirGrupo().subscribe(({ nomeGrupo, mensagens }) => {
      this.nomeGrupo = nomeGrupo;
      this.mensagens = mensagens.map(mensagem => this.convertObjUsuarioMsg(mensagem));
    });
  }

  logout() {
    this.storageService.deleteItem("usuarioLogado");
    this.signalService.invokeRemoverConnection(this.usuarioLogado.id, this.usuarioLogado.connectionId);

    location.href = '';
  }

  enviarMensagem() {
    const mensagem = this.formChat.value.mensagem!;

    this.signalService.invokeEnviarMensagem(this.usuarioLogado, mensagem, this.nomeGrupo);
    this.formChat.reset();
  }

  convertObjUsuarioMsg(mensagem: Mensagem): { mensagem: Mensagem, usuario: Usuario } {
    const usuarioParse = JSON.parse(mensagem.usuario) as any;

    const usuario: Usuario = {
      id: usuarioParse.ID,
      connectionId: usuarioParse.ConnectionIds,
      email: usuarioParse.Email,
      isOnline: usuarioParse.IsOnline,
      nome: usuarioParse.Nome,
      senha: usuarioParse.Senha
    };

    return { mensagem, usuario }
  }
}