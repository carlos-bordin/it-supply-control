# ADR 0005: hospedagem no Azure

- **Data:** 25/11/2026
- **Situação:** aceita

## Decisão

### Recursos

| Recurso | Uso |
|---|---|
| **App Service (Linux)** | Roda a API .NET **e** serve o build do React (pasta `wwwroot`): um endereço, uma publicação |
| **Azure SQL Database** | Banco de dados |
| **Application Insights** | Logs, erros e desempenho |

- O App Service acessa o Azure SQL e o Microsoft Graph pela sua **Managed Identity**: não existe
  senha de banco nem segredo de aplicativo. No MVP não há Key Vault.
- Região **Brazil South** (São Paulo): dados pessoais (matrícula) ficam no Brasil e a latência é
  menor.

### Ambientes

| Ambiente | Onde | Planos |
|---|---|---|
| **Local** | Máquina do desenvolvedor; Docker Compose só para o SQL Server | — |
| **Dev** | Assinatura própria do Azure | App Service F1 (gratuito), Azure SQL com a oferta gratuita (serverless), Application Insights na cota gratuita |
| **Produção** | Assinatura corporativa | Definidos com a TI; sugestão: App Service B1 e Azure SQL serverless |

### Publicação

- **GitHub Actions** compila, testa e publica.
- Cada push no `main` publica automaticamente no **dev**. A **produção** é publicada manualmente,
  por um botão no GitHub Actions, com exatamente a mesma versão testada no dev.
- O GitHub se autentica no Azure por **OIDC** (relação de confiança entre GitHub e Azure): nenhuma
  senha fica guardada no GitHub.
- As *migrations* do EF Core são aplicadas **pelo pipeline**, com um *migration bundle*, antes de
  publicar a nova versão. A API roda só com permissão de ler e gravar dados.
- No MVP, os recursos são criados **pelo portal**, seguindo o [passo a passo](../infra/azure-setup.md).

## Por quê

- App Service é o caminho mais direto para publicar .NET sem lidar com servidor ou contêiner.
- Servir o frontend pelo backend elimina CORS e um segundo serviço; frontend e backend não precisam
  de ciclos de publicação separados.
- Managed Identity e OIDC eliminam os segredos mais sensíveis do projeto.
- A assinatura própria permite aprender e testar sem depender da TI; a produção fica sob as regras
  da empresa.

## Alternativas consideradas

- **Container Apps**: exigiria empacotar em Docker e entender contêineres.
- **Azure Functions**: outro modelo de programação, não combina com uma API tradicional.
- **Static Web Apps** para o frontend: um serviço a mais sem ganho para este caso.
- **Migrations aplicadas pela API ao iniciar**: desaconselhado pela Microsoft em produção; duas
  instâncias podem migrar ao mesmo tempo e a API precisaria de permissão para alterar o esquema.
- **Infraestrutura como código (Bicep)**: adiado. Passa a valer a pena quando o passo a passo
  precisar ser repetido na migração para a assinatura corporativa.

## Consequências

- O plano F1 "dorme" sem uso e tem limite diário de CPU; a primeira requisição depois de um tempo
  parado é lenta. O banco serverless também pausa e demora alguns segundos para voltar.
- O firewall do Azure SQL não libera os servidores do GitHub Actions por padrão. O pipeline
  precisa liberar o IP do executor antes de aplicar as migrations e remover a regra depois.
- Se a TI exigir publicar pelo processo próprio deles, só a etapa de produção muda.
