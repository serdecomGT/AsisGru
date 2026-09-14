using AsisGru.Models;
using AsisGru.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Services
{
    public class GrupoService
    {
        private readonly DatabaseService _database;
        public GrupoService(DatabaseService database) => _database = database;

        public async Task<MGrupo?> ObtenerAsync()
        {
            var db = await _database.GetConnectionAsync();
            return await db.Table<MGrupo>().FirstOrDefaultAsync();
        }

        public async Task GuardarAsync(MGrupo grupo)
        {
            var db = await _database.GetConnectionAsync();
            if (grupo.Id == 0) await db.InsertAsync(grupo);
            else await db.UpdateAsync(grupo);
        }
    }
}
