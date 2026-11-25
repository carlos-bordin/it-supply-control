# ADR 0004: planilha de consulta gerada pela ferramenta no SharePoint

- **Data:** 25/11/2026
- **Situação:** aceita (substitui a parte "Excel via Power Query" da [ADR 0003](0003-source-of-truth-and-excel-feed.md))

## Contexto

A equipe quer consultar os ativos no Excel, com filtros rápidos, sem editar nada. A ADR 0003
previa um Excel lendo a API por Power Query. Dois fatos derrubaram esse plano:

- A equipe tem licença **Microsoft 365 F3**, que só inclui o Office **na web**.
- O Power Query do Excel na web **não atualiza** dados de uma API com login corporativo, e no
  plano F3 também não conecta a arquivos CSV (testado no tenant da empresa).

Um teste com conta F3 confirmou que o Excel na web **abre, filtra e ordena** uma planilha guardada
no SharePoint, mesmo com permissão só de leitura, e lida bem com a substituição do arquivo
enquanto ele está aberto.

## Decisão

- A ferramenta **gera o arquivo `ativos.xlsx`** e o **substitui** numa biblioteca dedicada do
  SharePoint da equipe.
- O arquivo tem duas abas, cada uma com uma tabela do Excel (filtro por coluna, cabeçalho fixo):
  - **Ativos**: uma linha por ativo, com a situação atual.
  - **Eventos**: todo o histórico, uma linha por evento, com coluna indicando se o evento foi
    desconsiderado.
- A regravação é **disparada por mudança**: depois de qualquer ação, a ferramenta espera cerca de
  30 segundos para juntar mudanças próximas e grava o arquivo uma vez.
- O acesso ao SharePoint é feito pela **Managed Identity** do App Service, com a permissão
  **Sites.Selected** do Microsoft Graph liberada **só para o site da equipe**. Não há senha.
- A biblioteca é **somente leitura** para a equipe e tem o **controle de versões limitado**
  (por exemplo, 10 versões). O histórico de verdade fica na ferramenta.
- Nos ambientes local e dev, o arquivo é gravado numa **pasta**; o destino é configurável e o
  SharePoint só é usado em produção.
- **Plano B**: se este caminho deixar de funcionar, a consulta passa a ser uma tela em grade
  dentro da própria ferramenta.

## Alternativas consideradas

- **Power Query lendo a API ou CSVs**: não funciona no Excel na web com licença F3.
- **Graph Excel API** (editar a pasta de trabalho célula a célula): não aceita acesso pelo próprio
  aplicativo, só em nome de um usuário; exigiria uma conta de serviço.
- **Lista do SharePoint**: tem filtros no navegador, mas exige sincronizar linha por linha, e o
  histórico de eventos passaria rápido do limite de 5.000 itens por exibição.
- **Suplemento (add-in) do Excel** ou **Power BI**: muito mais peças, implantação pelo
  administrador e, no caso do Power BI, licenças extras.

## Consequências

- O backend precisa de uma biblioteca para gerar `.xlsx` e do SDK do Microsoft Graph.
- Filtros feitos pela equipe valem só para quem está olhando e se perdem ao reabrir; isso é
  esperado, já que o arquivo é somente leitura.
- Os dados no Excel ficam até cerca de 1 minuto atrás da ferramenta.
- A TI corporativa precisa dar o consentimento de administrador para Sites.Selected e liberar o
  site da equipe para a identidade do App Service (ver [passo a passo](../infra/azure-setup.md)).
