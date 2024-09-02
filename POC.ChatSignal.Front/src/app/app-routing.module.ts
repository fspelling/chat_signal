import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ConversacaoComponent } from './pages/conversacao/conversacao.component';
import { LoginComponent } from './pages/login/login.component';
import { CadastroUsuarioComponent } from './pages/cadastro-usuario/cadastro-usuario.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'conversacao',
    pathMatch: 'full'
  },
  {
    path: 'conversacao',
    component: ConversacaoComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'cadastro-usuario',
    component: CadastroUsuarioComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
