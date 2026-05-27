using SQLite;

namespace AlbumdaCopa.Models
{
    [Table("Figurinha")]
    public class Figurinha
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string NomeJogador { get; set; } = string.Empty;

        [NotNull]
        public string Selecao { get; set; } = string.Empty;

        [NotNull]
        public string Tipo { get; set; } = "Comum"; // "Comum" ou "Especial"

        public bool Obtido { get; set; }

        public bool Desejado { get; set; }

        // Mapeia o nome do arquivo da imagem ou o caminho local em disco
        public string FotoPath { get; set; } = string.Empty;

        // Indica se a figurinha já foi colada no álbum físico
        public bool NoAlbum { get; set; }

        // Quantidade total de cópias adquiridas (para controle de repetidas)
        public int Quantidade { get; set; } = 1;

        [Ignore]
        public bool TemRepetidas => Quantidade > 1;
    }
}
