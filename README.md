# Media Streaming Platform

## Funcionalidades
### Dashboard (Input)
- Criação e exclusão de playlists
- Upload de arquivos de mídia (MP4, JPEG, PNG)
- Adição e remoção de arquivos nas playlists
- Controle de reprodução via SignalR
- Limite de arquivo de 500MB

### Player (Output)
- Reprodução automática da playlist selecionada
- Atualização em tempo real via WebSocket
- Transições simples entre mídias
- Sincronização automática com o dashboard

## Tecnologias Utilizadas

### Frontend
- **React ** com TypeScript
- **Vite** (versão mais recente)
- **Ant Design** 
- **Tailwind CSS** 
- **SignalR Client**
### Backend
- **.NET 8** - API REST
- **Entity Framework Core** - ORM
- **SignalR** - WebSockets
- **PostgreSQL** - Banco de dados

### Arquitetura
- **Clean Architecture** com separação de responsabilidades

## requisitos para rodar localmente

- Node.js 18+
- .NET 8 SDK
- PostgreSQL 13+
- Git

## Instalação e Execução

### 1. Clone o repositório
```bash
git clone https://github.com/seu-usuario/Media-Streaming-Platform.git
cd Media-Streaming-Platform
```

### 2. Configuração do Banco de Dados
```bash
# Criar banco PostgreSQL
createdb MediaStreamingDB

# Ou via psql:
psql -U postgres
CREATE DATABASE MediaStreamingDB;
\q
```

### 3. Configuração da API
```bash
cd MediaStreamingPlatform_API
cp appsettings.json
# Edite appsettings.json com suas configurações de banco
```

**appsettings.json:**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=MediaStreamingDB;Username=postgres;Password=SUA_SENHA"
  }
}
```

```bash
# Instalar dependências e executar migrations
dotnet restore
dotnet ef database update
dotnet run
```
A API estará rodando em: `http://localhost:5278`

### 4. Frontend - Dashboard (Input)
```bash
cd MediaStreamingPlatform_Input_UI
npm install
npm run dev
```
Dashboard disponível em: `http://localhost:5173`

### 5. Frontend - Player (Output)
```bash
cd MediaStreamingPlatform_Output_UI
npm install
npm run dev
```
Player disponível em: `http://localhost:5174`

## Status do Projeto

## Fases Concluídas (1-4)
## Fase 1 - Gerenciamento de Playlists

CRUD completo de playlists
Interface responsiva com Ant Design

## Fase 2 - Upload e Gerenciamento de Arquivos

Upload de MP4, JPEG, PNG (até 500MB)
Associação de arquivos às playlists
Remoção de arquivos

## Fase 3 - Reprodução Básica

Player de mídia funcional
Controle via dashboard

## Fase 4 - Tempo Real e Automação

Atualização automática de playlists
Transições simples entre mídias
WebSocket para sincronização em tempo real
SignalR para comunicação entre dashboards

## Próximas Implementações
Com mais tempo disponível:
Funcionalidades Pendentes do Desafio

Autenticação JWT básica
Testes automatizados (unitários e integração)

## Escalabilidade e Arquitetura

Separar SignalR em worker dedicado para WebSockets
Implementar sistema de filas (Kafka/RabbitMQ)
Microserviços para diferentes responsabilidades
Suporte a múltiplos outputs simultâneos

## Performance e Infraestrutura

Migrar armazenamento de BLOBs para AWS S3/Azure Blob Storage
Cache distribuído para arquivos de mídia
Otimização de queries com índices no PostgreSQL


## Controles Avançados
Sistema de logs estruturado
Monitoramento de saúde da aplicação
Configuração por variáveis de ambiente
