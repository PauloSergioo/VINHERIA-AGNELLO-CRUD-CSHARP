using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using VinheriaAgnelloCRUD.Factory;
using VinheriaAgnelloCRUD.Models;

namespace VinheriaAgnelloCRUD.DAO
{
    public class UsuarioDAO
    {
        public void Inserir(Usuario u)
        {
            using var conn = ConnectionFactory.GetConnection();
            string sql = @"INSERT INTO TB_USUARIO (ID_USUARIO, NOME, EMAIL, SENHA) 
                           VALUES (usuario_seq.NEXTVAL, :nome, :email, :senha)";
            using var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("nome", u.Nome ?? ""));
            cmd.Parameters.Add(new OracleParameter("email", u.Email ?? ""));
            cmd.Parameters.Add(new OracleParameter("senha", u.Senha ?? ""));
            cmd.ExecuteNonQuery();
            Console.WriteLine("Usuário inserido com sucesso!");
        }

        public List<Usuario> Listar()
        {
            var lista = new List<Usuario>();
            using var conn = ConnectionFactory.GetConnection();
            string sql = "SELECT ID_USUARIO, NOME, EMAIL, SENHA FROM TB_USUARIO ORDER BY ID_USUARIO";
            using var cmd = new OracleCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Usuario
                {
                    IdUsuario = Convert.ToInt32(reader["ID_USUARIO"]),
                    Nome = reader["NOME"].ToString(),
                    Email = reader["EMAIL"].ToString(),
                    Senha = reader["SENHA"].ToString()
                });
            }
            return lista;
        }

        public Usuario GetById(int id)
        {
            using var conn = ConnectionFactory.GetConnection();
            string sql = "SELECT * FROM TB_USUARIO WHERE ID_USUARIO = :id";
            using var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("id", id));
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new Usuario
                {
                    IdUsuario = Convert.ToInt32(reader["ID_USUARIO"]),
                    Nome = reader["NOME"].ToString(),
                    Email = reader["EMAIL"].ToString(),
                    Senha = reader["SENHA"].ToString()
                };
            }
            return null;
        }

        public void Atualizar(Usuario u)
        {
            using var conn = ConnectionFactory.GetConnection();
            string sql = @"UPDATE TB_USUARIO SET NOME = :nome, EMAIL = :email, SENHA = :senha 
                           WHERE ID_USUARIO = :id";
            using var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("nome", u.Nome ?? ""));
            cmd.Parameters.Add(new OracleParameter("email", u.Email ?? ""));
            cmd.Parameters.Add(new OracleParameter("senha", u.Senha ?? ""));
            cmd.Parameters.Add(new OracleParameter("id", u.IdUsuario));
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Usuário atualizado!" : "Usuário não encontrado.");
        }

        public void Deletar(int id)
        {
            using var conn = ConnectionFactory.GetConnection();
            string sql = "DELETE FROM TB_USUARIO WHERE ID_USUARIO = :id";
            using var cmd = new OracleCommand(sql, conn);
            cmd.Parameters.Add(new OracleParameter("id", id));
            int rows = cmd.ExecuteNonQuery();
            Console.WriteLine(rows > 0 ? "Usuário deletado!" : "Nada foi deletado.");
        }
    }
}
