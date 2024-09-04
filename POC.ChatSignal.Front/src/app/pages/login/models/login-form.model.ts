import { FormControl } from "@angular/forms";

export interface LoginForm {
    email: FormControl<string | null>;
    senha: FormControl<string | null>;
}