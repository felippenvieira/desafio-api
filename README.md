# DESAFIO - API

Desafio API | .NET/C# | Clecio

## Boas-vindas

Obrigado por visitar meu repositório. Nele eu realizei o Desafio API proposto pelo professor Clecio da Silva, utilizando a linguagem C# e o .NE T5.0.

## Estrutura

O repositório é dividido entre a API e os testes realizados utilizando XUnit. O diretório 'desafio' contém a API e o diretório 'desafio-tests' contém os testes unitários feitos com o pacote Fluent Assertions.

## Instruções de Uso

Para rodas o main program, basta acessar a pasta "desafio", e lá dentro utilizar os comandos "dotnet ef database update" e após "dotnet watch run". Caso contrário, o programa não irá rodas.

Caso os testes não estejam aparecendo, tente acessar a pasta "desafio-tests" para que o editor/IDE consiga localizá-los.

## Composição do Desafio:

- [x] CRUD completo para clientes;
- [x] CRUD completo para médicos veterinários;
- [x] CRUD completo para cachorros (cada animal deverá estar associado a um tutor/cliente);
- [x] Registrar dados do atendimento: veterinário, animal, data, hora, dados do animal no dia, diagnóstico e comentários;
- [x] Autenticação do usuário (básica);
- [x] Data seed (população do banco de dados).

### Exceeds:

- [x] Recuperar dados da raça do cachorro via THE DOG API;
- [x] Fornecer serviços da THE DOG API para o cliente;
- [x] Histórico dos atendimentos, permitindo que o cliente acesse apenas os atendimentos de seus cachorros, e os médicos possam acessar todos os atendimentos;
- [x] Swagger;
- [x] TDD.

## Observações

1. Na controller de clientes foi criada uma rota para que sejam acessados apenas os atendimentos do cliente.
2. Na controller de médicos foi criada uma rota para que sejam acessados todos os atendimentos realizados.
3. Autenticação via JWT Bearer: acessar a rota de login com o JSON abaixo:
{
  "email": "admin@gft.com",
  "senha": "admin"
}
Após isso, copiar o token gerado e utilizar o botão "Authorize" presente no canto superior direito da página e seguir os passos lá descritos.
4. É possível criar sua própria conta utilizando a rota /Api/Account/create (a primeira rota listada no swagger).
5. Ao registrar um novo "DadosCachorro", a THE DOG API é consumida para obter os dados daquela raça específica, comparando os dados do cachorro em atendimento com os dados da raça do cachorro.

## Espero que goste!
## Dúvidas? Entre em contato comigo: felippe.vieira@gft.com