# ADR 0002: frontend em React + TypeScript com Vite e Fluent UI

- **Data:** 25/11/2026
- **Situação:** aceita

## Decisão

- Aplicação de página única (SPA) em **React + TypeScript**, criada e compilada com **Vite**.
- Componentes visuais do **Fluent UI React v9**, o sistema de design da Microsoft.
- Tipos das chamadas à API **gerados a partir do OpenAPI** do backend (`openapi-typescript`),
  em vez de escritos à mão.
- O build do frontend é servido pelo próprio backend (ver [ADR 0005](0005-azure-hosting.md)).

## Por quê

- React é o padrão de mercado para interfaces web; TypeScript acusa erros antes de rodar,
  o que ajuda quem está aprendendo.
- Vite é o caminho recomendado hoje para criar projetos React: rápido e com pouca configuração.
- Fluent UI dá à ferramenta o visual do Microsoft 365, que a equipe já usa todos os dias, e traz
  prontos os componentes que as telas do MVP pedem (abas, formulários, tabela de dados). É
  acessível por padrão.
- Tipos gerados do OpenAPI fazem uma mudança na API quebrar a compilação do frontend, em vez de
  quebrar a tela em produção.

## Alternativas consideradas

- **MUI** (Material Design, do Google): completo, mas com visual de outro ecossistema.
- **Tailwind + shadcn/ui**: mais liberdade visual, porém mais estilo para escrever e manter.
