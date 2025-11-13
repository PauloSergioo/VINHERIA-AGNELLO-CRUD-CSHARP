using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using VinheriaAgnelloCRUD.Factory;
using VinheriaAgnelloCRUD.Models;

namespace VinheriaAgnelloCRUD.DAO
{
    public class VinhoDAO
    {
        public void Inserir(Vinho v)
        {
            using var conn = ConnectionFactory.GetConnection();
            string sql = @"INSERT INTO TB_VINHO 
            (ID_VINHO, NOME, MARCA, PAIS_ORIGEM, TIPO_UVA, TEMPO_ENVELHECIMENTO, TIPO, DESCRICAO, PERFIL_SABOR, OCASIAO, HARMONIZACAO, PRECO, URL_IMAGEM)
            VALUES (vinho_seq.NEXTVAL, :nome, :marca, :pais, :tipouva, :tempo, :tipo, :descricao, :perfil, :ocasiao, :harmonizacao, :preco, :url)";
            using var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("nome", v.Nome ?? ""));
            cmd.Parameters.Add(new OracleParameter("marca", v.Marca ?? ""));
            cmd.Parameters.Add(new OracleParameter("pais", v.PaisOrigem ?? ""));
            cmd.Parameters.Add(new OracleParameter("tipouva", v.TipoUva ?? ""));
            cmd.Parameters.Add(new OracleParameter("tempo", v.TempoEnvelhecimento ?? ""));
            cmd.Parameters.Add(new OracleParameter("tipo", v.Tipo ?? ""));
            cmd.Parameters.Add(new OracleParameter("descricao", v.Descricao ?? ""));
            cmd.Parameters.Add(new OracleParameter("perfil", v.PerfilSabor ?? ""));
            cmd.Parameters.Add(new OracleParameter("ocasiao", v.Ocasiao ?? ""));
            cmd.Parameters.Add(new OracleParameter("harmonizacao", v.Harmonizacao ?? ""));
            cmd.Parameters.Add(new OracleParameter("preco", v.Preco));
            cmd.Parameters.Add(new OracleParameter("url", v.UrlImagem ?? ""));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Vinho inserido com sucesso!");
        }

        public List<Vinho> Listar()
        {
            var lista = new List<Vinho>();
            using var conn = ConnectionFactory.GetConnection();
            string sql = "SELECT ID_VINHO, NOME, MARCA, PAIS_ORIGEM, TIPO_UVA, PRECO FROM TB_VINHO ORDER BY ID_VINHO";
            using var cmd = new OracleCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Vinho
                {
                    IdVinho = Convert.ToInt32(reader["ID_VINHO"]),
                    Nome = reader["NOME"].ToString(),
                    Marca = reader["MARCA"].ToString(),
                    PaisOrigem = reader["PAIS_ORIGEM"].ToString(),
                    TipoUva = reader["TIPO_UVA"].ToString(),
                    Preco = Convert.ToDouble(reader["PRECO"])
                });
            }

            return lista;
        }

        public Vinho GetById(int id)
        {
            using var conn = ConnectionFactory.GetConnection();
            string sql = "SELECT * FROM TB_VINHO WHERE ID_VINHO = :id";
            using var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("id", id));
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Vinho
                {
                    IdVinho = Convert.ToInt32(reader["ID_VINHO"]),
                    Nome = reader["NOME"].ToString(),
                    Marca = reader["MARCA"].ToString(),
                    PaisOrigem = reader["PAIS_ORIGEM"].ToString(),
                    TipoUva = reader["TIPO_UVA"].ToString(),
                    TempoEnvelhecimento = reader["TEMPO_ENVELHECIMENTO"]?.ToString(),
                    Tipo = reader["TIPO"]?.ToString(),
                    Descricao = reader["DESCRICAO"]?.ToString(),
                    PerfilSabor = reader["PERFIL_SABOR"]?.ToString(),
                    Ocasiao = reader["OCASIAO"]?.ToString(),
                    Harmonizacao = reader["HARMONIZACAO"]?.ToString(),
                    Preco = reader["PRECO"] == DBNull.Value ? 0.0 : Convert.ToDouble(reader["PRECO"]),
                    UrlImagem = reader["URL_IMAGEM"]?.ToString()
                };
            }
            return null;
        }

        public void Atualizar(Vinho v)
        {
            using var conn = ConnectionFactory.GetConnection();
            string sql = @"UPDATE TB_VINHO 
            SET NOME = :nome, MARCA = :marca, PAIS_ORIGEM = :pais, TIPO_UVA = :tipouva,
            TEMPO_ENVELHECIMENTO = :tempo, TIPO = :tipo, DESCRICAO = :descricao, PERFIL_SABOR = :perfil, 
            OCASIAO = :ocasiao, HARMONIZACAO = :harmonizacao, PRECO = :preco, URL_IMAGEM = :url
            WHERE ID_VINHO = :id";
            using var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("nome", v.Nome ?? ""));
            cmd.Parameters.Add(new OracleParameter("marca", v.Marca ?? ""));
            cmd.Parameters.Add(new OracleParameter("pais", v.PaisOrigem ?? ""));
            cmd.Parameters.Add(new OracleParameter("tipouva", v.TipoUva ?? ""));
            cmd.Parameters.Add(new OracleParameter("tempo", v.TempoEnvelhecimento ?? ""));
            cmd.Parameters.Add(new OracleParameter("tipo", v.Tipo ?? ""));
            cmd.Parameters.Add(new OracleParameter("descricao", v.Descricao ?? ""));
            cmd.Parameters.Add(new OracleParameter("perfil", v.PerfilSabor ?? ""));
            cmd.Parameters.Add(new OracleParameter("ocasiao", v.Ocasiao ?? ""));
            cmd.Parameters.Add(new OracleParameter("harmonizacao", v.Harmonizacao ?? ""));
            cmd.Parameters.Add(new OracleParameter("preco", v.Preco));
            cmd.Parameters.Add(new OracleParameter("url", v.UrlImagem ?? ""));
            cmd.Parameters.Add(new OracleParameter("id", v.IdVinho));
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Vinho atualizado com sucesso!" : "Vinho não encontrado.");
        }

        public void Deletar(int id)
        {
            using var conn = ConnectionFactory.GetConnection();
            string sql = "DELETE FROM TB_VINHO WHERE ID_VINHO = :id";
            using var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("id", id));
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Vinho deletado!" : "Nada foi deletado.");
        }
    }
}
