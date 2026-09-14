using AsisGru.Data;
using AsisGru.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Services
{
    public class AsistenciaService
    {
        private readonly DatabaseService _database;
        public AsistenciaService(DatabaseService database) => _database = database;

        public async Task<List<Asistencia>> ObtenerPorActividadAsync(int actividadId)
        {
            var db = await _database.GetConnectionAsync();
            return await db.Table<Asistencia>().Where(x => x.ActividadId == actividadId).ToListAsync();
        }

        public async Task<bool> EstaRegistradaAsync(int participanteId, int actividadId)
        {
            var db = await _database.GetConnectionAsync();
            var item = await db.Table<Asistencia>()
                .Where(x => x.ParticipanteId == participanteId && x.ActividadId == actividadId)
                .FirstOrDefaultAsync();
            return item is not null;
        }

        public async Task<bool> RegistrarAsync(int participanteId, int actividadId)
        {
            var db = await _database.GetConnectionAsync();
            if (await EstaRegistradaAsync(participanteId, actividadId)) return false;

            await db.InsertAsync(new Asistencia
            {
                ParticipanteId = participanteId,
                ActividadId = actividadId,
                FechaRegistro = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Estado = "Presente"
            });
            return true;
        }

        public async Task QuitarAsync(int participanteId, int actividadId)
        {
            var db = await _database.GetConnectionAsync();
            var item = await db.Table<Asistencia>()
                .Where(x => x.ParticipanteId == participanteId && x.ActividadId == actividadId)
                .FirstOrDefaultAsync();
            if (item is not null) await db.DeleteAsync(item);
        }

        public async Task<int> ContarAsync()
        {
            var db = await _database.GetConnectionAsync();
            return await db.Table<Asistencia>().CountAsync();
        }

    }
}
