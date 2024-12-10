# GoldenRaspberryAwardsAPI
Golden Raspberry Awards API
Descrição
Esta API RESTful processa uma lista de filmes e permite consultar os produtores com:

O maior intervalo entre dois prêmios consecutivos na categoria "Pior Filme" do Golden Raspberry Awards.
O menor intervalo entre dois prêmios consecutivos.

Pré-requisitos
Antes de começar, certifique-se de que você tem os seguintes softwares instalados em sua máquina:

.NET 6 ou superior
Git
Um editor de código, como Visual Studio ou Visual Studio Code
Configuração do Projeto
1. Clonar o Repositório
Clone o repositório para sua máquina local:

bash

git clone https://github.com/renatomdemello/GoldenRaspberryAwardsAPI.git
cd seu-repositorio
2. Estrutura do Arquivo movielist.csv
Certifique-se de que o arquivo movielist.csv está na pasta DataBase dentro do diretório do projeto. Caso não exista, use o formato abaixo para criar o arquivo:

csv

year;title;studios;producers;winner
1980;Can't Stop the Music;Associated Film Distribution;Allan Carr;yes
1981;Howard the Duck;Universal Pictures;Gloria Katz;no
3. Rodar o Projeto
Via CLI (Command Line Interface)
No diretório raiz do projeto, execute:

bash

dotnet run
Via Visual Studio
Abra o projeto no Visual Studio.
Selecione o perfil de inicialização (ex.: GoldenRaspberryAwardsAPI).
Pressione F5 para executar o projeto.
4. Testar a API
Após executar o projeto, acesse a interface Swagger para testar os endpoints da API. Por padrão, estará disponível em:

https://localhost:7090/swagger/index.html
Endpoints Disponíveis
GET /awards/intervals
Retorna os produtores com:

O menor intervalo entre prêmios consecutivos.
O maior intervalo entre prêmios consecutivos.
Exemplo de Resposta
json

{
  "min": [
    {
      "producer": "Producer 1",
      "interval": 1,
      "previousWin": 2008,
      "followingWin": 2009
    }
  ],
  "max": [
    {
      "producer": "Producer 2",
      "interval": 10,
      "previousWin": 1990,
      "followingWin": 2000
    }
  ]
}
Testes de Integração
1. Configuração dos Testes
Certifique-se de que o arquivo movielist.csv está presente na pasta DataBase antes de rodar os testes.

2. Executar os Testes
Para rodar os testes de integração, execute o seguinte comando na CLI:

bash

dotnet test
Resultados Esperados
Os testes devem validar se os dados da API estão consistentes com os dados fornecidos no arquivo CSV.
Caso algum teste falhe, verifique os logs de erro detalhados exibidos no terminal.
Estrutura do Projeto
bash

GoldenRaspberryAwardsAPI/
│
├── Controllers/           # Contém os controladores da API
├── Data/                  # Configuração do banco de dados em memória (H2/SQLite)
├── Models/                # Modelos de dados (Movies, Producers, etc.)
├── Services/              # Serviços para manipulação e cálculo de dados
├── Tests/                 # Testes de integração
├── DataBase/              # Contém o arquivo movielist.csv
├── Program.cs             # Ponto de entrada da aplicação
└── README.md              # Documentação do projeto

Contribuindo
Faça um fork do projeto.
Crie uma branch para sua feature/bugfix: git checkout -b minha-feature.
Commit suas mudanças: git commit -m 'Adicionei uma nova feature'.
Suba suas mudanças: git push origin minha-feature.
Abra um Pull Request.
Licença
Este projeto está licenciado sob a licença MIT. Consulte o arquivo LICENSE para mais informações.

Dúvidas?
Para mais informações ou problemas, entre em contato com: renatomdemello@gmail.com
