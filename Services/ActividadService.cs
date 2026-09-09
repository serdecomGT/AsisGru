using AsisGru.Data;
using AsisGru.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Services
{
    public class ActividadService
    {
        private readonly DatabaseService _database;
        public ActividadService(DatabaseService database) => _database = database;

        public async Task<List<Actividad>> ObtenerTodosAsync()
        {
            var db = await _database.GetConnectionAsync();
            return await db.Table<Actividad>().OrderByDescending(x => x.Fecha).ThenByDescending(x => x.Hora).ToListAsync();
        }

        public async Task GuardarAsync(Actividad actividad)
        {
            var db = await _database.GetConnectionAsync();
            if (actividad.Id == 0) await db.InsertAsync(actividad);
            else await db.UpdateAsync(actividad);
        }

        public async Task<Actividad?> ObtenerAsync(int id)
        {
            var db = await _database.GetConnectionAsync();
            return await db.FindAsync<Actividad>(id);
        }
    }
}
