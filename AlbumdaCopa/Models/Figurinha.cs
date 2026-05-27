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
        public string Tipo { get; set; } = "Comum"; // "comum" ou "especial"

        public bool Obtido { get; set; }

        public bool Desejado { get; set; }

        // salva o nome do arquivo da foto ou o caminho no celular
        public string FotoPath { get; set; } = string.Empty;

        // diz se a figurinha ja foi colada no album fisico
        public bool NoAlbum { get; set; }

        // total de copias para controle de repetidas
        public int Quantidade { get; set; } = 1;

        [Ignore]
        public bool TemRepetidas => Quantidade > 1;
    }
}
