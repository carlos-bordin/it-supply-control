# Recursos futuros

Ideias discutidas no levantamento de escopo e deixadas **fora do MVP** de propósito. Ficam
registradas para não se perderem.

## Contratos

Ativos externos pertencem a contratos, que podem ou não ser renovados.

- Um contrato agrupa vários ativos externos, com as mesmas datas para todos.
- Campos: fornecedor, nº do contrato, data de início, data de fim, valor mensal por tipo/modelo.
- Cada ativo externo pertence a um único contrato.
- **Alerta com 90 dias de antecedência**, numa tela própria de Contratos com contador no menu
  (não na tela inicial). Para cada contrato a vencer: quantos ativos estão em uso, com quem e em
  quais sites.
- No fim do contrato:
  - **Renovado**: atualiza a data de fim; os ativos seguem como estão.
  - **Não renovado**: todos os ativos precisam ser devolvidos. Os que estão em uso exigem trocar o
    equipamento do usuário antes da coleta. A devolução usa o mesmo registro da troca, sem
    reposição.
- No MVP existe só o campo de texto livre "nº do contrato" no ativo, que vira o vínculo real
  quando este módulo for feito.

## Saúde dos dados

Módulo que aponta inconsistências e informação faltando, em vez de obrigar campos ou bloquear
registros. Regras iniciais propostas:

1. Número de série ou origem + asset tag duplicados.
2. `Em uso` sem matrícula.
3. Ativo externo sem contrato.
4. Transferência entre sites sem NF.
5. Troca com coleta realizada e sem NF.
6. `Aguardando hash` há mais de 1 dia (o hash é sempre cadastrado na noite do mesmo dia).
7. `Pronto` sem hash cadastrado (para tipos que usam Intune).
8. `Devolvido ao fornecedor` com hash ainda cadastrado.
9. Ativo externo `Em uso` com contrato vencido.
10. `Reservado` há mais de X dias sem entrega.
11. Equipamento parado em `Formatar` há mais de 3 dias.

No MVP, só a duplicidade fica visível (filtro "somente duplicados" na pesquisa).

## Importação de Excel

Uma função única, com modelo fixo de colunas, para:

- a carga inicial a partir da planilha atual;
- lotes de equipamentos entregues de uma vez pelo fornecedor.

Linhas com problema são mostradas antes de confirmar. No MVP o cadastro é só manual.

## Relatório de faturamento

Custo mensal esperado por centro de custo (ativos em uso × valor mensal do contrato), exportável
para Excel. Depende do módulo de contratos. Pontos a decidir: centro de custo de ativos parados
em estoque e cálculo proporcional para entregas no meio do mês.

## Perfis com permissões diferentes

Asset (aprova e escolhe equipamentos, cuida de trocas) e suporte local (prepara, entrega,
recebe devoluções). No MVP todos têm as mesmas permissões.

## Painel inicial

Proposta descartada no MVP em favor da pesquisa com filtros: estoque por tipo/modelo (pronto ×
em preparo), equipamentos parados no preparo e trocas abertas com andamento.

## Integrações

ServiceNow (RITMs), Zendesk (chamados) e Intune (hash). No MVP só os números de referência são
digitados à mão. Integração exige liberação da TI corporativa.
