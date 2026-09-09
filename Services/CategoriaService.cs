using AsisGru.Data;
using AsisGru.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Services
{
    public class CategoriaService
    {
        private readonly DatabaseService _database;
        public CategoriaService(DatabaseService database) => _database = database;

        public async Task<List<CategoriaActividad>> ObtenerTodosAsync()
        {
            var db = await _database.GetConnectionAsync();
            return await db.Table<CategoriaActividad>().OrderBy(x => x.Nombre).ToListAsync();
        }

        public async Task GuardarAsync(CategoriaActividad categoria)
        {
            var db = await _database.GetConnectionAsync();
            var existente = await db.Table<CategoriaActividad>()
                .Where(x => x.Id != categoria.Id && x.Nombre == categoria.Nombre)
                .FirstOrDefaultAsync();

            if (existente is not null)
                throw new InvalidOperationException("Ya existe una categoría con ese nombre.");

            if (categoria.Id == 0) await db.InsertAsync(categoria);
            else await db.UpdateAsync(categoria);
        }
    }
}
