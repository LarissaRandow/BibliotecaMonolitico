using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Console.Data
{
    public class  Reserva
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Cpf { get; set; }
        public DateTime Data { get; set; }
        public int Livro { get; set; }
    }
}
