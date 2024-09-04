import { FormControl } from "@angular/forms";

export interface UsuarioCadastroForm {
    nome: FormControl<string | null>;
    email: FormControl<string | null>;
    senha: FormControl<string | null>;
}