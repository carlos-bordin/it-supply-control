# Stack de tecnologias

Visão geral do que o projeto usa e por quê, em uma página. As escolhas difíceis de reverter têm
ADR próprio em [decisions/](decisions/); as bibliotecas de apoio estão só aqui, porque trocar uma
delas é barato.

## Arquitetura em uma frase

Um **App Service** no Azure roda a **API .NET**, que também entrega o **React** compilado. A API
grava no **Azure SQL** e, a cada mudança, regrava a **planilha de consulta** no **SharePoint** da
equipe. O login é com a conta corporativa (**Entra ID**).

```
Navegador (React + MSAL) ──token──▶ App Service (API .NET + arquivos do React)
                                        │  Managed Identity (sem senha)
                                        ├──▶ Azure SQL
                                        └──▶ Microsoft Graph ──▶ SharePoint: ativos.xlsx
                                                                     ▲
                                        Equipe (Excel na web, F3) ───┘ só leitura
```

## Backend ([ADR 0001](decisions/0001-backend-aspnet-core.md))

| Item | Escolha | Para quê |
|---|---|---|
| Plataforma | .NET 10 (LTS), ASP.NET Core | API Web |
| Estilo da API | Minimal APIs, um grupo de rotas por módulo | Menos cerimônia que Controllers |
| Banco | EF Core + SQL Server | Acesso a dados e *migrations* |
| Contrato | OpenAPI nativo do ASP.NET Core | Descreve os endpoints; gera os tipos do frontend |
| Autenticação | Microsoft.Identity.Web | Valida os tokens do Entra ID |
| Planilha de consulta | ClosedXML | Gera o `ativos.xlsx` |
| SharePoint | Microsoft Graph SDK + Azure.Identity | Grava o arquivo usando a Managed Identity |
| Monitoramento | Azure Monitor OpenTelemetry (Application Insights) | Logs, erros e desempenho |
| Testes | xUnit + Testcontainers (SQL Server em contêiner) | Testes de unidade e de integração com banco real |

## Frontend ([ADR 0002](decisions/0002-frontend-react-typescript.md))

| Item | Escolha | Para quê |
|---|---|---|
| Base | React + TypeScript, Vite | SPA e ferramenta de build |
| Componentes | Fluent UI React v9 | Visual do Microsoft 365, abas, formulários, tabela |
| Navegação | React Router | Pesquisa → ativo → abas |
| Dados da API | TanStack Query | Busca, cache e recarregamento |
| Formulários | React Hook Form + Zod | Campos e validação |
| Login | MSAL React | Login no Entra ID e token para a API |
| Tipos da API | openapi-typescript | Tipos gerados do OpenAPI do backend |
| Testes | Vitest + Testing Library | Testes de componentes |

Testes de ponta a ponta no navegador (Playwright) ficam para depois do MVP.

## Infraestrutura ([ADR 0005](decisions/0005-azure-hosting.md), [ADR 0006](decisions/0006-authentication-entra-id.md))

| Item | Escolha |
|---|---|
| Hospedagem | Azure App Service (Linux), região Brazil South |
| Banco | Azure SQL Database (serverless) |
| Monitoramento | Application Insights |
| Segredos | Nenhum: Managed Identity para SQL e Graph, OIDC para o GitHub |
| Publicação | GitHub Actions: push no `main` → dev; produção por botão |
| Migrations | *Migration bundle* do EF Core, aplicado pelo pipeline |
| Infraestrutura | Criada pelo portal, seguindo o [passo a passo](infra/azure-setup.md) |
| Local | Docker Compose só para o SQL Server; API e Vite rodam direto na máquina |

## Planilha de consulta ([ADR 0004](decisions/0004-excel-report-file-in-sharepoint.md))

A equipe tem licença Microsoft 365 F3 (Office só na web). Por isso o Excel não busca dados: a
ferramenta **gera** o arquivo e o grava no SharePoint. Nos ambientes local e dev, o arquivo vai
para uma pasta.
