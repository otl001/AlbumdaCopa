using SQLite;
using System.IO;

namespace AlbumdaCopa.Services
{
    // classe para gerenciar a conexao com o banco de dados
    public class DataBaseService
    {
        // metodo que retorna a conexao com o bd
        public SQLiteConnection GetConnection()
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "AlbumCopa2026.db3");
            return new SQLiteConnection(dbPath);
        }
    }
}
