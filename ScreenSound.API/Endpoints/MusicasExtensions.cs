using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.API.Response;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
    {
        public static void AddEndPointMusicas(this WebApplication app)
        {
            #region Musicas

            app.MapGet("/Musicas", ([FromServices] DAL<Musica> dal) =>
            {
                var musicaList = dal.Listar();
                if (musicaList is null)
                {
                    return Results.NotFound();
                }
                var musicaListResponse = EntityListToResponseList(musicaList);
                return Results.Ok(musicaListResponse);
            });

            app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> dal, string nome) => {
                var musica = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (musica is null)
                {
                    return Results.Ok(musica);
                }
                return Results.Ok(musica.ToString());
            });

            app.MapPost("/Musicas/{id}", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequest musicaRequest) => {
                var musica = new Musica(musicaRequest.nome, musicaRequest.ArtistaId, musicaRequest.anoLancamento);
                dal.Adicionar(musica);
                return Results.Ok();
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

            app.MapPut("/Musicas", ([FromServices] DAL<Musica> dal, [FromBody] MusicaRequestEdit musicaRequestEdit) => {
                var musicaAtualizar = dal.RecuperarPor(a => a.Id == musicaRequestEdit.Id);
                
                if(musicaAtualizar is null)
                {
                    return Results.NotFound();
                }
                musicaAtualizar.Nome = musicaRequestEdit.nome;
                musicaAtualizar.AnoLancamento = musicaRequestEdit.anoLancamento;

                dal.Atualizar(musicaAtualizar);
                return Results.Ok();
            });

            #endregion
        }

        private static ICollection<MusicaResponse> EntityListToResponseList(IEnumerable<Musica> musicaList)
        {
            return musicaList.Select(a => EntityToResponse(a)).ToList();
        }

        private static MusicaResponse EntityToResponse(Musica musica)
        {
            return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome);
        }
    }
}
