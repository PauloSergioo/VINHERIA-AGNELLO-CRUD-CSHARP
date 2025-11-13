using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using VinheriaAgnelloCRUD.Factory;
using VinheriaAgnelloCRUD.Models;

namespace VinheriaAgnelloCRUD.DAO
{
    public class CompraDAO
    {
        public void InserirCompra(int usuarioId, int vinhoId, int quantidade)
        {
            using var conn = ConnectionFactory.GetConnection();
            using var tx = conn.BeginTransaction();

            try
            {
                // Buscar preço do vinho
                double preco;
                using (var cmdPreco = new OracleCommand("SELECT PRECO FROM TB_VINHO WHERE ID_VINHO = :vinhoId", conn))
                {
                    cmdPreco.Transaction = tx;
                    cmdPreco.Parameters.Add(new OracleParameter("vinhoId", vinhoId));
                    using var r = cmdPreco.ExecuteReader();
                    if (!r.Read())
                        throw new Exception("Vinho não encontrado.");

                    preco = Convert.ToDouble(r["PRECO"]);
                }

                double total = preco * quantidade;

                // Inserir compra
                string sql = @"INSERT INTO TB_COMPRA 
                               (ID_COMPRA, DATA, QUANTIDADE, TOTAL, ID_USUARIO, ID_VINHO)
                               VALUES (compra_seq.NEXTVAL, :dataCompra, :quantidade, :totalCompra, :usuarioId, :vinhoId)";
                using (var cmdIns = new OracleCommand(sql, conn))
                {
                    cmdIns.Transaction = tx;
                    cmdIns.Parameters.Add(new OracleParameter("dataCompra", DateTime.Now));
                    cmdIns.Parameters.Add(new OracleParameter("quantidade", quantidade));
                    cmdIns.Parameters.Add(new OracleParameter("totalCompra", total));
                    cmdIns.Parameters.Add(new OracleParameter("usuarioId", usuarioId));
                    cmdIns.Parameters.Add(new OracleParameter("vinhoId", vinhoId));
                    cmdIns.ExecuteNonQuery();
                }

                tx.Commit();
                Console.WriteLine($"✅ Compra registrada com sucesso! Total: R${total:0.00}");
            }
            catch (Exception ex)
            {
                try { tx.Rollback(); } catch { }
                Console.WriteLine("❌ Erro ao registrar compra: " + ex.Message);
            }
        }

        public List<Compra> Listar()
        {
            var lista = new List<Compra>();
            using var conn = ConnectionFactory.GetConnection();

            string sql = @"SELECT ID_COMPRA, DATA, QUANTIDADE, TOTAL, ID_USUARIO, ID_VINHO 
                           FROM TB_COMPRA ORDER BY ID_COMPRA";

            using var cmd = new OracleCommand(sql, conn);
            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                lista.Add(new Compra
                {
                    IdCompra = Convert.ToInt32(r["ID_COMPRA"]),
                    Data = Convert.ToDateTime(r["DATA"]),
                    Quantidade = Convert.ToInt32(r["QUANTIDADE"]),
                    Total = Convert.ToDouble(r["TOTAL"]),
                    UsuarioId = Convert.ToInt32(r["ID_USUARIO"]),
                    VinhoId = Convert.ToInt32(r["ID_VINHO"])
                });
            }

            return lista;
        }
    }
}
