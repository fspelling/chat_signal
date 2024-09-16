import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";

import { UsuarioService } from "src/app/core/services/usuario.service";
import { LoginForm } from "./models/login-form.model";
import { LocalStorageService } from "src/app/core/services/local-storage.service";
import { Usuario } from "src/app/core/models/usuario.model";
import { ChatSignalRService } from "src/app/core/services/chat-signar.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  formLogin: FormGroup<LoginForm>;

  constructor(
    private fb: FormBuilder,
    private usuarioService: UsuarioService,
    private route: Router,
    private storageService: LocalStorageService<Usuario>,
    private signalService: ChatSignalRService,
  ) { }

  ngOnInit(): void {
    this.loadForm();
  }

  loadForm() {
    this.formLogin = this.fb.group({
      email: ['', Validators.required],
      senha: ['', Validators.required]
    });
  }

  login() {
    const objUsuario = this.formLogin.value;
    this.usuarioService.login(objUsuario.email!, objUsuario.senha!)
      .subscribe(response => this.loadConnectionSignalR(response.result.usuario as Usuario));
  }

  loadConnectionSignalR(usuario: Usuario) {
    this.signalService.startConnection()
      .subscribe((connectionId: string) => {
        usuario.connectionId = connectionId;

        this.storageService.setItem('usuarioLogado', usuario)
        this.signalService.invokeAtualizarConnection(usuario.id, connectionId);
        this.route.navigate(['/conversacao']);
      });
  }
}