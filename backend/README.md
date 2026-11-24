# Backend

API Web em ASP.NET Core (.NET 10) + EF Core. **Ainda não criada.**

Será gerada com `dotnet new webapi` depois que o básico de C# for estudado.
Estrutura prevista:

```
backend/
├── ItSupplyControl.slnx
├── src/ItSupplyControl.Api/
│   ├── Program.cs
│   ├── Modules/      Devices/, Suppliers/, EndUsers/, Assignments/, Tickets/
│   ├── Data/         AppDbContext.cs, Migrations/
│   └── Common/
└── tests/
```

Por que esta tecnologia: [docs/decisions/0001](../docs/decisions/0001-backend-aspnet-core.md).
