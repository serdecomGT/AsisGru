using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Models
{
    public class ReporteModels
    {
        public string Participante { get; set; } = string.Empty;
        public int Cantidad { get; set; }
    }

    public class CategoriaReporte
    {
        public string Categoria { get; set; } = string.Empty;
        public int Actividades { get; set; }
        public int Asistencias { get; set; }
        public int ParticipantesUnicos { get; set; }
    }

    public class AsistenciaActividadReporte
    {
        public string Participante { get; set; } = string.Empty;
        public string Actividad { get; set; } = string.Empty;
        public string Fecha { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}
