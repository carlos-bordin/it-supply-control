# Ativos

Equipamentos de TI controlados pela ferramenta: desktops, notebooks, celulares, tablets e
monitores.

## Origem

| Origem | Significado |
|---|---|
| **Interno** | Comprado pelo próprio cliente |
| **Externo** | Fornecido (alugado) por um fornecedor |

Internos e externos têm **numerações independentes**: o asset tag 10 interno não é o mesmo
ativo que o asset tag 10 externo. A origem é um campo próprio do ativo, sempre visível.

## Atributos

| Atributo | Observação |
|---|---|
| Origem | Interno ou Externo |
| Tipo | Lista fixa |
| Marca | Lista fixa |
| Modelo | Lista fixa |
| Número de série | |
| Asset tag | Número de ativo; único só dentro da mesma origem |
| Estado | Ver abaixo |
| Site | Lista fixa; onde o equipamento está fisicamente |
| Hash no Intune | Sim/não, com a data em que foi cadastrado |
| Fornecedor | Só externos; lista fixa |
| Nº do contrato | Só externos; texto livre, apenas referência |
| Matrícula do usuário | Preenchida na reserva/entrega |
| RITM | Preenchida na reserva; é a única referência ao ServiceNow (o REQ não é guardado) |
| Centro de custo | Opcional, informado na entrega |
| Data de empréstimo | Preenchida na entrega |

**Nenhum campo é obrigatório.** Informação faltando não impede o registro.

Matrícula, RITM, centro de custo e data de empréstimo descrevem o **uso atual**: são limpos na
devolução. O histórico guarda os valores anteriores.

## Duplicidade

O sistema **não bloqueia** duplicidades. Na pesquisa, o filtro **"somente duplicados"** mostra em
tabela os ativos que compartilham:

- o mesmo número de série; ou
- a mesma combinação origem + asset tag.

## Estados

Um único estado descreve o ciclo de vida. **Qualquer estado pode ir para qualquer outro**; não há
ordem obrigatória nem mudança automática. Todo tipo de equipamento pode usar todos os estados.

| Estado | Significado |
|---|---|
| `Formatar` | Em estoque, precisa ser formatado |
| `Aguardando hash` | Em estoque, aguardando o cadastro do hash no Intune |
| `Pronto` | Em estoque, **disponível para entrega** |
| `Reservado` | Escolhido para uma RITM e um usuário, ainda não entregue |
| `Em uso` | Com o funcionário |
| `Aguardando troca/devolução` | Avariado, aguardando o processo com o fornecedor |
| `Devolvido ao fornecedor` | Encerrado (externo) |
| `Baixado` | Encerrado (interno); saiu de circulação com motivo e data |

"Estoque disponível" significa estado `Pronto`. `Reservado` existe para que o mesmo equipamento
não seja prometido a duas RITMs.

### Hash no Intune

O hash é um **atributo**, não um estado: é cadastrado uma vez por equipamento e removido na
devolução ao fornecedor. Um equipamento devolvido por funcionário em bom estado volta para
`Formatar` e segue para `Pronto` sem passar por `Aguardando hash`, porque o hash continua
cadastrado.

## Ações

Cada ação muda o que for preciso e grava um evento no histórico de uma só vez.

| Ação | Efeito |
|---|---|
| **Cadastrar** | Cria o ativo, normalmente em `Formatar` |
| **Editar** | Corrige atributos; grava evento com valores anteriores e novos |
| **Mudar estado** | Livre entre `Formatar`, `Aguardando hash` e `Pronto`; permite marcar "hash cadastrado" |
| **Reservar** | `Pronto` → `Reservado`, com RITM e matrícula |
| **Entregar** | → `Em uso`, com data de empréstimo e centro de custo (opcional) |
| **Devolver** | Bom estado → `Formatar`. Avariado e externo → `Aguardando troca/devolução` + abre [troca](supplier-exchanges.md). Avariado e interno → `Baixado` |
| **Substituir** | Troca urgente: entrega um ativo `Pronto` ao usuário e trata o antigo como devolução avariada |
| **Transferir** | Muda o site, com nº da NF |
| **Atualizar troca** | Ver [trocas com fornecedor](supplier-exchanges.md) |
| **Desconsiderar evento** | Marca um evento do histórico como desconsiderado, com motivo |

## Histórico

Cada ativo tem uma linha do tempo de eventos com data/hora, autor, tipo de evento e dados.

- Eventos **nunca são apagados nem editados**.
- Um evento errado é marcado como **desconsiderado**, com motivo, autor e data da marcação.
  Ele continua visível no histórico, identificado como desconsiderado.
