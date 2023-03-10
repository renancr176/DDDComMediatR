## Banco de dados

Abra o "Package Manager Console" e selecione o projeto "ApiMvno.Infra.Data" como opção do "Default project"

Comando para gerar uma nova migration
```bash
Add-Migration NomeDaMigration -Context MvnoDbContext -OutputDir Contexts/MvnoDb/Migrations
```
Para aplicar a nova migration no banco de dados.
```bash
Update-Database -Context MvnoDbContext
```

Para desfazer a ultima migration.
```bash
Update-Database NomeDaPenultimaMigration -Context MvnoDbContext
Remove-Migration -Context MvnoDbContext
```

Documentação das [migrations.](https://docs.microsoft.com/pt-br/ef/core/managing-schemas/migrations/)
