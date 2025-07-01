Devido o curto tempo para o envio, eu resolvi usar database first ao invés de code first, 
então o banco de dados já está criado e populado com os dados necessários para o funcionamento do sistema.

Nesse caso, o script do banco foi construido e para criar as classes do projeto foi utilizado o seguinte comando:
```bash

Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=AnimeDatabase;Trusted_Connection=True;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context AnimeDbContext -Tables Directors,Studios,Genres,Animes,Anime_Genres -DataAnnotations -Force

```

Talvez você precise mudar o campo server para o nome do seu servidor local, caso não esteja utilizando o localdb.