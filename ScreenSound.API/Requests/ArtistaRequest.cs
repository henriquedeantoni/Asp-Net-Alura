namespace ScreenSound.API.Requests
{
    public record ArtistaRequest(string nome, string bio, string fotoPerfil);

    public record ArtistaRequestEdit(int Id, string nome, string bio, string fotoPerfil) : ArtistaRequest(nome, bio, fotoPerfil);
}
