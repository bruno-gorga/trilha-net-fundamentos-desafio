using Npgsql;
using System.Threading;


namespace DesafioFundamentos.Models
{

    public class Estacionamento
    {
        public string construtorDeConexao="Host=localhost;Username=postgres;Password=admin;Database=sistema_estacionamento";

        Dictionary<string, decimal> valorFinal = new Dictionary<string, decimal>();

        public void ConectarAoBancoDeDados()
        {   
            // Testando conexão com o banco de dados
            Console.WriteLine("Carregando banco de dados...");
            using var conexao = new NpgsqlConnection(construtorDeConexao);
            conexao.Open();

            Thread.Sleep(5);

            Console.WriteLine("Banco de dados carregado!");

        }

        public void CriarTabela()
        {
            using var conexao = new NpgsqlConnection(construtorDeConexao);
            conexao.Open();

            using var comando = new NpgsqlCommand();
            comando.Connection = conexao;

            comando.CommandText = "DROP TABLE IF EXISTS carros";
            comando.ExecuteNonQuery();

            comando.CommandText = @"CREATE TABLE carros(id SERIAL PRIMARY KEY,
                                    placa VARCHAR(10), cor VARCHAR(10), ano VARCHAR(10), marca VARCHAR(10))";
            comando.ExecuteNonQuery();
        }
        
        public void AdicionarVeiculo()
        {

            decimal precoInicial;

            using var conexao = new NpgsqlConnection(construtorDeConexao);
            conexao.Open();

            using var comando = new NpgsqlCommand();
            comando.Connection = conexao;

            Console.WriteLine("Digite a placa do carro: ");
            string placa = Console.ReadLine();

            Console.WriteLine("Digite a cor do carro: ");
            string cor = Console.ReadLine();

            Console.WriteLine("Digite o ano do carro: ");
            string ano = Console.ReadLine();

            Console.WriteLine("Digite a marca do carro: ");
            string marca = Console.ReadLine();

            Console.WriteLine("Digite o preço inicial: ");
            precoInicial = Convert.ToDecimal(Console.ReadLine());

            valorFinal.Add(placa, precoInicial);

            
            comando.CommandText = $"INSERT INTO carros (placa, cor, ano, marca) VALUES ('{placa}', '{cor}', '{ano}', '{marca}')";
            comando.ExecuteNonQuery();
        }

        public void RemoverVeiculo()
        {
            using var conexao = new NpgsqlConnection(construtorDeConexao);
            conexao.Open();

            using var comando = new NpgsqlCommand();
            comando.Connection = conexao;

            Console.WriteLine("Digite a placa do veículo que deseja remover: ");
            string placa = Console.ReadLine();

            Console.WriteLine("Digite o valor da hora: ");
            decimal precoPorHora = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Digite a quantidade de horas que o carro ficou estacionado: ");
            decimal horasEstacionado = Convert.ToDecimal(Console.ReadLine());
            
            decimal precoFinal = valorFinal[placa] + precoPorHora * horasEstacionado;
            Console.WriteLine($"O carro foi liberado pelo valor de R$ {precoFinal}"); 

            comando.CommandText = $"DELETE FROM carros WHERE placa='{placa}'";
            comando.ExecuteNonQuery();
        }

        public void ListarVeiculos()
        {
            using var conexao = new NpgsqlConnection(construtorDeConexao);
            conexao.Open();

            string comandoSQL = "SELECT * FROM carros";
            using var comando = new NpgsqlCommand(comandoSQL, conexao);
            
            using NpgsqlDataReader leitorDeDados = comando.ExecuteReader();


            Console.WriteLine("id |   placa   |   cor   |   ano   | marca");
            while (leitorDeDados.Read())
            {
                    Console.WriteLine("{0}     {1}     {2}    {3}    {4}", leitorDeDados[0], leitorDeDados[1], leitorDeDados[2], leitorDeDados[3], leitorDeDados[4]);
            }


        }
    }
}
