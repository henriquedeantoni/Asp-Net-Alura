﻿using ScreenSoundShared.Modelos.Modelos;
using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests
{
    public record MusicaRequest([Required] string nome, [Required] int ArtistaId, int anoLancamento, ICollection<GeneroRequest> Generos=null );

    public record MusicaRequestEdit(int Id, string nome, int ArtistaId, int anoLancamento) : MusicaRequest(nome, ArtistaId, anoLancamento);

}
