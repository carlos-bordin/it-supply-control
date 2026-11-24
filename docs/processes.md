# Processos e onde a ferramenta atua

Cada processo abaixo descreve o que acontece no mundo real e, ao lado, a ação da ferramenta.
Os nomes das ações e dos estados estão definidos em [ativos](domain/assets.md).

## Funcionário recebe um equipamento

| # | Mundo real | Ferramenta |
|---|---|---|
| 1 | Requisição aberta no ServiceNow (RITM) | — |
| 2 | Asset consulta se há estoque no site (com o suporte local, se preciso) | Pesquisa: site + estado `Pronto` |
| 3 | RITM aprovada (só é aprovada se há estoque) | — |
| 4 | Asset escolhe o equipamento | **Reservar** (RITM + matrícula) → `Reservado` |
| 5 | Suporte local prepara, se necessário, e entrega | **Entregar** → `Em uso` |

## Saída do funcionário

| # | Mundo real | Ferramenta |
|---|---|---|
| 1 | Funcionário devolve o equipamento ao suporte local | **Devolver** |
| 2a | Bom estado: volta ao estoque | → `Formatar` (hash continua cadastrado) |
| 2b | Avariado e externo: troca com o fornecedor | → `Aguardando troca/devolução` + abre a troca |
| 2c | Avariado e interno: sai de circulação | → `Baixado` (com motivo) |

## Funcionário precisa trocar de equipamento

| # | Mundo real | Ferramenta |
|---|---|---|
| 1 | Avaliação do problema | — |
| 2 | Problema real: requisição no ServiceNow | — |
| 3a | Urgente: recebe outro do estoque na hora | **Substituir**: entrega um `Pronto` e manda o antigo para troca |
| 3b | Não urgente: aguarda a reposição do fornecedor | Antigo segue `Em uso`; troca aberta; reposição entra no estoque e é entregue depois |
| 4 | O antigo é trocado com o fornecedor | Fluxo de [troca](domain/supplier-exchanges.md) |

## Troca com fornecedor e preparo da reposição

| # | Mundo real | Ferramenta |
|---|---|---|
| 1 | Chamado aberto no Zendesk | Troca registrada com o nº do chamado |
| 2 | Fornecedor envia a reposição ao endereço escolhido | **Atualizar troca**: reposição cadastrada → `Formatar` no site de entrega |
| 3 | Formatação com pendrive (Windows da empresa) | Estado `Formatar` |
| 4 | Requisição no ServiceNow para cadastro do hash no Intune (sempre concluída na noite do dia de abertura) | → `Aguardando hash` |
| 5–7 | Autopilot, domínio, equipamento pronto | → `Pronto` + hash cadastrado = sim |
| 8 | Coleta do defeituoso agendada no endereço do cliente | **Atualizar troca**: data da coleta |
| 9 | Fornecedor emite a NF de devolução | **Atualizar troca**: NF → defeituoso `Devolvido ao fornecedor`, hash = não |

## Transferência entre sites

Só acontece mediante NF. Ação **Transferir**: muda o site e registra o nº da NF.

## Faturamento do aluguel

O aluguel de equipamentos externos é faturado mensalmente no centro de custo de quem recebe o
equipamento, e cada equipamento pode pertencer a contratos com prazos e preços diferentes.
No MVP a ferramenta **não** calcula faturamento; guarda apenas o centro de custo (opcional) na
entrega e um texto livre com o nº do contrato. Ver [futuro](future.md).
