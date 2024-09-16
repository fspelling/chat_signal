import { FormControl } from "@angular/forms";

export interface ChatForm {
    mensagem: FormControl<string | null>;
}