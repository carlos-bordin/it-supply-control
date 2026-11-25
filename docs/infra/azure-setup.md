# Passo a passo do Azure

Recursos criados **pelo portal do Azure** no MVP ([ADR 0005](../decisions/0005-azure-hosting.md)).
Quando este passo a passo precisar ser repetido (na migração para a assinatura corporativa), vale
transformá-lo em Bicep.

> Os passos ainda não foram executados. Ajuste nomes e detalhes conforme forem validados.

## Convenção de nomes

| Recurso | Dev | Produção |
|---|---|---|
| Grupo de recursos | `rg-itsupply-dev` | `rg-itsupply-prod` |
| Plano do App Service | `asp-itsupply-dev` | `asp-itsupply-prod` |
| App Service | `app-itsupply-dev` | `app-itsupply-prod` |
| Servidor SQL | `sql-itsupply-dev` | `sql-itsupply-prod` |
| Banco | `sqldb-itsupply` | `sqldb-itsupply` |
| Application Insights | `appi-itsupply-dev` | `appi-itsupply-prod` |
| Identidade do GitHub | `id-itsupply-github-dev` | `id-itsupply-github-prod` |

Nomes de App Service e de servidor SQL são únicos no mundo; se já existirem, acrescente um sufixo.
Região de tudo: **Brazil South**.

## Ambiente dev (assinatura própria)

1. **Grupo de recursos** `rg-itsupply-dev`.
2. **Azure SQL**
   - Servidor com **autenticação somente Microsoft Entra**; você como administrador.
   - Banco `sqldb-itsupply` com a **oferta gratuita** (serverless, pausa automática).
   - Rede: permitir acesso de serviços do Azure.
3. **App Service**
   - Plano Linux **F1** (gratuito); pilha **.NET 10**.
   - Ativar a **identidade gerenciada atribuída pelo sistema**.
4. **Permissão da API no banco**: conectado ao banco como administrador Entra, executar:

   ```sql
   CREATE USER [app-itsupply-dev] FROM EXTERNAL PROVIDER;
   ALTER ROLE db_datareader ADD MEMBER [app-itsupply-dev];
   ALTER ROLE db_datawriter ADD MEMBER [app-itsupply-dev];
   ```

5. **Application Insights** `appi-itsupply-dev` e vincular ao App Service.
6. **Registro de aplicativo** no Entra (tenant pessoal) ([ADR 0006](../decisions/0006-authentication-entra-id.md)):
   - plataforma **SPA** com retorno para `https://app-itsupply-dev.azurewebsites.net` e
     `http://localhost:5173`;
   - **Expor uma API**: identificador `api://<id-do-aplicativo>` e escopo `user_impersonation`;
   - criar um ou dois usuários de teste no tenant.
7. **Identidade do GitHub** (publicação sem senha, por OIDC):
   - criar a identidade gerenciada atribuída pelo usuário `id-itsupply-github-dev`;
   - credencial federada para o GitHub: repositório `carlos-bordin/it-supply-control`,
     branch `main`;
   - papel **Website Contributor** no App Service;
   - usuário no banco para aplicar *migrations*:

     ```sql
     CREATE USER [id-itsupply-github-dev] FROM EXTERNAL PROVIDER;
     ALTER ROLE db_ddladmin ADD MEMBER [id-itsupply-github-dev];
     ALTER ROLE db_datareader ADD MEMBER [id-itsupply-github-dev];
     ALTER ROLE db_datawriter ADD MEMBER [id-itsupply-github-dev];
     ```

8. **GitHub**: no repositório, criar as variáveis `AZURE_CLIENT_ID`, `AZURE_TENANT_ID` e
   `AZURE_SUBSCRIPTION_ID` (não são segredos; identificam a identidade do passo 7).

A planilha de consulta, no dev, é gravada numa pasta do App Service; não há SharePoint aqui.

## Produção (assinatura corporativa): pedidos à TI

Lista para levar à TI corporativa. Os itens marcados como "garantido" já foram confirmados.

| # | Pedido | Situação |
|---|---|---|
| 1 | Grupo de recursos em Brazil South, com acesso para criar os recursos abaixo (ou a TI cria) | Pendente |
| 2 | App Service Linux (sugestão: **B1**), .NET 10, identidade gerenciada do sistema ativada | Pendente |
| 3 | Azure SQL serverless, autenticação somente Entra, com os usuários do banco como no dev | Pendente |
| 4 | Application Insights | Pendente |
| 5 | Registro de aplicativo no tenant corporativo, como no passo 6 do dev, com o endereço de produção | Pendente |
| 6 | No aplicativo empresarial: **atribuição obrigatória** e um grupo com a equipe de Asset e o suporte local | Pendente |
| 7 | Permissão **Sites.Selected** do Microsoft Graph para a identidade do App Service, com consentimento de administrador | Garantido |
| 8 | Liberação de **escrita** dessa identidade **só no site** do SharePoint da equipe | Garantido |
| 9 | Biblioteca dedicada no site: **somente leitura** para a equipe e **controle de versões limitado** (ex.: 10) | Pendente |
| 10 | Identidade do GitHub para produção, com credencial federada para o *environment* `production` do repositório | Pendente |

### Referência para os itens 7 e 8

A permissão Sites.Selected sozinha não dá acesso a nenhum site; ela precisa ser combinada com a
liberação do site específico. Em resumo, um administrador:

1. atribui o papel de aplicativo `Sites.Selected` do Microsoft Graph à entidade de serviço da
   identidade gerenciada (por exemplo, com `New-MgServicePrincipalAppRoleAssignment` no
   Microsoft Graph PowerShell);
2. concede `write` no site da equipe para essa identidade
   (`POST /sites/{site-id}/permissions` no Microsoft Graph).
