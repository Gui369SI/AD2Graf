using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD2Graf.Models
{
    public class Estoque
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Tipo de Insumo")]
        public int InsumoId { get; set; }

        [ForeignKey("InsumoId")]
        public Insumo? Insumo { get; set; }

        [Required]
        [Display(Name = "Quantidade em Estoque")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade não pode ser negativa.")]
        public int QuantidadeEstoque { get; set; }

        [Required(ErrorMessage = "Informe o preço do item")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Preço Unit.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal PrecoUnitario { get; set; }
    }
}