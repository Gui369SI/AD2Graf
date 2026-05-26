using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD2Graf.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a empresa ou organização")]
        [StringLength(150, ErrorMessage = "O nome não pode exceder 150 caracteres")]
        [Display(Name = "Empresa / Organização")]
        public string Empresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a descrição do pedido")]
        [StringLength(200, ErrorMessage= "A descrição não pode exceder 200 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a quantidade de itens")]
        [Range(1, 100000, ErrorMessage = "A quantidade deve ser entre 1 e 100.000")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "Informe o preço do item")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, 999999.99, ErrorMessage = "O valor deve ser maior que zero")]
        [Display(Name = "Preço Unit.")]
        public decimal Preco { get; set; }

        public StatusPedido Status { get; set; } = StatusPedido.Pendente;

        [Required(ErrorMessage = "Informe a data de criação do pedido")]
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }

    public enum StatusPedido
    {
        Pendente,
        [Display(Name = "Em Produção")]
        EmProducao,
        Concluido,
        Cancelado
    }
}