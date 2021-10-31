using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Console.Data
{
    public class Livro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Reservado { get; set; }
        public int Genero { get; set; }

        public override string ToString()
        {
            return string.Format($"{Nome}");
        }
    }

}
