using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD2Graf.Models
{
    public class Insumo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do insumo")]
        [Display(Name = "Nome do Insumo")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o preço do item")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Preço Unit.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal PrecoUnitario { get; set; }

        [Required(ErrorMessage = "Informe a data de cadastro")]
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Display(Name = "Ativo")]
        public bool Ativo { get; set; } = true;
    }
}