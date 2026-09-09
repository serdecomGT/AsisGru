using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Models
{
    public class Participante
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nombres { get; set; } = string.Empty;

        [NotNull]
        public string Apellidos { get; set; } = string.Empty;

        public string? Direccion { get; set; }

        [NotNull]
        public string Telefono { get; set; } = string.Empty;

        public int GrupoId { get; set; }
        public bool Activo { get; set; } = true;

        [Ignore]
        public string NombreCompleto => $"{Nombres} {Apellidos}".Trim();
    }
}
