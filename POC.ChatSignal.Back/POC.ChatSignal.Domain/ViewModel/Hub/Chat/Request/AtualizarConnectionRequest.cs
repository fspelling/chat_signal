﻿namespace POC.ChatSignal.Domain.ViewModel.Hub.Chat.Request
{
    public class AtualizarConnectionRequest
    {
        public required int UsuarioId { get; set; }
        public required string ConnectionId { get; set; }
    }
}
