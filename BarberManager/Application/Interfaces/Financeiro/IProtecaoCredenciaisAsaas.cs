namespace Application.Interfaces.Financeiro
{
    public interface IProtecaoCredenciaisAsaas
    {
        string Proteger(string valor);

        string Desproteger(string valorProtegido);
    }
}