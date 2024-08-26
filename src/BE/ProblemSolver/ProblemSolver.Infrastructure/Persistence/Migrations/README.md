## Migrations

https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

### Commands

*from root directory of Eit.Infrastructure folder*

create new migration
```bash
dotnet ef migrations add "SampleMigration" --project . --startup-project ../ProblemSolver --output-dir Persistence/Migrations
```

apply all migrations
```bash
dotnet ef database update --project . --startup-project ../ProblemSolver
```

apply specific migration
```bash
dotnet ef database update "Init" --project . --startup-project ../ProblemSolver
```