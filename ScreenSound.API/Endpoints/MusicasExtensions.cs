using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndPointMusicas(this WebApplication app)
        {
            #region Musicas

            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) => {
                return dal.Listar();
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) => {
                var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica is null)
                {
                    return Results.Ok(musica);
                }
                return Results.Ok(musica.ToString());
            });

            app.MapPost("/Musicas/{id}", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) => {
                dal.Adicionar(musica);
                return Results.Ok(musica);
            });

            app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> dal, int id) => {
                var musica = dal.RecuperarPor(a => a.Id == id);
                if (musica is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(musica);
                return Results.NoContent();
            });

            app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] Musica musica) => {
                var musicaAtualizar = dal.RecuperarPor(a => a.Id == musica.Id);
                if (musicaAtualizar is null)
                {
                    return Results.NotFound();
                }
                musicaAtualizar.Nome = musica.Nome;
                musicaAtualizar.AnoLancamento = musica.AnoLancamento;

                dal.Atualizar(musicaAtualizar);
                return Results.Ok();
            });

            #endregion
        }
    }
}
