﻿using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class ArtistasExtension
    {
        public static void AddEndPointsArtistas(this WebApplication app)
        {
            #region Artistas

            app.MapGet("/Artistas", ([FromServices] DAL<Artista> dal) => {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> dal, string nome) => {
                var artista = dal.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
                if (artista is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(artista);
            });

            app.MapPost("/Artistas", ([FromServices] DAL<Artista> dal, [FromBody] ArtistaRequest artistaRequest) => {

                var artista = new Artista(artistaRequest.nome, artistaRequest.bio);

                dal.Adicionar(artista);
                return Results.Ok();
            });

            app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> dal, int id) =>
            {
                var artista = dal.RecuperarPor(a => a.Id == id);
                if (artista is null)
                {
                    return Results.NotFound();
                }
                dal.Deletar(artista);
                return Results.NoContent();
            });

            app.MapPut("/Artistas", ([FromServices] DAL<Artista> dal,
                [FromBody] ArtistaRequestEdit artistaRequestEdit) => {
                    var artistaAtualizar = dal.RecuperarPor(a => a.Id == artistaRequestEdit.Id);
                    if (artistaAtualizar is null)
                    {
                        return Results.NotFound();
                    }
                    artistaAtualizar.Nome = artistaRequestEdit.nome;
                    artistaAtualizar.Bio = artistaRequestEdit.bio;
                    artistaAtualizar.FotoPerfil = artistaRequestEdit.fotoPerfil;

                    dal.Atualizar(artistaAtualizar);
                    return Results.Ok();
                });

            #endregion
        }
    }
}
