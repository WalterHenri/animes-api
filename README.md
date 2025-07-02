# Animes API

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-green.svg)](https://docs.microsoft.com/ef/core/)
[![Docker](https://img.shields.io/badge/Docker-Supported-blue.svg)](https://www.docker.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

Uma Web API RESTful para gerenciamento de animes, desenvolvida seguindo os princípios da **Clean Architecture** e as boas práticas de desenvolvimento .NET.

## 📋 Índice

- [Animes API](#animes-api)
  - [📋 Índice](#-índice)
  - [🎯 Sobre o Projeto](#-sobre-o-projeto)
    - [Principais Características](#principais-características)
  - [✨ Funcionalidades](#-funcionalidades)
    - [Animes](#animes)
    - [Diretores](#diretores)
  - [🛠️ Tecnologias Utilizadas](#️-tecnologias-utilizadas)
  - [🏗️ Arquitetura](#️-arquitetura)
  - [🚀 Começando](#-começando)
    - [Pré-requisitos](#pré-requisitos)
    - [Instalação](#instalação)
  - [📚 Endpoints da API](#-endpoints-da-api)
    - [Base URL](#base-url)
    - [Animes](#animes-1)
    - [Diretores](#diretores-1)
    - [Exemplo de Requisição](#exemplo-de-requisição)
  - [🐳 Executando com Docker](#-executando-com-docker)
    - [Construir e executar](#construir-e-executar)
  - [🧪 Testes](#-testes)
    - [Executar todos os testes](#executar-todos-os-testes)
    - [Executar com relatório de cobertura](#executar-com-relatório-de-cobertura)
    - [Estrutura dos Testes](#estrutura-dos-testes)
  - [📁 Estrutura do Projeto](#-estrutura-do-projeto)
  - [🤝 Contribuindo](#-contribuindo)
    - [Padrões de Commit](#padrões-de-commit)
  - [📄 Licença](#-licença)
  - [📞 Contato](#-contato)

## 🎯 Sobre o Projeto

Este projeto foi desenvolvido como parte de um desafio técnico, com o intuito de demonstrar proficiência em .NET, Clean Architecture e boas práticas de desenvolvimento de software. A API oferece um sistema inicial para gerenciamento de animes, permitindo operações CRUD tanto para animes quanto para diretores.

### Principais Características

- **Clean Architecture**: Arquitetura limpa e desacoplada
- **API RESTful**: Endpoints bem definidos seguindo padrões REST
- **Documentação Swagger**: Documentação interativa da API
- **Containerização**: Suporte ao Docker
- **Testes Unitários e de Integração**: Cobertura de testes com xUnit

## ✨ Funcionalidades

### Animes
- ✅ Listar todos os animes
- ✅ Buscar animes por ID, nome ou diretor (filtros combinados)
- ✅ Cadastrar novos animes
- ✅ Atualizar animes existentes
- ✅ Excluir animes

### Diretores
- ✅ Listar todos os diretores
- ✅ Cadastrar novos diretores
- ✅ Atualizar diretores existentes
- ✅ Excluir diretores

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Versão | Descrição |
|------------|--------|-----------|
| [.NET](https://dotnet.microsoft.com/download/dotnet/8.0) | 8.0 | Framework principal |
| [ASP.NET Core](https://docs.microsoft.com/aspnet/core/) | 8.0 | Web API framework |
| [Entity Framework Core](https://docs.microsoft.com/ef/core/) | 8.0 | ORM para acesso a dados |
| [SQL Server](https://www.microsoft.com/sql-server) | 2022 | Banco de dados |
| [MediatR](https://github.com/jbogard/MediatR) | 12.x | Padrão Mediator |
| [AutoMapper](https://automapper.org/) | 12.x | Mapeamento de objetos |
| [xUnit](https://xunit.net/) | 2.x | Framework de testes |
| [FluentAssertions](https://fluentassertions.com/) | 6.x | Assertions para testes |
| [Swagger/OpenAPI](https://swagger.io/) | 3.0 | Documentação da API |
| [Docker](https://www.docker.com/) | Latest | Containerização |

## 🏗️ Arquitetura

O projeto segue os princípios da **Clean Architecture**, organizando o código em camadas bem definidas:

```
┌─────────────────────────────────────────┐
│              Presentation               │
│            (Animes.API)                 │
├─────────────────────────────────────────┤
│             Application                 │
│         (Animes.Application)            │
├─────────────────────────────────────────┤
│            Infrastructure               │
│        (Animes.Infrastructure)          │
├─────────────────────────────────────────┤
│               Domain                    │
│           (Animes.Domain)               │
└─────────────────────────────────────────┘
```

## 🚀 Começando

### Pré-requisitos

Certifique-se de ter instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/sql-server) (ou LocalDB)
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (opcional)

### Instalação

1. **Clone o repositório**

```bash
git clone https://github.com/WalterHenri/animes-api.git
cd animes-api
```

2. **Configure o banco de dados**

Execute o script de criação do banco:

```bash
# Navegue até o diretório do script
cd AnimesAPI/Animes.Infrastructure/Persistence/Database/

# Execute o script.sql no seu SQL Server
# Opcionalmente, execute Examples.sql para dados de exemplo
```

3. **Configure a string de conexão**

Edite o arquivo `AnimesAPI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=AnimeDatabase;Trusted_Connection=True;Encrypt=False"
  }
}
```

4. **Execute a aplicação**

```bash
cd AnimesAPI/AnimesAPI
dotnet run
```

5. **Acesse a documentação**

Abra seu navegador em: `https://localhost:7294/swagger`

## 📚 Endpoints da API

### Base URL
```
https://localhost:7294/api/v1
```

### Animes

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/Animes` | Lista todos os animes |
| `GET` | `/Animes?id={id}&name={nome}&director={diretor}` | Busca animes com filtros |
| `POST` | `/Animes` | Cria um novo anime |
| `PUT` | `/Animes/{id}` | Atualiza um anime |
| `DELETE` | `/Animes/{id}` | Exclui um anime |

### Diretores

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/Directors` | Lista todos os diretores |
| `POST` | `/Directors` | Cria um novo diretor |
| `PUT` | `/Directors/{id}` | Atualiza um diretor |
| `DELETE` | `/Directors/{id}` | Exclui um diretor |

### Exemplo de Requisição

```bash
# Buscar animes por nome
curl -X GET "https://localhost:7294/api/v1/Animes?name=Naruto" \
     -H "accept: application/json"

# Criar um novo anime
curl -X POST "https://localhost:7294/api/v1/Animes" \
     -H "accept: application/json" \
     -H "Content-Type: application/json" \
     -d '{
       "name": "Attack on Titan",
       "summary": "Humanity fights for survival against giant humanoid Titans",
       "directorId": 1
     }'
```

## 🐳 Executando com Docker

### Construir e executar

```bash
# Construir a imagem
docker build -t animes-api .

# Executar o container
docker run -p 8080:8080 -p 8081:443 animes-api
```

**Acesso**: `http://localhost:8080` ou `https://localhost:8081`

## 🧪 Testes

### Executar todos os testes

```bash
# Navegar para o diretório de testes
cd AnimesAPI/Animes.API.Tests

# Executar os testes
dotnet test
```

### Executar com relatório de cobertura

```bash
dotnet test --collect:"XPlat Code Coverage"
```

### Estrutura dos Testes

- **Testes Unitários**: Validam a lógica de negócio
- **Testes de Integração**: Testam os endpoints da API
- **Testes de Repositório**: Validam operações de banco de dados

## 📁 Estrutura do Projeto

```
animes-api/
├── AnimesAPI/
│   ├── Animes.API/                 # 🎯 Camada de Apresentação
│   │   ├── Controllers/            # Controllers da API
│   │   ├── Middlewares/            # Middlewares customizados
│   │   └── Program.cs              # Configuração da aplicação
│   │
│   ├── Animes.Application/         # 💼 Camada de Aplicação
│   │   ├── Commands/               # Comandos (CQRS)
│   │   ├── Queries/                # Consultas (CQRS)
│   │   ├── Handlers/               # Handlers do MediatR
│   │   ├── DTOs/                   # Objetos de transferência
│   │   └── Mappings/               # Perfis do AutoMapper
│   │
│   ├── Animes.Infrastructure/      # 🏗️ Camada de Infraestrutura
│   │   ├── Persistence/            # Contexto do EF Core
│   │   ├── Repositories/           # Implementação dos repositórios
│   │   └── Database/               # Scripts de banco
│   │
│   ├── Animes.Domain/              # 🏛️ Camada de Domínio
│   │   ├── Entities/               # Entidades de negócio
│   │   └── Interfaces/             # Contratos de repositório
│   │
│   └── Animes.API.Tests/           # 🧪 Testes
│       ├── Controllers/            # Testes de controllers
│       ├── Handlers/               # Testes de handlers
│       └── Integration/            # Testes de integração
│
├── Dockerfile                      # 🐳 Configuração Docker
├── docker-compose.yml              # 🐳 Compose (opcional)
└── README.md                       # 📖 Documentação
```

## 🤝 Contribuindo

Contribuições são bem-vindas! Para contribuir:

1. **Fork** o projeto
2. Crie uma **branch** para sua feature (`git checkout -b feature/MinhaFeature`)
3. **Commit** suas mudanças (`git commit -m 'Adiciona MinhaFeature'`)
4. **Push** para a branch (`git push origin feature/MinhaFeature`)
5. Abra um **Pull Request**

### Padrões de Commit

- `feat:` Nova funcionalidade
- `fix:` Correção de bug
- `docs:` Documentação
- `refactor:` Refatoração de código
- `test:` Adição de testes

## 📄 Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

---

## 📞 Contato

**Walter Henri** - [GitHub](https://github.com/WalterHenri)

**Link do Projeto**: [https://github.com/WalterHenri/animes-api](https://github.com/WalterHenri/animes-api)

---

⭐ **Se este projeto te ajudou, considere dar uma estrela no GitHub!**