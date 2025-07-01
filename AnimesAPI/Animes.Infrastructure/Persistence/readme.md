Devido o curto tempo para o envio, eu resolvi usar database first ao inv�s de code first, 
ent�o o banco de dados j� est� criado e populado com os dados necess�rios para o funcionamento do sistema.

Nesse caso, o script do banco foi construido e para criar as classes do projeto foi utilizado o seguinte comando:
```bash

Scaffold-DbContext "Server=(localdb)\MSSQLLocalDB;Database=AnimeDatabase;Trusted_Connection=True;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entities -Context AnimeDbContext -Tables Directors,Studios,Genres,Animes,Anime_Genres -DataAnnotations -Force

```

Talvez voc� precise mudar o campo server para o nome do seu servidor local, caso n�o esteja utilizando o localdb.