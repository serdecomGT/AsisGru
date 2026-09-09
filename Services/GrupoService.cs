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

        public async Task<Grupo?> ObtenerAsync()
        {
            var db = await _database.GetConnectionAsync();
            return await db.Table<Grupo>().FirstOrDefaultAsync();
        }

        public async Task GuardarAsync(Grupo grupo)
        {
            var db = await _database.GetConnectionAsync();
            if (grupo.Id == 0) await db.InsertAsync(grupo);
            else await db.UpdateAsync(grupo);
        }
    }
}
