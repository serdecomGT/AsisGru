using AsisGru.Data;
using AsisGru.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Services
{
    public class ParticipanteService
    {
        private readonly DatabaseService _database;
        public ParticipanteService(DatabaseService database) => _database = database;

        public async Task<List<Participante>> ObtenerTodosAsync(bool incluirInactivos = true)
        {
            var db = await _database.GetConnectionAsync();
            var query = db.Table<Participante>();
            var items = await query.OrderBy(x => x.Apellidos).ThenBy(x => x.Nombres).ToListAsync();
            return incluirInactivos ? items : items.Where(x => x.Activo).ToList();
        }

        public async Task<List<Participante>> BuscarAsync(string texto)
        {
            var items = await ObtenerTodosAsync(false);
            if (string.IsNullOrWhiteSpace(texto)) return items;
            texto = texto.Trim();
            return items.Where(x => x.Nombres.Contains(texto, StringComparison.OrdinalIgnoreCase)
                                 || x.Apellidos.Contains(texto, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public async Task<Participante?> ObtenerAsync(int id)
        {
            var db = await _database.GetConnectionAsync();
            return await db.FindAsync<Participante>(id);
        }

        public async Task GuardarAsync(Participante participante)
        {
            var db = await _database.GetConnectionAsync();
            if (participante.Id == 0) await db.InsertAsync(participante);
            else await db.UpdateAsync(participante);
        }

        public async Task DesactivarAsync(int id)
        {
            var item = await ObtenerAsync(id);
            if (item is null) return;
            item.Activo = false;
            await GuardarAsync(item);
        }
    }
}
