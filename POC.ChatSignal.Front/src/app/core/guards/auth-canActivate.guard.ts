import { inject } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";

import { LocalStorageService } from "../services/local-storage.service";
import { Usuario } from "../models/usuario.model";

export const authCanActivate: CanActivateFn = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ) => {
    const storageService = inject(LocalStorageService);
    const router = inject(Router);
    const usuarioLogado = storageService.getItem('usuarioLogado') as Usuario;

    return (usuarioLogado != null ? true : router.createUrlTree(['/']));
  };
  