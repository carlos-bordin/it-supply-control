# Primeiros passos

> **Situação:** o código da aplicação ainda não foi criado. Este guia descreve o ambiente local
> **previsto** e será atualizado quando backend e frontend existirem.

## Ferramentas necessárias

| Ferramenta | Para quê |
|---|---|
| Git | Controle de versão |
| .NET 10 SDK | Compilar e rodar a API |
| Node.js (versão LTS ativa) e npm | Compilar e rodar o frontend |
| Docker Desktop | Rodar o SQL Server local e os testes de integração |
| VS Code ou Visual Studio | Editor |

## Como o ambiente local funciona

- O **Docker Compose** sobe só o **SQL Server**.
- A **API** roda direto na máquina com `dotnet run`.
- O **frontend** roda com `npm run dev` (Vite, em `http://localhost:5173`). O Vite repassa as
  chamadas para `/api` à API local, então o navegador enxerga um endereço só, como na nuvem.
- O **login** usa o registro de aplicativo do ambiente dev (tenant pessoal, usuários de teste).
- A **planilha de consulta** é gravada numa pasta local, não no SharePoint.

## Comandos previstos

```powershell
docker compose up -d
```

```powershell
dotnet run --project backend/src/ItSupplyControl.Api
```

```powershell
npm --prefix frontend run dev
```

## Próximas leituras

- [Escopo do MVP](scope.md)
- [Stack de tecnologias](stack.md)
- [Passo a passo do Azure](infra/azure-setup.md)
