using AsisGru.Data;
using AsisGru.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Services
{
    public class ReporteService
    {
        private readonly DatabaseService _database;
        public ReporteService(DatabaseService database) => _database = database;

        private async Task<SQLiteAsyncConnection> Db() => await _database.GetConnectionAsync();

        public async Task<List<AsistenciaActividadReporte>> PorActividadAsync(DateTime inicio, DateTime fin, int? categoriaId = null, int? actividadId = null)
        {
            var db = await Db();
            var sql = """
            SELECT p.Nombres || ' ' || p.Apellidos AS Participante,
                   a.Nombre AS Actividad, a.Fecha, s.Estado
            FROM Asistencia s
            INNER JOIN Participante p ON p.Id = s.ParticipanteId
            INNER JOIN Actividad a ON a.Id = s.ActividadId
            WHERE a.Fecha >= ? AND a.Fecha <= ?
            """;
            var args = new List<object> { inicio.ToString("yyyy-MM-dd"), fin.ToString("yyyy-MM-dd") };

            if (categoriaId.HasValue)
            {
                sql += " AND a.CategoriaId = ?";
                args.Add(categoriaId.Value);
            }
            if (actividadId.HasValue)
            {
                sql += " AND a.Id = ?";
                args.Add(actividadId.Value);
            }

            sql += " ORDER BY a.Fecha DESC, a.Nombre, Participante";
            return await db.QueryAsync<AsistenciaActividadReporte>(sql, args.ToArray());
        }

        public async Task<List<AsistenciaParticipanteReporte>> PorCategoriaAsync(DateTime inicio, DateTime fin, int categoriaId)
        {
            var db = await Db();
            var sql = """
            SELECT p.Nombres || ' ' || p.Apellidos AS Participante,
                   COUNT(s.Id) AS Cantidad
            FROM Asistencia s
            INNER JOIN Participante p ON p.Id = s.ParticipanteId
            INNER JOIN Actividad a ON a.Id = s.ActividadId
            WHERE a.Fecha >= ? AND a.Fecha <= ? AND a.CategoriaId = ?
            GROUP BY p.Id, p.Nombres, p.Apellidos
            ORDER BY Cantidad DESC, Participante
            """;
            return await db.QueryAsync<AsistenciaParticipanteReporte>(
                sql, inicio.ToString("yyyy-MM-dd"), fin.ToString("yyyy-MM-dd"), categoriaId);
        }

        public async Task<List<AsistenciaParticipanteReporte>> GeneralAsync(DateTime inicio, DateTime fin)
        {
            var db = await Db();
            var sql = """
            SELECT p.Nombres || ' ' || p.Apellidos AS Participante,
                   COUNT(s.Id) AS Cantidad
            FROM Asistencia s
            INNER JOIN Participante p ON p.Id = s.ParticipanteId
            INNER JOIN Actividad a ON a.Id = s.ActividadId
            WHERE a.Fecha >= ? AND a.Fecha <= ?
            GROUP BY p.Id, p.Nombres, p.Apellidos
            ORDER BY Cantidad DESC, Participante
            """;
            return await db.QueryAsync<AsistenciaParticipanteReporte>(
                sql, inicio.ToString("yyyy-MM-dd"), fin.ToString("yyyy-MM-dd"));
        }

        public async Task<List<CategoriaReporte>> ResumenCategoriasAsync(DateTime inicio, DateTime fin)
        {
            var db = await Db();
            var sql = """
            SELECT c.Nombre AS Categoria,
                   COUNT(DISTINCT a.Id) AS Actividades,
                   COUNT(s.Id) AS Asistencias,
                   COUNT(DISTINCT s.ParticipanteId) AS ParticipantesUnicos
            FROM CategoriaActividad c
            LEFT JOIN Actividad a
              ON a.CategoriaId = c.Id
             AND a.Fecha >= ? AND a.Fecha <= ?
            LEFT JOIN Asistencia s ON s.ActividadId = a.Id
            GROUP BY c.Id, c.Nombre
            ORDER BY c.Nombre
            """;
            return await db.QueryAsync<CategoriaReporte>(
                sql, inicio.ToString("yyyy-MM-dd"), fin.ToString("yyyy-MM-dd"));
        }


    }
}
