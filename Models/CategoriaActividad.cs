using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Models
{
    public class CategoriaActividad
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }
        public bool Activo { get; set; } = true;
    }
}
