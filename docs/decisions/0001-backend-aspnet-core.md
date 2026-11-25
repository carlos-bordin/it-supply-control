# ADR 0001: backend em ASP.NET Core (.NET 10) com EF Core e Minimal APIs

- **Data:** 25/11/2026
- **Situação:** aceita

## Decisão

- API Web em **ASP.NET Core sobre .NET 10** (versão LTS, com suporte longo).
- Acesso a dados com **Entity Framework Core** sobre **SQL Server** (Docker no ambiente local,
  Azure SQL na nuvem). O esquema evolui por *migrations* do EF Core.
- Endpoints no estilo **Minimal APIs**, o padrão do template do .NET 10: cada módulo
  (`Assets`, `SupplierExchanges`, `MasterData`) registra o próprio grupo de rotas.
- Contrato publicado em **OpenAPI**, gerado pelo próprio ASP.NET Core.

## Por quê

- A hospedagem é no Azure, onde .NET e SQL Server têm o melhor suporte nativo: App Service,
  Azure SQL, Managed Identity e Application Insights funcionam juntos com pouca configuração.
- O dono do projeto vem de MySQL: um banco relacional com SQL é território conhecido, e o EF Core
  evita escrever SQL repetitivo sem esconder o modelo relacional.
- Minimal APIs têm menos cerimônia que Controllers (classes, atributos, herança). Para quem está
  aprendendo, cada rota fica visível num arquivo só por módulo.

## Alternativas consideradas

- **Controllers**: mais comuns em tutoriais antigos, porém mais código para o mesmo resultado.
- **Node.js (Express/Nest)**: uma linguagem só no projeto, mas integração com Azure SQL e
  Managed Identity menos direta, e o ecossistema da empresa é Microsoft.
