# Estágio 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos de solução e de projeto, respeitando a estrutura de pastas
COPY *.sln .
COPY AnimesAPI/AnimesAPI.csproj ./AnimesAPI/
COPY Service/Animes.Application.csproj ./Service/
COPY Core/Animes.Domain.csproj ./Core/
COPY Animes.Infrastructure/Animes.Infrastructure.csproj ./Animes.Infrastructure/
COPY Animes.Application.Tests/Animes.Application.Tests.csproj ./Animes.Application.Tests/
COPY Animes.API.Tests/Animes.API.Tests.csproj ./Animes.API.Tests/

# Restaura as dependências de todos os projetos
RUN dotnet restore

# Copia todo o restante do código-fonte
COPY . .
WORKDIR "/src/AnimesAPI"

# Publica a aplicação principal
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Estágio 2: Imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AnimesAPI.dll"]