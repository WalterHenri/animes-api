## Sumário da Alteração

**Issue:** Fecha # (número_da_issue)

## Alterações na API

| Endpoint | Método HTTP | Descrição da Alteração | Breaking Change? (Sim/Não) |
| :--- | :--- | :--- | :---: |
| `/v1/animes/{id}/staff` | `GET` | Adicionado novo campo `role` no objeto de resposta. | Não |
| `/v2/studios` | `POST` | Novo endpoint para criação de estúdios. | Não |
| `/v1/characters` | `GET` | Campo `age` removido do response. | **Sim** |
| | | | |

### Detalhes do Contrato (Request/Response)

**Exemplo para `GET /v1/animes/{id}/staff`:**
```diff
{
  "id": "string",
  "name": "string",
- "position": "string"
+ "role": "string" // 'director', 'writer', 'animator', etc.
}
```

## Descrição Técnica da Implementação

## Plano de Testes

### Testes Automatizados
- [ ] Novos testes unitários foram adicionados para cobrir a lógica de negócio.
- [ ] Novos testes de integração foram adicionados para validar o endpoint e o contrato.
- [ ] Todos os testes existentes (`unit` e `integration`) passam com sucesso.

### Verificação Manual
**Cenário 1: Validar novo campo `role`**
1. Execute a seguinte requisição:
   ```bash
   curl -X GET 'http://localhost:8080/api/v1/animes/1/staff' -H 'Authorization: Bearer <seu_token>'
   ```
2. **Resultado Esperado:** O corpo da resposta deve conter o campo `role` em cada item da lista de `staff`.
