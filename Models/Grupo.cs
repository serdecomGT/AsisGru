using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Models
{
    public class Grupo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }
        public string Responsable { get; set; } = string.Empty;
        public string TelefonoResponsable { get; set; } = string.Empty;
        public string? Organizacion { get; set; }
    }
}
