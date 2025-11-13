using System;

namespace VinheriaAgnelloCRUD.Models
{
    public class Compra
    {
        public int IdCompra { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public double Total { get; set; }

        // FKs
        public int UsuarioId { get; set; }
        public int VinhoId { get; set; }
    }
}
