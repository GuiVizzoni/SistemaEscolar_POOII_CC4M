using System;
using MySql.Data.MySqlClient;

namespace SistemaEscolar
{
    public class DatabaseConnection
    {
        // String de conexão
        private static readonly string connectionString = BuildConnectionString();

        // Instância única da classe
        private static DatabaseConnection _instance;

        // Objeto para lock em multithread
        private static readonly object lockObj = new object();

        // Conexão com o banco de dados
        private static MySqlConnection _connection;

        // Construtor privado
        private DatabaseConnection()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("A string de conexão está vazia ou inválida.");
            }

            _connection = new MySqlConnection(connectionString);
        }

        // Método para construir a string de conexão
        private static string BuildConnectionString()
        {
            string host = Environment.GetEnvironmentVariable("MYSQL_HOST") ?? throw new ArgumentException("MYSQL_HOST não configurado");
            string port = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306"; // Porta padrão
            string database = Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? throw new ArgumentException("MYSQL_DATABASE não configurado");
            string user = Environment.GetEnvironmentVariable("MYSQL_USER") ?? throw new ArgumentException("MYSQL_USER não configurado");
            string password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? throw new ArgumentException("MYSQL_PASSWORD não configurado");

            return $"Server={host};Port={port};Database={database};Uid={user};Pwd={password}";
        }

        // Método thread-safe para obter a instância única
        public static DatabaseConnection GetInstance()
        {
            if (_instance == null)
            {
                lock (lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new DatabaseConnection();
                    }
                }
            }
            return _instance;
        }

        // Método para obter a conexão com o banco de dados
        public MySqlConnection GetConnection()
        {
            try
            {
                if (_connection.State == System.Data.ConnectionState.Closed)
                {
                    _connection.Open();
                }
                return _connection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
                throw;
            }
        }
    }
}
