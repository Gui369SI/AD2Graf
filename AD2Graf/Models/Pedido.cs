using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD2Graf.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a empresa ou organização")]
        [StringLength(150)]
        public string Empresa { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe a descrição do pedido")]
        [StringLength(200)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [Range(1, 100000, ErrorMessage = "Quantidade deve ser entre 1 e 100.000")]
        public int Quantidade { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, 999999.99)]
        public decimal Preco { get; set; }

        public StatusPedido Status { get; set; } = StatusPedido.Pendente;

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