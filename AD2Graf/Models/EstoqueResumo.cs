namespace AD2Graf.Models
{
    public class EstoqueResumo
    {
        public int InsumoId { get; set; }
        public string NomeInsumo { get; set; } = string.Empty;
        public decimal TotalEntrada { get; set; }
        public decimal TotalSaida { get; set; }
        public decimal Saldo { get; set; }
    }
}