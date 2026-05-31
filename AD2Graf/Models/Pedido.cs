using Microsoft.Data.SqlClient.DataClassification;
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

        [Required(ErrorMessage = "Informe o tipo de serviço")]
        [Display(Name = "Serviço")]
        public int ServicoId { get; set; }

        [ForeignKey("ServicoId")]
        public Servico? Servico { get; set; }

        [Required(ErrorMessage = "Informe a quantidade de itens")]
        [Range(1, 100000, ErrorMessage = "A quantidade deve ser entre 1 e 100.000")]
        [Display(Name = "Quantidade")]
        public int Quantidade { get; set; }

        public StatusPedido Status { get; set; } = StatusPedido.Pendente;

        [Required(ErrorMessage = "Informe a data de criação do pedido")]
        public DateTime DataCriacao { get; set; } = DateTime.Now;
    }

    public enum StatusPedido
    {
        Pendente,
        EmAndamento,
        Concluido,
        Cancelado
    }
}