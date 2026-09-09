using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Models
{
    public class Actividad
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        [NotNull]
        public string Fecha { get; set; } = string.Empty; // yyyy-MM-dd

        public string? Hora { get; set; }
        public int CategoriaId { get; set; }
        public int GrupoId { get; set; }
        public bool Activo { get; set; } = true;
    }
}
