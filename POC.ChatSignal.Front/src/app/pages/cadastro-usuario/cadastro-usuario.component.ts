import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";

import { UsuarioCadastroForm } from "./usuario-cadastro-form.model";
import { UsuarioService } from "src/app/core/services/usuario.service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-cadastro-usuario',
  templateUrl: './cadastro-usuario.component.html',
  styleUrls: ['./cadastro-usuario.component.scss']
})
export class CadastroUsuarioComponent implements OnInit {
  formCadastro: FormGroup<UsuarioCadastroForm>;

  constructor(private fb: FormBuilder, private usuarioService: UsuarioService, private route: Router) { }

  ngOnInit(): void {
    this.loadForm();
  }

  loadForm() {
    this.formCadastro = this.fb.group({
      nome: ['', Validators.required],
      email: ['', Validators.required],
      senha: ['', Validators.required]
    });
  }

  salvar() {
    const objUsuario = this.formCadastro.value;

    this.usuarioService.signup(objUsuario.nome!, objUsuario.email!, objUsuario.senha!)
      .subscribe(() => this.route.navigate(['']));
  }
}