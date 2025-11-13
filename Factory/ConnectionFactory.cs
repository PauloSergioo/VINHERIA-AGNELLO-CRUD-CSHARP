using System;
using Oracle.ManagedDataAccess.Client;

namespace VinheriaAgnelloCRUD.Factory
{
    public static class ConnectionFactory
    {
    
        private static readonly string connectionString =
            "User Id=rm553012;Password=110304;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)))";

        public static OracleConnection GetConnection()
        {
            try
            {
                var conn = new OracleConnection(connectionString);
                conn.Open();
                Console.WriteLine("Conexão com o banco de dados estabelecida com sucesso!");
                return conn;
            }
            catch (OracleException ex)
            {
                Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
                throw;
            }
        }
    }
}
