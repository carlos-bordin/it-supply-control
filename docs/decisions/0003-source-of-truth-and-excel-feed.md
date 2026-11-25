# ADR 0003: ferramenta como fonte da verdade e Excel via Power Query

- **Data:** 24/11/2026
- **Situação:** aceita; a parte "Excel via Power Query" foi **substituída pela
  [ADR 0004](0004-excel-report-file-in-sharepoint.md)** em 25/11/2026. Continua valendo: a
  ferramenta é a fonte da verdade, a pesquisa interna tem só os filtros essenciais e não há botão
  de download.

## Contexto

Hoje o controle dos ativos é feito numa planilha Excel. A equipe domina o Excel e usa planilhas
em outras atividades, então elas não vão desaparecer. Ao mesmo tempo, se planilha e ferramenta
puderem ser editadas, os dados vão divergir, e histórico e acompanhamento de trocas só existem
se a ferramenta vir cada mudança acontecer.

Opções consideradas:

1. Ferramenta como fonte da verdade, com consulta em Excel.
2. Planilha como fonte da verdade, com a ferramenta importando e exibindo.
   Descartada: sem histórico de eventos nem acompanhamento de trocas.
3. Sincronização nos dois sentidos. Descartada: complexa e sujeita a conflitos.

Para a consulta em Excel: tela em grade dentro da ferramenta, Excel alimentado por Power Query,
ou os dois.

## Decisão

- **A ferramenta é a fonte da verdade** para estado, local, usuário e histórico de cada ativo.
  Toda mudança é feita nela.
- A consulta com filtros rápidos é feita num **Excel somente leitura alimentado por Power Query**
  ("Obter Dados" a partir da API). O arquivo tem duas abas: ativos (uma linha por ativo, com a
  situação atual) e eventos (uma linha por evento). Atualiza ao abrir e pelo "Atualizar tudo".
- Dentro da ferramenta, a tela de pesquisa tem busca e filtros essenciais (site, origem, tipo,
  estado, duplicados), mas não tenta reproduzir o Excel.
- **Sem botão de download** de planilha na ferramenta.

## Consequências

- O backend precisa expor endpoints de leitura próprios para o Power Query (formato tabular,
  estável), além da API usada pelo frontend.
- O Excel autentica na API com a conta corporativa (Entra ID). Isso depende do registro do
  aplicativo pela TI corporativa, o mesmo usado no login da ferramenta. **Não há plano B**: se o
  registro não for liberado, esta decisão precisa ser revista.
- Mudar dados pelo Excel não tem efeito na ferramenta; a equipe precisa saber disso.
