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
    }
}
