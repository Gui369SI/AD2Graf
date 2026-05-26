using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD2Graf.Models
{
    public class Movimentacao
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de insumo")]
        [Display(Name = "Tipo de Insumo")]
        public int InsumoId { get; set; }

        [ForeignKey("InsumoId")]
        public Insumo? Insumo { get; set; }

        [Required(ErrorMessage = "Selecione o tipo de movimentação")]
        [Display(Name = "Entrada / Saída")]
        public TipoMovimentacao TipoMovimentacao { get; set; }

        [Required(ErrorMessage = "Informe a quantidade de itens")]
        [Range(0.01, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        [Display(Name = "Quantidade")]
        public decimal Quantidade { get; set; }

        [Required(ErrorMessage = "Informe a data de movimentação")]
        [Display(Name = "Data da Movimentação")]
        public DateTime DataMovimentacao { get; set; } = DateTime.Now;
    }

    public enum TipoMovimentacao
    {
        [Display(Name = "Entrada")]
        Entrada = 1,

        [Display(Name = "Saída")]
        Saida = 2
    }
}