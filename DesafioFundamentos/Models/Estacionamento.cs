using Npgsql;
using System.Threading;


namespace DesafioFundamentos.Models
{

    public class Estacionamento
    {
        // Construtor que será passado como parametro para estabelecer a conexão com o banco de dados. 
        // Host = local onde o banco de dados está localizado (número de IP da máquina ou localhost em caso de endereço local);
        // Username = nome da conta administradora do banco de dados
        // Password = senha da conta administradora do banco de dados
        // Database = nome do banco de dados que será acessado
        public string construtorDeConexao="Host=localhost;Username=postgres;Password=SeuPasswordAqui;Database=NomeDaBaseDeDadosAqui";

        // Este dicionário tem como função armazenar o valor inicial para cada placa, para que se possa calcular o valor final 
        // no momento de retirada do veículo
        Dictionary<string, decimal> valorFinal = new Dictionary<string, decimal>();

        // Esta funcao tem como objetivo testar a conexao com o banco de dados antes mesmo de a aplicação começar, o menu ser exibido, etc.
        public void ConectarAoBancoDeDados()
        {   
            // Testando conexão com o banco de dados
            try
            {
                
            Console.WriteLine("Carregando banco de dados...");
            using var conexao = new NpgsqlConnection(construtorDeConexao);
            conexao.Open();

            Thread.Sleep(5);

            Console.WriteLine("Banco de dados carregado!");
            
            }
            catch (Exception)
            {
                Console.WriteLine("Ocorreu um erro em sua conexão com o banco de dados. Verifique seus parametros e tente novamente.");
            }

        }

        // Função que criará a tabela carros que servirá de apoio às funcionalidades do sistema
        public void CriarTabela()
        {
            // Conectando ao banco de dados
            using var conexao = new NpgsqlConnection(construtorDeConexao);
            conexao.Open();

            // Criando um ponteiro que irá executar os comandos SQL de criação da tabela
            using var comando = new NpgsqlCommand();
            comando.Connection = conexao;

            // Exclui a tabela anterior em caso de nova execução para evitar erros e poluição visual no Banco de Dados
            comando.CommandText = "DROP TABLE IF EXISTS carros";
            comando.ExecuteNonQuery();

            // Comando SQL que cria uma tabela "carros" com as colunas id (chave primária), placa, cor, ano, marca
            comando.CommandText = @"CREATE TABLE carros(id SERIAL PRIMARY KEY,
                                    placa VARCHAR(10), cor VARCHAR(10), ano VARCHAR(10), marca VARCHAR(10))";
            comando.ExecuteNonQuery();
        }

        // Função para adicionar entrada na tabela carros
        public void AdicionarVeiculo()
        {

            decimal precoInicial; // decimal que irá armazenar o preço inicial para determinada placa/carro

            // Conectando ao banco de dados
            using var conexao = new NpgsqlConnection(construtorDeConexao);
            conexao.Open();

            // Criando ponteiro que irá executar o comando SQL de entrada de dados
            using var comando = new NpgsqlCommand();
            comando.Connection = conexao;

            // Coletando os dados que serão armazenados na entrada "carro"
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

            valorFinal.Add(placa, precoInicial); // adicionando o valor inicial no dicionário para utilização na hora de liberar o carro

            // Executando comando SQL INSERT INTO
            comando.CommandText = $"INSERT INTO carros (placa, cor, ano, marca) VALUES ('{placa}', '{cor}', '{ano}', '{marca}')";
            comando.ExecuteNonQuery();
        }

        // Função de remoção do veículo
        public void RemoverVeiculo()
        {
            // Conectando ao Banco de Dados
            using var conexao = new NpgsqlConnection(construtorDeConexao);
            conexao.Open();

            // Criando ponteiro
            using var comando = new NpgsqlCommand();
            comando.Connection = conexao;

            // Obtendo a placa do carro que será liberado
            Console.WriteLine("Digite a placa do veículo que deseja remover: ");
            string placa = Console.ReadLine();

            // Calculando o valor total do serviço
            Console.WriteLine("Digite o valor da hora: ");
            decimal precoPorHora = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Digite a quantidade de horas que o carro ficou estacionado: ");
            decimal horasEstacionado = Convert.ToDecimal(Console.ReadLine());
            
            decimal precoFinal = valorFinal[placa] + precoPorHora * horasEstacionado;
            Console.WriteLine($"O carro foi liberado pelo valor de R$ {precoFinal}"); 

            // Removendo entrada da tabela
            comando.CommandText = $"DELETE FROM carros WHERE placa='{placa}'";
            comando.ExecuteNonQuery();
        }

        // Função que faz a listagem dos carros
        public void ListarVeiculos()
        {
            using var conexao = new NpgsqlConnection(construtorDeConexao);
            conexao.Open();

            // Ponteiro que executará o comando SELECT do SQL
            string comandoSQL = "SELECT * FROM carros";

            // Nesse caso, o objeto conexao que é uma instância do Banco de Dados foi passado como parametro 
            // junto com a variavel que armazena o comando SQL
            using var comando = new NpgsqlCommand(comandoSQL, conexao);

            // Criando objeto leitorDeDados
            using NpgsqlDataReader leitorDeDados = comando.ExecuteReader();

            // Iterando sobre o objeto leitorDeDados até que se esgotem as entradas disponíveis e a condição seja leitorDeDados.Read() = false
            Console.WriteLine("id |   placa   |   cor   |   ano   | marca");
            while (leitorDeDados.Read())
            {
                    Console.WriteLine("{0}     {1}     {2}    {3}    {4}", leitorDeDados[0], leitorDeDados[1], leitorDeDados[2], leitorDeDados[3], leitorDeDados[4]);
            }


        }
    }
}
