namespace Application.Interfaces.Notificacoes;

public interface IProtecaoCredenciaisWhatsApp
{
    string Proteger(string valor);

    string Desproteger(string valorProtegido);
}