using Application.Common.Middlewares.Handler;
using Application.Interfaces.Empresas;
using Application.Saas.Empresas.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Saas.Empresas.Commands.Handlers
{
    public class AlterarAcessoEmpresaHandler : ICommandHandler<AlterarAcessoEmpresaCommand,ResultadoAlteracaoAcessoEmpresa>
    {
        private readonly IRepositorioEmpresa repositorioEmpresa;

        public AlterarAcessoEmpresaHandler(IRepositorioEmpresa repositorioEmpresa)
        {
            this.repositorioEmpresa = repositorioEmpresa;
        }

        public async Task<ResultadoAlteracaoAcessoEmpresa> Handle(AlterarAcessoEmpresaCommand command, CancellationToken cancellationToken)
        {
            if(command.EmpresaId == Guid.Empty)
            {
                return ResultadoAlteracaoAcessoEmpresa.DadosInvalidos;
            }
            string? motivo = command.Motivo?.Trim();

            if(!command.HabilitarAcesso && string.IsNullOrWhiteSpace(motivo))
            {
                return ResultadoAlteracaoAcessoEmpresa.DadosInvalidos;
            }
            return await this.repositorioEmpresa.AlterarAcessoAsync(
                command.EmpresaId, command.HabilitarAcesso, motivo, cancellationToken);

        }
    }
}
