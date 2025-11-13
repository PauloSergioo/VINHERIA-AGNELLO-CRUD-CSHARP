namespace VinheriaAgnelloCRUD.Models
{
    public class Vinho
    {
        public int IdVinho { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string PaisOrigem { get; set; }
        public string TipoUva { get; set; }
        public string TempoEnvelhecimento { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public string PerfilSabor { get; set; }
        public string Ocasiao { get; set; }
        public string Harmonizacao { get; set; }
        public double Preco { get; set; }
        public string UrlImagem { get; set; }
    }
}
