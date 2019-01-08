using System;

namespace DocR.Infra.CrossCutting.Identity.Models.AccountViewModels
{
    public class ConfirmarEmailViewModel
    {
        public Guid Id { get; set; }
        public string Senha { get; set; }
        public string ConfirmeSenha { get; set; }
    }
}
