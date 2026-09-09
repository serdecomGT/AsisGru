using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Models
{
    public class Asistencia
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ParticipanteId { get; set; }
        public int ActividadId { get; set; }

        [NotNull]
        public string FechaRegistro { get; set; } = string.Empty;

        public string Estado { get; set; } = "Presente";
    }
}
