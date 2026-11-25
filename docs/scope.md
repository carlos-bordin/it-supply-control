# Escopo do MVP

Definido em sessão de levantamento em 24/11/2026. Este documento diz **em quais partes do
processo a ferramenta atua** na primeira versão e o que fica de fora.

## Problema

Hoje o controle dos equipamentos é feito numa planilha que reúne os dados de cada ativo e
indica se ele está pronto para entrega. Para tomar uma decisão (há notebook pronto no site X?
em que ponto está a troca daquele equipamento?), a equipe perde tempo navegando entre
planilhas e históricos.

## Objetivo

Ser a **fonte da verdade** sobre a situação de cada ativo (estado, local, com quem está, o que
aconteceu com ele) e mostrar ao utilizador exatamente o que precisa para decidir.

As planilhas continuam sendo usadas pela equipe para outras atividades. Para consulta e
filtros, a ferramenta gera uma **planilha de consulta somente leitura no SharePoint** da equipe
(ver [ADR 0003](decisions/0003-source-of-truth-and-excel-feed.md) e
[ADR 0004](decisions/0004-excel-report-file-in-sharepoint.md)).

## Quem usa

| Perfil | Papel no processo |
|---|---|
| Equipe de Asset | Aprova e escolhe qual equipamento cada usuário recebe; acompanha trocas |
| Suporte local (de cada site) | Prepara, entrega, recebe devoluções, participa da decisão de estoque |

No MVP **todos têm as mesmas permissões**. O histórico registra quem fez cada ação.
Login com a conta corporativa Microsoft (Entra ID). Todos veem todos os sites; as telas abrem
filtradas pelo site do utilizador.

O funcionário final **não** acessa a ferramenta.

## Dentro do escopo (MVP)

- Cadastro manual de ativos internos e externos ([ativos](domain/assets.md)).
- Ciclo de vida do ativo com estados livres e ações que gravam histórico.
- Preparo simplificado: `Formatar`, `Aguardando hash`, `Pronto`, sem ordem obrigatória.
- Reserva, entrega, devolução, substituição urgente e transferência entre sites.
- Troca de equipamento com fornecedor por defeito ([trocas](domain/supplier-exchanges.md)).
- Histórico de eventos por ativo (eventos nunca são apagados, só desconsiderados).
- Cadastros de apoio em listas fixas: sites, fornecedores, tipos, marcas, modelos
  ([cadastros de apoio](domain/master-data.md)).
- Planilha de consulta `ativos.xlsx` regravada no SharePoint a cada mudança: aba de ativos e
  aba de eventos.

## Telas do MVP

1. **Pesquisa de ativos** (tela inicial)
   - Busca por texto: asset tag, número de série, matrícula, RITM.
   - Filtros: site, origem, tipo, estado e "somente duplicados".
   - Resultado em tabela com o total de registros no topo.
   - "Há estoque no site X?" = filtrar site X + estado `Pronto`.
2. **Visualização do ativo**, com duas abas:
   - **Informações**: dados do ativo, ações disponíveis e, quando houver troca aberta,
     um bloco com os dados e o andamento da troca.
   - **Histórico**: linha do tempo de eventos.
3. **Cadastro/edição de ativo**.
4. **Cadastros de apoio**: sites, fornecedores, tipos, marcas, modelos.

## Fora do escopo

| Item | Situação |
|---|---|
| Manutenção de equipamentos | Não faz parte da regra de negócio |
| Fila de requisições (RITMs) | Fica no ServiceNow; RITM só é aprovada com estoque disponível |
| Integração com ServiceNow, Zendesk, Intune | Só números de referência digitados à mão |
| Contratos e alertas de vencimento | Documentado em [futuro](future.md) |
| Saúde dos dados | Documentado em [futuro](future.md) |
| Importação de Excel (carga inicial e lotes) | Documentado em [futuro](future.md) |
| Relatório de faturamento por centro de custo | Documentado em [futuro](future.md) |
| Perfis com permissões diferentes | Documentado em [futuro](future.md) |
| Painel com indicadores | Substituído pela pesquisa com filtros |
| Download de planilha pela ferramenta | Consulta em Excel só pela planilha no SharePoint |

## Princípios que guiaram as decisões

- **Nada é obrigatório, nada é bloqueado.** Campos faltando ou duplicidades não impedem o
  registro; ficam visíveis (filtro de duplicados) e, no futuro, no módulo de saúde dos dados.
- **Pouca atualização manual de rótulos.** Uma ação muda o estado e grava o evento de uma vez;
  o andamento da troca é calculado pelos campos preenchidos.
- **Listas fixas para evitar inconsistência.** Campos categóricos são escolhidos de listas.
- **Simples primeiro.** O que não é essencial para substituir o controle atual ficou para depois.

## Pontos em aberto

- **REQ x RITM**: a lista original de atributos cita "REQ (número da requisição)", e o processo
  cita RITM. No MVP há um único campo **RITM**. Confirmar se o REQ também precisa ser guardado.
- **Pedidos à TI corporativa**: o registro do aplicativo no Entra ID, a assinatura de produção e
  a permissão no SharePoint dependem dela (lista em [passo a passo do Azure](infra/azure-setup.md)).
  A permissão no SharePoint já foi garantida.
- **Quem mantém as listas fixas**: assumido que qualquer utilizador pode incluir itens nos
  cadastros de apoio (já que todos têm as mesmas permissões).
