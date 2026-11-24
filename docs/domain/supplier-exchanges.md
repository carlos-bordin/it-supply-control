# Trocas com fornecedor

Ativos **externos** com problema físico são trocados com o fornecedor. Manutenção não faz
parte da regra de negócio. No MVP a troca cobre só **defeito**; devolução por fim de contrato
fica para o módulo de contratos ([futuro](../future.md)).

Uma troca é aberta quando um ativo externo avariado é devolvido (**Devolver** ou **Substituir**).

## Campos

| Campo | Observação |
|---|---|
| Ativo defeituoso | O ativo externo que será recolhido |
| Nº do chamado no Zendesk | Texto |
| Site de entrega da reposição | Endereço escolhido por quem abriu o chamado |
| Ativo de reposição | Cadastrado quando chega |
| Data da coleta | Coleta do defeituoso no endereço do cliente |
| Nº da NF de devolução | Emitida pelo fornecedor; toda devolução tem NF |

## Andamento

O andamento **não é atualizado à mão**: é calculado pelos campos preenchidos.

| Condição | Andamento |
|---|---|
| Sem ativo de reposição | Aguardando reposição |
| Com reposição, sem data de coleta | Aguardando agendamento da coleta |
| Com coleta, sem NF | Aguardando NF |
| Com NF | Concluída |

## Regras

- O ativo de reposição é um **ativo novo** (outro asset tag e número de série). Ele herda
  fornecedor e nº do contrato do defeituoso, entra como `Formatar` no site de entrega e fica
  ligado à troca.
- Ao registrar a NF, o ativo defeituoso passa a `Devolvido ao fornecedor` e o hash no Intune
  fica "não".
- Defeituoso e reposição têm links um para o outro na visualização do ativo.

## Onde aparece

- Trocas abertas: pesquisa filtrando estado `Aguardando troca/devolução`.
- Dados e andamento: bloco dentro da aba **Informações** do ativo defeituoso, com a ação
  **Atualizar troca**.
