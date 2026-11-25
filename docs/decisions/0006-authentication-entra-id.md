# ADR 0006: autenticação com Microsoft Entra ID

- **Data:** 25/11/2026
- **Situação:** aceita

## Decisão

- Login com a **conta corporativa Microsoft** (Entra ID). Não há usuários e senhas próprios.
- O React faz o login com a **MSAL React** (`@azure/msal-react`) e envia um **token** em cada
  chamada à API.
- A API valida os tokens com **Microsoft.Identity.Web**. É o único mecanismo de autenticação.
- Um **único registro de aplicativo** no Entra, de um só tenant:
  - plataforma SPA, com os endereços de retorno do ambiente (produção e `http://localhost:5173`);
  - identificador da API no formato padrão `api://<id-do-aplicativo>`;
  - escopo `user_impersonation`, pedido pelo React.
- O endereço padrão `*.azurewebsites.net` é suficiente; um subdomínio da empresa é opcional.
- Recomendado: **atribuição obrigatória** no aplicativo empresarial, com um grupo da equipe de
  Asset e do suporte local. Assim só quem está no grupo consegue entrar.
- No MVP, todos os autenticados têm as mesmas permissões. O nome do usuário vem do token e é
  gravado como autor de cada evento do histórico.

### Identidade do próprio aplicativo

A Managed Identity do App Service (não o usuário) é quem acessa:

- o **Azure SQL**, com permissão de ler e gravar dados;
- o **Microsoft Graph**, com **Sites.Selected** só no site da equipe, para gravar a planilha de
  consulta ([ADR 0004](0004-excel-report-file-in-sharepoint.md)). Só em produção.

## Por quê

- A equipe já tem conta corporativa; não há senha nova para criar, guardar ou recuperar.
- Token em tudo mantém a API com um só mecanismo de autenticação. Como frontend e API estão no
  mesmo endereço, não há CORS.
- O plano anterior de o Excel chamar a API exigia domínio verificado e pré-autorização do Power
  Query. Com a planilha gerada no SharePoint, essas exigências deixaram de existir.

## Alternativas consideradas

- **Login por cookie no servidor (padrão BFF)**: mais protegido contra roubo de token no
  navegador, mas é um segundo padrão para aprender sem ganho claro para uma ferramenta interna.
- **Autenticação embutida do App Service ("Easy Auth")**: menos código, porém esconde o mecanismo
  de quem está aprendendo e dificulta rodar e testar localmente.

## Consequências

- No ambiente dev (assinatura própria), o registro fica no tenant pessoal, com usuários de teste.
  Em produção, o registro é feito pela TI no tenant corporativo.
- Com a API rodando localmente em modo Development, o login continua obrigatório; não há
  liberação de acesso anônimo.
