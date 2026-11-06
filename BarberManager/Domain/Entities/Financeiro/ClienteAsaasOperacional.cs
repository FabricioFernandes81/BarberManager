
using Domain.Entities.Common;

namespace Domain.Entities.Financeiro
{
    public class ClienteAsaasOperacional : EntityBase
    {
        public Guid EmpresaId { get; private set; }

        public Guid ClienteId { get; private set; }

        public string ClienteAsaasId { get; private set; } = string.Empty;

        public DateTime DataCriacaoGatewayUtc { get; private set; }

        public DateTime? DataAtualizacaoGatewayUtc { get; private set; }

        protected ClienteAsaasOperacional()
        {
        }

        public ClienteAsaasOperacional(
            Guid empresaId,
            Guid clienteId,
            string clienteAsaasId)
        {
            if (empresaId == Guid.Empty)
                throw new ArgumentException("Empresa inválida.");

            if (clienteId == Guid.Empty)
                throw new ArgumentException("Cliente inválido.");

            if (string.IsNullOrWhiteSpace(clienteAsaasId))
                throw new ArgumentException(
                    "O identificador do cliente no Asaas é obrigatório.");

            EmpresaId = empresaId;
            ClienteId = clienteId;
            ClienteAsaasId = clienteAsaasId.Trim();
            DataCriacaoGatewayUtc = DateTime.UtcNow;
        }

        public void AtualizarClienteAsaas(string clienteAsaasId)
        {
            if (string.IsNullOrWhiteSpace(clienteAsaasId))
                throw new ArgumentException(
                    "O identificador do cliente no Asaas é obrigatório.");

            ClienteAsaasId = clienteAsaasId.Trim();
            DataAtualizacaoGatewayUtc = DateTime.UtcNow;
        }
    }
}
