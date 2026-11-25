# Documentação

| Documento | Conteúdo |
|---|---|
| [getting-started.md](getting-started.md) | Ferramentas e ambiente local |
| [scope.md](scope.md) | Escopo do MVP: problema, usuários, telas, o que está dentro e fora |
| [processes.md](processes.md) | Processos do dia a dia e onde a ferramenta atua em cada etapa |
| [stack.md](stack.md) | Stack de tecnologias e arquitetura em uma página |
| [future.md](future.md) | Recursos discutidos e deixados para depois do MVP |
| [domain/assets.md](domain/assets.md) | Ativos: atributos, estados, ações, histórico |
| [domain/supplier-exchanges.md](domain/supplier-exchanges.md) | Trocas com fornecedor |
| [domain/master-data.md](domain/master-data.md) | Cadastros de apoio (listas fixas) |
| [domain/glossary.md](domain/glossary.md) | Glossário de termos do negócio e nomes no código |
| [infra/azure-setup.md](infra/azure-setup.md) | Passo a passo dos recursos no Azure e pedidos à TI |
| [decisions/](decisions/) | Registros de decisões técnicas (ADRs) |

## Decisões (ADRs)

| ADR | Decisão |
|---|---|
| [0001](decisions/0001-backend-aspnet-core.md) | Backend em ASP.NET Core (.NET 10), EF Core e Minimal APIs |
| [0002](decisions/0002-frontend-react-typescript.md) | Frontend em React + TypeScript, Vite e Fluent UI |
| [0003](decisions/0003-source-of-truth-and-excel-feed.md) | Ferramenta como fonte da verdade (parte do Excel substituída pela 0004) |
| [0004](decisions/0004-excel-report-file-in-sharepoint.md) | Planilha de consulta gerada pela ferramenta no SharePoint |
| [0005](decisions/0005-azure-hosting.md) | Hospedagem no Azure, ambientes e publicação |
| [0006](decisions/0006-authentication-entra-id.md) | Autenticação com Microsoft Entra ID |
