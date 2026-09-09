using AsisGru.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsisGru.Data
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection? _connection;
        private readonly SemaphoreSlim _lock = new(1, 1);

        private string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, "asisgru.db3");

        public async Task<SQLiteAsyncConnection> GetConnectionAsync()
        {
            if (_connection is not null) return _connection;

            await _lock.WaitAsync();
            try
            {
                if (_connection is null)
                {
                    _connection = new SQLiteAsyncConnection(DatabasePath);
                    await InitializeAsync(_connection);
                }
                return _connection;
            }
            finally
            {
                _lock.Release();
            }
        }

        private static async Task InitializeAsync(SQLiteAsyncConnection db)
        {
            await db.CreateTableAsync<Grupo>();
            await db.CreateTableAsync<Participante>();
            await db.CreateTableAsync<CategoriaActividad>();
            await db.CreateTableAsync<Actividad>();
            await db.CreateTableAsync<Asistencia>();

            await db.ExecuteAsync(
                "CREATE UNIQUE INDEX IF NOT EXISTS UX_Asistencia_Participante_Actividad " +
                "ON Asistencia(ParticipanteId, ActividadId);");

            await db.ExecuteAsync(
                "CREATE INDEX IF NOT EXISTS IX_Actividad_Fecha ON Actividad(Fecha);");

            await db.ExecuteAsync(
                "CREATE INDEX IF NOT EXISTS IX_Actividad_Categoria ON Actividad(CategoriaId);");

            await db.ExecuteAsync(
                "CREATE INDEX IF NOT EXISTS IX_Asistencia_Actividad ON Asistencia(ActividadId);");
        }
    }
}
