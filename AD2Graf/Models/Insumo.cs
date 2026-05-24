using System.ComponentModel.DataAnnotations;

namespace AD2Graf.Models
{
    public class Insumo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome do Insumo")]
        public string Nome { get; set; } = string.Empty;

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;
    }
}