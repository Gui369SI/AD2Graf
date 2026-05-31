using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AD2Graf.Models
{
    public class Servico
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do serviço")]
        [Display(Name = "Tipo de Serviço")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Informe o valor do serviço")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Preço do Serviço")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal PrecoServico { get; set; }

        [Required(ErrorMessage = "Informe a data de cadastro")]
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}
