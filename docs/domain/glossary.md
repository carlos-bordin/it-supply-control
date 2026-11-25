# Glossário

| Termo | Significado | No código |
|---|---|---|
| Ativo | Equipamento de TI controlado pela ferramenta | `Asset` |
| Origem | Interno (comprado pelo cliente) ou Externo (fornecido por fornecedor) | `AssetOrigin` |
| Asset tag | Número de ativo; numeração independente por origem | `AssetTag` |
| Estado | Fase do ciclo de vida do ativo | `AssetState` |
| Disponível para entrega | Ativo no estado `Pronto` | `Ready` |
| Site | Local físico com estoque próprio e suporte local | `Site` |
| Hash | Hardware hash cadastrado no Intune para o Windows Autopilot | `IntuneHashRegistered` |
| RITM | Item de requisição no ServiceNow que pede um equipamento | `Ritm` |
| Matrícula | Identificação do funcionário que usa o ativo | `EmployeeId` |
| Reserva | Ativo escolhido para uma RITM, ainda não entregue | `Reserved` |
| Troca | Processo de devolver um ativo externo avariado ao fornecedor e receber uma reposição | `SupplierExchange` |
| Reposição | Ativo novo enviado pelo fornecedor numa troca | `ReplacementAsset` |
| Coleta | Retirada do ativo pelo fornecedor no endereço do cliente | `PickupDate` |
| NF | Nota fiscal; exigida em devoluções e transferências entre sites | `InvoiceNumber` |
| Evento | Registro imutável no histórico de um ativo | `AssetEvent` |
| Desconsiderar | Marcar um evento como errado sem apagá-lo | `Disregard` |
| Baixa | Saída de circulação de um ativo interno | `WrittenOff` |
| Planilha de consulta | Arquivo Excel somente leitura, gerado pela ferramenta no SharePoint da equipe, com a situação atual dos ativos e o histórico de eventos | `AssetReport` |

Os nomes da coluna "No código" são sugestões e podem mudar ao implementar.
