using Application.Interfaces.Financeiro;
using Microsoft.AspNetCore.DataProtection;

namespace Infrastructure.Services
{
    public sealed class ProtecaoCredenciaisAsaas
        : IProtecaoCredenciaisAsaas
    {
        private readonly IDataProtector _protector;

        public ProtecaoCredenciaisAsaas(
            IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector(
                "BarberManager.Financeiro.Asaas.Credenciais.v1");
        }

        public string Proteger(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException(
                    "A credencial deve ser informada.",
                    nameof(valor));

            return _protector.Protect(valor);
        }

        public string Desproteger(string valorProtegido)
        {
            if (string.IsNullOrWhiteSpace(valorProtegido))
                throw new ArgumentException(
                    "A credencial protegida deve ser informada.",
                    nameof(valorProtegido));

            return _protector.Unprotect(valorProtegido);
        }
    }
}