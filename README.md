# DIO - Trilha .NET - Fundamentos
www.dio.me

## Desafio de projeto
Para este desafio, você precisará usar seus conhecimentos adquiridos no módulo de fundamentos, da trilha .NET da DIO.

## Contexto
Você foi contratado para construir um sistema para um estacionamento, que será usado para gerenciar os veículos estacionados e realizar suas operações, como por exemplo adicionar um veículo, remover um veículo (e exibir o valor cobrado durante o período) e listar os veículos.

## Proposta
Você precisará construir uma classe chamada "Estacionamento", conforme o diagrama abaixo:
![Diagrama de classe estacionamento](diagrama_classe_estacionamento.png)

A classe contém três variáveis, sendo:

**precoInicial**: Tipo decimal. É o preço cobrado para deixar seu veículo estacionado.

**precoPorHora**: Tipo decimal. É o preço por hora que o veículo permanecer estacionado.

**veiculos**: É uma lista de string, representando uma coleção de veículos estacionados. Contém apenas a placa do veículo.

A classe contém três métodos, sendo:

**AdicionarVeiculo**: Método responsável por receber uma placa digitada pelo usuário e guardar na variável **veiculos**.

**RemoverVeiculo**: Método responsável por verificar se um determinado veículo está estacionado, e caso positivo, irá pedir a quantidade de horas que ele permaneceu no estacionamento. Após isso, realiza o seguinte cálculo: **precoInicial** * **precoPorHora**, exibindo para o usuário.

**ListarVeiculos**: Lista todos os veículos presentes atualmente no estacionamento. Caso não haja nenhum, exibir a mensagem "Não há veículos estacionados".

Por último, deverá ser feito um menu interativo com as seguintes ações implementadas:
1. Cadastrar veículo
2. Remover veículo
3. Listar veículos
4. Encerrar


## Solução
O código está pela metade, e você deverá dar continuidade obedecendo as regras descritas acima, para que no final, tenhamos um programa funcional. Procure pela palavra comentada "TODO" no código, em seguida, implemente conforme as regras acima.

# Minha Solução
Para essa solução, utilizei as seguintes tecnologias:
	
		PostgreSQL v. 16.1
		VSCode  v. 1.84

## 1. Instalando e configurando o PostgreSQL
O programa faz uso de um banco de dados criado no SGBD PostgreSQL. Primeiramente, é necessário instalar a ferramenta e configurar a conta de administrador que será utilizada na execução do sistema de gerenciamento de estacionamento.

### 1.1 Download

* Entre no [site oficial do Postgres, na seção de downloads.](https://www.postgresql.org/download/)
* Selecione seu sistema operacional
* Clique na distibuição adequada ao seu sistema
![Tela com as distribuições do PostgreSQL. No meu caso estou utilizando uma versão para Windows.](https://imgur.com/Uu0wYMX.png)
* Faça o download e aguarde.
### 1.2 Instalação
* Execute o instalador do PostgreSQL
  
![](https://imgur.com/s5wCxPp.png)
* Aguarde até o instalador executar:

![](https://imgur.com/slpyZ3v.png)

* Durante o processo de instalação, será solicitado ao usuário a criação de uma senha para a conta que irá gerenciar o banco de dados. **Crie a senha e guarde-a, pois será necessária para fazer o login no terminal do Postgre e para configurar nosso aplicativo de gerenciamento posteriormente.**
  
![](https://imgur.com/gmo7mIP.png)

* Em seguida, o instalador pedirá ao usuário para confirmar a porta na qual o banco de dados poderá ser acessado. **Guarde esse número, ele é importante para fazer o login no terminal e caso esteja incorreto, poderão ocorrer erros durante a execução da aplicação.** No nosso exemplo deixaremos a porta padrão 5432.

![](https://imgur.com/Ph2IZ6A.png)

* Confirme as pastas nas quais o instalador irá instalar o PostgreSQL e seus dados. No nosso exemplo vamos deixar os caminhos padrão.
  

![](https://imgur.com/lhTtzSR.png)
![](https://imgur.com/2JW2c8I.png)


* O locale seria o padrão de data e hora utilizado em cada região, novamente pode-se deixar o padrão selecionado.

  
 ![](https://imgur.com/cGKULvu.png)
 
* Após a instalação, vá até a barra de início, na seção de aplicativos, na pasta PostgreSQL 16, e selecione o SQL Shell.
  
 ![](https://imgur.com/Y5svY9H.png)

 
* Ao abrir o terminal, **verifique e guarde as informações que aparecerão na tela inicial de login, pois iremos atribuí-las no código do nosso aplicativo para conectá-lo ao banco de dados.**
  
![Guarde essas informações: Server[localhost], Database[postgres], Port[5432], Username[postgres], e a Senha. Serão necessárias mais à frente.](https://imgur.com/biVHhyK.png)


* A senha deve ser aquela criada no momento da instalação. As informações entre colchetes são aquelas que estão por padrão (ex. o server padrão é o localhost, que é o servidor da máquina local, mas caso a conexão seja feita com um banco de dados localizado em outro servidor, seria necessário inserir o endereço IP do servidor, o banco de dados (database) padrão é o postgres, mas podem-se criar outros posteriormente, etc.). No nosso exemplo iremos deixar as informações padrão como referência.
  
* Ao inserir as informações corretamente, o login será feito e estaremos dentro do terminal.
  
![Terminal do PostgreSQL](https://imgur.com/glfcuqj.png)

* Estamos prontos para importar e executar o nosso código.
  
## 2. Importando nosso código do repositório
### 2.1 Criando a pasta de armazenamento do projeto
* Entre em sua pasta local na qual deseja importar os arquivos deste repositório. Para o nosso exemplo criei uma pasta chamada novo_projeto_estacionamento.
  
 ![](https://imgur.com/ta2Mrsn.png)
 
 * Na pasta, clique com o botão direito do mouse e abra do git bash, o terminal Git. Caso você não tenha o Git instalado em sua máquina, [consulte a página oficial do Git e as instruções de instalação e configuração.](https://git-scm.com/download/win)
   
 ![](https://imgur.com/gx8C70s.png)

### 2.2 Importando o  código para a máquina local
* Após abrir o terminal Git e configurá-lo de acordo, vá até o botão Code no topo desta página e selecione o link que irá utilizar para importar o código para a pasta recém criada. Temos a opção HTTPS e a SSH, sendo que está última requer uma configuração específica que pode ser encontrada no próprio site do GitHub. Para o exemplo iremos usar uma chave HTTPS. **Copie** o link fornecido pelo botão Code.

* Com o link copiado, abra o terminal git e digite o comando "git clone" e em seguida aperte as teclas Ctrl + Shift + Insert simultaneamente para colar o link copiado.![](https://imgur.com/z2OIJBL.png) 

* Aperte enter e aguarde até o Git fazer a clonagem do repositório no ambiente local.
  
![Após o Git completar a clonagem, você terá um repositório completo em sua máquina local.](https://imgur.com/x6lfs4F.png)

* Vá até a pasta "DesafioFundamentos" e clique com o botão direito novamente. Selecione a opção "Abrir com Code", [caso não tenha o Visual Code instalado, pode-se consultar a página oficial e instalar a aplicação por lá.](https://code.visualstudio.com/)
  
* O Visual Studio irá abrir os arquivos e inicializar o editor de código já com tudo pronto para executarmos nosso projeto.
## 3. Configurando a aplicação para conetar-se ao PostgreSQL
### 3.1 Inserindo os parâmetros na função construtorDeConexão
* Com o VS Code aberto, vá no menu à esquerda, clique na pasta "Models" e em seguida clique no arquivo "Estacionamento.cs". Ali, procure no código a seguinte linha:     
* public string construtorDeConexao="Host=Seu_Host_Aqui;" conforme abaixo:
  
![Linha de código com o construtor de conexão](https://imgur.com/VreUa0h.png)

* **Altere** os parâmetros com os dados guardados anteriormente (Server[localhost], Database[postgres], Port[5432], Username[postgres], e a Senha criada no momento da instalação). No nosso exemplo, vamos utilizar os parâmetros padrão.
  
![Lembrando que a SENHA é aquela que foi criada no momento da instalação do PostgreSQL.](https://imgur.com/rIHn6xT.png)

* Feito isso, estamos prontos para executar e utilizar nosso código.

## 4. Execução e funcionalidades
### 4.1 Executando o código
* No VS Code, abra o terminal apertando os botões Ctrl + Aspas (geralmente este botão fica em cima do Tab). O terminal será aberto. 
* Caso não tenha o pacote de desenvolvimento do C#, pode instalá-lo na aba "extensões" ao lado esquerdo do menu do VS Code.
* Se o seu VS Code não compila automaticamente, aperte F5 para compilar o código e, em seguida, no terminal, digite o comando dotnet run.
* O programa irá exibir uma mensagem "Carregando banco de dados.." e, caso as instruções anteriores tenham sido feitas corretamente, exibirá em seguida uma mensagem "Banco de dados carregado!" e exibirá o menu de uso do programa.
  
![Menu do usuário](https://imgur.com/LuZGgKc.png)

* Estamos prontos para testar suas funcionalidades.

### 4.2 Funcionalidades
#### 4.2.1 Adicionar Veículo

Primeiramente, para adicionar um veículo, basta digitar a opção 1 no menu. 
O terminal então pedirá uma série de informações relacionadas ao veículo: Placa, cor, ano, marca e, também, qual o valor da hora inicial do estacionamento. Após isso, o veículo será inserido no banco de dados. 

![Inserindo veículo de placa ABC-0001](https://imgur.com/BqHXzPJ.png)

### 4.2.2 Listar Veículo
Essa funcionalidade exibe no terminal os veículos que foram cadastrados no sistema. Para acessá-la, basta digitar a opção 3. Aqui, nós podemos consultar o terminal do PostgreSQL com um comando SELECT e veremos que, concomitantemente ao aplicativo em C#, os dados também foram armazenados no banco de dados Postgre, em uma tabela chamada "carros".
![Terminal do PostgreSQL ao lado do terminal VS Code. Vemos que os dados inseridos pelo aplicativo são gravados no banco de dados SQL.](https://imgur.com/UQ3bNXi.png)

### 4.2.3 Remover Veículo
Para remover um veículo, insere-se o número 2. Aqui, o terminal pede o número da placa do veículo a ser removido, junto com o preço por hora do estacionamento e a quantidade de horas que o veículo ficou estacionado. Ao final, o veículo é liberado (removido do banco) e o valor total é exibido em tela.

![](https://imgur.com/RpZ50Qq.png)

Pode-se ver também que o registro é apagado inclusive no banco de dados, no PostgreSQL. 

![Registro do carro com a placa ABC-0001 foi apagado da tabela carros no banco de dados.](https://imgur.com/u2EFp3k.png)

### 4.2.4 Encerrar programa
Para encerrar o programa, digita-se a opção 4 e em seguida uma tecla qualquer.

## 5. Considerações finais
Meu propósito com essa solução foi tornar a aplicação o mais próxima possível de uma aplicação utilizada em um contexto real. Sabemos que todas as aplicações de gerenciamento de negócio, via de regra, utilizam uma modelagem de banco de dados implementada em um SGBD (Sistema Gerenciador de Banco de Dados) qualquer. O PostgreSQL é um dos mais utilizados no mercado, ainda que, em aplicações em C#, costuma-se utilizar o SQL Server por este ser igualmente propriedade da Microsoft. 

