# CLAUDE.md

Notes for Claude Code when working in this repository.

## Project
IT supply control: device life cycles, suppliers, external tickets, end users. Target hosting: Azure.

## Stack (decided, see docs/decisions/)
- Backend: ASP.NET Core Web API (.NET 10) + EF Core, in `backend/`
- Frontend: React + TypeScript (Vite), in `frontend/`
- Database: SQL Server locally (Docker), Azure SQL in the cloud

## Language
- **Portuguese (Brazil)** for everything descriptive: documentation in `docs/`, READMEs,
  UI text shown to users, validation/error messages, code comments meant for readers.
- **English** for code: identifiers (classes, variables, endpoints, tables), file and folder names.
- Conversation with the owner may be in English.

## Working with the owner
- The owner is learning this stack (background: MySQL, MongoDB, HTML/CSS).
  Explain new concepts briefly and comment code where it helps learning.
- Each business area lives in matching places: `docs/domain/<area>.md`,
  `backend/.../Modules/<Area>/`, `frontend/src/features/<area>/`.
- Record significant technical choices as a new ADR in `docs/decisions/`.
